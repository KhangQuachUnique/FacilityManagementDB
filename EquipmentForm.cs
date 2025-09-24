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
            dtEquipment = DatabaseHelper.ExecuteProcedure("sp_LayTatCaCoSoVatChat");
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
            var dtTypes = DatabaseHelper.ExecuteProcedure("sp_LayTatCaLoaiCoSoVatChat");
            cmbFilterType.DataSource = dtTypes;
            cmbFilterType.DisplayMember = "TenLoai";
            cmbFilterType.ValueMember = "MaLoai";
            cmbFilterType.SelectedIndex = -1;
        }

        private void LoadAreas()
        {
            var dtAreas = DatabaseHelper.ExecuteProcedure("sp_LayTatCaKhuVuc");
            cmbFilterArea.DataSource = dtAreas;
            cmbFilterArea.DisplayMember = "TenKhuVuc";
            cmbFilterArea.ValueMember = "MaKhuVuc";
            cmbFilterArea.SelectedIndex = -1;
        }

        private void LoadStatuses()
        {
            // Demo statuses
            cmbFilterStatus.Items.AddRange(new string[] { "Hoạt Động", "Đang Bảo Trì", "Hỏng" });
            cmbFilterStatus.SelectedIndex = -1;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@MaKhuVuc", cmbFilterArea.SelectedValue ?? (object)DBNull.Value),
                new SqlParameter("@MaLoai", cmbFilterType.SelectedValue ?? (object)DBNull.Value),
                new SqlParameter("@TrangThai", cmbFilterStatus.SelectedItem ?? (object)DBNull.Value)
            };
            dtEquipment = DatabaseHelper.ExecuteProcedure("sp_LayCoSoVatChatTheoBoLoc", parameters);
            dgvEquipment.DataSource = GetPagedData(dtEquipment, currentPage = 1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new EquipmentEditForm())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadEquipment();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvEquipment.SelectedRows.Count > 0)
            {
                int equipmentID = Convert.ToInt32(dgvEquipment.SelectedRows[0].Cells["MaCoSoVatChat"].Value);
                using (var editForm = new EquipmentEditForm(equipmentID))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadEquipment();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để cập nhật.", "Chưa Chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEquipment.SelectedRows.Count > 0)
            {
                int equipmentID = Convert.ToInt32(dgvEquipment.SelectedRows[0].Cells["MaCoSoVatChat"].Value);
                SqlParameter[] parameters = { new SqlParameter("@MaCoSoVatChat", equipmentID) };
                DatabaseHelper.ExecuteNonQuery("sp_XoaCoSoVatChat", parameters);
                LoadEquipment();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.", "Chưa Chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvEquipment_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvEquipment.SelectedRows.Count > 0)
            {
                lblName.Text = dgvEquipment.SelectedRows[0].Cells["Ten"].Value?.ToString() ?? "";
                lblType.Text = dgvEquipment.SelectedRows[0].Cells["TenLoai"].Value?.ToString() ?? "";
                lblArea.Text = dgvEquipment.SelectedRows[0].Cells["TenKhuVuc"].Value?.ToString() ?? "";
                lblStatus.Text = dgvEquipment.SelectedRows[0].Cells["TrangThai"].Value?.ToString() ?? "";
                lblQuantity.Text = "1"; // Không có Quantity trong schema mới
                lblPrice.Text = dgvEquipment.SelectedRows[0].Cells["Gia"].Value != null ? Convert.ToDecimal(dgvEquipment.SelectedRows[0].Cells["Gia"].Value).ToString("C") : "$0.00";
                lblLastMaintenance.Text = ""; // Không có LastMaintenanceDate trong schema mới
            }
            else
            {
                // Clear labels if no row is selected
                lblName.Text = "";
                lblType.Text = "";
                lblArea.Text = "";
                lblStatus.Text = "";
                lblQuantity.Text = "";
                lblPrice.Text = "";
                lblLastMaintenance.Text = "";
            }
        }
    }
}