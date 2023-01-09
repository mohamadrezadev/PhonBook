using Microsoft.AspNetCore.Mvc;
using PhonBook.Core.Domain.Contacts;
using PhonBook.Core.Domain.Grops;
using PhonBook.Models;
using System;
using System.Diagnostics;

namespace PhonBook.Controllers;

public class HomeController : Controller
{
    private readonly IContactRepository _ContactRepository;
    private readonly IGroupRepository _GroupRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IContactRepository contactRepository, IGroupRepository groupRepository)
    {
        _ContactRepository = contactRepository;
        _GroupRepository = groupRepository;
        _logger = logger;
    }
    /// <summary>
    /// Index main page 
    ///</summary>
    public async Task<IActionResult> Index(string Status = "List_Coantact", string Message = null)
    {
        _logger.LogInformation("Called Index endpoint");
        var result = new Basemodel();
        var vm = new vm();
        var List_grop = new List<VmGrop>();
        var List_Contacts = new List<Vmcontact>();
        var Grops = await _GroupRepository.GetAllAsync();
        _logger.LogInformation($"Grops count is  :{Grops.Count}");
        var Contacts = await _ContactRepository.GetAllAsync();
        _logger.LogInformation($"Contact count is  :{Contacts.Count}");
        Contacts.ForEach(contact =>
        {
            var vmContact = new Vmcontact()
            {
                Email = contact.Email,
                Fullname = contact.Fullname,
                Id = contact.Id,
                Phone_number1 = contact.Phone_number1,
                Phone_number2 = contact.Phone_number2,
                POST = contact.POST,
                gropid = contact.Group_Id,
                Tell_phone = contact.Tell_phone,
            };
            if (contact.Group_Id != 0)
            {

                var grop = _GroupRepository.FindById(contact.Group_Id);
                if (grop != null)
                {
                    vmContact.Name_Grop = grop.Name;
                }
            }
            List_Contacts.Add(vmContact);
        });
        Grops.ForEach(grop =>
        {
            var vmgrop = new VmGrop()
            {
                Name = grop.Name,
                Address = grop.Address,
                Code_posti = grop.Code_posti,
                Fax = grop.Fax,
                Name_Company = grop.Name_Company,
                Organization = grop.Organization,
                Tell_phone = grop.Tell_phone,
                id = grop.Id,
            };
            List_grop.Add(vmgrop);
        });
        List_Contacts.ForEach(Contact =>
        {
            List_grop.ForEach(g =>
            {
                Contact.Grops.Add(g);
            });
        });
        result.Vmcontacts = List_Contacts;
        result.Groups = Grops;
        result.VmGrops = List_grop;
        result.Status_Page = Status;
        ViewBag.message = Message;
        return View(result);
    }
    ///<summary>
    ///Get group information specific
    /// </summary>
    [HttpGet]
    public async Task<JsonResult> Get_Group(int id)
    {
        var result = await _GroupRepository.FindByIdAsync(id);
        if (result == null)
        {
            return Json("message", "not found");
        }
        return Json(result);
    }

    /// <summary>
    /// Search for a group
    /// </summary>
    [HttpGet]
    public async Task<ActionResult> Search_Group(string Name_Grop)
    { 
        _logger.LogInformation($"Request is :{Name_Grop}");
        var result = new Basemodel();
        if (string.IsNullOrEmpty(Name_Grop))
        {
            _logger.LogInformation(" name grop is null ");
            return RedirectToAction("Index", "Home", new { Status = "Lis_Group" });
        }
        try
        {
            var Grop = _GroupRepository.FindByName(Name_Grop);
            if (Grop != null)
            {
                var vmgrop = new VmGrop()
                {
                    Name = Grop.Name,
                    Address = Grop.Address,
                    Code_posti = Grop.Code_posti,
                    Fax = Grop.Fax,
                    Name_Company = Grop.Name_Company,
                    Organization = Grop.Organization,
                    Tell_phone = Grop.Tell_phone,
                    id = Grop.Id,
                };
                result.VmGrops.Add(vmgrop);
            }
            var Contacts = await _ContactRepository.GetAllAsync();
            Contacts.ForEach(contact =>
            {
                var vmContact = new Vmcontact()
                {
                    Email = contact.Email,
                    Fullname = contact.Fullname,
                    Id = contact.Id,
                    Phone_number1 = contact.Phone_number1,
                    Phone_number2 = contact.Phone_number2,
                    POST = contact.POST,
                    gropid = contact.Group_Id,
                    Tell_phone = contact.Tell_phone,
                };
                if (contact.Group_Id != 0)
                {
                    var grop = _GroupRepository.FindById(contact.Group_Id);
                    if (grop != null)
                    {
                        vmContact.Name_Grop = grop.Name;
                    }
                }
                result.Vmcontacts.Add(vmContact);
                result.Vmcontacts.Add(vmContact);
            });
            result.Status_Page = "Lis_Group";
            return View("Index", result);
        }
        catch (System.Exception ex)
        {
            _logger.LogInformation($"Erorr is :{ex.Message}, time : {DateTime.Now}") ;
            var message = "مخاطب مورد نظر یافت نشد!!";
            return RedirectToAction("Index", "Home",new{Status = "Lis_Group" , Message = message });
        }
    }
    /// <summary>
    /// Search for a contact
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Search_Contact(string Fullname)
    {
        var result = new Basemodel();
        if (string.IsNullOrEmpty(Fullname))
        {
            _logger.LogInformation("name Contact is null");
            return RedirectToAction("Index", "Home");
        }
        try
        {

            var Contact = await _ContactRepository.find_Contact_by_name_Async(Fullname);
            var Groups = _GroupRepository.GetAll();
            if (Contact != null)
            {
                var vmContact = new Vmcontact()
                {
                    Email = Contact.Email,
                    Fullname = Contact.Fullname,
                    Id = Contact.Id,
                    Phone_number1 = Contact.Phone_number1,
                    Phone_number2 = Contact.Phone_number2,
                    POST = Contact.POST,
                    gropid = Contact.Group_Id,
                    Tell_phone = Contact.Tell_phone,
                };
                result.Vmcontacts.Add(vmContact);

            }
            Groups.ForEach(grop =>
            {
                var vmgrop = new VmGrop()
                {
                    Name = grop.Name,
                    Address = grop.Address,
                    Code_posti = grop.Code_posti,
                    Fax = grop.Fax,
                    Name_Company = grop.Name_Company,
                    Organization = grop.Organization,
                    Tell_phone = grop.Tell_phone,
                    id = grop.Id,
                };
                result.VmGrops.Add(vmgrop);
            });
            result.Status_Page = "List_Coantact";
            return View("Index", result);
        }
        catch (System.Exception ex)
        {
            _logger.LogInformation($" Erorr is :{ex.Message} , datetime:{DateTime.Now}");
            var message = "مخاطب مورد نظر یافت نشد!!";
            return RedirectToAction("Index", "Home", new { Message = message });
        }



    }

    /// <summary>
    /// Search for a Grops of Contacts
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Search_namegrop_Contact(string Name_Grop)
    {
        if (string.IsNullOrEmpty(Name_Grop))
        {
            _logger.LogInformation("name Contact is null");
            return RedirectToAction("Index", "Home");
        }
        var Result = new Basemodel();
        try
        {
            var Grops = await _GroupRepository.GetAllAsync();
            var Contacts = await _ContactRepository.GetAllAsync();
            foreach (var item in Grops)
            {
                Contacts.ForEach(contact =>
                {
                    if (item.Id == contact.Group_Id)
                    {
                        var vmContact = new Vmcontact()
                        {
                            Email = contact.Email,
                            Fullname = contact.Fullname,
                            Id = contact.Id,
                            Phone_number1 = contact.Phone_number1,
                            Phone_number2 = contact.Phone_number2,
                            POST = contact.POST,
                            gropid = contact.Group_Id,
                            Tell_phone = contact.Tell_phone,
                            Name_Grop = item.Name,
                        };
                        Result.Vmcontacts.Add(vmContact);
                    }
                });
                //list grops for model
                var vmgrop = new VmGrop()
                {
                    Name = item.Name,
                    Address = item.Address,
                    Code_posti = item.Code_posti,
                    Fax = item.Fax,
                    Name_Company = item.Name_Company,
                    Organization = item.Organization,
                    Tell_phone = item.Tell_phone,
                    id = item.Id,
                };
                Result.VmGrops.Add(vmgrop);
            }
            return View("Index", Result);
        }
        catch (System.Exception ex)
        {
           _logger.LogInformation($" Erorr is :{ex.Message} , datetime:{DateTime.Now}");
            var message = "مخاطب مورد نظر یافت نشد!!";
            return RedirectToAction("Index", "Home", new { Message = message });
        }
    }
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
