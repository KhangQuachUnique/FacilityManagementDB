// File: LoginForm.cs

using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            
            // Cải thiện UX
            this.AcceptButton = btnLogin; // Nhấn Enter sẽ trigger login
            txtUsername.Focus(); // Focus vào username khi mở form
            
            // Thêm KeyDown event cho password textbox
            txtPassword.KeyDown += TxtPassword_KeyDown;
        }

        private void TxtPassword_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text; // In real app, hash password

            // Kiểm tra thông tin đầu vào
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlParameter[] parameters = {
                new SqlParameter("@TenDangNhap", username)
            };
            var dt = DatabaseHelper.ExecuteProcedure("sp_LayNguoiDungTheoTenDangNhap", parameters);

            if (dt.Rows.Count > 0)
            {
                string storedHash = dt.Rows[0]["MatKhauHash"].ToString() ?? "";
                bool isActive = Convert.ToBoolean(dt.Rows[0]["HoatDong"]);
                
                if (!isActive)
                {
                    MessageBox.Show("Tài khoản đã bị vô hiệu hóa. Vui lòng liên hệ quản trị viên.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (password == storedHash) // Demo: plain text, replace with hash check
                {
                    int roleID = Convert.ToInt32(dt.Rows[0]["MaVaiTro"]);
                    
                    // Đăng nhập thành công - bỏ MFA, vào thẳng MainForm
                    MessageBox.Show($"Đăng nhập thành công! Chào mừng {username}.", "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    MainForm mainForm = new MainForm(roleID);
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Mật khẩu không chính xác.", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Tên đăng nhập không tồn tại.", "Lỗi Đăng Nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}