using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using Microsoft.Win32;

namespace CobaltWinTracker
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void ConfigForm_Shown(object sender, EventArgs e)
        {
            var configData = ConfigData.Instance;

            textBoxHWID.Text = configData.getHWID();
            numericIntervalSec.Value = configData.getIntervalSec();

            UInt32 bits = configData.getBits();

            checkBoxUseGPS.Checked = ((bits & ConfigBits.BIT_USE_GPS) != 0U);
            checkBoxUseCell.Checked = ((bits & ConfigBits.BIT_USE_CELL) != 0U);
            checkBoxUseWiFi.Checked = ((bits & ConfigBits.BIT_USE_WIFI) != 0U);
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {
            var configData = ConfigData.Instance;
            UInt32 bits = 0U;
            bool ok = true;

            if (ok)
            {
                ok = configData.setIntervalSec(int.Parse(numericIntervalSec.Value.ToString()));
            }

            if (ok)
            {
                if (checkBoxUseGPS.Checked)
                {
                    bits |= ConfigBits.BIT_USE_GPS;
                }
                if (checkBoxUseCell.Checked)
                {
                    bits |= ConfigBits.BIT_USE_CELL;
                }
                if (checkBoxUseWiFi.Checked)
                {
                    bits |= ConfigBits.BIT_USE_WIFI;
                }
                ok = configData.setBits(bits);
            }

            if (!ok)
            {
                MessageBox.Show("Error when saving the configuration data!");
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        public int getNumericIntervalSecValue()
        {
            return int.Parse(numericIntervalSec.Value.ToString());
        }
    }

    public static class ConfigBits
    {
        public const UInt32 BIT_USE_GPS = 0x00000001U;
        public const UInt32 BIT_USE_CELL = 0x00000002U;
        public const UInt32 BIT_USE_WIFI = 0x00000004U;
        public const UInt32 BITS_DEFAULT = BIT_USE_GPS | BIT_USE_CELL | BIT_USE_WIFI;
    }

    public sealed class ConfigData
    {
        private static readonly Lazy<ConfigData> lazy = new Lazy<ConfigData>(() => new ConfigData());

        private RegistryKey prefs = null;
        private string lastUpdateDT = "Never";
        private string lastError = "No Error";

        private const int intervalSecDefault = 300;
        private const int intervalSecMin = 30;
        private const int intervalSecMax = 3600;

        public const UInt32 bitsDefault = ConfigBits.BITS_DEFAULT;

        public static ConfigData Instance
        {
            get { return lazy.Value; }
        }

        private ConfigData()
        {

        }

        private bool prefsOpen(bool w)
        {
            if (w)
            {
                prefs = Registry.CurrentUser.OpenSubKey("CobaltWinTracker", true);

                if (prefs == null)
                {
                    prefs = Registry.CurrentUser.CreateSubKey("CobaltWinTracker", RegistryKeyPermissionCheck.ReadWriteSubTree);
                }
            }
            else
            {
                prefs = Registry.CurrentUser.OpenSubKey("CobaltWinTracker");
            }


            if (prefs == null)
            {
                return false;
            }

            return true;
        }

        private void prefsClose()
        {
            if (prefs != null)
            {
                prefs.Close();
            }
        }

        public string getAPIURL()
        {
            return "https://example.com/api/v1/location";
        }

        public string getAPIUsername()
        {
            return "api-username";
        }

        public string getAPIPassword()
        {
            return "api-password";
        }

        public string getHWID()
        {
            string firstMacAddress = NetworkInterface
                .GetAllNetworkInterfaces()
                .OrderBy(nic => nic.GetPhysicalAddress().ToString())
                .Where(nic => nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .Select(nic => nic.GetPhysicalAddress().ToString())
                .FirstOrDefault();

            if (firstMacAddress == null)
            {
                return "[ERROR]";
            }

            const string prefix = "029a";
            UInt64 mac = Convert.ToUInt64(prefix + firstMacAddress, 16);

            return mac.ToString();
        }

        public int getIntervalSec()
        {
            if (!prefsOpen(false))
            {
                return intervalSecDefault;
            }

            int val;
            try
            {
                val = int.Parse(prefs.GetValue("numericIntervalSec").ToString());
                if (val < intervalSecMin || val > intervalSecMax)
                {
                    val = intervalSecDefault;
                }
            }
            catch
            {
                val = intervalSecDefault;
            }

            prefsClose();

            return val;
        }

        public bool setIntervalSec(int val)
        {
            bool ok;

            if (val < intervalSecMin || val > intervalSecMax)
            {
                return false;
            }

            if (!prefsOpen(true))
            {
                return false;
            }

            try
            {
                prefs.SetValue("numericIntervalSec", val);
                ok = true;
            }
            catch
            {
                ok = false;
            }

            prefsClose();

            return ok;
        }

        public UInt32 getBits()
        {
            if (!prefsOpen(false))
            {
                return bitsDefault;
            }

            UInt32 val;
            try
            {
                val = UInt32.Parse(prefs.GetValue("Bits").ToString());
            }
            catch
            {
                val = bitsDefault;
            }

            prefsClose();

            return val;
        }

        public bool setBits(UInt32 val)
        {
            bool ok;

            if (!prefsOpen(true))
            {
                return false;
            }

            try
            {
                prefs.SetValue("Bits", val);
                ok = true;
            }
            catch
            {
                ok = false;
            }

            prefsClose();

            return ok;
        }

        public void updateLastUpdateDT(string info)
        {
            lastUpdateDT = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString() + ": " + info;
        }

        public string getLastUpdateDT()
        {
            return lastUpdateDT;
        }

        public void setLastError(string error)
        {
            lastError = DateTime.Now.ToShortTimeString() + ": " + error;
        }

        public string getLastError()
        {
            return lastError;
        }
    }

}
