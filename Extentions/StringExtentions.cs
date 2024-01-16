using System.Text;

namespace VideoConvertor.Extentions;

public static class StringExtentions
{
    public static bool IsValidPath(this string? path)
    {
        bool res = false;

        if (Path.IsPathRooted(path) && !Path.GetInvalidPathChars().Any(path.Contains))
            res = true;

        if (res && !File.Exists(path))
            res = false;

        return res;
    }
    public static bool IsValidPath(this StringBuilder? path)
    {
        bool res = false;

        if (Path.IsPathRooted(path.ToString()) && !Path.GetInvalidPathChars().Any(path.ToString().Contains))
            res = true;

        if (res && !File.Exists(path.ToString()))
            res = false;

        return res;
    }
    public static bool IsValidUrl(this string? url) => Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
    public static bool IsValidUrl(this StringBuilder? url) => Uri.IsWellFormedUriString(url?.ToString(), UriKind.RelativeOrAbsolute);
    public static bool HasValue(this string? txt) => !string.IsNullOrWhiteSpace(txt);
    public static bool HasValue(this StringBuilder? txt)=> txt is object && txt.Length > 0;
    public static bool HasValidMaxLength(this string txt, int validMaxLength) => txt.Length <= validMaxLength;
    public static bool HasValidMinLength(this string txt,int validMinLength)=> txt.Length >= validMinLength;
    public static string FindWords(this string txt,string word)
    {
        if (!txt.HasValue()) return string.Empty;

        int startIndex = txt.IndexOf(word);

        if(startIndex == -1) return string.Empty;

        string result = txt.Substring(startIndex, word.Length);

        return result;
    }
    public static string GetCleanForComparer(this string txt) => txt.TrimStart().TrimEnd();
    public static string GetCleanLowerForComparer(this string txt)=> txt.ToLower().TrimStart().TrimEnd();
    public static string GetCleanNormalizedForComparer(this string txt) => txt.ToUpper().TrimStart().TrimEnd();
    public static bool CompareStrings(this string str1, string str2, bool caseSensitive = true)
        => String.Equals(str1, str2, caseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);
}
