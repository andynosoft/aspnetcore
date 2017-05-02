namespace Alpaca.Web.Settings {

    public class AuthSettings {

        public string Username { get; set; }
        public string Password { get; set; }
        public int TokenExpiration { get; set; }
        public string TokenIssuer { get; set; }        
        public string TokenSecretKey { get; set; }

    }
}