using DAL;
using BOT;
using System.Data;

namespace BLL
{
    public class medical_bll
    {
        Connection cn = new Connection();

        public void inserMedical (medical_bot med_bot)
        {
            string sql = "insert into t_medical (eff_no,op_id,in_time,out_time,med_time) values ('" + med_bot.EffNo + "','" + med_bot.OPIdNo + "','" + med_bot.InTime + "','" + med_bot.OutTime + "','" + med_bot.MedTime + "')";
            cn.Update(sql);
        }

        public DataTable loadMedical (efficiency_bot eff_bot)
        {
            string sql = "select op_id, in_time, out_time, med_time from t_medical where eff_no = '" + eff_bot.EffNo + "'";
            return cn.GetAllValue(sql);
        }
    }
}
