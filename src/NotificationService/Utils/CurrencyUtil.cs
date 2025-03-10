
using System.Globalization;

namespace SaBooBo.Utils;


public enum CurrencyEnum
{
    VND,
    USD,
    EUR
}

public static class CurrencyUtil
{
    /// </summary>
    /// <param name="amount">The double amount to format</param>
    /// <param name="currency">The currency type</param>
    /// <returns>Formatted currency string</returns>
    public static string Format(double amount, CurrencyEnum currency)
    {
        CultureInfo culture;
        string format;

        switch (currency)
        {
            case CurrencyEnum.USD:
                culture = new CultureInfo("en-US");
                format = "{0:C2}";
                break;
            case CurrencyEnum.EUR:
                culture = new CultureInfo("fr-FR");
                format = "{0:C2}";
                break;
            case CurrencyEnum.VND:
            default:
                culture = new CultureInfo("vi-VN");
                format = "{0:C0}";
                break;
        }

        return string.Format(culture, format, amount);
    }
}