using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class ProfilesRepository
    {

        private readonly IDbConnection _db;

        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Profile GetById(string id)
        {
            string sql = "SELECT * FROM accounts WHERE accounts.id=@id LIMIT 1";
            return _db.Query<Profile>(sql, new { id }).FirstOrDefault();
        }
    }
}