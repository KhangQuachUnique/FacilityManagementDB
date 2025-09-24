using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class EquipmentEditForm : Form
    {
        private int? equipmentID;

        public EquipmentEditForm(int? equipmentID = null)
        {
            InitializeComponent();
            this.equipmentID = equipmentID;
            LoadTypes();
            LoadAreas();
            LoadStatuses();
            if (equipmentID.HasValue)
            {
                LoadEquipmentData(equipmentID.Value);
                this.Text = "Edit Equipment";
                btnSave.Text = "Update";
            }
            else
            {
                this.Text = "Add Equipment";
                btnSave.Text = "Add";
            }
        }

        private void LoadTypes()
        {
            var dtTypes = DatabaseHelper.ExecuteProcedure("sp_LayTatCaLoaiCoSoVatChat");
            cmbType.DataSource = dtTypes;
            cmbType.DisplayMember = "TenLoai";
            cmbType.ValueMember = "MaLoai";
            cmbType.SelectedIndex = -1;
        }

        private void LoadAreas()
        {
            var dtAreas = DatabaseHelper.ExecuteProcedure("sp_LayTatCaKhuVuc");
            cmbArea.DataSource = dtAreas;
            cmbArea.DisplayMember = "TenKhuVuc";
            cmbArea.ValueMember = "MaKhuVuc";
            cmbArea.SelectedIndex = -1;
        }

        private void LoadStatuses()
        {
            cmbStatus.Items.AddRange(new string[] { "Hoạt Động", "Đang Bảo Trì", "Hỏng" });
            cmbStatus.SelectedIndex = -1;
        }

        private void LoadEquipmentData(int equipmentID)
        {
            SqlParameter[] parameters = { new SqlParameter("@MaCoSoVatChat", equipmentID) };
            var dt = DatabaseHelper.ExecuteProcedure("sp_LayCoSoVatChatTheoID", parameters);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                txtName.Text = row["Ten"].ToString();
                cmbType.SelectedValue = row["MaLoai"];
                cmbArea.SelectedValue = row["MaKhuVuc"];
                cmbStatus.SelectedItem = row["TrangThai"].ToString();
                numQuantity.Value = 1; // Không có Quantity trong schema mới
                numPrice.Value = Convert.ToDecimal(row["Gia"]);
                // Không có LastMaintenanceDate trong schema mới
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbType.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a type.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbArea.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an area.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlParameter[] parameters = {
                new SqlParameter("@Ten", txtName.Text),
                new SqlParameter("@MaLoai", cmbType.SelectedValue),
                new SqlParameter("@MaKhuVuc", cmbArea.SelectedValue),
                new SqlParameter("@TrangThai", cmbStatus.SelectedItem),
                new SqlParameter("@Gia", numPrice.Value)
            };

            if (equipmentID.HasValue)
            {
                // Update
                Array.Resize(ref parameters, parameters.Length + 1);
                parameters[parameters.Length - 1] = new SqlParameter("@MaCoSoVatChat", equipmentID.Value);
                DatabaseHelper.ExecuteNonQuery("sp_CapNhatCoSoVatChat", parameters);
            }
            else
            {
                // Add
                DatabaseHelper.ExecuteNonQuery("sp_ThemCoSoVatChat", parameters);
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