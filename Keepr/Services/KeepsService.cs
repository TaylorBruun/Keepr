using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {

        private readonly KeepsRepository _repo;
        private readonly VaultsService _vaultsService;
        private readonly ProfilesService _profilesService;

        public KeepsService(KeepsRepository repo, VaultsService vaultsService, ProfilesService profilesService)
        {
            _repo = repo;
            _vaultsService = vaultsService;
            _profilesService = profilesService;
        }

        internal Keep Create(Keep keepData)
        {
            return _repo.Create(keepData);
        }

        internal List<Keep> GetAll()
        {
            return _repo.GetAll();
        }

        internal Keep GetById(int id)
        {
            Keep found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            _repo.AddView(found.Id);
            found.Views++;
            return found;
        }

        internal void AddKept(int keepId)
        {
            _repo.AddKept(keepId);
        }
        internal void ReduceKept(int keepId)
        {
            _repo.ReduceKept(keepId);
        }

        internal Keep Update(Keep keepUpdate)
        {
            Keep original = GetById(keepUpdate.Id);
            if (original.CreatorId != keepUpdate.CreatorId)
            {
                throw new Exception("You cannot edit a Keep you did not create.");
            }
            original.Name = keepUpdate.Name ?? original.Name;
            original.Description = keepUpdate.Description ?? original.Name;
            _repo.Update(original);
            return original;
        }

        internal List<Keep> GetKeepsByProfile(string profileId)
        {
            Profile found = _profilesService.GetById(profileId);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            
            return _repo.GetKeepsByProfile(profileId);
        }

        internal Keep Delete(int id, string userId)
        {
            Keep deleteCandidate = GetById(id);
            if (deleteCandidate.CreatorId != userId)
            {
                throw new Exception("You cannot delete a Keep you did not create.");
            }
            _repo.Delete(id);
            return deleteCandidate;

        }

        internal List<VaultKeepViewModel> GetKeepsByVault(int vaultId, string userId)
        {
            Vault found = _vaultsService.GetById(vaultId, userId);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            if (found.IsPrivate == true && found.CreatorId != userId)
            {
                throw new Exception("This Vault is private");
            }
            return _repo.GetKeepsByVault(vaultId);
        }
    }
}