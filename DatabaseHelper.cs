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
        private static string connectionString = "Server=localhost; Database=QuanLyCoSoVatChatDB; Trusted_Connection=True; TrustServerCertificate=True; MultipleActiveResultSets=True; Encrypt=False;";

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

        // ============================================
        // PHƯƠNG THỨC TÌM KIẾM THEO TÊN CƠ BẢN
        // ============================================

        /// <summary>
        /// Tìm kiếm Khu Vực theo tên
        /// </summary>
        public static DataTable SearchAreaByName(string tenKhuVuc)
        {
            return ExecuteProcedure("sp_TimKiemKhuVucTheoTen", 
                new SqlParameter("@TenKhuVuc", tenKhuVuc ?? ""));
        }

        /// <summary>
        /// Tìm kiếm Nhân Viên theo tên
        /// </summary>
        public static DataTable SearchEmployeeByName(string tenNhanVien)
        {
            return ExecuteProcedure("sp_TimKiemNhanVienTheoTen", 
                new SqlParameter("@TenNhanVien", tenNhanVien ?? ""));
        }

        /// <summary>
        /// Tìm kiếm Loại Cơ Sở Vật Chất theo tên
        /// </summary>
        public static DataTable SearchEquipmentTypeByName(string tenLoai)
        {
            return ExecuteProcedure("sp_TimKiemLoaiCoSoVatChatTheoTen", 
                new SqlParameter("@TenLoai", tenLoai ?? ""));
        }

        /// <summary>
        /// Tìm kiếm Cơ Sở Vật Chất theo tên
        /// </summary>
        public static DataTable SearchEquipmentByName(string tenCoSoVatChat)
        {
            return ExecuteProcedure("sp_TimKiemCoSoVatChatTheoTen", 
                new SqlParameter("@TenCoSoVatChat", tenCoSoVatChat ?? ""));
        }

        /// <summary>
        /// Tìm kiếm Bảo Trì theo tên cơ sở vật chất
        /// </summary>
        public static DataTable SearchMaintenanceByEquipmentName(string tenCoSoVatChat)
        {
            return ExecuteProcedure("sp_TimKiemBaoTriTheoTenCoSoVatChat", 
                new SqlParameter("@TenCoSoVatChat", tenCoSoVatChat ?? ""));
        }

        /// <summary>
        /// Tìm kiếm Bảo Trì theo tên nhân viên
        /// </summary>
        public static DataTable SearchMaintenanceByEmployeeName(string tenNhanVien)
        {
            return ExecuteProcedure("sp_TimKiemBaoTriTheoTenNhanVien", 
                new SqlParameter("@TenNhanVien", tenNhanVien ?? ""));
        }

        /// <summary>
        /// Tìm kiếm Vai Trò theo tên
        /// </summary>
        public static DataTable SearchRoleByName(string tenVaiTro)
        {
            return ExecuteProcedure("sp_TimKiemVaiTroTheoTen", 
                new SqlParameter("@TenVaiTro", tenVaiTro ?? ""));
        }

        /// <summary>
        /// Tìm kiếm Người Dùng theo tên đăng nhập
        /// </summary>
        public static DataTable SearchUserByUsername(string tenDangNhap)
        {
            return ExecuteProcedure("sp_TimKiemNguoiDungTheoTenDangNhap", 
                new SqlParameter("@TenDangNhap", tenDangNhap ?? ""));
        }
    }
}