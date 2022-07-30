using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _keepsService;

        public KeepsController(KeepsService keepsService)
        {
            _keepsService = keepsService;
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Keep>> Create([FromBody] Keep keepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                keepData.CreatorId = userInfo.Id;
                Keep newKeep = _keepsService.Create(keepData);
                newKeep.Creator = userInfo;
                return Ok(newKeep);
            }

            catch (System.Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }


        [HttpGet]
        public ActionResult<List<Keep>> GetAll()
        {
            try
            {
                List<Keep> keeps = _keepsService.GetAll();
                return Ok(keeps);
            }
            catch (System.Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }


        [HttpGet("{id}")]
        public ActionResult<Keep> GetById(int id)
        {
            try
            {
                
                Keep keep = _keepsService.GetById(id);
                return Ok(keep);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> Update(int id, [FromBody] Keep keepUpdate){
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                keepUpdate.CreatorId = userInfo.Id;
                keepUpdate.Id = id;
                Keep updatedKeep = _keepsService.Update(keepUpdate);
                keepUpdate.Creator = userInfo;
                return Ok(keepUpdate);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> Delete(int id)
        {

            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                
                Keep keep = _keepsService.Delete(id, userInfo.Id);
                return Ok(keep);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}









