// File: ReportsForm.cs

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
            LoadAreas();
            LoadTypes();
        }

        private void LoadAreas()
        {
            var dtAreas = DatabaseHelper.ExecuteProcedure("sp_LayTatCaKhuVuc");
            cmbArea.DataSource = dtAreas;
            cmbArea.DisplayMember = "TenKhuVuc";
            cmbArea.ValueMember = "MaKhuVuc";
        }

        private void LoadTypes()
        {
            var dtTypes = DatabaseHelper.ExecuteProcedure("sp_LayTatCaLoaiCoSoVatChat");
            cmbType.DataSource = dtTypes;
            cmbType.DisplayMember = "TenLoai";
            cmbType.ValueMember = "MaLoai";
        }

        private void btnMaintenanceCost_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Thang", numMonth.Value),
                new SqlParameter("@Nam", numYear.Value)
            };
            object result = DatabaseHelper.ExecuteScalar("sp_BaoCaoChiPhiBaoTriTheoThangNam", parameters);
            lblMaintenanceCost.Text = "Total Maintenance Cost: " + (result ?? "0").ToString();
        }

        private void btnValueByArea_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@MaKhuVuc", cmbArea.SelectedValue)
            };
            object result = DatabaseHelper.ExecuteScalar("sp_BaoCaoGiaTriCoSoVatChatTheoKhuVuc", parameters);
            lblValueByArea.Text = "Total Value by Area: " + (result ?? "0").ToString();
        }

        private void btnValueByType_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@MaLoai", cmbType.SelectedValue)
            };
            object result = DatabaseHelper.ExecuteScalar("sp_BaoCaoGiaTriCoSoVatChatTheoLoai", parameters);
            lblValueByType.Text = "Total Value by Type: " + (result ?? "0").ToString();
        }

        private void btnNeedingMaintenance_Click(object sender, EventArgs e)
        {
            // Vì schema mới không có LastMaintenanceDate, ta sẽ tạo báo cáo tổng số cơ sở vật chất
            DataTable dt = DatabaseHelper.ExecuteProcedure("sp_LayTatCaCoSoVatChat");
            dgvNeedingMaintenance.DataSource = dt;
        }
    }
}