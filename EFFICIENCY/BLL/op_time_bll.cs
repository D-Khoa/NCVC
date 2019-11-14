using DAL;
using BOT;

namespace BLL
{
    public class op_time_bll
    {
        Connection cn = new Connection();
        public void insertOPTime(op_time_bot op_bot)
        {
            string sql = "insert into t_op_time (eff_no,op_qty,op_time) values ('" + op_bot.EffNo + "','" + op_bot.OPQty + "','" + op_bot.OPTime + "')";
            cn.Update(sql);
        }

        public void insertOPEarly(op_time_bot op_bot)
        {
            string sql1 = "insert into t_op_early (eff_no,op_early_qty,op_early_time,noidung) values ('" + op_bot.EffNo + "','" + op_bot.OPQtyE + "','" + op_bot.OPTimeE + "','" + op_bot.NoiDung + "')";
            cn.Update(sql1);
        }
    }
}
