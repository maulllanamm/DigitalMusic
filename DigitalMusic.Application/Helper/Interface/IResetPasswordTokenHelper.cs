namespace DigitalMusic.Application.Helper.Interface
{
    public interface IResetPasswordTokenHelper
    {
        public string GeneratePasswordResetToken(string Username, string Email);
    }
}
