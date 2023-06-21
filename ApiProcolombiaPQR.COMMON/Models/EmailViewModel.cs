using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.COMMON.Models
{
    public class EmailViewModel
    {
        public string[] Destinations { get; set; }

        public string Destination { get; set; }

        public string Suject { get; set; }

        public string Message { get; set; }

        public string? Attachments { get; set; }

        public bool IsHtml { get; set; }


    }
}
