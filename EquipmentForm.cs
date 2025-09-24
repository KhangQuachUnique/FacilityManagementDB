using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class EquipmentForm : Form
    {
        private DataTable dtEquipment;
        private int currentPage = 1;
        private const int pageSize = 20;

        public EquipmentForm()
        {
            InitializeComponent();
            LoadEquipment();
            LoadTypes();
            LoadAreas();
            LoadStatuses();
        }

        private void LoadEquipment()
        {
            dtEquipment = DatabaseHelper.ExecuteProcedure("sp_GetAllEquipment");
            dgvEquipment.DataSource = GetPagedData(dtEquipment, currentPage);
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
            if (currentPage * pageSize < dtEquipment.Rows.Count)
            {
                currentPage++;
                dgvEquipment.DataSource = GetPagedData(dtEquipment, currentPage);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                dgvEquipment.DataSource = GetPagedData(dtEquipment, currentPage);
            }
        }

        private void LoadTypes()
        {
            var dtTypes = DatabaseHelper.ExecuteProcedure("sp_GetAllEquipmentTypes");
            cmbFilterType.DataSource = dtTypes;
            cmbFilterType.DisplayMember = "TypeName";
            cmbFilterType.ValueMember = "TypeID";
            cmbFilterType.SelectedIndex = -1;
        }

        private void LoadAreas()
        {
            var dtAreas = DatabaseHelper.ExecuteProcedure("sp_GetAllAreas");
            cmbFilterArea.DataSource = dtAreas;
            cmbFilterArea.DisplayMember = "AreaName";
            cmbFilterArea.ValueMember = "AreaID";
            cmbFilterArea.SelectedIndex = -1;
        }

        private void LoadStatuses()
        {
            // Demo statuses
            cmbFilterStatus.Items.AddRange(new string[] { "Operational", "Under Maintenance", "Broken" });
            cmbFilterStatus.SelectedIndex = -1;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@AreaID", cmbFilterArea.SelectedValue ?? (object)DBNull.Value),
                new SqlParameter("@TypeID", cmbFilterType.SelectedValue ?? (object)DBNull.Value),
                new SqlParameter("@Status", cmbFilterStatus.SelectedItem ?? (object)DBNull.Value)
            };
            dtEquipment = DatabaseHelper.ExecuteProcedure("sp_GetEquipmentByFilter", parameters);
            dgvEquipment.DataSource = GetPagedData(dtEquipment, currentPage = 1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new EquipmentEditForm())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadEquipment();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvEquipment.SelectedRows.Count > 0)
            {
                int equipmentID = Convert.ToInt32(dgvEquipment.SelectedRows[0].Cells["EquipmentID"].Value);
                using (var editForm = new EquipmentEditForm(equipmentID))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadEquipment();
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
            if (dgvEquipment.SelectedRows.Count > 0)
            {
                int equipmentID = Convert.ToInt32(dgvEquipment.SelectedRows[0].Cells["EquipmentID"].Value);
                SqlParameter[] parameters = { new SqlParameter("@EquipmentID", equipmentID) };
                DatabaseHelper.ExecuteNonQuery("sp_DeleteEquipment", parameters);
                LoadEquipment();
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvEquipment_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEquipment.SelectedRows.Count > 0)
            {
                lblName.Text = dgvEquipment.SelectedRows[0].Cells["Name"].Value?.ToString() ?? "";
                lblType.Text = dgvEquipment.SelectedRows[0].Cells["TypeName"].Value?.ToString() ?? "";
                lblArea.Text = dgvEquipment.SelectedRows[0].Cells["AreaName"].Value?.ToString() ?? "";
                lblStatus.Text = dgvEquipment.SelectedRows[0].Cells["Status"].Value?.ToString() ?? "";
                lblQuantity.Text = dgvEquipment.SelectedRows[0].Cells["Quantity"].Value?.ToString() ?? "0";
                lblPrice.Text = dgvEquipment.SelectedRows[0].Cells["Price"].Value != null ? Convert.ToDecimal(dgvEquipment.SelectedRows[0].Cells["Price"].Value).ToString("C") : "$0.00";
                lblLastMaintenance.Text = dgvEquipment.SelectedRows[0].Cells["LastMaintenanceDate"].Value != DBNull.Value
                    ? Convert.ToDateTime(dgvEquipment.SelectedRows[0].Cells["LastMaintenanceDate"].Value).ToShortDateString()
                    : "";
            }
            else
            {
                // Clear labels if no row is selected
                lblName.Text = "";
                lblType.Text = "";
                lblArea.Text = "";
                lblStatus.Text = "";
                lblQuantity.Text = "";
                lblPrice.Text = "";
                lblLastMaintenance.Text = "";
            }
        }
    }
}