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
    }
}