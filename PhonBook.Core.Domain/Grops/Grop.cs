using PhonBook.Core.Domain.Contacts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PhonBook.Core.Domain.Grops
{
    public class Grop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name_Company { get; set; }
        public string Organization  { get; set; }
        public string Tell_phone { get; set; }
        public string Address { get; set; }
        public string Code_posti { get; set; }
        public int Fax { get; set; }
        public int Contact_id { get; set; }
        [ForeignKey(nameof(Contact_id))]
        public virtual List<Contact> Contacts { get; set; }
    }
}
