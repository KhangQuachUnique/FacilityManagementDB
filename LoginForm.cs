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
            ConfigureUI();
            
            // Cải thiện UX
            this.AcceptButton = btnLogin; // Nhấn Enter sẽ trigger login
            txtUsername.Focus(); // Focus vào username khi mở form
            
            // Thêm KeyDown event cho password textbox
            txtPassword.KeyDown += TxtPassword_KeyDown;
        }
        
        private void ConfigureUI()
        {
            // Cấu hình form
            this.BackColor = UIHelper.BackgroundColor;
            this.Font = UIHelper.StandardFont;
            this.Size = new System.Drawing.Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Cấu hình controls
            UIHelper.ConfigureButton(btnLogin, true);
            UIHelper.ConfigureTextBox(txtUsername);
            UIHelper.ConfigureTextBox(txtPassword);
            
            // Cấu hình labels
            foreach (Control control in this.Controls)
            {
                if (control is Label lbl)
                    UIHelper.ConfigureLabel(lbl);
            }
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
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Kiểm tra thông tin đầu vào
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Sử dụng SQL Server Authentication
            if (DatabaseHelper.LoginUser(username, password))
            {
                // Xác định role từ login name
                UserRole role = CurrentUser.GetRoleFromLogin(username);
                CurrentUser.SetUser(username, role);
                
                MessageBox.Show($"Đăng nhập thành công!\nChào mừng {CurrentUser.GetRoleDisplayName(role)}.", 
                              "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                MainForm mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
            }
            // Nếu login thất bại, thông báo lỗi đã được hiển thị trong DatabaseHelper.LoginUser()
        }
    }
}