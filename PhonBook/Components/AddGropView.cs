using Microsoft.AspNetCore.Mvc;
using PhonBook.Core.Domain.Grops;
using PhonBook.infra.Data.sqlserver.Grops;

namespace PhonBook.Components
{
    public class AddGropViewComponent : ViewComponent
    {
        private readonly IGroupRepository _groupRepository;

        public AddGropViewComponent(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public IViewComponentResult Invoke()
        {
            var grops= _groupRepository.GetAllAsync();
            return  View("index");
        }
    }
}
