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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vaultsService;
        private readonly KeepsService _keepsService;

        public VaultsController(VaultsService vaultsService, KeepsService keepsService)
        {
            _vaultsService = vaultsService;
            _keepsService = keepsService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultData.CreatorId = userInfo.Id;
                Vault newVault = _vaultsService.Create(vaultData);
                newVault.Creator = userInfo;
                return Ok(newVault);
            }

            catch (System.Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> GetById(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vaultsService.GetById(id, userInfo?.Id);
                return Ok(vault);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

         [HttpGet("{id}/keeps")]
        public async Task<ActionResult<List<VaultKeepViewModel>>> GetKeepsByVault(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                List<VaultKeepViewModel> keeps = _keepsService.GetKeepsByVault(id, userInfo?.Id);
                return Ok(keeps);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Update(int id, [FromBody] Vault vaultUpdate)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                vaultUpdate.CreatorId = userInfo.Id;
                vaultUpdate.Id = id;
                Vault updatedVault = _vaultsService.Update(vaultUpdate);
                vaultUpdate.Creator = userInfo;
                return Ok(vaultUpdate);
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

                Vault vault = _vaultsService.Delete(id, userInfo.Id);
                return Ok(vault);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}