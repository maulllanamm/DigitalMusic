namespace DigitalMusic.Application.Helper.Interface
{
    public interface IPasswordHelper
    {
        public string ComputeHash(string password, string salt, string papper, int iteration);
        public string GenerateSalt();
    }
}
