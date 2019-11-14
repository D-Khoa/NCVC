using DAL;
using BOT;
using System.Data;

namespace BLL
{
    public class sub_assy_ie_bll
    {
        Connection cn = new Connection();

        public string getMTP(efficiency_bot eff_bot)
        {
            string sql = "select a.eff_period from m_sub_assy_ie a left join m_model_sub_assy b on b.model_sub_assy_id = a.model_sub_assy_id where model_no = '" + eff_bot.Model + "' and sub_assy_no = '" + eff_bot.SubAssy + "'";
            return cn.GetValueString(sql);
        }

        public string getST(efficiency_bot eff_bot)
        {
            string sql = "select a.eff_st from m_sub_assy_ie a left join m_model_sub_assy b on b.model_sub_assy_id = a.model_sub_assy_id where model_no = '" + eff_bot.Model + "' and sub_assy_no = '" + eff_bot.SubAssy + "'";
            return cn.GetValueString(sql);
        }

        public DataTable getListST(sub_assy_ie_bot sub_ie_bot, string place)
        {
            string sql = "select b.model_no, b.sub_assy_no, b.eff_prefix, a.eff_period, a.eff_st from m_sub_assy_ie a left join m_model_sub_assy b on a.model_sub_assy_id = b.model_sub_assy_id left join m_model_line c on b.model_no = c.model_no where a.model_sub_assy_id = b.model_sub_assy_id and c.place = '" + place + "'";

            return cn.GetAllValue(sql);
        }

        public DataTable searchListST(sub_assy_ie_bot sub_ie_bot)
        {
            string sql = "select b.model_no, b.sub_assy_no, b.eff_prefix, a.eff_period, a.eff_st from m_sub_assy_ie a left join m_model_sub_assy b on a.model_sub_assy_id = b.model_sub_assy_id left join m_model c on b.model_no = c.model_no where b.model_no = '" + sub_ie_bot.Model + "'"; //and b.sub_assy_no = '" + sub_ie_bot.SubAssy + "'";

            return cn.GetAllValue(sql);
        }

        public void UpdateST(sub_assy_ie_bot sub_ie_bot)
        {
            int ie_id = cn.GetValueInt("select model_sub_assy_id from m_model_sub_assy where sub_assy_no = '" + sub_ie_bot.SubAssy + "' and model_no = '" + sub_ie_bot.Model + "'");
            string sql = "update m_sub_assy_ie set eff_period = '" + sub_ie_bot.EffPeriod + "', eff_st = '" + sub_ie_bot.EffST + "' where model_sub_assy_id = '" + ie_id + "'";
            cn.Update(sql);
        }
    }
}