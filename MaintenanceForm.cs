using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace FacilityManagementSystem
{
    public partial class MaintenanceForm : Form
    {
        private const int PageSize = 15;
        private DataTable? _maintenanceData;
        private int _currentPage = 1;

        private readonly TableLayoutPanel _mainLayout;
        private readonly FlowLayoutPanel _searchPanel;
        private readonly FlowLayoutPanel _filterPanel;
        private readonly FlowLayoutPanel _actionPanel;
        private readonly Label _pageInfoLabel;
        private readonly Label _selectedSummaryLabel;
    private readonly ComboBox _cmbFilterArea;
    private readonly ComboBox _cmbFilterType;

        public MaintenanceForm()
        {
            InitializeComponent();
            _mainLayout = new TableLayoutPanel();
            _searchPanel = new FlowLayoutPanel();
            _filterPanel = new FlowLayoutPanel();
            _actionPanel = new FlowLayoutPanel();
            _pageInfoLabel = new Label { AutoSize = true, Margin = new Padding(10, 8, 10, 0) };
            _selectedSummaryLabel = new Label { AutoSize = true, Margin = new Padding(10, 8, 20, 0) };
            _cmbFilterArea = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 150, Margin = new Padding(5) };
            _cmbFilterType = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 150, Margin = new Padding(5) };

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
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpEnd.Format = DateTimePickerFormat.Short;
        }

        private void ConfigureDataGridView()
        {
            dgvMaintenance.BackgroundColor = Color.White;
            dgvMaintenance.BorderStyle = BorderStyle.Fixed3D;
            dgvMaintenance.ReadOnly = true;
            dgvMaintenance.AllowUserToAddRows = false;
            dgvMaintenance.AllowUserToDeleteRows = false;
            dgvMaintenance.MultiSelect = false;
            dgvMaintenance.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMaintenance.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMaintenance.RowTemplate.Height = 28;
            dgvMaintenance.Dock = DockStyle.Fill;
            dgvMaintenance.RowHeadersVisible = false;
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
                _cmbFilterArea,
                new Label { Text = "Loại:", AutoSize = true, Margin = new Padding(10, 8, 5, 0) },
                _cmbFilterType,
                new Label { Text = "Nhân viên:", AutoSize = true, Margin = new Padding(10, 8, 5, 0) },
                cmbFilterEmployee,
                new Label { Text = "Từ:", AutoSize = true, Margin = new Padding(10, 8, 5, 0) },
                dtpStart,
                new Label { Text = "Đến:", AutoSize = true, Margin = new Padding(10, 8, 5, 0) },
                dtpEnd,
                btnFilter,
                btnResetFilter
            });

            foreach (var combo in new[] { _cmbFilterArea, _cmbFilterType, cmbFilterEmployee })
            {
                combo.Width = 150;
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
                combo.Margin = new Padding(5);
            }
            dtpStart.Width = 120;
            dtpEnd.Width = 120;
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
                dgvMaintenance, lblSearch, txtSearch, btnSearch, btnClearSearch,
                cmbFilterEquipment, cmbFilterEmployee, dtpStart, dtpEnd, btnFilter, btnResetFilter,
                btnAdd, btnUpdate, btnDelete, btnPrev, btnNext,
                cmbEquipment, cmbEmployee, dtpDate, numCost, txtDescription,
                lblEquipmentValue, lblEmployeeValue, lblDateValue, lblCostValue, lblDescriptionValue,
                label1, label2, label3, label4, label5, label6, label7, label8, label9
            })
            {
                Controls.Remove(control);
            }
        }

        private void AddControlsToLayout()
        {
            _mainLayout.Controls.Add(_searchPanel, 0, 0);
            _mainLayout.Controls.Add(_filterPanel, 0, 1);
            _mainLayout.Controls.Add(dgvMaintenance, 0, 2);
            _mainLayout.Controls.Add(_actionPanel, 0, 3);
        }

        private void RegisterEventHandlers()
        {
            dgvMaintenance.DataBindingComplete += DgvMaintenance_DataBindingComplete;
            dgvMaintenance.SelectionChanged += DgvMaintenance_SelectionChanged;
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
            _cmbFilterArea.SelectedIndexChanged += _cmbFilterArea_SelectedIndexChanged;
        }

        private void LoadInitialData()
        {
            LoadMaintenance();
            LoadEmployees();
            LoadAreasForFilter();
            LoadTypesForFilter();
        }

        private void LoadMaintenance()
        {
            try
            {
                _maintenanceData = DatabaseHelper.ExecuteProcedure("sp_LayTatCaBaoTri");
                SetupColumnHeaders();
                UpdateDataGridView(_maintenanceData);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải dữ liệu bảo trì", ex);
            }
        }

        private void SetupColumnHeaders()
        {
            if (dgvMaintenance.Columns.Count == 0) return;

            ConfigureColumn("MaBaoTri", "Mã Bảo Trì", 80);
            ConfigureColumn("TenCoSoVatChat", "Tên Cơ Sở Vật Chất", 200);
            ConfigureColumn("TenNhanVien", "Nhân Viên Bảo Trì", 150);
            ConfigureColumn("NgayBaoTri", "Ngày Bảo Trì", 100, "dd/MM/yyyy");
            ConfigureColumn("ChiPhi", "Chi Phí (VND)", 120, "N0");
            ConfigureColumn("MoTa", "Mô Tả", 200);
            ConfigureColumn("TrangThai", "Trạng Thái", 120);
        }

        private void ConfigureColumn(string columnName, string headerText, int width, string? format = null)
        {
            if (dgvMaintenance.Columns[columnName] is DataGridViewColumn column)
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
                foreach (DataGridViewRow row in dgvMaintenance.Rows)
                {
                    if (row.Cells["TrangThai"]?.Value is string status)
                    {
                        (Color backColor, Color foreColor) = status.ToLower() switch
                        {
                            "hoàn thành" => (Color.LightGreen, Color.DarkGreen),
                            "chưa hoàn thành" => (Color.LightYellow, Color.DarkOrange),
                            "quá hạn" => (Color.LightCoral, Color.DarkRed),
                            _ => (Color.White, Color.Black)
                        };

                        row.DefaultCellStyle.BackColor = backColor;
                        row.DefaultCellStyle.ForeColor = foreColor;
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

        private void DgvMaintenance_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            ApplyStatusColorCoding();
        }

        private void UpdateDataGridView(DataTable? data, int page = 1)
        {
            if (data == null) return;

            _currentPage = page;
            dgvMaintenance.DataSource = GetPagedData(data, page);
            UpdatePagingInfo();
        }

        private void btnNext_Click(object? sender, EventArgs e)
        {
            if (_maintenanceData != null && _currentPage * PageSize < _maintenanceData.Rows.Count)
            {
                UpdateDataGridView(_maintenanceData, _currentPage + 1);
            }
        }

        private void btnPrev_Click(object? sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                UpdateDataGridView(_maintenanceData, _currentPage - 1);
            }
        }

        private void LoadEquipment()
        {
            try
            {
                var equipment = DatabaseHelper.ExecuteProcedure("sp_LayTatCaCoSoVatChat");
                ConfigureComboBox(cmbEquipment, equipment, "Ten", "MaCoSoVatChat");
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải danh sách thiết bị", ex);
            }
        }

        private void LoadEmployees()
        {
            try
            {
                var employees = DatabaseHelper.ExecuteProcedure("sp_LayNhanVienKyThuat");
                ConfigureComboBox(cmbEmployee, employees, "Ten", "MaNhanVien");
                ConfigureComboBox(cmbFilterEmployee, employees.Copy(), "Ten", "MaNhanVien");
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải danh sách nhân viên", ex);
            }
        }

        private void LoadAreasForFilter()
        {
            try
            {
                var areas = DatabaseHelper.ExecuteProcedure("sp_LayTatCaKhuVuc");
                _cmbFilterArea.DataSource = areas;
                _cmbFilterArea.DisplayMember = "TenKhuVuc";
                _cmbFilterArea.ValueMember = "MaKhuVuc";
                _cmbFilterArea.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải danh sách khu vực", ex);
            }
        }

        private void LoadTypesForFilter()
        {
            try
            {
                DataTable types;
                if (_cmbFilterArea.SelectedValue is int areaId)
                {
                    var parameters = new SqlParameter[] { new SqlParameter("@MaKhuVuc", areaId) };
                    types = DatabaseHelper.ExecuteProcedure("sp_LayLoaiTheoKhuVuc", parameters);
                }
                else
                {
                    types = DatabaseHelper.ExecuteProcedure("sp_LayTatCaLoaiCoSoVatChat");
                }

                _cmbFilterType.DataSource = types;
                _cmbFilterType.DisplayMember = "TenLoai";
                _cmbFilterType.ValueMember = "MaLoai";
                _cmbFilterType.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải danh sách loại", ex);
            }
        }

        private void _cmbFilterArea_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LoadTypesForFilter();
        }

        private void ConfigureComboBox(ComboBox comboBox, DataTable data, string displayMember, string valueMember)
        {
            comboBox.DataSource = data;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            comboBox.SelectedIndex = -1;
        }

        private void btnFilter_Click(object? sender, EventArgs e)
        {
            try
            {
                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@MaKhuVuc", _cmbFilterArea.SelectedValue ?? DBNull.Value),
                    new SqlParameter("@MaLoai", _cmbFilterType.SelectedValue ?? DBNull.Value),
                    new SqlParameter("@MaNhanVien", cmbFilterEmployee.SelectedValue ?? DBNull.Value),
                    new SqlParameter("@NgayBatDau", dtpStart.Value.Date),
                    new SqlParameter("@NgayKetThuc", dtpEnd.Value.Date)
                };

                _maintenanceData = DatabaseHelper.ExecuteProcedure("sp_LayBaoTriTheoBoLoc_MoRong", parameters);
                UpdateDataGridView(_maintenanceData, 1);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi lọc dữ liệu", ex);
            }
        }

        private void btnResetFilter_Click(object? sender, EventArgs e)
        {
            _cmbFilterArea.SelectedIndex = -1;
            _cmbFilterType.SelectedIndex = -1;
            cmbFilterEmployee.SelectedIndex = -1;
            dtpStart.Value = DateTime.Now.AddMonths(-1);
            dtpEnd.Value = DateTime.Now;
            LoadMaintenance();
        }

        private void btnAdd_Click(object? sender, EventArgs e)
        {
            using var editForm = new MaintenanceEditForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadMaintenance();
            }
        }

        private void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (dgvMaintenance.SelectedRows.Count == 0)
            {
                ShowWarning("Vui lòng chọn một hàng để cập nhật.");
                return;
            }

            int maintenanceId = Convert.ToInt32(dgvMaintenance.SelectedRows[0].Cells["MaBaoTri"].Value);
            using var editForm = new MaintenanceEditForm(maintenanceId);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadMaintenance();
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvMaintenance.SelectedRows.Count == 0)
            {
                ShowWarning("Vui lòng chọn một hàng để xóa.");
                return;
            }

            string equipment = dgvMaintenance.SelectedRows[0].Cells["TenCoSoVatChat"].Value?.ToString() ?? "";
            if (MessageBox.Show($"Bạn có chắc muốn xóa mục bảo trì của '{equipment}'?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int maintenanceId = Convert.ToInt32(dgvMaintenance.SelectedRows[0].Cells["MaBaoTri"].Value);
                var parameters = new SqlParameter[] { new SqlParameter("@MaBaoTri", maintenanceId) };
                DatabaseHelper.ExecuteNonQuery("sp_XoaBaoTri", parameters);
                LoadMaintenance();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi xóa bảo trì", ex);
            }
        }

        private void DgvMaintenance_SelectionChanged(object? sender, EventArgs e)
        {
            if (_selectedSummaryLabel == null) return;

            if (dgvMaintenance.SelectedRows.Count > 0)
            {
                var row = dgvMaintenance.SelectedRows[0];
                string equipment = row.Cells["TenCoSoVatChat"].Value?.ToString() ?? "";
                string employee = row.Cells["TenNhanVien"].Value?.ToString() ?? "";
                string date = DateTime.TryParse(row.Cells["NgayBaoTri"].Value?.ToString(), out var d)
                    ? d.ToString("dd/MM/yyyy")
                    : "";
                string cost = decimal.TryParse(row.Cells["ChiPhi"].Value?.ToString(), out var c)
                    ? $"{c:N0} đ"
                    : "0 đ";

                _selectedSummaryLabel.Text = $"Đang chọn: {equipment} | Nhân viên: {employee} | Ngày: {date} | Chi phí: {cost}";
            }
            else
            {
                _selectedSummaryLabel.Text = "Chưa chọn bảo trì";
            }
        }

        public void SearchMaintenanceByEquipment(string equipmentName)
        {
            if (string.IsNullOrEmpty(equipmentName))
            {
                LoadMaintenance();
                return;
            }

            try
            {
                _maintenanceData = DatabaseHelper.ExecuteProcedure(
                    "sp_TimKiemBaoTriTheoTenCoSoVatChat",
                    new SqlParameter("@TenCoSoVatChat", equipmentName)
                );
                UpdateDataGridView(_maintenanceData, 1);
                SetupColumnHeaders();
                ShowSearchResult($"bảo trì cho cơ sở vật chất '{equipmentName}'", _maintenanceData?.Rows.Count ?? 0);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tìm kiếm bảo trì theo thiết bị", ex);
            }
        }

        public void SearchMaintenanceByEmployee(string employeeName)
        {
            if (string.IsNullOrEmpty(employeeName))
            {
                LoadMaintenance();
                return;
            }

            try
            {
                _maintenanceData = DatabaseHelper.ExecuteProcedure(
                    "sp_TimKiemBaoTriTheoTenNhanVien",
                    new SqlParameter("@TenNhanVien", employeeName)
                );
                UpdateDataGridView(_maintenanceData, 1);
                SetupColumnHeaders();
                ShowSearchResult($"bảo trì được thực hiện bởi '{employeeName}'", _maintenanceData?.Rows.Count ?? 0);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tìm kiếm bảo trì theo nhân viên", ex);
            }
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
            LoadMaintenance();
            _currentPage = 1;
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
                _maintenanceData = DatabaseHelper.ExecuteProcedure(
                    "sp_TimKiemBaoTriTheoTenCoSoVatChat",
                    new SqlParameter("@TenCoSoVatChat", searchTerm)
                );
                if (_maintenanceData.Rows.Count > 0)
                {
                    UpdateDataGridView(_maintenanceData, 1);
                    ShowSearchResult($"bảo trì cho thiết bị có tên chứa '{searchTerm}'", _maintenanceData.Rows.Count);
                    return;
                }

                _maintenanceData = DatabaseHelper.ExecuteProcedure(
                    "sp_TimKiemBaoTriTheoTenNhanVien",
                    new SqlParameter("@TenNhanVien", searchTerm)
                );
                UpdateDataGridView(_maintenanceData, 1);
                ShowSearchResult($"bảo trì được thực hiện bởi nhân viên có tên chứa '{searchTerm}'", _maintenanceData.Rows.Count);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tìm kiếm", ex);
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
            if (_maintenanceData == null)
            {
                _pageInfoLabel.Text = string.Empty;
                btnPrev.Enabled = btnNext.Enabled = false;
                return;
            }

            int total = _maintenanceData.Rows.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
            int start = total == 0 ? 0 : (_currentPage - 1) * PageSize + 1;
            int end = Math.Min(_currentPage * PageSize, total);

            _pageInfoLabel.Text = $"Hiển thị {start}-{end}/{total} (Trang {_currentPage}/{totalPages})";
            btnPrev.Enabled = _currentPage > 1;
            btnNext.Enabled = _currentPage < totalPages;
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