using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class KeepsRepository
    {

        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Keep Create(Keep keepData)
        {
            string sql = "INSERT INTO keeps (name, description, img, views, kept, shares, creatorId) VALUES (@Name, @Description, @Img, @Views, @Kept, @Shares, @CreatorId); SELECT LAST_INSERT_ID();";

           int id = _db.ExecuteScalar<int>(sql, keepData);
           keepData.Id = id;
           return (keepData);

        }

        internal List<Keep> GetAll()
        {
            string sql = "SELECT accounts.*, keeps.* FROM keeps JOIN accounts ON accounts.id = keeps.creatorId";
            return _db.Query<Profile, Keep, Keep>(sql, (profile, keep) => {
                keep.Creator = profile;
                return keep;
            }).ToList();
        }


        internal Keep GetById(int id)
        {
            string sql = "SELECT accounts.*, keeps.* FROM keeps JOIN accounts ON accounts.id = keeps.creatorId WHERE keeps.id=@id LIMIT 1";
            return _db.Query<Profile, Keep, Keep>(sql, (profile, keep) =>{
                keep.Creator = profile;
                return keep;
            }, new{id}).FirstOrDefault();
        }

        internal void Update(Keep update)
        {
            string sql = "UPDATE keeps SET name = @Name, description = @Description WHERE id = @Id LIMIT 1;";
            _db.Execute(sql, update);
        }

        internal List<VaultKeepViewModel> GetKeepsByVault(int vaultId)
        {
            string sql = "SELECT keeps.*, accounts.*, vaultKeeps.id from keeps JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id JOIN accounts ON accounts.id = keeps.creatorId WHERE vaultKeeps.vaultId = @vaultId;";
            return _db.Query<VaultKeepViewModel, Profile, VaultKeep, VaultKeepViewModel>(sql, (vaultKeepViewModel, profile, vaultKeep) => {
                vaultKeepViewModel.Creator = profile;
                vaultKeepViewModel.vaultKeepId = vaultKeep.Id;
                return vaultKeepViewModel;
            }, new{vaultId}).ToList();
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";
            _db.Execute(sql, new {id});
        }
    }
}