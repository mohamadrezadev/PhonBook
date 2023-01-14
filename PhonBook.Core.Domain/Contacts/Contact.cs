using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PhonBook.Core.Domain.Grops;
namespace PhonBook.Core.Domain.Contacts
{
    public class Contact
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string POST { get; set; }
        public string Tell_phone { get; set; }
        public string Phone_number1 { get; set; }
        public string Phone_number2 { get; set; }
        public string Email { get; set; }
        public int Group_Id { get; set; }
        //[ForeignKey(nameof(Group_Id))]
        public virtual List<Grop> Groups { get; set; } = new List<Grop>();
    }
}
