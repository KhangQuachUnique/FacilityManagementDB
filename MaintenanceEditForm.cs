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
            UIHelper.ConfigureDialog(this);
            
            this.maintenanceID = maintenanceID;
            LoadEquipment();
            LoadEmployees();
            LoadStatusOptions();
            if (maintenanceID.HasValue)
            {
                LoadMaintenance(maintenanceID.Value);
                this.Text = "Chỉnh Sửa Bảo Trì";
                btnSave.Text = "Cập Nhật";
            }
            else
            {
                this.Text = "Thêm Bảo Trì";
                btnSave.Text = "Thêm";
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

        private void LoadStatusOptions()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("Chưa Hoàn Thành");
            cmbStatus.Items.Add("Hoàn Thành");
            cmbStatus.SelectedIndex = 0; // Default to "Chưa Hoàn Thành"
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
                if (row["TrangThai"] != DBNull.Value)
                    cmbStatus.Text = row["TrangThai"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbEquipment.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn cơ sở vật chất.", "Lỗi Xác Thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbEmployee.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhân viên.", "Lỗi Xác Thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn trạng thái.", "Lỗi Xác Thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlParameter[] parameters = {
                new SqlParameter("@MaCoSoVatChat", cmbEquipment.SelectedValue),
                new SqlParameter("@MaNhanVien", cmbEmployee.SelectedValue),
                new SqlParameter("@NgayBaoTri", dtpDate.Value.Date),
                new SqlParameter("@ChiPhi", numCost.Value),
                new SqlParameter("@MoTa", txtDescription.Text),
                new SqlParameter("@TrangThai", cmbStatus.Text)
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
