// File: MaintenanceForm.cs

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class MaintenanceForm : Form
    {
        private DataTable? dtMaintenance;
        private int currentPage = 1;
        private const int pageSize = 50;

        public MaintenanceForm()
        {
            InitializeComponent();
            // Thêm event handler cho DataBindingComplete để áp dụng màu sắc
            dgvMaintenance.DataBindingComplete += DgvMaintenance_DataBindingComplete;
            LoadMaintenance();
            LoadEquipment();
            LoadEmployees();
        }

        private void LoadMaintenance()
        {
            dtMaintenance = DatabaseHelper.ExecuteProcedure("sp_LayTatCaBaoTri");
            SetupColumnHeaders();
            UpdateDataGridView(dtMaintenance, currentPage);
        }

        private void SetupColumnHeaders()
        {
            if (dgvMaintenance.Columns.Count > 0)
            {
                var colMa = dgvMaintenance.Columns["MaBaoTri"];
                if (colMa != null)
                {
                    colMa.HeaderText = "Mã Bảo Trì";
                    colMa.Width = 80;
                }
                
                var colCoSoVatChat = dgvMaintenance.Columns["TenCoSoVatChat"];
                if (colCoSoVatChat != null)
                {
                    colCoSoVatChat.HeaderText = "Tên Cơ Sở Vật Chất";
                    colCoSoVatChat.Width = 200;
                }
                
                var colNhanVien = dgvMaintenance.Columns["TenNhanVien"];
                if (colNhanVien != null)
                {
                    colNhanVien.HeaderText = "Nhân Viên Bảo Trì";
                    colNhanVien.Width = 150;
                }
                
                var colNgay = dgvMaintenance.Columns["NgayBaoTri"];
                if (colNgay != null)
                {
                    colNgay.HeaderText = "Ngày Bảo Trì";
                    colNgay.Width = 100;
                    colNgay.DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                
                var colChiPhi = dgvMaintenance.Columns["ChiPhi"];
                if (colChiPhi != null)
                {
                    colChiPhi.HeaderText = "Chi Phí (VND)";
                    colChiPhi.Width = 120;
                    colChiPhi.DefaultCellStyle.Format = "N0";
                }
                
                var colMoTa = dgvMaintenance.Columns["MoTa"];
                if (colMoTa != null)
                {
                    colMoTa.HeaderText = "Mô Tả";
                    colMoTa.Width = 200;
                }
                
                var colTrangThai = dgvMaintenance.Columns["TrangThai"];
                if (colTrangThai != null)
                {
                    colTrangThai.HeaderText = "Trạng Thái";
                    colTrangThai.Width = 120;
                }
            }
        }

        private DataTable GetPagedData(DataTable? dt, int page)
        {
            if (dt == null) return new DataTable();
            
            DataTable paged = dt.Clone();
            int start = (page - 1) * pageSize;
            for (int i = start; i < start + pageSize && i < dt.Rows.Count; i++)
            {
                paged.ImportRow(dt.Rows[i]);
            }
            return paged;
        }

        /// <summary>
        /// Áp dụng màu sắc cho các dòng dựa trên trạng thái bảo trì
        /// </summary>
        private void ApplyStatusColorCoding()
        {
            foreach (DataGridViewRow row in dgvMaintenance.Rows)
            {
                if (row.Cells["TrangThai"].Value != null)
                {
                    string status = row.Cells["TrangThai"].Value?.ToString()?.Trim() ?? "";
                    
                    switch (status.ToLower())
                    {
                        case "hoàn thành":
                            // Xanh lá - Bảo trì đã hoàn thành
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkGreen;
                            break;

                        case "chưa hoàn thành":
                            // Vàng - Đang thực hiện bảo trì
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkOrange;
                            break;

                        case "quá hạn":
                            // Đỏ - Quá hạn
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkRed;
                            break;
                            
                        default:
                            // Trắng - Trạng thái khác
                            row.DefaultCellStyle.BackColor = System.Drawing.Color.White;
                            row.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Event handler được gọi khi DataGridView hoàn thành data binding
        /// </summary>
        private void DgvMaintenance_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyStatusColorCoding();
        }

        /// <summary>
        /// Helper method để cập nhật DataSource và áp dụng color coding
        /// </summary>
        private void UpdateDataGridView(DataTable? data, int page = 1)
        {
            if (data != null)
            {
                dgvMaintenance.DataSource = GetPagedData(data, page);
                // Màu sắc sẽ được áp dụng tự động qua event DataBindingComplete
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (dtMaintenance != null && currentPage * pageSize < dtMaintenance.Rows.Count)
            {
                currentPage++;
                UpdateDataGridView(dtMaintenance, currentPage);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdateDataGridView(dtMaintenance, currentPage);
            }
        }

        private void LoadEquipment()
        {
            var dtEquipment = DatabaseHelper.ExecuteProcedure("sp_LayTatCaCoSoVatChat");
            cmbEquipment.DataSource = dtEquipment;
            cmbEquipment.DisplayMember = "Ten";
            cmbEquipment.ValueMember = "MaCoSoVatChat";
            cmbFilterEquipment.DataSource = dtEquipment.Copy();
            cmbFilterEquipment.DisplayMember = "Ten";
            cmbFilterEquipment.ValueMember = "MaCoSoVatChat";
            cmbFilterEquipment.SelectedIndex = -1;
        }

        private void LoadEmployees()
        {
            var dtEmployees = DatabaseHelper.ExecuteProcedure("sp_LayTatCaNhanVien");
            cmbEmployee.DataSource = dtEmployees;
            cmbEmployee.DisplayMember = "Ten";
            cmbEmployee.ValueMember = "MaNhanVien";
            cmbFilterEmployee.DataSource = dtEmployees.Copy();
            cmbFilterEmployee.DisplayMember = "Ten";
            cmbFilterEmployee.ValueMember = "MaNhanVien";
            cmbFilterEmployee.SelectedIndex = -1;
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@MaCoSoVatChat", cmbFilterEquipment.SelectedValue ?? (object)DBNull.Value),
                new SqlParameter("@MaNhanVien", cmbFilterEmployee.SelectedValue ?? (object)DBNull.Value),
                new SqlParameter("@NgayBatDau", dtpStart.Value.Date),
                new SqlParameter("@NgayKetThuc", dtpEnd.Value.Date)
            };
            dtMaintenance = DatabaseHelper.ExecuteProcedure("sp_LayBaoTriTheoBoLoc", parameters);
            currentPage = 1;
            UpdateDataGridView(dtMaintenance, currentPage);
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            // Reset các combo box và date picker về trạng thái ban đầu
            cmbFilterEquipment.SelectedIndex = -1;
            cmbFilterEmployee.SelectedIndex = -1;
            dtpStart.Value = DateTime.Now.AddMonths(-1);
            dtpEnd.Value = DateTime.Now;
            
            // Load lại tất cả dữ liệu
            LoadMaintenance();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new MaintenanceEditForm())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadMaintenance();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvMaintenance.SelectedRows.Count > 0)
            {
                int maintenanceID = Convert.ToInt32(dgvMaintenance.SelectedRows[0].Cells["MaBaoTri"].Value);
                using (var editForm = new MaintenanceEditForm(maintenanceID))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadMaintenance();
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
            if (dgvMaintenance.SelectedRows.Count > 0)
            {
                int maintenanceID = Convert.ToInt32(dgvMaintenance.SelectedRows[0].Cells["MaBaoTri"].Value);
                SqlParameter[] parameters = { new SqlParameter("@MaBaoTri", maintenanceID) };
                DatabaseHelper.ExecuteNonQuery("sp_XoaBaoTri", parameters);
                LoadMaintenance();
            }
        }

        private void dgvMaintenance_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMaintenance.SelectedRows.Count > 0)
            {
                var row = dgvMaintenance.SelectedRows[0];
                // sp_LayTatCaBaoTri returns e.Ten AS TenCoSoVatChat
                lblEquipmentValue.Text = row.Cells["TenCoSoVatChat"]?.Value?.ToString() ?? string.Empty;
                lblEmployeeValue.Text = row.Cells["TenNhanVien"]?.Value?.ToString() ?? string.Empty;
                if (DateTime.TryParse(row.Cells["NgayBaoTri"].Value?.ToString(), out var d))
                    lblDateValue.Text = d.ToShortDateString();
                else
                    lblDateValue.Text = string.Empty;
                if (decimal.TryParse(row.Cells["ChiPhi"].Value?.ToString(), out var c))
                    lblCostValue.Text = c.ToString("C");
                else
                    lblCostValue.Text = string.Empty;
                lblDescriptionValue.Text = row.Cells["MoTa"].Value?.ToString() ?? string.Empty;
            }
            else
            {
                lblEquipmentValue.Text = string.Empty;
                lblEmployeeValue.Text = string.Empty;
                lblDateValue.Text = string.Empty;
                lblCostValue.Text = string.Empty;
                lblDescriptionValue.Text = string.Empty;
            }
        }

        // ============================================
        // PHƯƠNG THỨC TÌM KIẾM BẢO TRÌ
        // ============================================

        /// <summary>
        /// Tìm kiếm bảo trì theo tên cơ sở vật chất
        /// </summary>
        public void SearchMaintenanceByEquipment(string equipmentName)
        {
            if (string.IsNullOrEmpty(equipmentName))
            {
                LoadMaintenance();
                return;
            }

            try
            {
                dtMaintenance = DatabaseHelper.SearchMaintenanceByEquipmentName(equipmentName);
                currentPage = 1;
                UpdateDataGridView(dtMaintenance, currentPage);
                SetupColumnHeaders();

                if (dtMaintenance.Rows.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy bảo trì nào cho cơ sở vật chất '{equipmentName}'.", 
                                    "Không Tìm Thấy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Tìm thấy {dtMaintenance.Rows.Count} bảo trì cho cơ sở vật chất '{equipmentName}'.", 
                                    "Kết Quả Tìm Kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tìm kiếm bảo trì theo tên nhân viên
        /// </summary>
        public void SearchMaintenanceByEmployee(string employeeName)
        {
            if (string.IsNullOrEmpty(employeeName))
            {
                LoadMaintenance();
                return;
            }

            try
            {
                dtMaintenance = DatabaseHelper.SearchMaintenanceByEmployeeName(employeeName);
                currentPage = 1;
                UpdateDataGridView(dtMaintenance, currentPage);
                SetupColumnHeaders();

                if (dtMaintenance.Rows.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy bảo trì nào được thực hiện bởi '{employeeName}'.", 
                                    "Không Tìm Thấy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Tìm thấy {dtMaintenance.Rows.Count} bảo trì được thực hiện bởi '{employeeName}'.", 
                                    "Kết Quả Tìm Kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
            }
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadMaintenance(); // Load lại dữ liệu gốc
        }

        private void PerformSearch()
        {
            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                // Tìm kiếm theo tên thiết bị trước
                DataTable equipmentResult = DatabaseHelper.SearchMaintenanceByEquipmentName(searchTerm);
                
                if (equipmentResult.Rows.Count > 0)
                {
                    dtMaintenance = equipmentResult;
                    currentPage = 1;
                    UpdateDataGridView(dtMaintenance, currentPage);
                    MessageBox.Show($"Tìm thấy {dtMaintenance.Rows.Count} bảo trì cho thiết bị có tên chứa '{searchTerm}'.", 
                                    "Kết Quả Tìm Kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Nếu không tìm thấy theo thiết bị thì tìm theo nhân viên
                DataTable employeeResult = DatabaseHelper.SearchMaintenanceByEmployeeName(searchTerm);
                
                if (employeeResult.Rows.Count > 0)
                {
                    dtMaintenance = employeeResult;
                    currentPage = 1;
                    UpdateDataGridView(dtMaintenance, currentPage);
                    MessageBox.Show($"Tìm thấy {dtMaintenance.Rows.Count} bảo trì được thực hiện bởi nhân viên có tên chứa '{searchTerm}'.", 
                                    "Kết Quả Tìm Kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy bảo trì nào liên quan đến '{searchTerm}'.", 
                                    "Không Tìm Thấy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}