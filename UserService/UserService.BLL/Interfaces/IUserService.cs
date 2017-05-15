using System.Collections.Generic;
using UserService.BLL.DTO;

namespace UserService.BLL.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserDto> GetAll();

        /// <summary>
        /// Gets user by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        UserDto Get(int id);

        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        void Create(UserDto userDto);

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="userDto">The user dto.</param>
        void Update(UserDto userDto);

        /// <summary>
        /// Delete user by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(int id);
    }
}