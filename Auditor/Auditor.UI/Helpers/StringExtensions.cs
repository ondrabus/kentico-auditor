
namespace Auditor.UI.Helpers
{
    public static class StringExtensions
    {
        public static string TrimLength(this string text, int length)
        {
            if (text.Length <= length)
                return text;

            return text.Substring(0, length);
        }
    }
}
