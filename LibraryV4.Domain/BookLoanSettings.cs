using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryV4.Domain
{
    public class BookLoanSettings
    {
        public int MaxBooksBorrowed { get; set; } = 3; 
        public int LoanDurationDays { get; set; } = 10; 
    }
}
