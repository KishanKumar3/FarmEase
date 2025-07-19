using FarmEase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmEase.Application.Services.Interface
{
    public interface IJWTService
    {
        public Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
    }
}
