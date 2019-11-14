using DAL;
using BOT;
using System.Data;

namespace BLL
{
    public class model_line_bll
    {
        Connection cn = new Connection();
        public DataTable LoadModel(model_line_bot model_bot)
        {
            string sql_ = "select distinct model_no from m_model_line where place = '" + model_bot.Place + "' order by model_no";
            return cn.GetAllValue(sql_);
        }

        public DataTable LoadLine(model_line_bot model_bot)
        {
            string sql_ = "select line from m_model_line where model_no = '" + model_bot.ModelNo + "' order by line";
            return cn.GetAllValue(sql_);
        } 
        
        public DataTable LoadSubAssy(model_line_bot model_bot)
        {
            string sql_ = "select sub_assy_no from m_model_sub_assy where model_no = '" + model_bot.ModelNo + "'";
            return cn.GetAllValue(sql_);
        }
    }
}
