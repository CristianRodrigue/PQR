﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.ENTITY
{
    public class PqrEntity
    {
        public Guid? Id { get; set; }

        public int CaseNumber { get; set; }

        public Guid? CountryId { get; set; }

        public Guid? CaseTypeId { get; set; }

        public Guid? UserTypeId { get; set; }

        public string RazonSocial { get; set; }

        public string? Nit { get; set; }

        public  string? Cedula { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Comentario { get; set; }

        public bool AutorizaTratamientoDatos { get; set; }

        public DateTime PQRDate { get; set; }

        public Guid? CaseStatus { get; set; }

        public Guid? FileId { get; set; }
        
        /*
        public virtual CountryEntity Country { get; set; }
        public virtual CaseTypeEntity CaseType { get; set; }
        public virtual UserTypeEntity UserType { get; set; }
        public virtual StatusEntity Status { get; set; }
        public virtual FilesEntity File { get; set; }
        public virtual RoleEntity Role { get; set; }
        public virtual ConsecutiveEntity Consecutive { get; set; }
        public virtual MailTemplateEntity MailTemplate { get; set; }*/
        // public virtual EmployeeEntity Employee { get; set; }
        // public virtual AssignEntity Assign { get; set; }
    }
}
