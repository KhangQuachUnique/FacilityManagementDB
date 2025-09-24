using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class AreaEditForm : Form
    {
        private int? areaID;

        public AreaEditForm(int? areaID = null)
        {
            InitializeComponent();
            this.areaID = areaID;
            if (areaID.HasValue)
            {
                LoadArea(areaID.Value);
                this.Text = "Edit Area";
                btnSave.Text = "Update";
            }
            else
            {
                this.Text = "Add Area";
                btnSave.Text = "Add";
            }
        }

        private void LoadArea(int id)
        {
            SqlParameter[] parameters = { new SqlParameter("@MaKhuVuc", id) };
            var dt = DatabaseHelper.ExecuteProcedure("sp_LayKhuVucTheoID", parameters);
            if (dt.Rows.Count > 0)
            {
                txtAreaName.Text = dt.Rows[0]["TenKhuVuc"].ToString();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAreaName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khu vực.", "Lỗi Xác Thực", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (areaID.HasValue)
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@MaKhuVuc", areaID.Value),
                    new SqlParameter("@TenKhuVuc", txtAreaName.Text)
                };
                DatabaseHelper.ExecuteNonQuery("sp_CapNhatKhuVuc", parameters);
            }
            else
            {
                SqlParameter[] parameters = {
                    new SqlParameter("@TenKhuVuc", txtAreaName.Text)
                };
                DatabaseHelper.ExecuteNonQuery("sp_ThemKhuVuc", parameters);
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
