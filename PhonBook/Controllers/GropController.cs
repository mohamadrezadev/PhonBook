using Microsoft.AspNetCore.Mvc;
using PhonBook.Core.Domain.Contacts;
using PhonBook.Core.Domain.Grops;
using PhonBook.infra.Data.sqlserver.Contacts;
using PhonBook.Models;
using System.Text.RegularExpressions;

namespace PhonBook.Controllers
{
    public class GropController : Controller
    {
        private readonly IGroupRepository _GroupRepository;
        private readonly IContactRepository _ContactRepository;

        public GropController(IGroupRepository groupRepository, IContactRepository contactRepository)
        {
            _GroupRepository = groupRepository;
            _ContactRepository = contactRepository;
        }
        /// <summary>
        /// Create a New Grop
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(VmGrop model)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(model);
                }
                var Grup = new Grop
                {
                    Address = model.Address,
                    Code_posti = model.Code_posti,
                    //Contacts = model.Contacts,
                    Fax = model.Fax,
                    Name = model.Name,
                    Name_Company = model.Name_Company,
                    Organization = model.Organization,
                    Tell_phone = model.Tell_phone,


                };
                await _GroupRepository.Add(Grup);
                return RedirectToAction("Index", "Home",new {Status="Lis_Group"});
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
        /// <summary>
        /// Get a Grop
        /// </summary>
        [HttpGet]
        public JsonResult Get_Group(int id)
        {
            var result =  _GroupRepository.FindById(id);
            return Json(result);
        }
        /// <summary>
        /// Delete a group
        /// </summary>
        /// GET: /groups
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var grop = await _GroupRepository.FindByIdAsync(id);
                if (grop == null)
                    return NotFound();
                var contacts = await _ContactRepository.GetAllAsync();
                foreach (var Contactedit in contacts)
                {
                    var conatct_edit = Contactedit;
                    conatct_edit.Group_Id = 0;
                    _ContactRepository.Update(Contactedit.Id, conatct_edit);
                }
                _GroupRepository.Delete(id);
                return RedirectToAction("Index", "Home",new {Status="Lis_Group"});
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

        }

        /// <summary>
        /// Edit a Grop
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Edit(VmGrop model)
        {
            try
            {
                var res = await _GroupRepository.FindByIdAsync(model.id);
                if (res == null)
                {
                    ViewBag.message = " group is  Not Found";
                    return BadRequest(ViewBag.message);
                }
                if (!ModelState.IsValid)
                {
                    ViewBag.message = " group is  Not invalid";
                    return BadRequest(model);
                }
                var EditGroup = new Grop
                {
                    Address = model.Address,
                    Code_posti = model.Code_posti,
                    Fax = model.Fax,
                    Name = model.Name,
                    Name_Company = model.Name_Company,
                    Organization = model.Organization,
                    Tell_phone = model.Tell_phone,
                };
                _GroupRepository.Update(model.id, EditGroup);
                return RedirectToAction("index", "Home",new {Status="Lis_Group"});
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public JsonResult Find_members(int id_grop){
            try
            {
                var List_Coantact=_ContactRepository.GetAll();
                var res=List_Coantact.Any(c=>c.Group_Id==id_grop);
                return Json(res);
            }
            catch (System.Exception ex)
            {
                return Json(StatusCode(500,ex.Message));
            }
        }
    }
}
