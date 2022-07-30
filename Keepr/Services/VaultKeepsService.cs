using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {

        private readonly VaultKeepsRepository _repo;
        private readonly VaultsService _vaultsService;
        private readonly KeepsService _keepsService;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsService vaultsService, KeepsService keepsService)
        {
            _repo = repo;
            _vaultsService = vaultsService;
            _keepsService = keepsService;
        }

        internal VaultKeep Create(VaultKeep vaultKeepData)
        {
            Vault foundVault = _vaultsService.GetById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
            if (vaultKeepData.CreatorId != foundVault.CreatorId)
            {
                throw new Exception("You cannot add a Keep to a Vault you did not create.");
            }
            _keepsService.AddKept(vaultKeepData.KeepId);
            return _repo.Create(vaultKeepData);
            

        }

        internal VaultKeep GetById(int vaultKeepId, string userId)
        {
            VaultKeep found = _repo.GetById(vaultKeepId);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }

            return found;
        }


        internal VaultKeep Delete(int vaultKeepId, string userId)
        {
            VaultKeep deleteCandidate = GetById(vaultKeepId, userId);
            if (deleteCandidate.CreatorId != userId)
            {
                throw new Exception("You cannot remove a Keep from a Vault if it was not you who placed it there.");
            }
            _repo.Delete(vaultKeepId);
            _keepsService.ReduceKept(deleteCandidate.KeepId);
            return deleteCandidate;
        }
    }
}