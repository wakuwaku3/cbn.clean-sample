using System.Data;

namespace Cbn.Infrastructure.Common.Data.Interfaces
{
    public interface IDbQuery
    {
        object[] GetParameters();
        IDbQuery Append(string sql, params IDataParameter[] parameters);
        IDbQuery AddParameters(params IDataParameter[] parameters);
        IDbQuery AddParameters(params(string name, object value) [] parameters);
        IDbQuery AppendToWhere(string condition, params IDataParameter[] parameters);
        IDbQuery AppendToWhere(string condition, params(string name, object value) [] parameters);
        IDbQuery AppendToOrderBy(string condition, params IDataParameter[] parameters);
        IDbQuery AppendToOrderBy(string condition, params(string name, object value) [] parameters);
        IDbQuery SetToOffset(int offset, int? fetch = null);
        IDbQuery Replace(string oldValue, string newValue, params IDataParameter[] parameters);
        IDbQuery Replace(string oldValue, string newValue, params(string name, object value) [] parameters);
        IDataParameter CreateParameter(string name, object value);
    }
}