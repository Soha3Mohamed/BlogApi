using UserPostApi.Helpers;
using UserPostApi.Models.DTOs.User;

namespace UserPostApi.Services.Interfaces
{
    public interface IUserService
    {

        ServiceResult<List<GetUserDTO>> GetAllUsers();

        ServiceResult<GetUserDTO> GetUser(int id);

        ServiceResult<GetUserDTO> AddUser (AddUserDTO addUserDTO);
        ServiceResult<GetUserDTO> UpdateUser(AddUserDTO addUserDTO, int userId);
        ServiceResult<bool> DeleteUser(int id);
        

    }
}
