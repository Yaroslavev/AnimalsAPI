using AutoMapper;
using Core.IServices;
using Core.Models;
using Data;
using Data.Enteties;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AccountsService(AnimalsDbContext context, IMapper mapper, UserManager<User> userManager) : IAccountsService
    {
        public async Task Register(RegisterModel model)
        {
            var user = mapper.Map<User>(model);

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
    }
}
