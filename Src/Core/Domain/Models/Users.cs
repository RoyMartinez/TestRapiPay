using Domain.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class Users: Entity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
