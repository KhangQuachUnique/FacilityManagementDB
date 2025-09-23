// File: MainForm.cs

using System;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class MainForm : Form
    {
        private int roleID;

        public MainForm(int roleID)
        {
            this.roleID = roleID;
            InitializeComponent();
            ApplyPermissions();
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

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            new UserManagementForm().ShowDialog();
        }

        private void btnRoleManagement_Click(object sender, EventArgs e)
        {
            new RoleManagementForm().ShowDialog();
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            new EmployeeForm().ShowDialog();
        }

        private void btnArea_Click(object sender, EventArgs e)
        {
            new AreaForm().ShowDialog();
        }

        private void btnEquipmentType_Click(object sender, EventArgs e)
        {
            new EquipmentTypeForm().ShowDialog();
        }

        private void btnEquipment_Click(object sender, EventArgs e)
        {
            new EquipmentForm().ShowDialog();
        }

        private void btnMaintenance_Click(object sender, EventArgs e)
        {
            new MaintenanceForm().ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            new ReportsForm().ShowDialog();
        }
    }
}