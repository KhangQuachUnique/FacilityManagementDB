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
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text; // In real app, hash password

            SqlParameter[] parameters = {
                new SqlParameter("@TenDangNhap", username)
            };
            var dt = DatabaseHelper.ExecuteProcedure("sp_LayNguoiDungTheoTenDangNhap", parameters);

            if (dt.Rows.Count > 0)
            {
                string storedHash = dt.Rows[0]["MatKhauHash"].ToString() ?? "";
                if (password == storedHash) // Demo: plain text, replace with hash check
                {
                    int roleID = Convert.ToInt32(dt.Rows[0]["MaVaiTro"]);
                    // Simulate MFA
                    MFASimulationForm mfaForm = new MFASimulationForm();
                    if (mfaForm.ShowDialog() == DialogResult.OK)
                    {
                        MainForm mainForm = new MainForm(roleID);
                        mainForm.Show();
                        this.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid credentials.");
                }
            }
            else
            {
                MessageBox.Show("Invalid credentials.");
            }
        }
    }
}