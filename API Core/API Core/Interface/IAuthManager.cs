using API_Core.Model.Data_Transfer_Objects;
using API_Core.Model.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Core.Interface
{
    public interface IAuthManager
    {
        public Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
        public Task<AuthResponseDto> Login(LoginDto loginDto);
        public Task<String> CreateResfreshToken();

        public Task<AuthResponseDto> VerifyResfreshToken(AuthResponseDto authResponseDto);



    }
}
