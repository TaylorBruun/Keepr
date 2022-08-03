using System.Collections.Generic;
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
            return _db.Query<Profile, Vault, Vault>(sql, (profile, vault) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }).FirstOrDefault();
        }

        internal void Update(Vault update)
        {
            string sql = "UPDATE vaults SET name = @Name, description = @Description WHERE id = @Id LIMIT 1;";
            _db.Execute(sql, update);
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }

        internal List<Vault> GetVaultsByProfile(string profileId)

        {
            string sql = "SELECT vaults.*, accounts.* FROM vaults JOIN accounts ON vaults.creatorId = accounts.id WHERE vaults.creatorId = @profileId AND vaults.isPrivate = 0;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { profileId }).ToList();
        }

        internal List<Vault> GetVaultsByAccount(string id)
        {
            string sql = "SELECT vaults.*, accounts.* FROM vaults JOIN accounts ON vaults.creatorId = accounts.id WHERE vaults.creatorId = @id;";
            return _db.Query<Vault, Profile, Vault>(sql, (vault, profile) =>
            {
                vault.Creator = profile;
                return vault;
            }, new { id }).ToList();
        }



        internal Dictionary<int, string> GetFirstImagesPerVaultByAccount(string profileId)
        {
            
            string sql = "SELECT (vaultKeeps.vaultId) AS Id, keeps.img AS Url FROM vaultKeeps JOIN keeps ON keeps.id = vaultKeeps.keepId WHERE vaultKeeps.creatorId = @profileId GROUP BY vaultId;";
            return _db.Query<int, string,  KeyValuePair<int, string>>(sql, (int Id, string Url) => {
                KeyValuePair<int, string> kv = new KeyValuePair<int, string>(Id, Url);
                return kv;
            }, new { profileId }).ToDictionary(pair => pair.Key, pair => pair.Value );
        }
    }
}