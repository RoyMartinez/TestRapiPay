﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Primitives
{
    public class Entity
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public int UserCreatorId { get; set; }
    }
}
