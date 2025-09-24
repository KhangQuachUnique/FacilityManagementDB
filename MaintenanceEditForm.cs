using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class MaintenanceEditForm : Form
    {
        private int? maintenanceID;

        public MaintenanceEditForm(int? maintenanceID = null)
        {
            InitializeComponent();
            this.maintenanceID = maintenanceID;
            LoadEquipment();
            LoadEmployees();
            if (maintenanceID.HasValue)
            {
                LoadMaintenance(maintenanceID.Value);
                this.Text = "Edit Maintenance";
                btnSave.Text = "Update";
            }
            else
            {
                this.Text = "Add Maintenance";
                btnSave.Text = "Add";
            }
        }

        private void LoadEquipment()
        {
            var dtEquipment = DatabaseHelper.ExecuteProcedure("sp_GetAllEquipment");
            cmbEquipment.DataSource = dtEquipment;
            cmbEquipment.DisplayMember = "Name";
            cmbEquipment.ValueMember = "EquipmentID";
            cmbEquipment.SelectedIndex = -1;
        }

        private void LoadEmployees()
        {
            var dtEmployees = DatabaseHelper.ExecuteProcedure("sp_GetAllEmployees");
            cmbEmployee.DataSource = dtEmployees;
            cmbEmployee.DisplayMember = "Name";
            cmbEmployee.ValueMember = "EmployeeID";
            cmbEmployee.SelectedIndex = -1;
        }

        private void LoadMaintenance(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@MaintenanceID", id) };
            var dt = DatabaseHelper.ExecuteProcedure("sp_GetMaintenanceByID", parameters);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                cmbEquipment.SelectedValue = row["EquipmentID"];
                cmbEmployee.SelectedValue = row["EmployeeID"];
                if (row["MaintenanceDate"] != DBNull.Value)
                    dtpDate.Value = Convert.ToDateTime(row["MaintenanceDate"]);
                if (row["Cost"] != DBNull.Value)
                    numCost.Value = Convert.ToDecimal(row["Cost"]);
                txtDescription.Text = row["Description"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbEquipment.SelectedIndex == -1)
            {
                MessageBox.Show("Please select equipment.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbEmployee.SelectedIndex == -1)
            {
                MessageBox.Show("Please select employee.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlParameter[] parameters = {
                new SqlParameter("@EquipmentID", cmbEquipment.SelectedValue),
                new SqlParameter("@EmployeeID", cmbEmployee.SelectedValue),
                new SqlParameter("@MaintenanceDate", dtpDate.Value.Date),
                new SqlParameter("@Cost", numCost.Value),
                new SqlParameter("@Description", txtDescription.Text)
            };

            if (maintenanceID.HasValue)
            {
                Array.Resize(ref parameters, parameters.Length + 1);
                parameters[^1] = new SqlParameter("@MaintenanceID", maintenanceID.Value);
                DatabaseHelper.ExecuteNonQuery("sp_UpdateMaintenance", parameters);
            }
            else
            {
                DatabaseHelper.ExecuteNonQuery("sp_InsertMaintenance", parameters);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
