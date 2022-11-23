namespace emr.Support
{
    public static class ExtensionMethods
    {
        public static string ToCleanString(this string data)
        {
            string cleanString = string.Empty;
            if (data != null)
                cleanString = data.ToString().Trim();

            return cleanString;
        }
    }
}
