namespace customermanagement
{
    public static class SessioStorage
    {
        public static Dictionary<string, string> _sessionStorage = new Dictionary<string, string>();
        public static void Set(string key, string value)
        {
            _sessionStorage.Add(key, value);
        }
        public static string Get(string key)
        {
            _sessionStorage.TryGetValue(key, out string user);
           return user;
        }
    }
}
