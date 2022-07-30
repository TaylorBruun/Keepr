using System;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {

        private readonly VaultKeepsRepository _repo;

        public VaultKeepsService(VaultKeepsRepository repo)
        {
            _repo = repo;
        }

        internal VaultKeep Create(VaultKeep vaultKeepData)
        {
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
            return deleteCandidate;
        }
    }
}