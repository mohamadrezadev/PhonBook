using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using PhonBook.Core.Domain;
using PhonBook.Core.Domain.Contacts;
using PhonBook.Models;
using PhonBook.Core.Domain.Grops;

namespace PhonBook.Components
{
    public class ContactSummaryViewComponent: ViewComponent
    {
        private readonly IContactRepository _ContactRepository;
        private readonly IGroupRepository _GroupRepository;

        public ContactSummaryViewComponent(IContactRepository contactRepository ,IGroupRepository groupRepository)
        {
            _ContactRepository = contactRepository;
            _GroupRepository = groupRepository;
        }
        public IViewComponentResult Invoke()
        {
            var ContactVm = new List<Vmcontact>();
            var contacts = _ContactRepository.GetAll();
            contacts.ForEach(async item =>
            {
                var grop = await _GroupRepository.FindByIdAsync(item.Group_Id);
                if (grop != null)
                {
                    var VmGrop = new VmGrop()
                    {
                        id = item.Group_Id,
                        Address = grop.Address,
                        Code_posti = grop.Code_posti,
                        Fax = grop.Fax,
                        Name = grop.Name,
                        Name_Company = grop.Name_Company,
                        Organization = grop.Organization,
                        Tell_phone = grop.Tell_phone,

                    };
                    ContactVm.ForEach(g =>
                    {
                        g.Grops.Add(VmGrop);
                    });
                }
            });
            return View(ContactVm);
        }
        
       
    }   
}
