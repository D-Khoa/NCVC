using DAL;
using BOT;
using System.Data;

namespace BLL
{
    public class ng_item_bll
    {
        Connection cn = new Connection();

        public DataTable loadNG (efficiency_bot eff_bot)
        {
            string sql = "select ng_item,ng_before,ng_after,ng_rate from t_ng_item where eff_no = '" + eff_bot.EffNo + "'";
            return cn.GetAllValue(sql);
        }

        public void insertNG (ng_item_bot ng_bot)
        {
            string sql = "insert into t_ng_item (eff_no,ng_item,ng_before,ng_after,ng_rate) values ('" + ng_bot.EffNo + "','" + ng_bot.NGItem + "','" + ng_bot.NGBefore + "','" + ng_bot.NGAfter + "','" + ng_bot.NGRate + "')";
            cn.Update(sql);
        }
    }
}