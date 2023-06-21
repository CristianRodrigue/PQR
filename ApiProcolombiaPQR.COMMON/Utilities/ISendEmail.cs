using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiProcolombiaPQR.COMMON.Utilities
{
    public interface ISendEmail<T> where T : class
    {
        void SendAsync(T model);
    }
}
