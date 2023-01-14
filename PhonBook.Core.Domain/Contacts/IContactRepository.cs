using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonBook.Core.Domain.Contacts
{
    public interface IContactRepository
    {
        Task Add(Contact contact);
        void Update(int id, Contact contact);
        void Delete(int id);
        Task<List<Contact>> GetAllAsync();
        List<Contact> GetAll();
        Task<Contact> FindByIdAsync(int id);
        Contact FindById(int id);
        Task<Contact> Create(Contact contact);
        Task<Contact> find_Contact_by_name_Async(string name);
        Contact find_Contact_by_name(string name);
        Task< List<Contact>> Serch_contact(string Fullname);
    }
}
