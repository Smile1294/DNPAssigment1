using DNPAssigment1.Login;

namespace DNPAssigment1.Login
{
    public interface IUserService
    {
        User ValidateUser(string Username, string Password);
    }
}