using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace FacilityManagementSystem
{
    public partial class LoginForm : Form
    {
        private readonly TableLayoutPanel? _mainLayout;
        private readonly FlowLayoutPanel? _buttonPanel;

        public LoginForm()
        {
            InitializeComponent();
            if (txtUsername == null || txtPassword == null || btnLogin == null || label1 == null || label2 == null)
            {
                MessageBox.Show("Một hoặc nhiều điều khiển không được khởi tạo.", "Lỗi Khởi Tạo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _mainLayout = new TableLayoutPanel();
            _buttonPanel = new FlowLayoutPanel();

            ConfigureUI();
            BuildLayout();
            RegisterEventHandlers();
            if (txtUsername != null) txtUsername.Focus();
        }

        private void ConfigureUI()
        {
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            StartPosition = FormStartPosition.CenterScreen;
            // Remove Size to respect designer ClientSize
            Text = "Đăng Nhập"; // Align with designer

            ConfigureTextBoxes();
            ConfigureButton();
            ConfigureLabels();
        }

        private void ConfigureTextBoxes()
        {
            if (txtUsername != null)
            {
                txtUsername.Width = 200;
                txtUsername.Height = 28;
                txtUsername.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
                txtUsername.Margin = new Padding(5);
                txtUsername.PlaceholderText = "Tên đăng nhập";
                txtUsername.Anchor = AnchorStyles.None;
            }

            if (txtPassword != null)
            {
                txtPassword.Width = 200;
                txtPassword.Height = 28;
                txtPassword.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
                txtPassword.Margin = new Padding(5);
                txtPassword.PasswordChar = '*';
                txtPassword.PlaceholderText = "Mật khẩu";
                txtPassword.Anchor = AnchorStyles.None;
            }
        }

        private void ConfigureButton()
        {
            if (btnLogin != null)
            {
                btnLogin.Width = 100;
                btnLogin.Height = 32;
                btnLogin.FlatStyle = FlatStyle.Flat;
                btnLogin.BackColor = Color.FromArgb(0, 120, 215);
                btnLogin.ForeColor = Color.White;
                btnLogin.Margin = new Padding(5);
                btnLogin.Anchor = AnchorStyles.None;
            }
        }

        private void ConfigureLabels()
        {
            if (label1 != null)
            {
                label1.AutoSize = true;
                label1.Text = "Tên Đăng Nhập:";
                label1.Margin = new Padding(5, 8, 5, 0);
                label1.Anchor = AnchorStyles.None;
            }

            if (label2 != null)
            {
                label2.AutoSize = true;
                label2.Text = "Mật Khẩu:";
                label2.Margin = new Padding(5, 8, 5, 0);
                label2.Anchor = AnchorStyles.None;
            }
        }

        private void BuildLayout()
        {
            ConfigureMainLayout();
            ConfigureButtonPanel();
            RemoveLegacyControls();
            AddControlsToLayout();
            Controls.Add(_mainLayout);
        }

        private void ConfigureMainLayout()
        {
            if (_mainLayout == null) return;
            _mainLayout.Dock = DockStyle.Fill;
            _mainLayout.ColumnCount = 1;
            _mainLayout.RowCount = 6;
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 10));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.Padding = new Padding(20);
            _mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        }

        private void ConfigureButtonPanel()
        {
            if (_buttonPanel == null) return;
            _buttonPanel.Dock = DockStyle.None;
            _buttonPanel.FlowDirection = FlowDirection.LeftToRight; // Center-aligned
            _buttonPanel.AutoSize = true;
            _buttonPanel.Padding = new Padding(5);
            if (btnLogin != null)
            {
                _buttonPanel.Controls.Add(btnLogin);
            }
            _buttonPanel.Anchor = AnchorStyles.None;
        }

        private void RemoveLegacyControls()
        {
            if (txtUsername != null) Controls.Remove(txtUsername);
            if (txtPassword != null) Controls.Remove(txtPassword);
            if (btnLogin != null) Controls.Remove(btnLogin);
            if (label1 != null) Controls.Remove(label1);
            if (label2 != null) Controls.Remove(label2);
        }

        private void AddControlsToLayout()
        {
            if (_mainLayout != null)
            {
                _mainLayout.Controls.Add(new Panel(), 0, 0); // Spacer
                if (label1 != null) _mainLayout.Controls.Add(label1, 0, 1);
                if (txtUsername != null) _mainLayout.Controls.Add(txtUsername, 0, 2);
                if (label2 != null) _mainLayout.Controls.Add(label2, 0, 3);
                if (txtPassword != null) _mainLayout.Controls.Add(txtPassword, 0, 4);
                if (_buttonPanel != null)
                    _mainLayout.Controls.Add(_buttonPanel, 0, 5);
            }
        }

        private void RegisterEventHandlers()
        {
            if (btnLogin != null)
            {
                AcceptButton = btnLogin;
                btnLogin.Click += btnLogin_Click;
            }
            if (txtPassword != null)
            {
                txtPassword.KeyDown += TxtPassword_KeyDown;
            }
        }

        private void TxtPassword_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && btnLogin != null)
            {
                btnLogin.PerformClick();
            }
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            string username = txtUsername?.Text.Trim() ?? "";
            string password = txtPassword?.Text.Trim() ?? "";
            Console.WriteLine($"Attempting login with username: {username}");

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ShowWarning("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.");
                return;
            }

            try
            {
                Console.WriteLine("Calling DatabaseHelper.LoginUser...");
                if (DatabaseHelper.LoginUser(username, password))
                {
                    Console.WriteLine("Login successful, getting role...");
                    UserRole role = CurrentUser.GetRoleFromLogin(username);
                    CurrentUser.SetUser(username, role);

                    MessageBox.Show($"Đăng nhập thành công!\nChào mừng {CurrentUser.GetRoleDisplayName(role)}.",
                        "Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                    this.Hide();
                }
                else
                {
                    ShowWarning("Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in btnLogin_Click: {ex.Message}\n{ex.StackTrace}");
                ShowError("Lỗi khi đăng nhập", ex);
            }
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}