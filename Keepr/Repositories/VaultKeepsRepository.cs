using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {

        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep vaultKeepData)
        {
            string sql = "INSERT INTO vaultKeeps (vaultId, keepId, creatorId) VALUES (@VaultId, @KeepId, @CreatorId); SELECT LAST_INSERT_ID();";

           int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
           vaultKeepData.Id = id;
           return (vaultKeepData);
        }

        internal VaultKeep GetById(int vaultKeepId)
        {
        string sql = "SELECT * FROM vaultKeeps WHERE vaultKeeps.id=@vaultKeepId LIMIT 1";
            return _db.Query<VaultKeep>(sql, new{vaultKeepId}).FirstOrDefault();

           
        }

        internal void Delete(int vaultKeepId)
        {
            string sql = "DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1";
            _db.Execute(sql, new {vaultKeepId});
        }
    }
}