using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace FacilityManagementSystem
{
    public partial class EquipmentForm : Form
    {
        private DataTable? dtEquipment;
        private int currentPage = 1;
        private const int pageSize = 15;

        private TableLayoutPanel? layout;
        private FlowLayoutPanel? topPanel;
        private FlowLayoutPanel? bottomPanel;
        private Label? lblPageInfo;

        public EquipmentForm()
        {
            InitializeComponent();
            ConfigureUI();
            BuildBasicLayout();
            // Thêm event handler cho DataBindingComplete để áp dụng màu sắc
            dgvEquipment.DataBindingComplete += DgvEquipment_DataBindingComplete;
            LoadEquipment();
            LoadTypes();
            LoadAreas();
            LoadStatuses();
        }
        
        private void ConfigureUI()
        {
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvEquipment.BackgroundColor = Color.White;
            dgvEquipment.BorderStyle = BorderStyle.Fixed3D;
            dgvEquipment.ReadOnly = true;
            dgvEquipment.AllowUserToAddRows = false;
            dgvEquipment.AllowUserToDeleteRows = false;
            dgvEquipment.MultiSelect = false;
            dgvEquipment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEquipment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEquipment.RowTemplate.Height = 28;
            dgvEquipment.Dock = DockStyle.Fill;
            dgvEquipment.RowHeadersVisible = false;

            foreach (var btn in new[] { btnAdd, btnUpdate, btnDelete, btnFilter, btnResetFilter, btnNext, btnPrev, btnSearch, btnClearSearch })
            {
                btn.Height = 32;
            }
            txtSearch.Height = 28;
        }

        private void BuildBasicLayout()
        {
            layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
            };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Top search/filter panel
            topPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoSize = true,
                Padding = new Padding(10),
            };
            topPanel.Controls.Add(lblSearch);
            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(btnSearch);
            topPanel.Controls.Add(btnClearSearch);
            topPanel.Controls.Add(label8);
            topPanel.Controls.Add(cmbFilterArea);
            topPanel.Controls.Add(label9);
            topPanel.Controls.Add(cmbFilterType);
            topPanel.Controls.Add(label10);
            topPanel.Controls.Add(cmbFilterStatus);
            topPanel.Controls.Add(btnFilter);
            topPanel.Controls.Add(btnResetFilter);

            // Bottom details/actions/paging
            bottomPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoSize = true,
                Padding = new Padding(10),
            };
            lblPageInfo = new Label { AutoSize = true, Margin = new Padding(10, 8, 10, 0) };

            // Detail labels
            bottomPanel.Controls.Add(label1);
            bottomPanel.Controls.Add(lblName);
            bottomPanel.Controls.Add(label2);
            bottomPanel.Controls.Add(lblType);
            bottomPanel.Controls.Add(label3);
            bottomPanel.Controls.Add(lblArea);
            bottomPanel.Controls.Add(label4);
            bottomPanel.Controls.Add(lblStatus);
            bottomPanel.Controls.Add(label5);
            bottomPanel.Controls.Add(lblQuantity);
            bottomPanel.Controls.Add(label6);
            bottomPanel.Controls.Add(lblPrice);
            bottomPanel.Controls.Add(label7);
            bottomPanel.Controls.Add(lblLastMaintenance);

            // Actions
            bottomPanel.Controls.Add(btnAdd);
            bottomPanel.Controls.Add(btnUpdate);
            bottomPanel.Controls.Add(btnDelete);
            bottomPanel.Controls.Add(btnPrev);
            bottomPanel.Controls.Add(btnNext);
            bottomPanel.Controls.Add(lblPageInfo);

            // Remove existing absolute-positioning from root and rebuild
            foreach (Control c in new Control[] { dgvEquipment, lblSearch, txtSearch, btnSearch, btnClearSearch, label8, cmbFilterArea, label9, cmbFilterType, label10, cmbFilterStatus, btnFilter, btnResetFilter, label1, lblName, label2, lblType, label3, lblArea, label4, lblStatus, label5, lblQuantity, label6, lblPrice, label7, lblLastMaintenance, btnAdd, btnUpdate, btnDelete, btnPrev, btnNext })
            {
                this.Controls.Remove(c);
            }
            layout.Controls.Add(topPanel, 0, 0);
            layout.Controls.Add(dgvEquipment, 0, 1);
            layout.Controls.Add(bottomPanel, 0, 2);
            this.Controls.Add(layout);
        }

        private void LoadEquipment()
        {
            dtEquipment = DatabaseHelper.ExecuteProcedure("sp_LayTatCaCoSoVatChat");
            SetupColumnHeaders();
            UpdateDataGridView(dtEquipment, currentPage);
            UpdatePagingInfo();
        }

        private void SetupColumnHeaders()
        {
            if (dgvEquipment.Columns.Count > 0)
            {
                var colMa = dgvEquipment.Columns["MaCoSoVatChat"];
                if (colMa != null)
                {
                    colMa.HeaderText = "Mã";
                    colMa.Width = 60;
                }
                
                var colTen = dgvEquipment.Columns["Ten"];
                if (colTen != null)
                {
                    colTen.HeaderText = "Tên Cơ Sở Vật Chất";
                    colTen.Width = 200;
                }
                
                var colLoai = dgvEquipment.Columns["TenLoai"];
                if (colLoai != null)
                {
                    colLoai.HeaderText = "Loại";
                    colLoai.Width = 120;
                }
                
                var colKhuVuc = dgvEquipment.Columns["TenKhuVuc"];
                if (colKhuVuc != null)
                {
                    colKhuVuc.HeaderText = "Khu Vực";
                    colKhuVuc.Width = 120;
                }
                
                var colTrangThai = dgvEquipment.Columns["TrangThai"];
                if (colTrangThai != null)
                {
                    colTrangThai.HeaderText = "Trạng Thái";
                    colTrangThai.Width = 100;
                }
                
                var colGia = dgvEquipment.Columns["Gia"];
                if (colGia != null)
                {
                    colGia.HeaderText = "Giá (VND)";
                    colGia.Width = 100;
                    colGia.DefaultCellStyle.Format = "N0";
                }
            }
        }

        /// <summary>
        /// Áp dụng color coding cho các row theo trạng thái:
        /// - Hoạt Động: Xanh lá nhạt
        /// - Đang Bảo Trì: Vàng nhạt  
        /// - Hỏng: Đỏ nhạt
        /// - Ngừng Hoạt Động: Xám nhạt
        /// </summary>
        private void ApplyStatusColorCoding()
        {
            try
            {
                foreach (DataGridViewRow row in dgvEquipment.Rows)
                {
                    if (row.Cells["TrangThai"]?.Value != null)
                    {
                        string status = row.Cells["TrangThai"].Value?.ToString() ?? "";
                        
                        switch (status)
                        {
                            case "Hoạt Động":
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                                row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkGreen;
                                break;
                                
                            case "Đang Bảo Trì":
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                                row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkOrange;
                                break;
                                
                            case "Hỏng":
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                                row.DefaultCellStyle.ForeColor = System.Drawing.Color.DarkRed;
                                break;
                                
                            case "Ngừng Hoạt Động":
                                row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                                row.DefaultCellStyle.ForeColor = System.Drawing.Color.DimGray;
                                break;
                                
                            default:
                                // Giữ màu mặc định
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi áp dụng color coding: {ex.Message}", "Lỗi", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        /// Event handler được gọi khi DataGridView hoàn thành data binding
        /// </summary>
        private void DgvEquipment_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
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
                dgvEquipment.DataSource = GetPagedData(data, page);
                // Màu sắc sẽ được áp dụng tự động qua event DataBindingComplete
                UpdatePagingInfo();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (dtEquipment != null && currentPage * pageSize < dtEquipment.Rows.Count)
            {
                currentPage++;
                UpdateDataGridView(dtEquipment, currentPage);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                UpdateDataGridView(dtEquipment, currentPage);
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
            currentPage = 1;
            UpdateDataGridView(dtEquipment, currentPage);
        }

        private void btnResetFilter_Click(object sender, EventArgs e)
        {
            // Reset các combo box về trạng thái ban đầu
            cmbFilterArea.SelectedIndex = -1;
            cmbFilterType.SelectedIndex = -1;
            cmbFilterStatus.SelectedIndex = -1;
            
            // Load lại tất cả dữ liệu
            LoadEquipment();
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

        // ============================================
        // PHƯƠNG THỨC TÌM KIẾM CƠ SỞ VẬT CHẤT
        // ============================================

        /// <summary>
        /// Tìm kiếm cơ sở vật chất theo tên (gọi từ các form khác)
        /// </summary>
        public void SearchEquipment(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadEquipment(); // Load tất cả nếu không có từ khóa
                return;
            }

            try
            {
                dtEquipment = DatabaseHelper.SearchEquipmentByName(searchTerm);
                currentPage = 1;
                UpdateDataGridView(dtEquipment, currentPage);
                SetupColumnHeaders();
                UpdatePagingInfo();

                if (dtEquipment.Rows.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy cơ sở vật chất nào với từ khóa '{searchTerm}'.", 
                                    "Không Tìm Thấy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Tìm thấy {dtEquipment.Rows.Count} cơ sở vật chất với từ khóa '{searchTerm}'.", 
                                    "Kết Quả Tìm Kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị danh sách cơ sở vật chất bị hỏng (gọi từ các form khác)
        /// </summary>
        public void SearchBrokenEquipment()
        {
            try
            {
                dtEquipment = DatabaseHelper.ExecuteProcedure("sp_LayCoSoVatChatBiHong");
                currentPage = 1;
                UpdateDataGridView(dtEquipment, currentPage);
                SetupColumnHeaders();
                UpdatePagingInfo();

                if (dtEquipment.Rows.Count == 0)
                {
                    MessageBox.Show("Không có cơ sở vật chất nào bị hỏng.", 
                                    "Không Tìm Thấy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Tìm thấy {dtEquipment.Rows.Count} cơ sở vật chất bị hỏng cần sửa chữa.", 
                                    "Kết Quả Tìm Kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePagingInfo()
        {
            if (dtEquipment == null)
            {
                if (lblPageInfo != null) lblPageInfo.Text = string.Empty;
                btnPrev.Enabled = btnNext.Enabled = false;
                return;
            }
            int total = dtEquipment.Rows.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
            int start = total == 0 ? 0 : (currentPage - 1) * pageSize + 1;
            int end = Math.Min(currentPage * pageSize, total);
            if (lblPageInfo != null)
                lblPageInfo.Text = $"Hiển thị {start}-{end}/{total} (Trang {currentPage}/{totalPages})";
            btnPrev.Enabled = currentPage > 1;
            btnNext.Enabled = currentPage < totalPages;
        }

        // ============================================
        // CÁC PHƯƠNG THỨC TÌM KIẾM UI
        // ============================================

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
            txtSearch.Clear();
            LoadEquipment(); // Load lại tất cả dữ liệu
            currentPage = 1;
        }

        private void PerformSearch()
        {
            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadEquipment(); // Nếu không có từ khóa tìm kiếm, load tất cả
                return;
            }

            try
            {
                dtEquipment = DatabaseHelper.SearchEquipmentByName(searchTerm);
                currentPage = 1;
                UpdateDataGridView(dtEquipment, currentPage);
                SetupColumnHeaders();

                if (dtEquipment.Rows.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy cơ sở vật chất nào với từ khóa '{searchTerm}'.", 
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