using Post.Common.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application.Interfaces
{
    public interface IAuthService
    {

        Task<bool> Login(LoginDto loginDto);
        Task<bool> Register(RegisterDto registeration);
        Task<bool> Logout();

    }
}
