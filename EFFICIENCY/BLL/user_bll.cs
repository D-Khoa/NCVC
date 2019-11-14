using DAL;
using BOT;
using System.Data;

namespace BLL
{
    public class user_bll
    {
        Connection cn = new Connection();
        public int CheckUser(user_bot ub)
        {
            try
            {
                int count = cn.GetValueInt("Select count(*) from m_user where user_cd='" + ub.user_cd + "'");
                if (count.Equals(0))
                {
                    return 0;
                }
                else
                {
                    if (ub.pass == cn.GetValueString("Select pass from m_user where user_cd='" + ub.user_cd + "'"))
                    {
                        ub.user_name = cn.GetValueString("Select user_name from m_user where user_cd = '" + ub.user_cd + "'");
                        ub.permission = cn.GetValueString("Select permission from m_user where user_cd = '" + ub.user_cd + "'");
                        ub.admin_flag = cn.GetValueBool("Select adminflag from m_user where user_cd = '" + ub.user_cd + "'");
                        ub.place = cn.GetValueString("Select place from m_user where user_cd = '" + ub.user_cd + "'");
                        return 1;
                    }
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public DataTable loadUserID(user_bot ub)
        {
            string sql = "select user_cd from m_user where place = '" + ub.place + "' order by user_cd";
            return cn.GetAllValue(sql);
        }

        public string loadUserName(user_bot ub)
        {
            string sql = "select user_name from m_user where user_cd = '" + ub.user_cd + "'";
            return cn.GetValueString(sql);
        }

        public void loadUserInfo(user_bot ub)
        {
            ub.user_name = cn.GetValueString("select user_name from m_user where user_cd = '" + ub.user_cd + "'");
            ub.place = cn.GetValueString("select place from m_user where user_cd = '" + ub.user_cd + "'");
        }

        public int CheckUserAccount(user_bot bot_u)
        {
            string strTemp = "Select count(*) from m_user where user_cd='" + bot_u.user_cd + "'";
            int count = cn.GetValueInt(strTemp);
            if (count.Equals(0))
            {
                return 1;
            }
            else return 0;
        }

        public string UpdatePassword(user_bot bot_u)
        {
            string sql = "update m_user set pass = '" + bot_u.pass + "' where user_cd = '" + bot_u.user_cd + "'";
            cn.Update(sql);
            return Properties.Resources.llci00001;
        }

        public string UpdateSection(user_bot bot_u)
        {
            string sql = "update m_user set place = '" + bot_u.place + "' where user_cd = '" + bot_u.user_cd + "'";
            cn.Update(sql);
            return Properties.Resources.llci00003;
        }

        public void DeleteZero()
        {
            cn.Update("delete from t_ng_item where ng_before = 0 and ng_after = 0");
            cn.Update("delete from t_op_early where op_early_qty = 0 and op_early_time = 0");
            cn.Update("delete from t_op_time where op_qty = 0 and op_time = 0");
        }
    }
}
