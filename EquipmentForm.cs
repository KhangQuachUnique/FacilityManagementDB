using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace FacilityManagementSystem
{
    public partial class EquipmentForm : Form
    {
        private const int PageSize = 15;
        private DataTable? _equipmentData;
        private int _currentPage = 1;

        private readonly TableLayoutPanel _mainLayout;
        private readonly FlowLayoutPanel _searchPanel;
        private readonly FlowLayoutPanel _filterPanel;
        private readonly FlowLayoutPanel _actionPanel;
        private readonly Label _pageInfoLabel;
        private readonly Label _selectedSummaryLabel;

        public EquipmentForm()
        {
            InitializeComponent();
            _mainLayout = new TableLayoutPanel();
            _searchPanel = new FlowLayoutPanel();
            _filterPanel = new FlowLayoutPanel();
            _actionPanel = new FlowLayoutPanel();
            _pageInfoLabel = new Label { AutoSize = true, Margin = new Padding(10, 8, 10, 0) };
            _selectedSummaryLabel = new Label { AutoSize = true, Margin = new Padding(10, 8, 20, 0) };

            ConfigureUI();
            BuildLayout();
            RegisterEventHandlers();
            LoadInitialData();
        }

        private void ConfigureUI()
        {
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1000, 600);

            ConfigureDataGridView();
            ConfigureButtons();
            txtSearch.Height = 28;
            txtSearch.Width = 200;
        }

        private void ConfigureDataGridView()
        {
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
        }

        private void ConfigureButtons()
        {
            foreach (var button in new[] { btnAdd, btnUpdate, btnDelete, btnFilter, btnResetFilter, btnNext, btnPrev, btnSearch, btnClearSearch })
            {
                button.Height = 32;
                button.Width = 80;
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = Color.FromArgb(0, 120, 215);
                button.ForeColor = Color.White;
                button.Margin = new Padding(5);
            }
        }

        private void BuildLayout()
        {
            ConfigureMainLayout();
            ConfigureSearchPanel();
            ConfigureFilterPanel();
            ConfigureActionPanel();
            RemoveLegacyControls();
            AddControlsToLayout();
            Controls.Add(_mainLayout);
        }

        private void ConfigureMainLayout()
        {
            _mainLayout.Dock = DockStyle.Fill;
            _mainLayout.ColumnCount = 1;
            _mainLayout.RowCount = 4;
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            _mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _mainLayout.Padding = new Padding(10);
        }

        private void ConfigureSearchPanel()
        {
            _searchPanel.Dock = DockStyle.Fill;
            _searchPanel.FlowDirection = FlowDirection.LeftToRight;
            _searchPanel.AutoSize = true;
            _searchPanel.Padding = new Padding(5);
            _searchPanel.Controls.AddRange(new Control[]
            {
                lblSearch,
                txtSearch,
                btnSearch,
                btnClearSearch
            });
        }

        private void ConfigureFilterPanel()
        {
            _filterPanel.Dock = DockStyle.Fill;
            _filterPanel.FlowDirection = FlowDirection.LeftToRight;
            _filterPanel.AutoSize = true;
            _filterPanel.Padding = new Padding(5);
            _filterPanel.Controls.AddRange(new Control[]
            {
                new Label { Text = "Khu vực:", AutoSize = true, Margin = new Padding(5, 8, 5, 0) },
                cmbFilterArea,
                new Label { Text = "Loại:", AutoSize = true, Margin = new Padding(10, 8, 5, 0) },
                cmbFilterType,
                new Label { Text = "Trạng thái:", AutoSize = true, Margin = new Padding(10, 8, 5, 0) },
                cmbFilterStatus,
                btnFilter,
                btnResetFilter
            });

            foreach (var combo in new[] { cmbFilterArea, cmbFilterType, cmbFilterStatus })
            {
                combo.Width = 150;
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
                combo.Margin = new Padding(5);
            }
        }

        private void ConfigureActionPanel()
        {
            _actionPanel.Dock = DockStyle.Fill;
            _actionPanel.FlowDirection = FlowDirection.LeftToRight;
            _actionPanel.AutoSize = true;
            _actionPanel.Padding = new Padding(5);
            _actionPanel.Controls.AddRange(new Control[]
            {
                _selectedSummaryLabel,
                btnAdd,
                btnUpdate,
                btnDelete,
                btnPrev,
                btnNext,
                _pageInfoLabel
            });
        }

        private void RemoveLegacyControls()
        {
            foreach (var control in new Control[]
            {
                dgvEquipment, lblSearch, txtSearch, btnSearch, btnClearSearch,
                cmbFilterArea, cmbFilterType, cmbFilterStatus, btnFilter, btnResetFilter,
                btnAdd, btnUpdate, btnDelete, btnPrev, btnNext,
                label1, lblName, label2, lblType, label3, lblArea,
                label4, lblStatus, label5, lblQuantity, label6, lblPrice,
                label7, lblLastMaintenance, label8, label9, label10
            })
            {
                Controls.Remove(control);
            }
        }

        private void AddControlsToLayout()
        {
            _mainLayout.Controls.Add(_searchPanel, 0, 0);
            _mainLayout.Controls.Add(_filterPanel, 0, 1);
            _mainLayout.Controls.Add(dgvEquipment, 0, 2);
            _mainLayout.Controls.Add(_actionPanel, 0, 3);
        }

        private void RegisterEventHandlers()
        {
            dgvEquipment.DataBindingComplete += DgvEquipment_DataBindingComplete;
            dgvEquipment.SelectionChanged += DgvEquipment_SelectionChanged;
            btnSearch.Click += btnSearch_Click;
            btnClearSearch.Click += btnClearSearch_Click;
            txtSearch.KeyDown += txtSearch_KeyDown;
            btnFilter.Click += btnFilter_Click;
            btnResetFilter.Click += btnResetFilter_Click;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnNext.Click += btnNext_Click;
            btnPrev.Click += btnPrev_Click;
        }

        private void LoadInitialData()
        {
            LoadEquipment();
            LoadTypes();
            LoadAreas();
            LoadStatuses();
        }

        private void LoadEquipment()
        {
            try
            {
                _equipmentData = DatabaseHelper.ExecuteProcedure("sp_LayTatCaCoSoVatChat");
                SetupColumnHeaders();
                UpdateDataGridView(_equipmentData);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải dữ liệu cơ sở vật chất", ex);
            }
        }

        private void SetupColumnHeaders()
        {
            if (dgvEquipment.Columns.Count == 0) return;

            ConfigureColumn("MaCoSoVatChat", "Mã", 60);
            ConfigureColumn("Ten", "Tên Cơ Sở Vật Chất", 200);
            ConfigureColumn("TenLoai", "Loại", 160);
            ConfigureColumn("TenKhuVuc", "Khu Vực", 200);
            ConfigureColumn("TrangThai", "Trạng Thái", 150);
            ConfigureColumn("Gia", "Giá (VND)", 100, "N0");
        }

        private void ConfigureColumn(string columnName, string headerText, int width, string? format = null)
        {
            if (dgvEquipment.Columns[columnName] is DataGridViewColumn column)
            {
                column.HeaderText = headerText;
                column.Width = width;
                if (!string.IsNullOrEmpty(format))
                {
                    column.DefaultCellStyle.Format = format;
                }
            }
        }

        private void ApplyStatusColorCoding()
        {
            try
            {
                foreach (DataGridViewRow row in dgvEquipment.Rows)
                {
                    if (row.Cells["TrangThai"]?.Value is string status)
                    {
                        (Color backColor, Color foreColor) = status switch
                        {
                            "Hoạt Động" => (Color.LightGreen, Color.DarkGreen),
                            "Đang Bảo Trì" => (Color.LightYellow, Color.DarkOrange),
                            "Hỏng" => (Color.LightCoral, Color.DarkRed),
                            "Ngừng Hoạt Động" => (Color.LightGray, Color.DimGray),
                            _ => (Color.Empty, Color.Empty)
                        };

                        if (backColor != Color.Empty)
                        {
                            row.DefaultCellStyle.BackColor = backColor;
                            row.DefaultCellStyle.ForeColor = foreColor;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi áp dụng màu sắc theo trạng thái", ex);
            }
        }

        private DataTable GetPagedData(DataTable? data, int page)
        {
            if (data == null) return new DataTable();

            var paged = data.Clone();
            int start = (page - 1) * PageSize;
            for (int i = start; i < start + PageSize && i < data.Rows.Count; i++)
            {
                paged.ImportRow(data.Rows[i]);
            }
            return paged;
        }

        private void DgvEquipment_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyStatusColorCoding();
        }

        private void UpdateDataGridView(DataTable? data, int page = 1)
        {
            if (data == null) return;

            _currentPage = page;
            dgvEquipment.DataSource = GetPagedData(data, page);
            UpdatePagingInfo();
        }

        private void btnNext_Click(object? sender, EventArgs e)
        {
            if (_equipmentData != null && _currentPage * PageSize < _equipmentData.Rows.Count)
            {
                UpdateDataGridView(_equipmentData, _currentPage + 1);
            }
        }

        private void btnPrev_Click(object? sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                UpdateDataGridView(_equipmentData, _currentPage - 1);
            }
        }

        private void LoadTypes()
        {
            try
            {
                var types = DatabaseHelper.ExecuteProcedure("sp_LayTatCaLoaiCoSoVatChat");
                ConfigureComboBox(cmbFilterType, types, "TenLoai", "MaLoai");
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải danh sách loại", ex);
            }
        }

        private void LoadAreas()
        {
            try
            {
                var areas = DatabaseHelper.ExecuteProcedure("sp_LayTatCaKhuVuc");
                ConfigureComboBox(cmbFilterArea, areas, "TenKhuVuc", "MaKhuVuc");
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải danh sách khu vực", ex);
            }
        }

        private void ConfigureComboBox(ComboBox comboBox, DataTable data, string displayMember, string valueMember)
        {
            comboBox.DataSource = data;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            comboBox.SelectedIndex = -1;
        }

        private void LoadStatuses()
        {
            cmbFilterStatus.Items.AddRange(new[] { "Hoạt Động", "Đang Bảo Trì", "Hỏng" });
            cmbFilterStatus.SelectedIndex = -1;
        }

        private void btnFilter_Click(object? sender, EventArgs e)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKhuVuc", cmbFilterArea.SelectedValue ?? DBNull.Value),
                    new SqlParameter("@MaLoai", cmbFilterType.SelectedValue ?? DBNull.Value),
                    new SqlParameter("@TrangThai", cmbFilterStatus.SelectedItem ?? DBNull.Value)
                };

                _equipmentData = DatabaseHelper.ExecuteProcedure("sp_LayCoSoVatChatTheoBoLoc", parameters);
                UpdateDataGridView(_equipmentData, 1);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi lọc dữ liệu", ex);
            }
        }

        private void btnResetFilter_Click(object? sender, EventArgs e)
        {
            cmbFilterArea.SelectedIndex = -1;
            cmbFilterType.SelectedIndex = -1;
            cmbFilterStatus.SelectedIndex = -1;
            LoadEquipment();
        }

        private void btnAdd_Click(object? sender, EventArgs e)
        {
            using var editForm = new EquipmentEditForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadEquipment();
            }
        }

        private void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (dgvEquipment.SelectedRows.Count == 0)
            {
                ShowWarning("Vui lòng chọn một hàng để cập nhật.");
                return;
            }

            int equipmentId = Convert.ToInt32(dgvEquipment.SelectedRows[0].Cells["MaCoSoVatChat"].Value);
            using var editForm = new EquipmentEditForm(equipmentId);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadEquipment();
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvEquipment.SelectedRows.Count == 0)
            {
                ShowWarning("Vui lòng chọn một hàng để xóa.");
                return;
            }

            string name = dgvEquipment.SelectedRows[0].Cells["Ten"].Value?.ToString() ?? "";
            if (MessageBox.Show($"Bạn có chắc muốn xóa Thiết Bị: '{name}'?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int equipmentId = Convert.ToInt32(dgvEquipment.SelectedRows[0].Cells["MaCoSoVatChat"].Value);
                var parameters = new SqlParameter[] { new SqlParameter("@MaCoSoVatChat", equipmentId) };
                DatabaseHelper.ExecuteNonQuery("sp_XoaCoSoVatChat", parameters);
                LoadEquipment();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi xóa thiết bị", ex);
            }
        }

        private void DgvEquipment_SelectionChanged(object? sender, EventArgs e)
        {
            if (_selectedSummaryLabel == null) return;

            if (dgvEquipment.SelectedRows.Count > 0)
            {
                var row = dgvEquipment.SelectedRows[0];
                string name = row.Cells["Ten"].Value?.ToString() ?? "";
                string type = row.Cells["TenLoai"].Value?.ToString() ?? "";
                string area = row.Cells["TenKhuVuc"].Value?.ToString() ?? "";
                string status = row.Cells["TrangThai"].Value?.ToString() ?? "";
                string price = decimal.TryParse(row.Cells["Gia"].Value?.ToString(), out var value)
                    ? $"{value:N0} đ"
                    : "0 đ";

                _selectedSummaryLabel.Text = $"Đang chọn: {name} | Loại: {type} | Khu vực: {area} | Trạng thái: {status} | Giá: {price}";
            }
            else
            {
                _selectedSummaryLabel.Text = "Chưa chọn thiết bị";
            }
        }

        public void SearchEquipment(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadEquipment();
                return;
            }

            try
            {
                _equipmentData = DatabaseHelper.ExecuteProcedure(
                    "sp_TimKiemCoSoVatChatTheoTen",
                    new SqlParameter("@TenCoSoVatChat", searchTerm)
                );
                UpdateDataGridView(_equipmentData, 1);
                SetupColumnHeaders();
                ShowSearchResult($"cơ sở vật chất với từ khóa '{searchTerm}'", _equipmentData?.Rows.Count ?? 0);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tìm kiếm", ex);
            }
        }

        public void SearchBrokenEquipment()
        {
            try
            {
                _equipmentData = DatabaseHelper.ExecuteProcedure("sp_LayCoSoVatChatBiHong");
                UpdateDataGridView(_equipmentData, 1);
                SetupColumnHeaders();
                ShowSearchResult("cơ sở vật chất bị hỏng", _equipmentData?.Rows.Count ?? 0);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tìm kiếm cơ sở vật chất bị hỏng", ex);
            }
        }

        private void ShowSearchResult(string searchTerm, int resultCount)
        {
            string message = resultCount == 0
                ? $"Không tìm thấy {searchTerm}."
                : $"Tìm thấy {resultCount} {searchTerm}.";
            MessageBox.Show(message, resultCount == 0 ? "Không Tìm Thấy" : "Kết Quả Tìm Kiếm",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdatePagingInfo()
        {
            if (_equipmentData == null)
            {
                _pageInfoLabel.Text = string.Empty;
                btnPrev.Enabled = btnNext.Enabled = false;
                return;
            }

            int total = _equipmentData.Rows.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
            int start = total == 0 ? 0 : (_currentPage - 1) * PageSize + 1;
            int end = Math.Min(_currentPage * PageSize, total);

            _pageInfoLabel.Text = $"Hiển thị {start}-{end}/{total} (Trang {_currentPage}/{totalPages})";
            btnPrev.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < totalPages;
        }

        private void btnSearch_Click(object? sender, EventArgs e) => PerformSearch();

        private void txtSearch_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformSearch();
            }
        }

        private void btnClearSearch_Click(object? sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadEquipment();
            _currentPage = 1;
        }

        private void PerformSearch()
        {
            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadEquipment();
                return;
            }

            try
            {
                _equipmentData = DatabaseHelper.ExecuteProcedure(
                    "sp_TimKiemCoSoVatChatTheoTen",
                    new SqlParameter("@TenCoSoVatChat", searchTerm)
                );
                UpdateDataGridView(_equipmentData, 1);
                SetupColumnHeaders();
                ShowSearchResult($"cơ sở vật chất với từ khóa '{searchTerm}'", _equipmentData?.Rows.Count ?? 0);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tìm kiếm", ex);
            }
        }

        private void ShowError(string message, Exception ex)
        {
            MessageBox.Show($"{message}: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Chưa Chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}