//--------------------------------------------------------------------------
// <copyright file="TestUtilities.cs" company="Jeff Winn">
//      Copyright (c) Jeff Winn. All rights reserved.
//
//      The use and distribution terms for this software is covered by the
//      GNU Library General Public License (LGPL) v2.1 which can be found
//      in the License.rtf at the root of this distribution.
//      By using this software in any fashion, you are agreeing to be bound by
//      the terms of this license.
//
//      You must not remove this notice, or any other, from this software.
// </copyright>
//--------------------------------------------------------------------------

namespace DotRas.IntegrationTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Contains utility methods for the test assembly.
    /// </summary>
    internal static class TestUtilities
    {
        /// <summary>
        /// Returns the path to a unique temporary folder.
        /// </summary>
        /// <param name="autoCreatePath"><b>true</b> to automatically create the path once a unique path has been found, otherwise <b>false</b>.</param>
        /// <returns>The directory information of the temporary folder.</returns>
        public static DirectoryInfo GetTempPath(bool autoCreatePath)
        {
            DirectoryInfo result = null;
            
            string path = null;
            do
            {
                path = Path.Combine(Path.GetTempPath(), TestUtilities.StripNonAlphaNumericChars(Guid.NewGuid().ToString()));

                if (!Directory.Exists(path))
                {
                    if (autoCreatePath)
                    {
                        Directory.CreateDirectory(path);
                    }

                    result = new DirectoryInfo(path);
                    break;
                }
            }
            while (true);

            return result;
        }

        /// <summary>
        /// Retrieves valid credentials to connect to the test VPN servers.
        /// </summary>
        /// <returns>A new <see cref="NetworkCredential"/> object.</returns>
        public static NetworkCredential GetValidCredentials()
        {
            return new NetworkCredential("testuser", "passw0rd");
        }

        /// <summary>
        /// Creates a new VPN entry in the phonebook.
        /// </summary>
        /// <param name="phonebook">The phonebook to receive the entry.</param>
        /// <param name="entryName">The name of the entry.</param>
        /// <returns>The entry id.</returns>
        public static Guid CreateValidVpnEntry(RasPhoneBook phonebook, string entryName)
        {
            return CreateVpnEntry(phonebook, entryName, "testvpn");
        }

        /// <summary>
        /// Creates a new invalid VPN entry in the phonebook.
        /// </summary>
        /// <param name="phonebook">The phonebook to receive the entry.</param>
        /// <param name="entryName">The name of the entry.</param>
        /// <returns>The entry id.</returns>
        public static Guid CreateInvalidVpnEntry(RasPhoneBook phonebook, string entryName)
        {
            return CreateVpnEntry(phonebook, entryName, "yahoo.com");
        }

        /// <summary>
        /// Strips all non-alphanumeric characters from the value specified.
        /// </summary>
        /// <param name="value">The value to strip.</param>
        /// <returns>A <see cref="System.String"/> whose non-alphanumeric characters have been removed.</returns>
        public static string StripNonAlphaNumericChars(string value)
        {
            string retval = null;

            if (value != null)
            {
                StringBuilder sb = new StringBuilder();

                foreach (char c in value)
                {
                    if (char.IsLetterOrDigit(c))
                    {
                        sb.Append(c);
                    }
                }

                retval = sb.ToString();
            }

            return retval;
        }

        /// <summary>
        /// Creates a VPN entry for the phonebook.
        /// </summary>
        /// <param name="phonebook">The phonebook to receive the entry.</param>
        /// <param name="entryName">The name of the entry.</param>
        /// <param name="serverAddress">The server address to connect to.</param>
        /// <returns>The entry id.</returns>
        private static Guid CreateVpnEntry(RasPhoneBook phonebook, string entryName, string serverAddress)
        {
            Guid entryId = Guid.Empty;

            RasDevice device = RasDevice.GetDevices().Where(o => o.Name.Contains("(PPTP)") && o.DeviceType == RasDeviceType.Vpn).FirstOrDefault();

            RasEntry entry = RasEntry.CreateVpnEntry(entryName, serverAddress, RasVpnStrategy.PptpOnly, device);
            if (entry != null)
            {
                phonebook.Entries.Add(entry);

                entryId = entry.Id;
            }

            return entryId;
        }
    }
}