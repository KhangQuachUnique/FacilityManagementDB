// File: MainForm.cs

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class MainForm : Form
    {
        private int roleID;
    private TabControl? tabMain; // runtime-created tab host
        private readonly Dictionary<string, Form> openTabs = new Dictionary<string, Form>();

        public MainForm(int roleID)
        {
            this.roleID = roleID;
            InitializeComponent();
            ApplyPermissions();
            // Prepare a tab host area without changing Designer layout of buttons
            EnsureTabHost();
        }

        private void EnsureTabHost()
        {
            if (tabMain != null) return;

            // Expand window if too small to host tabs next to the left-side buttons
            if (this.ClientSize.Width < 1000 || this.ClientSize.Height < 700)
            {
                this.ClientSize = new Size(Math.Max(1200, this.ClientSize.Width), Math.Max(800, this.ClientSize.Height));
            }

            // Calculate the right edge of left-side buttons to place the TabControl next to them
            int leftEdge = 12;
            foreach (Control c in this.Controls)
            {
                if (c is Button)
                {
                    leftEdge = Math.Max(leftEdge, c.Right);
                }
            }
            leftEdge += 18; // padding

            tabMain = new TabControl
            {
                Name = "tabMain",
                Location = new Point(leftEdge, 12),
                Size = new Size(Math.Max(200, this.ClientSize.Width - (leftEdge + 12)), Math.Max(100, this.ClientSize.Height - 24)),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            this.Controls.Add(tabMain);
            tabMain.BringToFront();
        }

        private void OpenInTab(string title, Func<Form> formFactory)
        {
            EnsureTabHost();

            // If tab already exists, activate it
            var host = tabMain!; // ensured non-null by EnsureTabHost
            foreach (TabPage page in host.TabPages)
            {
                if (page.Text == title)
                {
                    host.SelectedTab = page;
                    return;
                }
            }

            var form = formFactory();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            var tab = new TabPage(title);
            tab.Controls.Add(form);
            host.TabPages.Add(tab);
            host.SelectedTab = tab;

            openTabs[title] = form;

            // If the embedded form closes itself, remove the tab as well
            form.FormClosed += (s, e) =>
            {
                if (host.TabPages.Contains(tab))
                {
                    host.TabPages.Remove(tab);
                    tab.Dispose();
                }
                openTabs.Remove(title);
            };
            form.Show();
        }

        private void ApplyPermissions()
        {
            // Admin (1): All
            // Manager (2): All except User/Role
            // Staff (3): Only Equipment, Maintenance, Reports
            if (roleID != 1)
            {
                btnUserManagement.Visible = false;
                btnRoleManagement.Visible = false;
            }
            if (roleID == 3)
            {
                btnEmployee.Visible = false;
                btnArea.Visible = false;
                btnEquipmentType.Visible = false;
            }
        }

        // private void btnUserManagement_Click(object sender, EventArgs e)
        // {
        //     OpenInTab("Users", () => new UserManagementForm());
        // }

        // private void btnRoleManagement_Click(object sender, EventArgs e)
        // {
        //     OpenInTab("Roles", () => new RoleManagementForm());
        // }

        // private void btnEmployee_Click(object sender, EventArgs e)
        // {
        //     OpenInTab("Employees", () => new EmployeeForm());
        // }

        private void btnArea_Click(object sender, EventArgs e)
        {
            OpenInTab("Areas", () => new AreaForm());
        }

        private void btnEquipmentType_Click(object sender, EventArgs e)
        {
            OpenInTab("Equipment Types", () => new EquipmentTypeForm());
        }

        private void btnEquipment_Click(object sender, EventArgs e)
        {
            OpenInTab("Equipment", () => new EquipmentForm());
        }

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            OpenInTab("Maintenance", () => new MaintenanceForm());
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            OpenInTab("Reports", () => new ReportsForm());
        }

        // ============================================
        // CHỨC NĂNG TÌM KIẾM
        // ============================================

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ShowSearchDialog();
        }

        private void ShowSearchDialog()
        {
            // Sử dụng InputBox đơn giản thay vì Microsoft.VisualBasic
            using (var inputForm = new Form())
            {
                inputForm.Text = "Tìm Kiếm Trong Hệ Thống";
                inputForm.Size = new Size(350, 150);
                inputForm.StartPosition = FormStartPosition.CenterParent;
                inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputForm.MaximizeBox = false;
                inputForm.MinimizeBox = false;

                var lblPrompt = new Label()
                {
                    Text = "Nhập từ khóa tìm kiếm:",
                    Location = new Point(10, 20),
                    Size = new Size(150, 20)
                };

                var txtInput = new TextBox()
                {
                    Location = new Point(10, 45),
                    Size = new Size(200, 20)
                };

                var btnOK = new Button()
                {
                    Text = "OK",
                    Location = new Point(220, 43),
                    Size = new Size(50, 25),
                    DialogResult = DialogResult.OK
                };

                var btnCancel = new Button()
                {
                    Text = "Hủy",
                    Location = new Point(280, 43),
                    Size = new Size(50, 25),
                    DialogResult = DialogResult.Cancel
                };

                inputForm.Controls.AddRange(new Control[] { lblPrompt, txtInput, btnOK, btnCancel });
                inputForm.AcceptButton = btnOK;
                inputForm.CancelButton = btnCancel;

                if (inputForm.ShowDialog(this) == DialogResult.OK && !string.IsNullOrEmpty(txtInput.Text))
                {
                    ShowSearchTypeDialog(txtInput.Text.Trim());
                }
            }
        }

        private void ShowSearchTypeDialog(string searchTerm)
        {
            // Hiển thị menu lựa chọn loại tìm kiếm
            using (var searchForm = new Form())
            {
                searchForm.Text = "Chọn Loại Tìm Kiếm";
                searchForm.Size = new Size(300, 250);
                searchForm.StartPosition = FormStartPosition.CenterParent;
                searchForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                searchForm.MaximizeBox = false;
                searchForm.MinimizeBox = false;

                var lblInstruction = new Label()
                {
                    Text = $"Tìm kiếm: '{searchTerm}'\nChọn loại dữ liệu cần tìm:",
                    Location = new Point(10, 10),
                    Size = new Size(260, 40)
                };

                var btnArea = new Button()
                {
                    Text = "Khu Vực",
                    Location = new Point(10, 60),
                    Size = new Size(100, 30)
                };
                btnArea.Click += (s, e) => { SearchArea(searchTerm); searchForm.Close(); };

                var btnEquipment = new Button()
                {
                    Text = "Cơ Sở Vật Chất",
                    Location = new Point(120, 60),
                    Size = new Size(100, 30)
                };
                btnEquipment.Click += (s, e) => { SearchEquipment(searchTerm); searchForm.Close(); };

                var btnMaintenance = new Button()
                {
                    Text = "Bảo Trì",
                    Location = new Point(10, 100),
                    Size = new Size(100, 30)
                };
                btnMaintenance.Click += (s, e) => { SearchMaintenance(searchTerm); searchForm.Close(); };

                var btnCancelSearch = new Button()
                {
                    Text = "Hủy",
                    Location = new Point(120, 140),
                    Size = new Size(100, 30)
                };
                btnCancelSearch.Click += (s, e) => searchForm.Close();

                searchForm.Controls.AddRange(new Control[] { lblInstruction, btnArea, btnEquipment, btnMaintenance, btnCancelSearch });
                searchForm.ShowDialog(this);
            }
        }

        private void SearchArea(string searchTerm)
        {
            OpenInTab("Areas", () => new AreaForm());
            MessageBox.Show($"Đã mở tab Khu Vực. Sử dụng ô tìm kiếm với từ khóa: {searchTerm}", 
                            "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SearchEquipment(string searchTerm)
        {
            OpenInTab("Equipment", () => new EquipmentForm());
            
            // Tìm form trong openTabs và gọi phương thức tìm kiếm
            if (openTabs.ContainsKey("Equipment") && openTabs["Equipment"] is EquipmentForm equipmentForm)
            {
                equipmentForm.SearchEquipment(searchTerm);
            }
        }

        private void SearchMaintenance(string searchTerm)
        {
            OpenInTab("Maintenance", () => new MaintenanceForm());
            
            // Tìm form trong openTabs và gọi phương thức tìm kiếm
            if (openTabs.ContainsKey("Maintenance") && openTabs["Maintenance"] is MaintenanceForm maintenanceForm)
            {
                maintenanceForm.SearchMaintenanceByEquipment(searchTerm);
            }
        }
    }
}