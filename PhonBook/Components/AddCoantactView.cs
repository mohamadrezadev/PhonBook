using Microsoft.AspNetCore.Mvc;
using PhonBook.Core.Domain.Contacts;
using PhonBook.Core.Domain.Grops;
using PhonBook.Models;

namespace PhonBook.Components
{
    
    public class AddCoantactViewComponent : ViewComponent
    {
        private readonly IContactRepository _ContactRepository;
        private readonly IGroupRepository _GroupRepository;

        public AddCoantactViewComponent(IContactRepository contactRepository, IGroupRepository groupRepository)
        {
            _ContactRepository = contactRepository;
            _GroupRepository = groupRepository;
        }
       

        public IViewComponentResult Invoke()
        {
            ViewBag.Grop = _GroupRepository.GetAll();
            return View();
        }
    }
}
