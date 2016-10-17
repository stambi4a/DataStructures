namespace Collection_of_Persons
{
    using System.Text.RegularExpressions;

    public static class StringUtils
    {
        public static string GetEmailDomain(this string email)
        {
            string pattern = @"[\w\_\.]+@([[a-z0-9\.\-]+)";
            string domain = Regex.Replace(email, pattern, "$1");

            return domain;
        }
    }
}
