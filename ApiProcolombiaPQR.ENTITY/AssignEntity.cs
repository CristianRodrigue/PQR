﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.ENTITY
{
    public class AssignEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid IdMailTemplate { get; set; }

        public Guid IdEmployee { get; set; }   
    }
}
