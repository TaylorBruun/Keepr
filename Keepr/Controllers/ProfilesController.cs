using System.Collections.Generic;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _profilesService;
        private readonly KeepsService _keepsService;
        private readonly VaultsService _vaultsService;

        public ProfilesController(ProfilesService profilesService, KeepsService keepsService, VaultsService vaultsService)
        {
            _profilesService = profilesService;
            _keepsService = keepsService;
            _vaultsService = vaultsService;
        }

        //NOTE get by id
        [HttpGet("{id}")]
        public ActionResult<Profile> GetById(string id)
        {
            try
            {

                Profile profile = _profilesService.GetById(id);
                return Ok(profile);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        //NOTE get keeps by profile

        [HttpGet("{profileId}/keeps")]
        public ActionResult<List<Keep>> GetKeepsByProfile(string profileId)
        {
            try
            {

                List<Keep> keeps = _keepsService.GetKeepsByProfile(profileId);
                return Ok(keeps);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        //NOTE get vaults by profile
        [HttpGet("{profileId}/vaults")]
        public ActionResult<List<Vault>> GetVaultsByProfile(string profileId)
        {
            try
            {

                List<Vault> vaults = _vaultsService.GetVaultsByProfile(profileId);
                return Ok(vaults);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}