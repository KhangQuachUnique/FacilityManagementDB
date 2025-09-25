using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace FacilityManagementSystem
{
    public partial class ReportsForm : Form
    {
        private DataTable? dtMaintenanceCost;
        private DataTable? dtAssetValue;
        private DataTable? dtMaintenanceNeeded;

        public ReportsForm()
        {
            InitializeComponent();
            ConfigureUI();
        }
        
        private void ConfigureUI()
        {
            UIHelper.ConfigureForm(this);
            
            // Configure all DataGridViews
            UIHelper.ConfigureDataGridView(dgvMaintenanceCost);
            UIHelper.ConfigureDataGridView(dgvAssetValue);
            UIHelper.ConfigureDataGridView(dgvMaintenanceNeeded);
            
            // Configure buttons
            UIHelper.ConfigureButton(btnViewMaintenanceCost, true);
            UIHelper.ConfigureButton(btnRefreshStatus, true);
            UIHelper.ConfigureButton(btnViewAssetValue, true);
            UIHelper.ConfigureButton(btnRefreshMaintenanceNeeded, true);
            
            // Configure ComboBoxes
            UIHelper.ConfigureComboBox(cmbMonth);
            UIHelper.ConfigureComboBox(cmbYear);
            UIHelper.ConfigureComboBox(cmbArea);
            
            // Configure Labels
            UIHelper.ConfigureLabel(lblTotalCost, true);
            UIHelper.ConfigureLabel(lblAreaTotalValue, true);
            UIHelper.ConfigureLabel(lblMaintenanceCount, true);
            UIHelper.ConfigureLabel(lblActive, true);
            UIHelper.ConfigureLabel(lblMaintenance, true);
            UIHelper.ConfigureLabel(lblBroken, true);
            UIHelper.ConfigureLabel(lblStopped, true);
        }

        private void ReportsForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadComboBoxData();
                LoadEquipmentStatusReport();
                // Không tự động load MaintenanceNeededReport để tránh lỗi khi mới mở form
                // Người dùng sẽ click button "Làm Mới Dữ Liệu" để tải báo cáo này
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo báo cáo: {ex.Message}\n\nVui lòng kiểm tra kết nối database.", 
                               "Lỗi Khởi Tạo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #region Load ComboBox Data
        private void LoadComboBoxData()
        {
            LoadMonthComboBox();
            LoadYearComboBox();
            LoadAreaComboBox();
        }

        private void LoadMonthComboBox()
        {
            cmbMonth.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                cmbMonth.Items.Add(new { Value = i, Text = $"Tháng {i}" });
            }
            cmbMonth.DisplayMember = "Text";
            cmbMonth.ValueMember = "Value";
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1; // Current month
        }

        private void LoadYearComboBox()
        {
            cmbYear.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 5; i <= currentYear + 1; i++)
            {
                cmbYear.Items.Add(i);
            }
            cmbYear.SelectedItem = currentYear; // Current year
        }

        private void LoadAreaComboBox()
        {
            try
            {
                var dtAreas = DatabaseHelper.ExecuteProcedure("sp_LayTatCaKhuVuc");
                if (dtAreas != null && dtAreas.Rows.Count > 0)
                {
                    cmbArea.DataSource = dtAreas;
                    cmbArea.DisplayMember = "TenKhuVuc";
                    cmbArea.ValueMember = "MaKhuVuc";
                    cmbArea.SelectedIndex = -1;
                }
                else
                {
                    cmbArea.DataSource = null;
                    cmbArea.Items.Clear();
                    cmbArea.Items.Add("Không có dữ liệu khu vực");
                    cmbArea.SelectedIndex = 0;
                    cmbArea.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách khu vực: {ex.Message}\n\nVui lòng kiểm tra stored procedure 'sp_LayTatCaKhuVuc'", 
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                cmbArea.DataSource = null;
                cmbArea.Items.Clear();
                cmbArea.Items.Add("Lỗi tải dữ liệu");
                cmbArea.SelectedIndex = 0;
                cmbArea.Enabled = false;
            }
        }
        #endregion

        #region 1. Báo Cáo Chi Phí Bảo Trì Theo Tháng
        private void btnViewMaintenanceCost_Click(object sender, EventArgs e)
        {
            if (cmbMonth.SelectedItem == null || cmbYear.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn tháng và năm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadMaintenanceCostReport();
        }

        private void LoadMaintenanceCostReport()
        {
            try
            {
                if (cmbMonth.SelectedItem == null || cmbYear.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn tháng và năm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                int selectedMonth = ((dynamic)cmbMonth.SelectedItem).Value;
                int selectedYear = (int)cmbYear.SelectedItem;

                SqlParameter[] parameters = {
                    new SqlParameter("@Thang", selectedMonth),
                    new SqlParameter("@Nam", selectedYear)
                };

                dtMaintenanceCost = DatabaseHelper.ExecuteProcedure("sp_BaoCaoChiPhiBaoTriTheoThang", parameters);
                dgvMaintenanceCost.DataSource = dtMaintenanceCost;

                SetupMaintenanceCostHeaders();
                CalculateTotalMaintenanceCost();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo chi phí bảo trì: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupMaintenanceCostHeaders()
        {
            if (dgvMaintenanceCost.Columns.Count > 0)
            {
                foreach (DataGridViewColumn col in dgvMaintenanceCost.Columns)
                {
                    switch (col.Name)
                    {
                        case "TenCoSoVatChat":
                            col.HeaderText = "Tên Thiết Bị";
                            col.Width = 200;
                            break;
                        case "TenKhuVuc":
                            col.HeaderText = "Khu Vực";
                            col.Width = 120;
                            break;
                        case "NgayBaoTri":
                            col.HeaderText = "Ngày Bảo Trì";
                            col.Width = 100;
                            col.DefaultCellStyle.Format = "dd/MM/yyyy";
                            break;
                        case "ChiPhi":
                            col.HeaderText = "Chi Phí (VND)";
                            col.Width = 120;
                            col.DefaultCellStyle.Format = "N0";
                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "TenNhanVien":
                            col.HeaderText = "Nhân Viên";
                            col.Width = 150;
                            break;
                        case "MoTa":
                            col.HeaderText = "Mô Tả";
                            col.Width = 250;
                            break;
                    }
                }
            }
        }

        private void CalculateTotalMaintenanceCost()
        {
            if (dtMaintenanceCost != null && dtMaintenanceCost.Rows.Count > 0)
            {
                decimal totalCost = 0;
                foreach (DataRow row in dtMaintenanceCost.Rows)
                {
                    if (row["ChiPhi"] != DBNull.Value)
                    {
                        totalCost += Convert.ToDecimal(row["ChiPhi"]);
                    }
                }
                lblTotalCost.Text = $"Tổng chi phí: {totalCost:N0} đ";
            }
            else
            {
                lblTotalCost.Text = "Tổng chi phí: 0 đ";
            }
        }
        #endregion

        #region 2. Báo Cáo Thiết Bị Theo Trạng Thái
        private void btnRefreshStatus_Click(object sender, EventArgs e)
        {
            LoadEquipmentStatusReport();
        }

        private void LoadEquipmentStatusReport()
        {
            try
            {
                var dtStatus = DatabaseHelper.ExecuteProcedure("sp_BaoCaoThietBiTheoTrangThai");
                
                // Reset all counts
                int activeCount = 0, maintenanceCount = 0, brokenCount = 0, stoppedCount = 0;

                if (dtStatus != null && dtStatus.Rows.Count > 0)
                {
                    foreach (DataRow row in dtStatus.Rows)
                    {
                        string status = row["TrangThai"]?.ToString() ?? "";
                        int count = row["SoLuong"] != DBNull.Value ? Convert.ToInt32(row["SoLuong"]) : 0;

                        switch (status)
                        {
                            case "Hoạt Động":
                                activeCount = count;
                                break;
                            case "Đang Bảo Trì":
                                maintenanceCount = count;
                                break;
                            case "Hỏng":
                                brokenCount = count;
                                break;
                            case "Ngừng Hoạt Động":
                                stoppedCount = count;
                                break;
                        }
                    }
                }

                // Update labels
                lblActive.Text = $"{activeCount} thiết bị";
                lblMaintenance.Text = $"{maintenanceCount} thiết bị";
                lblBroken.Text = $"{brokenCount} thiết bị";
                lblStopped.Text = $"{stoppedCount} thiết bị";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo trạng thái thiết bị: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message}", 
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Reset labels về 0
                lblActive.Text = "0 thiết bị";
                lblMaintenance.Text = "0 thiết bị";
                lblBroken.Text = "0 thiết bị";
                lblStopped.Text = "0 thiết bị";
            }
        }
        #endregion

        #region 3. Báo Cáo Giá Trị Tài Sản Theo Khu Vực
        private void btnViewAssetValue_Click(object sender, EventArgs e)
        {
            if (cmbArea.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khu vực!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadAssetValueReport();
        }

        private void LoadAssetValueReport()
        {
            try
            {
                if (cmbArea.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn khu vực!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                int selectedAreaId = (int)cmbArea.SelectedValue;

                SqlParameter[] parameters = {
                    new SqlParameter("@MaKhuVuc", selectedAreaId)
                };

                dtAssetValue = DatabaseHelper.ExecuteProcedure("sp_BaoCaoGiaTriTaiSanTheoKhuVuc", parameters);
                dgvAssetValue.DataSource = dtAssetValue;

                SetupAssetValueHeaders();
                CalculateTotalAssetValue();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo giá trị tài sản: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupAssetValueHeaders()
        {
            if (dgvAssetValue.Columns.Count > 0)
            {
                foreach (DataGridViewColumn col in dgvAssetValue.Columns)
                {
                    switch (col.Name)
                    {
                        case "TenCoSoVatChat":
                            col.HeaderText = "Tên Thiết Bị";
                            col.Width = 250;
                            break;
                        case "TenLoai":
                            col.HeaderText = "Loại Thiết Bị";
                            col.Width = 150;
                            break;
                        case "TrangThai":
                            col.HeaderText = "Trạng Thái";
                            col.Width = 120;
                            break;
                        case "Gia":
                            col.HeaderText = "Giá Trị (VND)";
                            col.Width = 150;
                            col.DefaultCellStyle.Format = "N0";
                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                    }
                }
            }
        }

        private void CalculateTotalAssetValue()
        {
            if (dtAssetValue != null && dtAssetValue.Rows.Count > 0)
            {
                decimal totalValue = 0;
                foreach (DataRow row in dtAssetValue.Rows)
                {
                    if (row["Gia"] != DBNull.Value)
                    {
                        totalValue += Convert.ToDecimal(row["Gia"]);
                    }
                }
                lblAreaTotalValue.Text = $"Tổng giá trị: {totalValue:N0} đ";
            }
            else
            {
                lblAreaTotalValue.Text = "Tổng giá trị: 0 đ";
            }
        }
        #endregion

        #region 4. Báo Cáo Thiết Bị Cần Bảo Trì
        private void btnRefreshMaintenanceNeeded_Click(object sender, EventArgs e)
        {
            LoadMaintenanceNeededReport();
        }

        private void LoadMaintenanceNeededReport()
        {
            try
            {
                dtMaintenanceNeeded = DatabaseHelper.ExecuteProcedure("sp_BaoCaoThietBiCanBaoTri");
                
                if (dtMaintenanceNeeded != null)
                {
                    dgvMaintenanceNeeded.DataSource = dtMaintenanceNeeded;
                    SetupMaintenanceNeededHeaders();
                    UpdateMaintenanceNeededCount();
                }
                else
                {
                    dgvMaintenanceNeeded.DataSource = null;
                    lblMaintenanceCount.Text = "Tổng số thiết bị cần bảo trì: 0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải báo cáo thiết bị cần bảo trì: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message}", 
                               "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Reset UI về trạng thái mặc định
                dgvMaintenanceNeeded.DataSource = null;
                lblMaintenanceCount.Text = "Tổng số thiết bị cần bảo trì: 0";
            }
        }

        private void SetupMaintenanceNeededHeaders()
        {
            if (dgvMaintenanceNeeded.Columns.Count > 0)
            {
                foreach (DataGridViewColumn col in dgvMaintenanceNeeded.Columns)
                {
                    switch (col.Name)
                    {
                        case "TenCoSoVatChat":
                            col.HeaderText = "Tên Thiết Bị";
                            col.Width = 200;
                            break;
                        case "TenKhuVuc":
                            col.HeaderText = "Khu Vực";
                            col.Width = 120;
                            break;
                        case "TenLoai":
                            col.HeaderText = "Loại Thiết Bị";
                            col.Width = 150;
                            break;
                        case "TrangThai":
                            col.HeaderText = "Trạng Thái";
                            col.Width = 120;
                            break;
                        case "Gia":
                            col.HeaderText = "Giá Trị (VND)";
                            col.Width = 120;
                            col.DefaultCellStyle.Format = "N0";
                            col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            break;
                        case "NgayBaoTri":
                            col.HeaderText = "Ngày Bảo Trì Gần Nhất";
                            col.Width = 130;
                            col.DefaultCellStyle.Format = "dd/MM/yyyy";
                            break;
                        case "TrangThaiBaoTri":
                            col.HeaderText = "Trạng Thái Bảo Trì";
                            col.Width = 130;
                            break;
                        case "ThuTuUuTien":
                            col.Visible = false; // Ẩn cột phụ trợ
                            break;
                    }
                }
            }
        }

        private void UpdateMaintenanceNeededCount()
        {
            int count = dtMaintenanceNeeded?.Rows.Count ?? 0;
            lblMaintenanceCount.Text = $"Tổng số thiết bị cần bảo trì: {count}";
        }
        #endregion
    }
}