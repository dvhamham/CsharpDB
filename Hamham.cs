using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Hamham
{
    /// <summary>
    /// <para>Developer : Mohammed Hamham</para>
    /// <para>Email     : dv.hamham@gmail.com</para>
    /// <para>Website   : hamhame.me</para>
    /// </summary>
    class DB : MDHM
    {
        public static Dictionary<string, dynamic> MParam = new Dictionary<string, dynamic>();

        public static string MConnectionString
        {
            get { return MConnString; }
            set { MConnString = value; }
        }

        //
        // Select
        //
        /// <summary>
        /// <para>Example how to use :</para>
        /// <code>
        /// DB.Select("TableName");
        /// <para>Or</para>
        /// DB.Select("TableName","ColumnsName='Text searched'");
        /// </code>
        /// Return : DataTable
        /// </summary>
        public static DataTable Select(string table, string MColumns = "*", string MCondition = null)
        {
            string MWhere = MCondition is null ? "" : $" WHERE { MCondition }";
            return query($"SELECT { MColumns } FROM { table + MWhere }", true);
        }

        //
        // Insert
        //
        /// <summary>
        /// <para>Example how to use :</para>
        /// <code>
        /// <para>DB.MParam.Add("ColumnsName1","Value1");</para>
        /// <para>DB.MParam.Add("ColumnsName2","Value2");</para>
        /// DB.Insert("TableName");
        /// </code>
        /// Return : int -> Number of rows affected.
        /// </summary>
        public static int Insert(string table)
        {
            int MResponse = 0;
            try
            {
                string columns = "", values = "";
                if (MParam.Count > 0)
                {
                    // Fetch columns and values
                    foreach (var item in MParam)
                    {
                        columns += $"{ item.Key },";
                        values += $"'{ item.Value }',";
                    }
                    MResponse = query($"INSERT INTO { table } ({ columns.TrimEnd(',') }) values({ values.TrimEnd(',') })");
                }
                else
                {
                    throw new Exception("Please fill parameter (MParam)");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                MParam.Clear();
            }
            return MResponse;
        }

        //
        // Update
        //
        /// <summary>
        /// <para>Example how to use :</para>
        /// <code>
        /// <para>DB.MParam.Add("ColumnsName1","Value1");</para>
        /// <para>DB.MParam.Add("ColumnsName2","Value2");</para>
        /// DB.Update("TableName","ID=10");
        /// </code>
        /// Return : int -> Number of rows affected.
        /// </summary>
        public static int Update(string table, string MCondition)
        {
            int MResponse = 0;
            try
            {
                if (!string.IsNullOrEmpty(MCondition))
                {
                    string MUpdate = "";
                    if (MParam.Count > 0)
                    {
                        // Fetch columns and values
                        foreach (var item in MParam)
                        {
                            MUpdate += $"{ item.Key }='{ item.Value }',";
                        }
                        MResponse = query($"UPDATE { table } SET { MUpdate.TrimEnd(',') } WHERE { MCondition }");
                    }
                    else
                    {
                        throw new Exception("Please fill parameter (MParam)");
                    }
                }
                else
                {
                    throw new Exception("The second parameter of condition is required");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                MParam.Clear();
            }
            return MResponse;
        }

        //
        // Delete
        //
        /// <summary>
        /// <para>Example how to use :</para>
        /// <code>
        /// DB.Delete("TableName","ID=10");
        /// </code>
        /// Return : int -> Number of rows affected.
        /// </summary>
        public static int Delete(string table, string MCondition)
        {
            int MResponse = 0;
            try
            {
                if (!string.IsNullOrEmpty(MCondition))
                {
                    MResponse = query($"DELETE FROM { table } WHERE { MCondition }");
                }
                else
                {
                    throw new Exception("The second parameter of condition is required");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return MResponse;
        }
    }

    /// <summary>
    /// <para>Data access</para>
    /// </summary>
    class MDHM
    {
        //
        // Connection string
        //
        protected static string MConnString;

        //
        // Execute query
        // Return int or data table
        //
        protected static dynamic query(string MQuery, bool MSelect = false)
        {
            int MResponse = 0;
            DataTable MDataTable = new DataTable();
            SqlConnection MConnection = new SqlConnection(MConnString);
            try
            {
                using (MConnection)
                {
                    SqlCommand MCommand = new SqlCommand(MQuery, MConnection);
                    MConnection.Open();
                    if (MSelect)
                    {
                        SqlDataAdapter MAdapter = new SqlDataAdapter(MCommand);
                        MAdapter.Fill(MDataTable);
                        MCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        MResponse = MCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                if (MConnection.State == ConnectionState.Open)
                {
                    MConnection.Close();
                }
            }
            if (MSelect) return MDataTable; return MResponse;
        }
    }
}
