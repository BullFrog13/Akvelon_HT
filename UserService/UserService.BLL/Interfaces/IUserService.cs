using System.Collections.Generic;
using UserService.BLL.DTO;

namespace UserService.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAll();

        UserDto Get(int id);

        void Create(UserDto userDto);

        void Update(UserDto userDto);

        void Delete(int id);
    }
}