namespace Kpax.Core.Verification
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Throws
    {
        public static void IfNull(object parameter, string parameterName = null, string message = null)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }
        }
    }
}
