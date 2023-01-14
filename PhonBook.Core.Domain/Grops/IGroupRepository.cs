using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PhonBook.Core.Domain.Contacts;

namespace PhonBook.Core.Domain.Grops
{
    public interface IGroupRepository
    {
        Task Add(Grop group);
        Task<int> Update(int id, Grop group);
        void Delete(int id);
        Task<int> DeleteAsync(int Id);
        Task<List<Grop>> GetAllAsync();
        List<Grop> GetAll();
        Task<Grop> Create(Grop group);
        Task<Grop> FindByIdAsync(int id);
        Grop FindById(int id);
        Task<Grop> FindByNameAsync(string name);
        Grop FindByName(string name);
        List<Contact> GetContacts(int idgrop);
        Task<List<Grop>> Serch_grop(string Fullname);
    }
}
