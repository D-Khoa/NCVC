using DAL;
using System.Data;

namespace BLL
{
    public class production_status_bll
    {
        Connection cn = new Connection();
        
        public DataTable loadProdStatus()
        {
            string sql = "select prod_status_id, status from m_prod_status";
            return cn.GetAllValue(sql);
        }
    }
}
