using System.ComponentModel.DataAnnotations;

namespace PhonBook.Models
{
    public class VmGrop
    {
        public int id { get; set; }
        [Required(ErrorMessage = " این فیلد نمیتواند خالی باشد")]
        public string Name { get; set; }
        [Required(ErrorMessage = " این فیلد نمیتواند خالی باشد")]
        public string Name_Company { get; set; }
        [Required(ErrorMessage = " این فیلد نمیتواند خالی باشد")]
        public string Organization { get; set; }
        [Required(ErrorMessage = " این فیلد نمیتواند خالی باشد")]
        public string Tell_phone { get; set; }
        [Required(ErrorMessage = " این فیلد نمیتواند خالی باشد")]
        public string Address { get; set; }
        [Required(ErrorMessage = " این فیلد نمیتواند خالی باشد")]
        public string Code_posti { get; set; }
        [Required(ErrorMessage = " این فیلد نمیتواند خالی باشد")]
        public int Fax { get; set; }
    }
    
}
