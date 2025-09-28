using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class MaintenanceEditForm : Form
    {
        private int? maintenanceID;
        private bool _suppressEvents = false;

        public MaintenanceEditForm(int? maintenanceID = null)
        {
            InitializeComponent();
            // Basic dialog config (replacing UIHelper.ConfigureDialog)
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            this.maintenanceID = maintenanceID;
            // Cascading selections: load Areas first, then Types and Equipment based on selection
            LoadAreas();
            cmbArea.SelectedIndexChanged += cmbArea_SelectedIndexChanged;
            cmbType.SelectedIndexChanged += cmbType_SelectedIndexChanged;
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

        private void LoadAreas()
        {
            var dtAreas = DatabaseHelper.ExecuteProcedure("sp_LayTatCaKhuVuc");
            cmbArea.DataSource = dtAreas;
            cmbArea.DisplayMember = "TenKhuVuc";
            cmbArea.ValueMember = "MaKhuVuc";
            if (dtAreas != null && dtAreas.Rows.Count > 0)
            {
                cmbArea.SelectedIndex = 0;
                LoadTypesForSelectedArea();
                LoadEquipmentsForSelectedAreaAndType();
            }
            else
            {
                cmbArea.SelectedIndex = -1;
            }
        }

        private void LoadTypesForSelectedArea()
        {
            try
            {
                cmbType.DataSource = null;
                if (cmbArea.SelectedValue == null)
                {
                    cmbType.Items.Clear();
                    cmbEquipment.DataSource = null;
                    return;
                }

                // Get equipments by area to derive distinct types available in that area
                var dtEquipForArea = DatabaseHelper.ExecuteProcedure(
                    "sp_LayCoSoVatChatTheoBoLoc",
                    new SqlParameter("@MaKhuVuc", cmbArea.SelectedValue ?? (object)DBNull.Value),
                    new SqlParameter("@MaLoai", DBNull.Value),
                    new SqlParameter("@TrangThai", DBNull.Value)
                );

                if (dtEquipForArea == null)
                {
                    return;
                }

                // Create a distinct list of types from equipment in the selected area
                var view = dtEquipForArea.DefaultView;
                DataTable dtTypes = view.ToTable(true, "MaLoai", "TenLoai");
                cmbType.DataSource = dtTypes;
                cmbType.DisplayMember = "TenLoai";
                cmbType.ValueMember = "MaLoai";
                if (dtTypes.Rows.Count > 0)
                {
                    cmbType.SelectedIndex = 0;
                }
                else
                {
                    cmbType.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách loại theo khu vực: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEquipmentsForSelectedAreaAndType()
        {
            try
            {
                cmbEquipment.DataSource = null;
                object maKhuVuc = cmbArea.SelectedValue ?? (object)DBNull.Value;
                object maLoai = cmbType.SelectedValue ?? (object)DBNull.Value;

                var dtEquipment = DatabaseHelper.ExecuteProcedure(
                    "sp_LayCoSoVatChatTheoBoLoc",
                    new SqlParameter("@MaKhuVuc", maKhuVuc),
                    new SqlParameter("@MaLoai", maLoai),
                    new SqlParameter("@TrangThai", DBNull.Value)
                );
                cmbEquipment.DataSource = dtEquipment;
                cmbEquipment.DisplayMember = "Ten";
                cmbEquipment.ValueMember = "MaCoSoVatChat";
                cmbEquipment.SelectedIndex = (dtEquipment != null && dtEquipment.Rows.Count > 0) ? 0 : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách cơ sở vật chất: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbArea_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_suppressEvents) return;
            _suppressEvents = true;
            try
            {
                LoadTypesForSelectedArea();
                LoadEquipmentsForSelectedAreaAndType();
            }
            finally
            {
                _suppressEvents = false;
            }
        }

        private void cmbType_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (_suppressEvents) return;
            LoadEquipmentsForSelectedAreaAndType();
        }

        private void LoadEmployees()
        {
            // Chỉ hiển thị nhân viên kỹ thuật trong danh sách phân công bảo trì
            var dtEmployees = DatabaseHelper.ExecuteProcedure("sp_LayNhanVienKyThuat");
            cmbEmployee.DataSource = dtEmployees;
            cmbEmployee.DisplayMember = "Ten";
            cmbEmployee.ValueMember = "MaNhanVien";
            cmbEmployee.SelectedIndex = -1;

            if (dtEmployees == null || dtEmployees.Rows.Count == 0)
            {
                MessageBox.Show("Không có Nhân Viên Kỹ Thuật trong hệ thống. Vui lòng thêm nhân viên kỹ thuật trước khi tạo bảo trì.",
                    "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                // Set Area and Type to match the equipment being edited
                int maCoSoVatChat = Convert.ToInt32(row["MaCoSoVatChat"]);
                var dtEquip = DatabaseHelper.ExecuteProcedure("sp_LayCoSoVatChatTheoID", new SqlParameter("@MaCoSoVatChat", maCoSoVatChat));
                int? maKhuVuc = null;
                int? maLoai = null;
                if (dtEquip != null && dtEquip.Rows.Count > 0)
                {
                    var equipRow = dtEquip.Rows[0];
                    if (equipRow["MaKhuVuc"] != DBNull.Value) maKhuVuc = Convert.ToInt32(equipRow["MaKhuVuc"]);
                    if (equipRow["MaLoai"] != DBNull.Value) maLoai = Convert.ToInt32(equipRow["MaLoai"]);
                }

                _suppressEvents = true;
                try
                {
                    if (maKhuVuc.HasValue)
                    {
                        cmbArea.SelectedValue = maKhuVuc.Value;
                    }
                    LoadTypesForSelectedArea();
                    if (maLoai.HasValue)
                    {
                        cmbType.SelectedValue = maLoai.Value;
                    }
                    LoadEquipmentsForSelectedAreaAndType();
                    cmbEquipment.SelectedValue = maCoSoVatChat;
                }
                finally
                {
                    _suppressEvents = false;
                }
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
