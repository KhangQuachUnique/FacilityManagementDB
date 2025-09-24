// File: EmployeeForm.cs

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class EmployeeForm : Form
    {
        private DataTable dtEmployees;
        private int currentPage = 1;
        private const int pageSize = 20;

        public EmployeeForm()
        {
            InitializeComponent();
            LoadEmployees();
            LoadAreas();
        }

        private void LoadEmployees()
        {
            dtEmployees = DatabaseHelper.ExecuteProcedure("sp_GetAllEmployees");
            dgvEmployees.DataSource = GetPagedData(dtEmployees, currentPage);
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
            if (currentPage * pageSize < dtEmployees.Rows.Count)
            {
                currentPage++;
                dgvEmployees.DataSource = GetPagedData(dtEmployees, currentPage);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                dgvEmployees.DataSource = GetPagedData(dtEmployees, currentPage);
            }
        }

        private void LoadAreas()
        {
            var dtAreas = DatabaseHelper.ExecuteProcedure("sp_GetAllAreas");
            cmbArea.DataSource = dtAreas;
            cmbArea.DisplayMember = "AreaName";
            cmbArea.ValueMember = "AreaID";
            cmbArea.SelectedIndex = -1; // Optional
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new EmployeeEditForm())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadEmployees();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                int employeeID = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["EmployeeID"].Value);
                using (var editForm = new EmployeeEditForm(employeeID))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadEmployees();
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
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                int employeeID = Convert.ToInt32(dgvEmployees.SelectedRows[0].Cells["EmployeeID"].Value);
                SqlParameter[] parameters = { new SqlParameter("@EmployeeID", employeeID) };
                DatabaseHelper.ExecuteNonQuery("sp_DeleteEmployee", parameters);
                LoadEmployees();
            }
        }

        private void dgvEmployees_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEmployees.SelectedRows.Count > 0)
            {
                var row = dgvEmployees.SelectedRows[0];
                lblNameValue.Text = row.Cells["Name"].Value?.ToString() ?? string.Empty;
                lblPositionValue.Text = row.Cells["Position"].Value?.ToString() ?? string.Empty;
                lblAreaValue.Text = row.Cells["AreaName"]?.Value?.ToString() ?? string.Empty;
            }
            else
            {
                lblNameValue.Text = string.Empty;
                lblPositionValue.Text = string.Empty;
                lblAreaValue.Text = string.Empty;
            }
        }
    }
}