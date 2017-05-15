using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UserService.BLL.DTO;
using UserService.BLL.Exceptions;
using UserService.BLL.Interfaces;
using UserService.DAL.Entities;
using UserService.DAL.Interfaces;

namespace UserService.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<UserDto> GetAll()
        {
            var users = _unitOfWork.Users.GetAll().ToList();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return userDtos;
        }

        public UserDto Get(int id)
        {
            var user = _unitOfWork.Users.Get(id);

            if (user == null)
            {
                throw new EntityNotFoundException($"User with such id cannot be found. Id: {id}", "User");
            }

            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public void Create(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _unitOfWork.Users.Create(user);
            _unitOfWork.Save();
        }

        public void Update(UserDto userDto)
        {
            var user = _unitOfWork.Users.Get(userDto.Id);

            if (user == null)
            {
                throw new EntityNotFoundException($"User with such id cannot be found for deleting. Id : {userDto.Id}", "User");
            }

            user = _mapper.Map<User>(userDto);
            _unitOfWork.Users.Update(user);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var user = _unitOfWork.Users.Get(id);

            if (user == null)
            {
                throw new EntityNotFoundException($"User with such id cannot be found for deleting. Id : {id}", "User");
            }

            _unitOfWork.Users.Delete(id);
            _unitOfWork.Save();
        }
    }
}