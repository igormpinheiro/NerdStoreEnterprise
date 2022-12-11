using System.Globalization;
using System.Text.RegularExpressions;

namespace NSE.Core.DomainObjects;

public class Email
{
    public const int AddressMaxLength = 254;
    public const int AddressMinLength = 5;
    public string Address { get; private set; }

    // EF Constructor
    protected Email()
    { }

    public Email(string address)
    {
        if (!Validate(address)) throw new DomainException("E-mail inválido");

        Address = address;
    }

    public static bool Validate(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        if (email.Length > AddressMaxLength)
            return false;

        if (email.Length < AddressMinLength)
            return false;

        try
        {
            // Normalize the domain
            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                RegexOptions.None, TimeSpan.FromMilliseconds(200));

            // Examines the domain part of the email and normalizes it.
            string DomainMapper(Match match)
            {
                // Use IdnMapping class to convert Unicode domain names.
                var idn = new IdnMapping();

                // Pull out and process domain name (throws ArgumentException on invalid)
                var domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }
        }
        catch (RegexMatchTimeoutException e)
        {
            return false;
        }
        catch (ArgumentException e)
        {
            return false;
        }

        try
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }

    public override string ToString()
    {
        return Address;
    }
}
