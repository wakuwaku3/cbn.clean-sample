namespace Cbn.Infrastructure.Common.Data.Interfaces
{
    public interface IDbQueryCache
    {
        string GetSqlById(string sqlId);
    }
}