// File: EquipmentForm.cs

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
            cmbType.DataSource = dtTypes;
            cmbType.DisplayMember = "TypeName";
            cmbType.ValueMember = "TypeID";
            cmbFilterType.DataSource = dtTypes.Copy();
            cmbFilterType.DisplayMember = "TypeName";
            cmbFilterType.ValueMember = "TypeID";
            cmbFilterType.SelectedIndex = -1;
        }

        private void LoadAreas()
        {
            var dtAreas = DatabaseHelper.ExecuteProcedure("sp_GetAllAreas");
            cmbArea.DataSource = dtAreas;
            cmbArea.DisplayMember = "AreaName";
            cmbArea.ValueMember = "AreaID";
            cmbFilterArea.DataSource = dtAreas.Copy();
            cmbFilterArea.DisplayMember = "AreaName";
            cmbFilterArea.ValueMember = "AreaID";
            cmbFilterArea.SelectedIndex = -1;
        }

        private void LoadStatuses()
        {
            // Demo statuses
            cmbStatus.Items.AddRange(new string[] { "Operational", "Under Maintenance", "Broken" });
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
            SqlParameter[] parameters = {
                new SqlParameter("@Name", txtName.Text),
                new SqlParameter("@TypeID", cmbType.SelectedValue),
                new SqlParameter("@AreaID", cmbArea.SelectedValue),
                new SqlParameter("@Status", cmbStatus.SelectedItem),
                new SqlParameter("@Quantity", numQuantity.Value),
                new SqlParameter("@Price", numPrice.Value),
                new SqlParameter("@LastMaintenanceDate", dtpLastMaintenance.Value.Date)
            };
            DatabaseHelper.ExecuteNonQuery("sp_InsertEquipment", parameters);
            LoadEquipment();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvEquipment.SelectedRows.Count > 0)
            {
                int equipmentID = Convert.ToInt32(dgvEquipment.SelectedRows[0].Cells["EquipmentID"].Value);
                SqlParameter[] parameters = {
                    new SqlParameter("@EquipmentID", equipmentID),
                    new SqlParameter("@Name", txtName.Text),
                    new SqlParameter("@TypeID", cmbType.SelectedValue),
                    new SqlParameter("@AreaID", cmbArea.SelectedValue),
                    new SqlParameter("@Status", cmbStatus.SelectedItem),
                    new SqlParameter("@Quantity", numQuantity.Value),
                    new SqlParameter("@Price", numPrice.Value),
                    new SqlParameter("@LastMaintenanceDate", dtpLastMaintenance.Value.Date)
                };
                DatabaseHelper.ExecuteNonQuery("sp_UpdateEquipment", parameters);
                LoadEquipment();
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
        }

        private void dgvEquipment_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEquipment.SelectedRows.Count > 0)
            {
                txtName.Text = dgvEquipment.SelectedRows[0].Cells["Name"].Value.ToString();
                cmbType.SelectedValue = dgvEquipment.SelectedRows[0].Cells["TypeID"].Value;
                cmbArea.SelectedValue = dgvEquipment.SelectedRows[0].Cells["AreaID"].Value;
                cmbStatus.SelectedItem = dgvEquipment.SelectedRows[0].Cells["Status"].Value.ToString();
                numQuantity.Value = Convert.ToDecimal(dgvEquipment.SelectedRows[0].Cells["Quantity"].Value);
                numPrice.Value = Convert.ToDecimal(dgvEquipment.SelectedRows[0].Cells["Price"].Value);
                if (dgvEquipment.SelectedRows[0].Cells["LastMaintenanceDate"].Value != DBNull.Value)
                {
                    dtpLastMaintenance.Value = Convert.ToDateTime(dgvEquipment.SelectedRows[0].Cells["LastMaintenanceDate"].Value);
                }
            }
        }
    }
}