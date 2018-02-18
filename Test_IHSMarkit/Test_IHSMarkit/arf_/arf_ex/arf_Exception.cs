using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_IHSMarkit.arf_.arf_ex
{
    public class arf_Exception : Exception
    {
        public arf_Exception(String message)
            : base(message)
        { }
    }
}
