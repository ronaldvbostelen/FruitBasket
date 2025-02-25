using System.Globalization;

namespace FruitBasket.Core.Extensions;

public static class StringExtensions
{
    public static string CapitalizeFirstLetter
        (this string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var culture = CultureInfo.CurrentCulture;
        return char.ToUpper(input[0], culture) + input[1..];
    }
}