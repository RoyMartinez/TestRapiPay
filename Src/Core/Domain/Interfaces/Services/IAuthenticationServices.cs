﻿using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Services
{
    public interface IAuthenticationServices
    {
        string Login(LoginRequest request);
    }
}
