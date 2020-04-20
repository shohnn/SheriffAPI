using System;
using System.Text.RegularExpressions;

namespace Sheriff.Domain.Models
{
    public class EmailAddress
    {
        public EmailAddress(string address) : this(address, String.Empty)
        {
        }

        public EmailAddress(string address, string displayName)
        {
            if (!IsValidAddress(address))
                throw new FormatException(Strings.UnrecognizedFormat.Format(nameof(address)));

            Address = address;
            DisplayName = displayName;
            User = address.Split('@')[0];
            Host = address.Split('@')[1];
        }

        public string Address { get; private set; }
        public string DisplayName { get; private set; }
        public string User { get; private set; }
        public string Host { get; private set; }

        public static bool IsValidAddress(string address)
        {
            return Regex.IsMatch(address,
                @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
    }
}