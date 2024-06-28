using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Util.Exceptions;

public class SystemContextException : Exception
{
    public SystemContextException()
    {
    }

    public SystemContextException(string message)
        : base(message)
    {
    }

    public SystemContextException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
