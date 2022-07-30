using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {

        private readonly KeepsRepository _repo;

        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
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
            return found;
        }

        internal Keep Delete(int id, string userId)
        {
            Keep deleteCandidate = GetById(id);
            if(deleteCandidate.CreatorId != userId){
                throw new Exception("You cannot delete a Keep you did not create.");
            }
            _repo.Delete(id);
            return deleteCandidate;

        }

        internal Keep Update(Keep keepUpdate)
        {
            Keep original = GetById(keepUpdate.Id);
            original.Name = keepUpdate.Name ?? original.Name;
            original.Description = keepUpdate.Description ?? original.Name;
            _repo.Update(original);
            return original;
        }
    }
}