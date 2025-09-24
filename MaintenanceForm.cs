// File: MaintenanceForm.cs

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class MaintenanceForm : Form
    {
        private DataTable dtMaintenance;
        private int currentPage = 1;
        private const int pageSize = 20;

        public MaintenanceForm()
        {
            InitializeComponent();
            LoadMaintenance();
            LoadEquipment();
            LoadEmployees();
        }

        private void LoadMaintenance()
        {
            dtMaintenance = DatabaseHelper.ExecuteProcedure("sp_GetAllMaintenance");
            dgvMaintenance.DataSource = GetPagedData(dtMaintenance, currentPage);
        }

        private DataTable GetPagedData(DataTable dt, int page)
        {
            DataTable paged = dt.Clone();
            int start = (page - 1) * pageSize;
            for (int i = start; i < start + pageSize && i < dt.Rows.Count; i++)
            {
                paged.ImportRow(dt.Rows[i]);
            }
            return paged;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (currentPage * pageSize < dtMaintenance.Rows.Count)
            {
                currentPage++;
                dgvMaintenance.DataSource = GetPagedData(dtMaintenance, currentPage);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                dgvMaintenance.DataSource = GetPagedData(dtMaintenance, currentPage);
            }
        }

        private void LoadEquipment()
        {
            var dtEquipment = DatabaseHelper.ExecuteProcedure("sp_GetAllEquipment");
            cmbEquipment.DataSource = dtEquipment;
            cmbEquipment.DisplayMember = "Name";
            cmbEquipment.ValueMember = "EquipmentID";
            cmbFilterEquipment.DataSource = dtEquipment.Copy();
            cmbFilterEquipment.DisplayMember = "Name";
            cmbFilterEquipment.ValueMember = "EquipmentID";
            cmbFilterEquipment.SelectedIndex = -1;
        }

        private void LoadEmployees()
        {
            var dtEmployees = DatabaseHelper.ExecuteProcedure("sp_GetAllEmployees");
            cmbEmployee.DataSource = dtEmployees;
            cmbEmployee.DisplayMember = "Name";
            cmbEmployee.ValueMember = "EmployeeID";
            cmbFilterEmployee.DataSource = dtEmployees.Copy();
            cmbFilterEmployee.DisplayMember = "Name";
            cmbFilterEmployee.ValueMember = "EmployeeID";
            cmbFilterEmployee.SelectedIndex = -1;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@EquipmentID", cmbFilterEquipment.SelectedValue ?? (object)DBNull.Value),
                new SqlParameter("@EmployeeID", cmbFilterEmployee.SelectedValue ?? (object)DBNull.Value),
                new SqlParameter("@StartDate", dtpStart.Value.Date),
                new SqlParameter("@EndDate", dtpEnd.Value.Date)
            };
            dtMaintenance = DatabaseHelper.ExecuteProcedure("sp_GetMaintenanceByFilter", parameters);
            dgvMaintenance.DataSource = GetPagedData(dtMaintenance, currentPage = 1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new MaintenanceEditForm())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadMaintenance();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMaintenance.SelectedRows.Count > 0)
            {
                int maintenanceID = Convert.ToInt32(dgvMaintenance.SelectedRows[0].Cells["MaintenanceID"].Value);
                using (var editForm = new MaintenanceEditForm(maintenanceID))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadMaintenance();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a row to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvMaintenance.SelectedRows.Count > 0)
            {
                int maintenanceID = Convert.ToInt32(dgvMaintenance.SelectedRows[0].Cells["MaintenanceID"].Value);
                SqlParameter[] parameters = { new SqlParameter("@MaintenanceID", maintenanceID) };
                DatabaseHelper.ExecuteNonQuery("sp_DeleteMaintenance", parameters);
                LoadMaintenance();
            }
        }

        private void dgvMaintenance_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMaintenance.SelectedRows.Count > 0)
            {
                var row = dgvMaintenance.SelectedRows[0];
                // sp_GetAllMaintenance returns e.Name AS EquipmentName
                lblEquipmentValue.Text = row.Cells["EquipmentName"]?.Value?.ToString() ?? string.Empty;
                lblEmployeeValue.Text = row.Cells["EmployeeName"]?.Value?.ToString() ?? string.Empty;
                if (DateTime.TryParse(row.Cells["MaintenanceDate"].Value?.ToString(), out var d))
                    lblDateValue.Text = d.ToShortDateString();
                else
                    lblDateValue.Text = string.Empty;
                if (decimal.TryParse(row.Cells["Cost"].Value?.ToString(), out var c))
                    lblCostValue.Text = c.ToString("C");
                else
                    lblCostValue.Text = string.Empty;
                lblDescriptionValue.Text = row.Cells["Description"].Value?.ToString() ?? string.Empty;
            }
            else
            {
                lblEquipmentValue.Text = string.Empty;
                lblEmployeeValue.Text = string.Empty;
                lblDateValue.Text = string.Empty;
                lblCostValue.Text = string.Empty;
                lblDescriptionValue.Text = string.Empty;
            }
        }
    }
}