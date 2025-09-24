// File: DatabaseHelper.cs

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public class DatabaseHelper
    {
        private static string connectionString = "Server=localhost; Database=FacilityManagementDB; Trusted_Connection=True; TrustServerCertificate=True;";

        /// <summary>
        /// Executes a stored procedure and returns the result as DataTable.
        /// </summary>
        public static DataTable ExecuteProcedure(string procedureName, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// Executes a non-query stored procedure (INSERT/UPDATE/DELETE).
        /// </summary>
        public static int ExecuteNonQuery(string procedureName, params SqlParameter[] parameters)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Operation successful.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rowsAffected;
        }

        /// <summary>
        /// Executes a scalar stored procedure (e.g., for sums).
        /// </summary>
        public static object ExecuteScalar(string procedureName, params SqlParameter[] parameters)
        {
            object result = null;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (parameters != null)
                            cmd.Parameters.AddRange(parameters);
                        result = cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
    }
}