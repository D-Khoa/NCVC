using System;
using System.Text;
using DAL;
using BOT;
using System.Data;

namespace BLL
{
    public class efficiency_bll
    {
        Connection cn = new Connection();
        public string DeleteEff(efficiency_bot eff_bot)
        {
            string sql1 = "delete from t_eff_no where eff_no = '" + eff_bot.EffNo + "'";
            string sql2 = "delete from t_medical where eff_no = '" + eff_bot.EffNo + "'";
            string sql3 = "delete from t_ng_item where eff_no = '" + eff_bot.EffNo + "'";
            string sql4 = "delete from t_op_time where eff_no = '" + eff_bot.EffNo + "'";
            string sql5 = "delete from t_op_early where eff_no = '" + eff_bot.EffNo + "'";

            cn.Update(sql1);
            cn.Update(sql2);
            cn.Update(sql3);
            cn.Update(sql4);
            cn.Update(sql5);

            return Properties.Resources.llci00004;
        }

        public void DeleteEffBf(efficiency_bot eff_bot)
        {
            string sql2 = "delete from t_medical where eff_no = '" + eff_bot.EffNo + "'";
            string sql3 = "delete from t_ng_item where eff_no = '" + eff_bot.EffNo + "'";
            string sql4 = "delete from t_op_time where eff_no = '" + eff_bot.EffNo + "'";
            string sql5 = "delete from t_op_early where eff_no = '" + eff_bot.EffNo + "'";

            cn.Update(sql2);
            cn.Update(sql3);
            cn.Update(sql4);
            cn.Update(sql5);
        }

        public DataTable LoadEffList (efficiency_bot eff_bot, int time)
        {
            StringBuilder sql_ = new StringBuilder();
            sql_.Append("select a.approve_by, a.check_by, a.eff_no, a.model_no, a.sub_assy_no, a.eff_date, a.shift, a.line, a.user_name from t_eff_no a where 1 = 1");

            sql_.Append(" and a.dept = '" + eff_bot.Dept + "'");

            if (time == 0)
            {
                DateTime lastDate = eff_bot.CreateDate;
                sql_.Append(" and a.eff_date >= '" + lastDate + "' and a.eff_date < '" + lastDate.AddDays(1) + "'");
            }

            if (!String.IsNullOrEmpty(eff_bot.Model))
            {
                sql_.Append(" and a.model_no = '" + eff_bot.Model + "'");
            }

            if (!String.IsNullOrEmpty(eff_bot.SubAssy))
            {
                sql_.Append(" and a.sub_assy_no = '" + eff_bot.SubAssy + "'");
            }

            if (!String.IsNullOrEmpty(eff_bot.Shift))
            {
                sql_.Append(" and a.shift = '" + eff_bot.Shift + "'");
            }

            if (!String.IsNullOrEmpty(eff_bot.Line))
            {
                sql_.Append(" and a.line = '" + eff_bot.Line + "'");
            }

            sql_.Append(" order by eff_id");
            return cn.GetAllValue(sql_.ToString());
        }

        public string getEffNo(efficiency_bot eff_bot)
        {
            string sql = "select max(eff_no) from t_eff_no where eff_no like '" + eff_bot.EffNo + "#" + eff_bot.SubAssy + "%'";
            return cn.GetValueString(sql);                
        }

        public string insertEfficiency(efficiency_bot eff_bot)
        {
            string sql = "insert into t_eff_no (eff_no,dept,model_no,sub_assy_no,eff_date,shift,line,user_name,prod_status_id,ot,add_time,plan_qty,item1_1,item1_2,item2_1,item2_2,item2_3,item3_1,item3_2,item3_3,item3_4,in_qty,out_qty,ng_qty,lot,remark,main_power,normal,ot_time,eff_st,act_date) values ('" + eff_bot.EffNo + "','" + eff_bot.Dept + "','" + eff_bot.Model + "','" + eff_bot.SubAssy + "','" + eff_bot.CreateDate + "','" + eff_bot.Shift + "','" + eff_bot.Line + "','" + eff_bot.UserName + "','" + eff_bot.StatusID + "','" + eff_bot.OT + "','" + eff_bot.AddTime + "','" + eff_bot.PlanQty + "','" + eff_bot.Item1_1 + "','" + eff_bot.Item1_2 + "','" + eff_bot.Item2_1 + "','" + eff_bot.Item2_2 + "','" + eff_bot.Item2_3 + "','" + eff_bot.Item3_1 + "','" + eff_bot.Item3_2 + "','" + eff_bot.Item3_3 + "','" + eff_bot.Item3_4 + "','" + eff_bot.Input + "','" + eff_bot.Output + "','" + eff_bot.NG + "','" + eff_bot.Lot + "','" + eff_bot.Remark + "', '" + eff_bot.PlanManPower + "','" + eff_bot.NormalTime + "','" + eff_bot.OTTime + "','" + eff_bot.ST + "','" + eff_bot.ActDate + "')";
            cn.Update(sql);
            return Properties.Resources.llci00002;
        }

        public string updateEfficiency(efficiency_bot eff_bot)
        {
            string sql = "update t_eff_no set add_time = '" + eff_bot.AddTime + "', plan_qty = '" + eff_bot.PlanQty + "', item1_1 = '" + eff_bot.Item1_1 + "', item1_2 = '" + eff_bot.Item1_2 + "', item2_1 = '" + eff_bot.Item2_1 + "', item2_2 = '" + eff_bot.Item2_2 + "', item2_3 = '" + eff_bot.Item2_3 + "', item3_1 = '" + eff_bot.Item3_1 + "', item3_2 = '" + eff_bot.Item3_2 + "', item3_3 = '" + eff_bot.Item3_3 + "', item3_4 = '" + eff_bot.Item3_4 + "', in_qty = '" + eff_bot.Input + "', out_qty = '" + eff_bot.Output + "', ng_qty = '" + eff_bot.NG + "', lot = '" + eff_bot.Lot + "', remark = '" + eff_bot.Remark + "', main_power = '" + eff_bot.PlanManPower + "', normal = '" + eff_bot.NormalTime + "', ot_time = '" + eff_bot.OTTime + "', eff_st = '" + eff_bot.ST + "' where eff_no = '" + eff_bot.EffNo + "'";
            cn.Update(sql);
            return Properties.Resources.llci00003;
        }

        public DataTable loadEfficiencyInfo(efficiency_bot eff_bot)
        {
            string sql = "select a.dept,b.status,a.ot,a.add_time,a.plan_qty,a.item1_1,a.item1_2,a.item2_1,a.item2_2,a.item2_3,a.item3_1,a.item3_2,a.item3_3,a.item3_4,a.in_qty,a.out_qty,a.ng_qty,a.lot,a.remark,a.eff_st from t_eff_no a left join m_prod_status b on b.prod_status_id = a.prod_status_id where eff_no = '" + eff_bot.EffNo + "'";
            return cn.GetAllValue(sql);
        }

        public DataTable loadOPQty(efficiency_bot eff_bot)
        {
            string sql = "select op_qty,op_time from t_op_time where eff_no = '" + eff_bot.EffNo + "'";
            return cn.GetAllValue(sql);
        }

        public DataTable loadOPEarlyQty(efficiency_bot eff_bot)
        {
            string sql = "select op_early_qty,op_early_time,noidung from t_op_early where eff_no = '" + eff_bot.EffNo + "'";
            return cn.GetAllValue(sql);
        }

        public void CheckReport(efficiency_bot eff_bot)
        {
            string sql = "UPDATE t_eff_no SET check_by = '" + eff_bot.Check + "' WHERE eff_no = '" + eff_bot.EffNo + "'";
            cn.Update(sql);
        }

        public void ApproveReport(efficiency_bot eff_bot)
        {
            string sql = "UPDATE t_eff_no SET approve_by = '" + eff_bot.Approve + "' WHERE eff_no = '" + eff_bot.EffNo + "'";
            cn.Update(sql);
        }

        public string GetSubAssyName(efficiency_bot eff_bot)
        {
            string sql = "select eff_prefix from m_model_sub_assy where sub_assy_no = '" + eff_bot.SubAssy + "'";
            return cn.GetValueString(sql);
        }
    }
}