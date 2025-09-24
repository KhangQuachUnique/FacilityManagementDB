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
            dtMaintenance = DatabaseHelper.ExecuteProcedure("sp_LayTatCaBaoTri");
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
            var dtEquipment = DatabaseHelper.ExecuteProcedure("sp_LayTatCaCoSoVatChat");
            cmbEquipment.DataSource = dtEquipment;
            cmbEquipment.DisplayMember = "Ten";
            cmbEquipment.ValueMember = "MaCoSoVatChat";
            cmbFilterEquipment.DataSource = dtEquipment.Copy();
            cmbFilterEquipment.DisplayMember = "Ten";
            cmbFilterEquipment.ValueMember = "MaCoSoVatChat";
            cmbFilterEquipment.SelectedIndex = -1;
        }

        private void LoadEmployees()
        {
            var dtEmployees = DatabaseHelper.ExecuteProcedure("sp_LayTatCaNhanVien");
            cmbEmployee.DataSource = dtEmployees;
            cmbEmployee.DisplayMember = "Ten";
            cmbEmployee.ValueMember = "MaNhanVien";
            cmbFilterEmployee.DataSource = dtEmployees.Copy();
            cmbFilterEmployee.DisplayMember = "Ten";
            cmbFilterEmployee.ValueMember = "MaNhanVien";
            cmbFilterEmployee.SelectedIndex = -1;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@MaCoSoVatChat", cmbFilterEquipment.SelectedValue ?? (object)DBNull.Value),
                new SqlParameter("@MaNhanVien", cmbFilterEmployee.SelectedValue ?? (object)DBNull.Value),
                new SqlParameter("@NgayBatDau", dtpStart.Value.Date),
                new SqlParameter("@NgayKetThuc", dtpEnd.Value.Date)
            };
            dtMaintenance = DatabaseHelper.ExecuteProcedure("sp_LayBaoTriTheoBoLoc", parameters);
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
                int maintenanceID = Convert.ToInt32(dgvMaintenance.SelectedRows[0].Cells["MaBaoTri"].Value);
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
                int maintenanceID = Convert.ToInt32(dgvMaintenance.SelectedRows[0].Cells["MaBaoTri"].Value);
                SqlParameter[] parameters = { new SqlParameter("@MaBaoTri", maintenanceID) };
                DatabaseHelper.ExecuteNonQuery("sp_XoaBaoTri", parameters);
                LoadMaintenance();
            }
        }

        private void dgvMaintenance_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMaintenance.SelectedRows.Count > 0)
            {
                var row = dgvMaintenance.SelectedRows[0];
                // sp_LayTatCaBaoTri returns e.Ten AS TenCoSoVatChat
                lblEquipmentValue.Text = row.Cells["TenCoSoVatChat"]?.Value?.ToString() ?? string.Empty;
                lblEmployeeValue.Text = row.Cells["TenNhanVien"]?.Value?.ToString() ?? string.Empty;
                if (DateTime.TryParse(row.Cells["NgayBaoTri"].Value?.ToString(), out var d))
                    lblDateValue.Text = d.ToShortDateString();
                else
                    lblDateValue.Text = string.Empty;
                if (decimal.TryParse(row.Cells["ChiPhi"].Value?.ToString(), out var c))
                    lblCostValue.Text = c.ToString("C");
                else
                    lblCostValue.Text = string.Empty;
                lblDescriptionValue.Text = row.Cells["MoTa"].Value?.ToString() ?? string.Empty;
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