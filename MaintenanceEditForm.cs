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
            var dtEquipment = DatabaseHelper.ExecuteProcedure("sp_LayTatCaCoSoVatChat");
            cmbEquipment.DataSource = dtEquipment;
            cmbEquipment.DisplayMember = "Ten";
            cmbEquipment.ValueMember = "MaCoSoVatChat";
            cmbEquipment.SelectedIndex = -1;
        }

        private void LoadEmployees()
        {
            var dtEmployees = DatabaseHelper.ExecuteProcedure("sp_LayTatCaNhanVien");
            cmbEmployee.DataSource = dtEmployees;
            cmbEmployee.DisplayMember = "Ten";
            cmbEmployee.ValueMember = "MaNhanVien";
            cmbEmployee.SelectedIndex = -1;
        }

        private void LoadMaintenance(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@MaBaoTri", id) };
            var dt = DatabaseHelper.ExecuteProcedure("sp_LayBaoTriTheoID", parameters);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                cmbEquipment.SelectedValue = row["MaCoSoVatChat"];
                cmbEmployee.SelectedValue = row["MaNhanVien"];
                if (row["NgayBaoTri"] != DBNull.Value)
                    dtpDate.Value = Convert.ToDateTime(row["NgayBaoTri"]);
                if (row["ChiPhi"] != DBNull.Value)
                    numCost.Value = Convert.ToDecimal(row["ChiPhi"]);
                txtDescription.Text = row["MoTa"].ToString();
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
                new SqlParameter("@MaCoSoVatChat", cmbEquipment.SelectedValue),
                new SqlParameter("@MaNhanVien", cmbEmployee.SelectedValue),
                new SqlParameter("@NgayBaoTri", dtpDate.Value.Date),
                new SqlParameter("@ChiPhi", numCost.Value),
                new SqlParameter("@MoTa", txtDescription.Text)
            };

            if (maintenanceID.HasValue)
            {
                Array.Resize(ref parameters, parameters.Length + 1);
                parameters[^1] = new SqlParameter("@MaBaoTri", maintenanceID.Value);
                DatabaseHelper.ExecuteNonQuery("sp_CapNhatBaoTri", parameters);
            }
            else
            {
                DatabaseHelper.ExecuteNonQuery("sp_ThemBaoTri", parameters);
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
