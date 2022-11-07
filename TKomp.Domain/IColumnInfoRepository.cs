using System.Collections.Generic;

namespace TKomp.Domain
{
    public interface IColumnInfoRepository
    {
        public List<ColumnInfo> GetColumnInfo();
    }
}