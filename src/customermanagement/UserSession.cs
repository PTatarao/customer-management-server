namespace customermanagement
{
    public static class UserSession
    {
        public static string CreateToken(string value)
        {
            string key = Guid.NewGuid().ToString();
            SessioStorage.Set(key, value);
            return key;
        }
    }
}
