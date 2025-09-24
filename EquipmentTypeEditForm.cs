using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class EquipmentTypeEditForm : Form
    {
        private int? typeID;

        public EquipmentTypeEditForm(int? typeID = null)
        {
            InitializeComponent();
            this.typeID = typeID;
            if (typeID.HasValue)
            {
                LoadType(typeID.Value);
                this.Text = "Edit Equipment Type";
                btnSave.Text = "Update";
            }
            else
            {
                this.Text = "Add Equipment Type";
                btnSave.Text = "Add";
            }
        }

        private void LoadType(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@TypeID", id) };
            var dt = DatabaseHelper.ExecuteProcedure("sp_GetEquipmentTypeByID", parameters);
            if (dt.Rows.Count > 0)
            {
                txtTypeName.Text = dt.Rows[0]["TypeName"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTypeName.Text))
            {
                MessageBox.Show("Please enter type name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (typeID.HasValue)
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@TypeID", typeID.Value),
                    new SqlParameter("@TypeName", txtTypeName.Text)
                };
                DatabaseHelper.ExecuteNonQuery("sp_UpdateEquipmentType", parameters);
            }
            else
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@TypeName", txtTypeName.Text)
                };
                DatabaseHelper.ExecuteNonQuery("sp_InsertEquipmentType", parameters);
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
