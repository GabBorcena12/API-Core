using API_Core.Model.Data_Transfer_Objects;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Core.Interface
{
    public interface IAuthManager
    {
        public Task<IEnumerable<IdentityError>> Register(ApiUserDto userDto);
        public Task<bool> Login(LoginDto loginDto);

    }
}
