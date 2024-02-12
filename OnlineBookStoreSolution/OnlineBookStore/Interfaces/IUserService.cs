using OnlineBookStore.Models.DTOs;

namespace OnlineBookStore.Interfaces
{
    public interface IUserService
    {
        UserDTO Login(UserDTO userDTO);
        UserDTO Register(UserDTO userDTO);
    }
}
