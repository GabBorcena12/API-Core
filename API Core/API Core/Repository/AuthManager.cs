using API_Core.Interface;
using API_Core.Model.Data_Transfer_Objects;
using API_Core.Model.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Core.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AuthManager(IMapper mapper,UserManager<ApiUser> userManager)
        {
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<bool> Login(LoginDto loginDto)
        {
            bool isValidUser = false;

            try
            {
                var user = await _userManager.FindByEmailAsync(loginDto.Email);
                if (user == null) {
                    return default;
                }

                isValidUser = await _userManager.CheckPasswordAsync(user, loginDto.Password);
                if (!isValidUser)
                {
                    return default;
                }
            }
            catch(Exception){ 
                
            }
            return isValidUser;
        }

        //Register
        public async Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto)
        {
            var user = _mapper.Map<ApiUser>(userDto);
            user.UserName = userDto.Email;

            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded) {
                await _userManager.AddToRoleAsync(user, "User");
            }
            return result.Errors;
        }
    }
} 
