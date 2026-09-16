using Microsoft.AspNetCore.Identity;
using MovieApi.Application.Features.CQRSDesignPattern.Commands.UserRegisterCommands;
using MovieApi.Persistence.Context;
using MovieApi.Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApi.Application.Features.CQRSDesignPattern.Handlers.UserRegisterHandlers
{
    public class CreateUserRegisterCommandHandler
    {
        private readonly MovieContext _context;
        private readonly UserManager<AppUser> _userInManager;

        public CreateUserRegisterCommandHandler(MovieContext context, UserManager<AppUser> userInManager)
        {
            _context = context;
            _userInManager = userInManager;
        }


        public async Task Handle(CreateUserRegisterCommand command)
        {
            var user = new AppUser()
            {
                Name = command.Name,
                Surname = command.Surname,
                Email = command.Email,
                UserName = command.Username
            };

            await _userInManager.CreateAsync(user, command.Password);
        }


    }
}
