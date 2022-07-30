using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {

        private readonly VaultsRepository _repo;
        private readonly ProfilesService _profilesService;

        public VaultsService(VaultsRepository repo, ProfilesService profilesService)
        {
            _repo = repo;
            _profilesService = profilesService;
        }

        internal Vault Create(Vault vaultData)
        {
            return _repo.Create(vaultData);
        }

        internal Vault GetById(int id, string userId)
        {
            Vault found = _repo.GetById(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            if (found.IsPrivate == true && found.CreatorId != userId){
                throw new Exception("This Vault is private");
            }
            return found;
        }

        internal Vault Update(Vault vaultUpdate)
        {
            Vault original = GetById(vaultUpdate.Id, vaultUpdate.CreatorId);
            if (original.CreatorId != vaultUpdate.CreatorId)
            {
                throw new Exception("You edit delete a Vault you did not create.");
            }
            original.Name = vaultUpdate.Name ?? original.Name;
            original.Description = vaultUpdate.Description ?? original.Name;
            _repo.Update(original);
            return original;
        }

        internal List<Vault> GetVaultsByAccount(string id)
        {
            return _repo.GetVaultsByAccount(id);
        }

        internal Vault Delete(int id, string userId)
        {
            Vault deleteCandidate = GetById(id, userId);
            if (deleteCandidate.CreatorId != userId)
            {
                throw new Exception("You cannot delete a Vault you did not create.");
            }
            _repo.Delete(id);
            return deleteCandidate;
        }

        internal List<Vault> GetVaultsByProfile(string profileId)
        {
             Profile found = _profilesService.GetById(profileId);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            
            return _repo.GetVaultsByProfile(profileId);
        }
    }
}