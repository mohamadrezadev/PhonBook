using Microsoft.AspNetCore.Mvc;
using PhonBook.Core.Domain.Contacts;
using PhonBook.Core.Domain.Grops;
using PhonBook.Models;

namespace PhonBook.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepository _ContactRepository;
        private IGroupRepository _GroupRepository;

        public ContactController(IContactRepository contactRepository, IGroupRepository groupRepository)
        {
            _ContactRepository = contactRepository;
            _GroupRepository = groupRepository;
        }
        /// <summary>
        /// Create a new Contact object
        /// </summary>
        [HttpPost]
        public  async Task< ActionResult> Add_Contact (Vmcontact model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("invalid contact");
                }
                var gruop = _GroupRepository.FindById(model.gropid);
                var new_contact = new Contact()
                {
                    Email = model.Email,
                    Fullname = model.Fullname,
                    Phone_number1 = model.Phone_number1,
                    Phone_number2 = model.Phone_number2,
                    POST = model.POST,
                    Tell_phone = model.Tell_phone,
                    Group_Id=0,
                };
                if (gruop!=null){
                    new_contact.Group_Id = model.gropid;
                }
                await _ContactRepository.Add(new_contact);
                return RedirectToAction("index", "Home");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }

        /// <summary>
        /// Deletes the specified contact
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var contecat = await _ContactRepository.FindByIdAsync(Id);
                if (contecat == null)
                    return NotFound($"Message :{Id} not found" );
                _ContactRepository.Delete(Id);
                return RedirectToAction("index", "Home");
                // return RedirectToAction("Index", "Home");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        /// <summary>
        /// Updates the contact
        /// </summary>
        [HttpPost]
        public async Task< IActionResult> Update(Vmcontact model)
        {
            if (!ModelState.IsValid )
            {
                ViewBag.message = " Contact is  Not invalid";
                return BadRequest(model);
            }
            var Contact=await _ContactRepository.FindByIdAsync(model.Id);
            if (Contact==null){
                ViewBag.message = " Contact is  Not Found";
                return NotFound(model);
            }
            try
            {
                var editconact = new Contact()
                {
                    Email = model.Email,
                    Fullname = model.Fullname,
                    POST = model.POST,
                    Phone_number1 = model.Phone_number1,
                    Phone_number2 = model.Phone_number2,
                    Tell_phone = model.Tell_phone,
                    Group_Id = model.gropid,
                    Id=model.Id,
                };
                var c= await _ContactRepository.FindByIdAsync(model.Id);
                _ContactRepository.Update(model.Id, editconact);
                return RedirectToAction("index", "Home");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        /// <summary>
        /// get the contact
        /// </summary>
        [HttpGet]
        public JsonResult Find_Contact(int id)
        {
            var contact = _ContactRepository.FindById(id);
            return Json(contact);
        }
    }
}
