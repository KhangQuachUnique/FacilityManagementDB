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
            // Basic dialog config (replacing UIHelper.ConfigureDialog)
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            this.typeID = typeID;
            if (typeID.HasValue)
            {
                LoadType(typeID.Value);
                this.Text = "Chỉnh Sửa Loại Thiết Bị";
                btnSave.Text = "Cập Nhật";
            }
            else
            {
                this.Text = "Thêm Loại Thiết Bị";
                btnSave.Text = "Thêm";
            }
        }

        private void LoadType(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@MaLoai", id) };
            var dt = DatabaseHelper.ExecuteProcedure("sp_LayLoaiCoSoVatChatTheoID", parameters);
            if (dt.Rows.Count > 0)
            {
                txtTypeName.Text = dt.Rows[0]["TenLoai"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTypeName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại.", "Lỗi Xác Thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (typeID.HasValue)
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@MaLoai", typeID.Value),
                    new SqlParameter("@TenLoai", txtTypeName.Text)
                };
                DatabaseHelper.ExecuteNonQuery("sp_CapNhatLoaiCoSoVatChat", parameters);
            }
            else
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@TenLoai", txtTypeName.Text)
                };
                DatabaseHelper.ExecuteNonQuery("sp_ThemLoaiCoSoVatChat", parameters);
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
