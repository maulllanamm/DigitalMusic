using DigitalMusic.Domain.Entities;

namespace DigitalMusic.Application.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAll();
        public Task<User> GetById(Guid id);
        public Task<User> GetByUsername(string username);
        public Task<User> GetByVerifyToken(string verifyToken);
        public Task<User> GetByEmail(string email);
        public Task<User> GetByPasswordResetToken(string resetPasswordToken);
        public Task<User> Create(User user);
        public Task<User> Update(User user);
        public Task<bool> Delete(Guid id);
    }
}
