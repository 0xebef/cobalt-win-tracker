using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Devices.Geolocation;

namespace CobaltWinTracker
{
    public partial class MainForm : Form
    {
        private Queue<string> data = new Queue<string>();
        private const int dataMaxCount = 24 * 60;
        private Geolocator geolocator = null;
        private GeolocationAccessStatus accessStatus;
        private System.Timers.Timer timerSender = new System.Timers.Timer(10000);
        private bool configFormIsOpen = false;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            timerSender.Enabled = false;
            timerSender.AutoReset = true;
            timerSender.Elapsed += TimerSender_Tick;

            timerSplash.Enabled = true;

            reload();
        }

        private async void reload()
        {
            var configData = ConfigData.Instance;

            if (accessStatus == GeolocationAccessStatus.Unspecified)
            {
                accessStatus = await Geolocator.RequestAccessAsync();
            }

            if (geolocator != null)
            {
                geolocator.StatusChanged -= loc_StatusChanged;
                geolocator.PositionChanged -= loc_PositionChanged;
                geolocator = null;
            }

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    geolocator = new Geolocator {
                        DesiredAccuracyInMeters = 50,
                        ReportInterval = 1000U * (uint)configData.getIntervalSec()
                    };
                    geolocator.StatusChanged += loc_StatusChanged;
                    geolocator.PositionChanged += loc_PositionChanged;
                    break;

                case GeolocationAccessStatus.Denied:
                    MessageBox.Show("CobaltWinTracker: Position Access Denied!");
#if !DEBUG
                    disposeNotifyIcon();
                    Environment.Exit(0);
#endif
                    break;

                default:
                    MessageBox.Show("CobaltWinTracker: Position Acces Unspecified!");
#if !DEBUG
                    disposeNotifyIcon();
                    Environment.Exit(0);
#endif
                    break;
            }
        }

        private bool auth()
        {
            using (PwdForm pwdForm = new PwdForm())
            {
                DialogResult dr = pwdForm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    return true;
                }
            }

            return false;
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (configFormIsOpen)
            {
                return;
            }

            if (auth())
            {
                configFormIsOpen = true;
                using (ConfigForm configForm = new ConfigForm())
                {
                    DialogResult dr2 = configForm.ShowDialog();
                    if (dr2 == DialogResult.OK)
                    {
                        MessageBox.Show("The Configuration was Saved");
                        reload();
                    }
                }
                configFormIsOpen = false;
            }
            else
            {
                MessageBox.Show("Authorization Failed");
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void TimerSplash_Tick(object sender, EventArgs e)
        {
            timerSplash.Enabled = false;
            Hide();
        }

        private void UpdateLocationData(Geoposition pos)
        {
            var configData = ConfigData.Instance;
            string positionType;

            switch (pos.Coordinate.PositionSource)
            {
                case PositionSource.Cellular:
                    positionType = "Cellular";
                    break;
                case PositionSource.Satellite:
                    positionType = "Satellite";
                    break;
                case PositionSource.WiFi:
                    positionType = "WiFi";
                    break;
                case PositionSource.IPAddress:
                    positionType = "IPAddress";
                    break;
                case PositionSource.Default:
                    positionType = "Default";
                    break;
                case PositionSource.Obfuscated:
                    positionType = "Obfuscated";
                    break;
                default:
                    positionType = "Unknown";
                    break;
            }

            configData.updateLastUpdateDT(positionType);

            UInt32 configBits = configData.getBits();

            bool use_position = (
                (pos.Coordinate.PositionSource == PositionSource.Cellular && (configBits & ConfigBits.BIT_USE_CELL) != 0U) ||
                (pos.Coordinate.PositionSource == PositionSource.Satellite && (configBits & ConfigBits.BIT_USE_GPS) != 0U) ||
                (pos.Coordinate.PositionSource == PositionSource.WiFi && (configBits & ConfigBits.BIT_USE_WIFI) != 0U)
                );

            if (!use_position)
            {
                return;
            }

#if DEBUG
            MessageBox.Show(pos.Coordinate.Point.Position.Longitude + "," + pos.Coordinate.Point.Position.Latitude);
#endif

            var nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";


            string uriParams =
                "hwid=" + configData.getHWID() +
                "&lon=" + pos.Coordinate.Point.Position.Longitude.ToString(nfi) +
                "&lat=" + pos.Coordinate.Point.Position.Latitude.ToString(nfi) +
                "&alt=" + pos.Coordinate.Point.Position.Altitude.ToString(nfi);

            if (!sendData(uriParams))
            {
                uriParams += "&dt=" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                data.Enqueue(uriParams);
                if (data.Count > dataMaxCount)
                {
                    data.Dequeue();
                }
            }
            else
            {
                if (data.Count > 0 && !timerSender.Enabled)
                {
                    timerSender.Enabled = true;
                }
            }
        }

        private bool sendData(string uriParams)
        {
            var configData = ConfigData.Instance;
            WebClient client = new WebClient();
            Uri uri = new Uri(configData.getAPIURL() + "?" + uriParams);

            string credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(configData.getAPIUsername() + ":" + configData.getAPIPassword()));
            client.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", credentials);

            Stream stream = null;
            StreamReader streamReader = null;
            bool ok = false;
            try
            {
                stream = client.OpenRead(uri);
                streamReader = new StreamReader(stream);
                streamReader.ReadToEnd();
                ok = true;
            }
            catch (Exception e)
            {
                configData.setLastError(e.Message);
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                    streamReader = null;
                }
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
            }

            return ok;
        }

        private async Task<GeolocationAccessStatus> requestAccess()
        {
            GeolocationAccessStatus status = await Geolocator.RequestAccessAsync();
            return status;
        }

        private void loc_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
#if DEBUG
            MessageBox.Show("loc_StatusChanged: " + args.Status);
#endif
        }

        private void loc_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            UpdateLocationData(args.Position);
        }

        private void ToolStripMenuItemConfigure_Click(object sender, EventArgs e)
        {
            NotifyIcon_DoubleClick(sender, e);
        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            if (auth())
            {
                disposeNotifyIcon();
                Environment.Exit(0);
            }
            else
            {
                MessageBox.Show("Authorization Failed");
            }
        }

        private void disposeNotifyIcon()
        {
            if (notifyIcon == null)
            {
                return;
            }

            try
            {
                notifyIcon.Visible = false;
                notifyIcon.Icon = null;
                notifyIcon.Dispose();
                notifyIcon = null;
            }
            catch
            {

            }
        }

        private void InformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var configData = ConfigData.Instance;

            var msg =
                "CobaltWinTracker\r\n\r\n" +
                "Last Update: " + configData.getLastUpdateDT() + "\r\n" +
                "Last Error: " + configData.getLastError() + "\r\n" +
                "Data Queue: " + data.Count().ToString() + " Items, " + (timerSender.Enabled ? "Acitve" : "Inactive");
            MessageBox.Show(msg);
        }

        private void TimerSender_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            var configData = ConfigData.Instance;

            timerSender.Enabled = false;

            const int maxCount = 10;
            bool ok = true;
            string err = "";

            for (int i = 0; ok && i < maxCount && data.Count > 0; i++)
            {
                try
                {
                    string uriParams = data.Dequeue();
                    ok = sendData(uriParams);
                    if (!ok)
                    {
                        data.Enqueue(uriParams);
                    }
                }
                catch (Exception e2)
                {
                    err = e2.Message;
                    ok = false;
                }
            }

            if (!ok && err != "")
            {
                configData.setLastError(err);
            }

            if (ok && data.Count > 0)
            {
                timerSender.Enabled = true;
            }
        }
    }
}
