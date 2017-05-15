using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using AutoMapper;
using UserService.BLL.DTO;
using UserService.BLL.Exceptions;
using UserService.WEB.Interfaces;
using UserService.WEB.Models;
using UserService.WEB.Util.DI;

namespace UserService.WEB.HttpServices
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class UserService : IUserService
    {
        private readonly BLL.Interfaces.IUserService _userService;
        private readonly IMapper _mapper;

        public UserService()
        {
            _userService = ServiceLocator.Current.Get<BLL.Interfaces.IUserService>();
            _mapper = ServiceLocator.Current.Get<IMapper>();
        }

        public IEnumerable<User> GetAll()
        {
            var userDtos = _userService.GetAll();
            var users = _mapper.Map<IEnumerable<User>>(userDtos);

            return users;
        }

        public User Get(string id)
        {
            try
            {
                var integerId = int.Parse(id);
                var userDto = _userService.Get(integerId);
                var user = _mapper.Map<User>(userDto);

                return user;
            }
            catch (EntityNotFoundException)
            {
                return null;
            }
        }

        public HttpStatusCode Create(User user)
        {
            var userDto = _mapper.Map<UserDto>(user);
            _userService.Create(userDto);

            return HttpStatusCode.OK;
        }

        public HttpStatusCode Delete(string id)
        {
            try
            {
                var integerId = int.Parse(id);
                _userService.Delete(integerId);

                return HttpStatusCode.OK;
            }
            catch (EntityNotFoundException)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public HttpStatusCode Update(User user)
        {
            try
            {
                var userDto = _mapper.Map<UserDto>(user);
                _userService.Update(userDto);

                return HttpStatusCode.OK;
            }
            catch (EntityNotFoundException)
            {
                return HttpStatusCode.BadRequest;
            }
        }
    }
}