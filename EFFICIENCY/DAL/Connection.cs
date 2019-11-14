using System;
using System.Data;
using Npgsql;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DAL
{
    public class Connection
    {
        static string conString = @"Server = 192.168.193.4;Port=5432;UserId=effi;Password=effi;Database=efficiencydb; CommandTimeout=100; Timeout=100;";

        NpgsqlConnection conn = new NpgsqlConnection(conString);
        NpgsqlCommand cmd = new NpgsqlCommand();

        public void Update(string strSQL)
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = strSQL;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        //public string[] GetInfo(string strSQL)
        //{
        //    conn.Open();
        //    cmd.Connection = conn;
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = strSQL;
        //    NpgsqlDataReader rdr = cmd.ExecuteReader();
        //    var result = new string[2];
        //    while (rdr.Read())
        //    {
        //        string usname = (string)rdr["user_name"];
        //        string section = (string)rdr["section"];
        //        result[0] = usname;
        //        result[1] = section;
        //    }
        //    conn.Close();
        //    return result;
        //}

        public DataTable GetAllValue(string strSQL)
        {
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            NpgsqlDataAdapter dapter = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public string GetValueString(string sqlSQL)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlSQL;

                string value = Convert.ToString(cmd.ExecuteScalar());

                conn.Close();
                return value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL execute scalar method failed." + "\r\n" + ex.Message
                                , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                conn.Close();
                return String.Empty;
            }
        }

        public int GetValueInt(string sqlSQL)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlSQL;

                int value = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
                return value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL execute scalar method failed." + "\r\n" + ex.Message
                                , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                conn.Close();
                return 0;
            }
        }

        public bool GetValueBool(string sqlSQL)
        {
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sqlSQL;

                bool value = Convert.ToBoolean(cmd.ExecuteScalar());
                conn.Close();
                return value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SQL execute scalar method failed." + "\r\n" + ex.Message
                                , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                conn.Close();
                return false;
            }
        }

        #region New
        //public bool GetBool(string strSQL)//, bool result_message_show)
        //{
        //    try
        //    {
        //        conn = new NpgsqlConnection(conString);
        //        conn.Open();
        //        NpgsqlCommand command = new NpgsqlCommand(strSQL, conn);
        //        int response = command.ExecuteNonQuery();
        //        if (response >= 1)
        //        {
        //            //if (result_message_show) { MessageBox.Show("Successful!", "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        //            conn.Close();
        //            return true;
        //        }
        //        else
        //        {
        //            //MessageBox.Show("Not successful!", "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //            conn.Close();
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Not successful!" + System.Environment.NewLine + ex.Message
        //                        , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        conn.Close();
        //        return false;
        //    }
        //}

        //public void GetAllValue(string strSQL, ref DataTable dt)
        //{
        //    NpgsqlConnection connection = new NpgsqlConnection(conString);
        //    NpgsqlCommand command = new NpgsqlCommand();

        //    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter())
        //    {
        //        command.CommandText = strSQL;
        //        command.Connection = connection;
        //        adapter.SelectCommand = command;
        //        adapter.Fill(dt);
        //    }
        //}

        //public string GetValueString(string sqlSQL)
        //{
        //    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
        //    string value;
        //    try
        //    {
        //        connection = new NpgsqlConnection(conString);
        //        connection.Open();
        //        NpgsqlCommand cmd = new NpgsqlCommand(sqlSQL, connection);
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = sqlSQL;

        //        value = Convert.ToString(cmd.ExecuteScalar());

        //        connection.Close();
        //        return value;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("SQL executeschalar moethod failed." + "\r\n" + ex.Message
        //                        , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        connection.Close();
        //        return String.Empty;
        //    }
        //}

        //public int GetValueInt(string sqlSQL)
        //{
        //    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
        //    int value;
        //    try
        //    {
        //        connection = new NpgsqlConnection(conString);
        //        connection.Open();
        //        NpgsqlCommand cmd = new NpgsqlCommand(sqlSQL, connection);
        //        cmd.Connection = connection;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = sqlSQL;

        //        value = Convert.ToInt32(cmd.ExecuteScalar());

        //        connection.Close();
        //        return value;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("SQL executeschalar moethod failed." + "\r\n" + ex.Message
        //                        , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        connection.Close();
        //        return 0;
        //    }
        //}

        //public void getComboBoxData(string sql, ref ComboBox cmb)
        //{
        //    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
        //    NpgsqlCommand command;
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        connection = new NpgsqlConnection(conString);
        //        connection.Open();
        //        command = new NpgsqlCommand(sql, connection);
        //        adapter.SelectCommand = command;
        //        adapter.Fill(ds);
        //        adapter.Dispose();
        //        command.Dispose();

        //        cmb.Items.Clear();
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            cmb.Items.Add(row[0].ToString());
        //        }
        //        connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        connection.Close();
        //    }
        //}

        //public void getAutoCompleteData(string sql, ref TextBox txt)
        //{
        //    txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //    txt.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();

        //    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
        //    NpgsqlCommand command;
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        connection = new NpgsqlConnection(conString);
        //        connection.Open();
        //        command = new NpgsqlCommand(sql, connection);
        //        adapter.SelectCommand = command;
        //        adapter.Fill(ds);
        //        adapter.Dispose();
        //        command.Dispose();
        //        connection.Close();
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            DataCollection.Add(row[0].ToString());
        //        }
        //        txt.AutoCompleteCustomSource = DataCollection;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        connection.Close();
        //    }
        //}

        //public double sqlExecuteScalarDouble(string sql)
        //{
        //    double response;
        //    try
        //    {
        //        connection = new NpgsqlConnection(conString);
        //        connection.Open();
        //        NpgsqlCommand command = new NpgsqlCommand(sql, connection);
        //        response = Convert.ToDouble(command.ExecuteScalar());
        //        connection.Close();
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("SQL executeschalar moethod failed." + System.Environment.NewLine + ex.Message
        //                        , "Database Responce", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        connection.Close();
        //        return 100;
        //    }
        //}
    }
    #endregion
}
