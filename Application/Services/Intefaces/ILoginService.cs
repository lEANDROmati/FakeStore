﻿using Application.Models.Categories;
using Application.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Intefaces
{
    public interface ILoginService
    {
        Task<string> Login(LoginRequestModel request);

       
    }
}
