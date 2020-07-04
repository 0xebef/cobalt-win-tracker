# CobaltWinTracker

This little .NET/C# tool is created to track the location of the Windows-based device on which it is running.

The tool will send its coordinates to a remote server using HTTPS GET request with HTTP BASIC authentication.

## Project Homepage

https://github.com/0xebef/cobalt-win-tracker

## Notes

Please change the *hash* value in PwdForm.cs and set to a valid BCrypt password hash. It is used for granting access to the configuration values. Or you can try to brute-force the provided hash value.

Please see the *ConfigData* class in ConfigForm.cs to change the API endpoint address and authentication credentials.

The program uses the system registry to save its configuration values under the "CobaltWinTracker" subkey of the "CurrentUser" section.

The program will periodically send HTTP GET requests to the API endpoint with the following parameters:

* "hwid" - the hardware ID of the device generated based on the hardware network interface physical address parameter
* "lon" - location longitude
* "lat" - location latitude
* "alt" - location altitude
* "dt" - date and time in standard ISO format (yyyy-MM-dd HH:mm:ss)

## License and Copyright

License: GPLv3 or later

Copyright (c) 2020, 0xebef

### Note

The BCrypt.cs file, written by Damien Miller and Derek Slager, has a different license and copyright. Please see its source code for more information.
