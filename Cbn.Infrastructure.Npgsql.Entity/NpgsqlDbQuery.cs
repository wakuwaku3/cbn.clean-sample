using System.Data;
using Cbn.Infrastructure.Common.Data;
using Cbn.Infrastructure.Common.Data.Interfaces;
using Npgsql;

namespace Cbn.Infrastructure.Npgsql.Entity
{
    public class NpgsqlDbQuery : DbQuery
    {
        public NpgsqlDbQuery(string sql = null) : base(sql) { }

        public override IDataParameter CreateParameter(string name, object value)
        {
            return new NpgsqlParameter(name, value);
        }
    }
}