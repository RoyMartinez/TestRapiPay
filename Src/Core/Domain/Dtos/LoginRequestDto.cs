﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos
{
    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
