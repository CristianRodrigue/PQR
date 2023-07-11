﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.ENTITY
{
    public class ConsecutiveEntity
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public virtual ICollection<PqrEntity> Pqrs { get; set; }
    }
}
