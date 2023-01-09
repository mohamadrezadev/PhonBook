using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhonBook.Api.Dto;
using PhonBook.Core.Domain.Grops;
using System.Net;
using System.Net.WebSockets;

namespace PhonBook.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GropsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;

        public GropsController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        ///<summary>
        /// Get All Groups
        ///</summary>
        [HttpGet("GetAll", Name = "Get_All")]
        public async Task<List<GropDto>> Get_All()
        {
            var list_grop = new List<GropDto>();
            try
            {
                var grops = await _groupRepository.GetAllAsync();

                if (grops.Count() > 0)
                {
                    foreach (var item in grops)
                    {
                        var gropdto = new GropDto()
                        {
                            id = item.Id,
                            Name = item.Name,
                            Name_Company = item.Name_Company,
                            Address = item.Address,
                            Code_posti = item.Code_posti,
                            Organiztion = item.Organization,
                            Tell_phone = item.Tell_phone,
                            Fax = item.Fax

                        };
                        list_grop.Add(gropdto);
                    }

                }
                return list_grop;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"erorr inserver :{ex.Message}");
                throw;
            }

        }
        /// <summary>
        /// Creates a new Grop object
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Add_Grop(GropDto Model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var grop = new Grop()
                {
                    Name = Model.Name,
                    Name_Company = Model.Name_Company,
                    Address = Model.Address,
                    Code_posti = Model.Code_posti,
                    Organization = Model.Organiztion,
                    Tell_phone = Model.Tell_phone,
                    Fax = Model.Fax

                };
                await _groupRepository.Add(grop);
                Model.id = _groupRepository.FindByName(Model.Name).Id;
                return Created("created grop",Model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"server erorr:{ex.Message}");
                throw new Exception($"{ex.Message}");
            }





        }

        ///<summary>
        /// get the specified Grop by id 
        ///</summary
        [HttpGet("GetGrop", Name = "Find_Grop")]
        public async Task<IActionResult> GetGrop(int id)
        {
            try
            {
                var grop = await _groupRepository.FindByIdAsync(id);
                if (grop !=null)
                {
                    var res = new GropDto() 
                    {
                        id=grop.Id,
                        Address = grop.Address,
                        Name = grop.Name,
                        Name_Company = grop.Name_Company,
                        Code_posti = grop.Code_posti,
                        Fax = grop.Fax,
                        Organiztion = grop.Organization,
                        Tell_phone = grop.Tell_phone,
                       
                       
                    };
                    return Ok(res);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server Erorr :{ex.Message}");
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// deletes the specified Grop with id
        /// </summary>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id <0)
            {
                return BadRequest();
            }
            try
            {
                 _groupRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                
            }
        }


        /// <summary>
        /// Update the group
        /// </summary>
        [HttpPut]
        public IActionResult Update(int id, GropDto model)
        {
            if (id==0 && !ModelState.IsValid)
            {
                return BadRequest(model);
            }
            try
            {
                var grop = new Grop() 
                {
                    Id = id,
                    Name = model.Name,
                    Address = model.Address,
                    Code_posti=model.Code_posti,
                    Fax=model.Fax,
                    Name_Company=model.Name_Company,
                    Organization=model.Organiztion,
                    Tell_phone=model.Tell_phone,
                    
                };
                _groupRepository.Update(id, grop);
                return Ok();
            }
            catch (Exception ex)
            {

                return Content(ex.Message);
            }
        }
    }
}
