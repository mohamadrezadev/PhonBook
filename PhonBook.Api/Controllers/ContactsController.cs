using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Validations;
using PhonBook.Api.Dto;
using PhonBook.Core.Domain.Contacts;
using PhonBook.Core.Domain.Grops;
using System.Collections.Immutable;
using System.Net;

namespace PhonBook.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _ContactRepository;
        private readonly IGroupRepository _GroupRepository;

        public ContactsController(IContactRepository contactRepository, IGroupRepository groupRepository)
        {
            _ContactRepository = contactRepository;
            _GroupRepository = groupRepository;
        }
        /// <summary>
        /// Gets or sets the
        ///</summary>
        [HttpGet]
        public async Task<List<ContactDto>> Get_All()
        {
            var list_Contacts = new List<ContactDto>();
            try
            {
                var result = await _ContactRepository.GetAllAsync();
                if (result.Any())
                {
                    foreach (var contact in result)
                    {
                        var contactDto = new ContactDto()
                        {
                            Id = contact.Id,
                            Email = contact.Email,
                            Fullname = contact.Fullname,
                            Phone_number1 = contact.Phone_number1,
                            Phone_number2 = contact.Phone_number2,
                            POST = contact.POST,
                            Tell_phone = contact.Tell_phone,
                            gropid = contact.Group_Id,
                        };
                        if (contactDto.gropid !=0)
                        {

                           var grop= _GroupRepository.FindById(contactDto.gropid);
                            if (grop!=null)
                                contactDto.Name_Grop = grop.Name;
                        }
                        list_Contacts.Add(contactDto);
                    }
                }
                return list_Contacts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server Erorr{ex.Message}");
                throw new Exception(ex.Message);
            }
        }



        [HttpGet("mycontact", Name = "Find_contact")]
        /// <summary>
        /// finds a contact in the specified
        ///</summary>
        public async Task<IActionResult> Get_By_id(int id)
        {
            if (id < 0)
                BadRequest("invalid Id");
                
            try
            {
                var Contact = await _ContactRepository.FindByIdAsync(id);
                if (Contact != null)
                {
                    var ContactDto = new ContactDto()
                    {
                        
                        Email = Contact.Email,
                        Fullname = Contact.Fullname,
                        Phone_number1 = Contact.Phone_number1,
                        Phone_number2 = Contact.Phone_number2,
                        POST = Contact.POST,
                        Tell_phone = Contact.Tell_phone,
                        gropid = Contact.Group_Id,
                        Id = Contact.Id,
                    };
                    if (ContactDto.gropid !=0)
                    {

                        ContactDto.Name_Grop = _GroupRepository.FindById(ContactDto.gropid).Name;

                    }
                    return Ok(ContactDto);
                }
                return NotFound() ;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server Erorr is :{ex.Message}");
                throw new Exception(ex.Message);
            }
        }


        ///<summary>
        /// creates a new instance of the <see cref="ContactDto"/> class
        ///</summary>
        [HttpPost]
        public async Task<IActionResult> Add_Grop(ContactDto Model)
        {
            if (!ModelState.IsValid)
                    BadRequest(Model);
            try
            {
                var grop = new Contact()
                {
                    Fullname = Model.Fullname,
                    Email = Model.Email,
                    Phone_number1 = Model.Phone_number1,
                    Phone_number2 = Model.Phone_number2,
                    POST=Model.POST,
                    Tell_phone = Model.Tell_phone,
                };
                await _ContactRepository.Add(grop);
                return Created("Created contact",null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server Erorr is : {ex.Message}");
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// update the Contact information
        /// </summary>
        [HttpPut]
        public IActionResult Update(int id  ,ContactDto model)
        {
            if (id < 0 && !ModelState.IsValid)
                return BadRequest(model);
            try
            {
                var conract_update = new Contact()
                {
                    Tell_phone = model.Tell_phone,
                    POST = model.POST,
                    Email = model.Email,
                    Phone_number1 = model.Phone_number1,
                    Phone_number2 = model.Phone_number2,
                    Fullname = model.Fullname,
                    Group_Id = model.gropid,
                   
                };
                 _ContactRepository.Update(id, conract_update);
                return  Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server Erorr is :{ex.Message}");
                throw new Exception(ex.Message );
            }
        }


        ///<summary>
        /// deletes the specified Contact
        ///</summary>
        [HttpDelete]
        public IActionResult Delete(int id) 
        {
            if (id < 0)
                BadRequest("invalid Id");
            try
            {
                _ContactRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server Erorr is :{ex.Message}");
                throw new Exception(ex.Message);
            }
        
        }
    }
}
