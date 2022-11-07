using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TKomp.Domain;

namespace TKomp.DataAccessLayer
{
    public class ColumnInfoRepository : IColumnInfoRepository
    {        
        private readonly ISqlConnectionProvider _sqlConnectionProvider;        
        public ColumnInfoRepository(ISqlConnectionProvider sqlConnectionProvider)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
        }
        
        public List<ColumnInfo> GetColumnInfo()
        {
            using SqlConnection connection = new(_sqlConnectionProvider.GetConnectionString());
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = Queries.QuerieFiles.GetColumnInfo;
            using var reader = command.ExecuteReader();
            return reader.Cast<IDataRecord>().Select(x => new ColumnInfo { ColumnName = x.GetString(0), TableName = x.GetString(1) }).ToList();
        }
    }
}