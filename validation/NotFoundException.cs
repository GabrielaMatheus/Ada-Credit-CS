using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AdaCredit.validation
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) {
            Console.WriteLine(message);
        }
    }
}
