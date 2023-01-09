using System.ComponentModel.DataAnnotations;

namespace PhonBook.Api.Dto
{
    public class ContactDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = " این فیلد نمیتواند خالی باشد")]
        [MaxLength(250)]
        public string Fullname { get; set; }
        [Required(ErrorMessage = " این فیلد نمیتواند خالی باشد")]
        public string POST { get; set; }
        [Required(ErrorMessage = " این فیلد نمیتواند خالی باشد")]
        public string Tell_phone { get; set; }
        public string Phone_number1 { get; set; }
        public string Phone_number2 { get; set; }
        public string Email { get; set; }
        public int gropid { get; set; }
        public string Name_Grop { get; set; }
    }
}
