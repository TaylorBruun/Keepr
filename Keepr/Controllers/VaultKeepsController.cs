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
    public class VaultKeepsController : ControllerBase
    {
        private readonly VaultKeepsService _vaultKeepsService;

        public VaultKeepsController(VaultKeepsService vaultKeepsService)
        {
            _vaultKeepsService = vaultKeepsService;
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep vaultKeepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultKeepData.CreatorId = userInfo.Id;
                VaultKeep newVaultKeep = _vaultKeepsService.Create(vaultKeepData);
                return Ok(newVaultKeep);
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

                VaultKeep vaultKeep = _vaultKeepsService.Delete(id, userInfo.Id);
                return Ok(vaultKeep);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


    }
}









