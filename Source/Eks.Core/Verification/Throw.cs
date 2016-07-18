using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eks.Core.Verification
{
    public static class Throw
    {
        public static void IfNull(object o, string parameterName = null, string message = null)
        {
            if (o == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }
        }
    }
}
