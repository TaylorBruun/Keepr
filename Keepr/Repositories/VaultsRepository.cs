using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultsRepository

    {
    private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault Create(Vault vaultData)
        {
            string sql = "INSERT INTO vaults (name, description, isPrivate, creatorId) VALUES (@Name, @Description, @IsPrivate, @CreatorId); SELECT LAST_INSERT_ID();";
            
            int id = _db.ExecuteScalar<int>(sql, vaultData);
           vaultData.Id = id;
           return (vaultData);
        }

        internal Vault GetById(int id)
        {
             string sql = "SELECT accounts.*, vaults.* FROM vaults JOIN accounts ON accounts.id = vaults.creatorId WHERE vaults.id=@id LIMIT 1";
            return _db.Query<Profile, Vault, Vault>(sql, (profile, vault) =>{
                vault.Creator = profile;
                return vault;
            }, new{id}).FirstOrDefault();
        }

        internal void Update(Vault update)
        {
            string sql = "UPDATE vaults SET name = @Name, description = @Description WHERE id = @Id LIMIT 1;";
            _db.Execute(sql, update);
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
            _db.Execute(sql, new {id});
        }
    }
}