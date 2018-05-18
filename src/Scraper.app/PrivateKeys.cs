namespace Scraper.app
{
    public class PrivateKeys
    {
        public string Password { get; private set; }
        public string Email { get; private set;}
        public PrivateKeys()
        {
            Password = "ScraperLogin51518!";
            Email = "finance.user51518@yahoo.com";
        }

    }
}