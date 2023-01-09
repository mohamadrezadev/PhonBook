using Microsoft.AspNetCore.Mvc;
using PhonBook.Core.Domain.Contacts;
using PhonBook.Core.Domain.Grops;
using PhonBook.Models;

namespace PhonBook.Components
{
    public class EditContactViewComponent : ViewComponent
    {
        private readonly IGroupRepository _GroupRepository;
        private readonly IContactRepository _ContactRepository;

        public EditContactViewComponent(IContactRepository contactRepository, IGroupRepository groupRepository)
        {
            _GroupRepository = groupRepository;
            _ContactRepository = contactRepository;
        }
        public IViewComponentResult Invoke(int id_contact)
        {
            var contact = _ContactRepository.FindById(id_contact);
            var grop = _GroupRepository.FindById(contact.Group_Id);
            ViewBag.grops = _GroupRepository.GetAll();
            var Result = new Vmcontact()
            {
                Email = contact.Email,
                Fullname = contact.Fullname,
                Id = contact.Id,
                gropid = contact.Group_Id,
                Phone_number1 = contact.Phone_number1,
                Phone_number2 = contact.Phone_number2,
                Tell_phone = contact.Tell_phone,
                POST = contact.POST,
            };
            if (grop != null)
            {
                var vmGrop= new VmGrop()
                    {
                        Address = grop.Address,
                        Code_posti = grop.Code_posti,
                        Fax = grop.Fax,
                        Name = grop.Name,
                        Name_Company = grop.Name_Company,
                        Organization = grop.Organization,
                        Tell_phone = grop.Tell_phone,
                        id = grop.Id,
                    };
                Result.Grops.Add(vmGrop);
            }
            return View("Edit", Result);
        }

    }
}
