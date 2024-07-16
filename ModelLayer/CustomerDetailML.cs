using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class CustomerDetailML
    {
        public string? AddressType { get; set; }
        public string? FullAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int UserId { get; set; }
    }
}
