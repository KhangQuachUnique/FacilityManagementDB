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
            var dtTypes = DatabaseHelper.ExecuteProcedure("sp_GetAllEquipmentTypes");
            cmbType.DataSource = dtTypes;
            cmbType.DisplayMember = "TypeName";
            cmbType.ValueMember = "TypeID";
            cmbType.SelectedIndex = -1;
        }

        private void LoadAreas()
        {
            var dtAreas = DatabaseHelper.ExecuteProcedure("sp_GetAllAreas");
            cmbArea.DataSource = dtAreas;
            cmbArea.DisplayMember = "AreaName";
            cmbArea.ValueMember = "AreaID";
            cmbArea.SelectedIndex = -1;
        }

        private void LoadStatuses()
        {
            cmbStatus.Items.AddRange(new string[] { "Operational", "Under Maintenance", "Broken" });
            cmbStatus.SelectedIndex = -1;
        }

        private void LoadEquipmentData(int equipmentID)
        {
            SqlParameter[] parameters = { new SqlParameter("@EquipmentID", equipmentID) };
            var dt = DatabaseHelper.ExecuteProcedure("sp_GetEquipmentByID", parameters);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                txtName.Text = row["Name"].ToString();
                cmbType.SelectedValue = row["TypeID"];
                cmbArea.SelectedValue = row["AreaID"];
                cmbStatus.SelectedItem = row["Status"].ToString();
                numQuantity.Value = Convert.ToDecimal(row["Quantity"]);
                numPrice.Value = Convert.ToDecimal(row["Price"]);
                if (row["LastMaintenanceDate"] != DBNull.Value)
                {
                    dtpLastMaintenance.Value = Convert.ToDateTime(row["LastMaintenanceDate"]);
                }
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
                new SqlParameter("@Name", txtName.Text),
                new SqlParameter("@TypeID", cmbType.SelectedValue),
                new SqlParameter("@AreaID", cmbArea.SelectedValue),
                new SqlParameter("@Status", cmbStatus.SelectedItem),
                new SqlParameter("@Quantity", numQuantity.Value),
                new SqlParameter("@Price", numPrice.Value),
                new SqlParameter("@LastMaintenanceDate", dtpLastMaintenance.Value.Date)
            };

            if (equipmentID.HasValue)
            {
                // Update
                Array.Resize(ref parameters, parameters.Length + 1);
                parameters[parameters.Length - 1] = new SqlParameter("@EquipmentID", equipmentID.Value);
                DatabaseHelper.ExecuteNonQuery("sp_UpdateEquipment", parameters);
            }
            else
            {
                // Add
                DatabaseHelper.ExecuteNonQuery("sp_InsertEquipment", parameters);
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