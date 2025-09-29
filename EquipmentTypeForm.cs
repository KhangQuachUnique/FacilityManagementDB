using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace FacilityManagementSystem
{
    public partial class EquipmentTypeForm : Form
    {
        private const int PageSize = 15;
        private DataTable? _typeData;
        private int _currentPage = 1;

        private readonly TableLayoutPanel _mainLayout;
        private readonly FlowLayoutPanel _searchPanel;
        private readonly FlowLayoutPanel _actionPanel;
        private readonly Label _pageInfoLabel;
        private readonly Label _selectedSummaryLabel;

        public EquipmentTypeForm()
        {
            InitializeComponent();
            _mainLayout = new TableLayoutPanel();
            _searchPanel = new FlowLayoutPanel();
            _actionPanel = new FlowLayoutPanel();
            _pageInfoLabel = new Label { AutoSize = true, Margin = new Padding(10, 8, 10, 0) };
            _selectedSummaryLabel = new Label { AutoSize = true, Margin = new Padding(10, 8, 20, 0) };

            ConfigureUI();
            BuildLayout();
            RegisterEventHandlers();
            LoadTypes();
        }

        private void ConfigureUI()
        {
            BackColor = Color.WhiteSmoke;
            Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(1000, 600);
            Text = "Quản Lý Loại Cơ Sở Vật Chất";

            ConfigureDataGridView();
            ConfigureButtons();
            ConfigureTextBox();
            ConfigureLabels();
        }

        private void ConfigureDataGridView()
        {
            dgvTypes.BackgroundColor = Color.White;
            dgvTypes.BorderStyle = BorderStyle.Fixed3D;
            dgvTypes.ReadOnly = true;
            dgvTypes.AllowUserToAddRows = false;
            dgvTypes.AllowUserToDeleteRows = false;
            dgvTypes.MultiSelect = false;
            dgvTypes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTypes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTypes.RowTemplate.Height = 28;
            dgvTypes.Dock = DockStyle.Fill;
            dgvTypes.RowHeadersVisible = false;
        }

        private void ConfigureButtons()
        {
            foreach (var button in new[] { btnAdd, btnUpdate, btnDelete, btnSearch, btnClearSearch, btnNext, btnPrev })
            {
                button.Height = 32;
                button.Width = 80;
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = Color.FromArgb(0, 120, 215);
                button.ForeColor = Color.White;
                button.Margin = new Padding(5);
            }
        }

        private void ConfigureTextBox()
        {
            txtSearch.Height = 28;
            txtSearch.Width = 200;
            txtSearch.Margin = new Padding(5);
            txtSearch.PlaceholderText = "Nhập tên loại để tìm kiếm...";
        }

        private void ConfigureLabels()
        {
            lblSearch.AutoSize = true;
            lblSearch.Text = "Tìm kiếm:";
            lblSearch.Margin = new Padding(5, 8, 5, 0);
        }

        private void BuildLayout()
        {
            ConfigureMainLayout();
            ConfigureSearchPanel();
            ConfigureActionPanel();
            RemoveLegacyControls();
            AddControlsToLayout();
            Controls.Add(_mainLayout);
        }

        private void ConfigureMainLayout()
        {
            _mainLayout.Dock = DockStyle.Fill;
            _mainLayout.ColumnCount = 1;
            _mainLayout.RowCount = 3;
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
            Controls.Remove(dgvTypes);
            Controls.Remove(lblSearch);
            Controls.Remove(txtSearch);
            Controls.Remove(btnSearch);
            Controls.Remove(btnClearSearch);
            Controls.Remove(label1);
            Controls.Remove(lblTypeNameValue);
            Controls.Remove(btnAdd);
            Controls.Remove(btnUpdate);
            Controls.Remove(btnDelete);
            Controls.Remove(btnPrev);
            Controls.Remove(btnNext);
        }

        private void AddControlsToLayout()
        {
            _mainLayout.Controls.Add(_searchPanel, 0, 0);
            _mainLayout.Controls.Add(dgvTypes, 0, 1);
            _mainLayout.Controls.Add(_actionPanel, 0, 2);
        }

        private void RegisterEventHandlers()
        {
            dgvTypes.SelectionChanged += DgvTypes_SelectionChanged;
            btnSearch.Click += btnSearch_Click;
            btnClearSearch.Click += btnClearSearch_Click;
            txtSearch.KeyDown += txtSearch_KeyDown;
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnNext.Click += btnNext_Click;
            btnPrev.Click += btnPrev_Click;
        }

        private void LoadTypes()
        {
            try
            {
                _typeData = DatabaseHelper.ExecuteProcedure("sp_LayTatCaLoaiCoSoVatChat");
                if (_typeData != null)
                {
                    UpdateDataGridView(_typeData, 1);
                    SetupColumnHeaders();
                }
                else
                {
                    dgvTypes.DataSource = null;
                    ShowWarning("Không thể tải dữ liệu loại cơ sở vật chất. Vui lòng kiểm tra kết nối database.");
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải dữ liệu loại cơ sở vật chất", ex);
                dgvTypes.DataSource = null;
            }
        }

        private void SetupColumnHeaders()
        {
            try
            {
                if (dgvTypes.Columns.Count == 0) return;

                ConfigureColumn("MaLoai", "Mã Loại", 100);
                ConfigureColumn("TenLoai", "Tên Loại", 0, DataGridViewAutoSizeColumnMode.Fill);
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi cấu hình tiêu đề cột", ex);
            }
        }

        private void ConfigureColumn(string columnName, string headerText, int width, DataGridViewAutoSizeColumnMode autoSizeMode = DataGridViewAutoSizeColumnMode.None)
        {
            if (dgvTypes.Columns[columnName] is DataGridViewColumn column)
            {
                column.HeaderText = headerText;
                column.AutoSizeMode = autoSizeMode;
                if (width > 0)
                {
                    column.Width = width;
                }
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

        private void UpdateDataGridView(DataTable? data, int page = 1)
        {
            if (data == null) return;

            _currentPage = page;
            dgvTypes.DataSource = GetPagedData(data, page);
            UpdatePagingInfo();
        }

        private void btnNext_Click(object? sender, EventArgs e)
        {
            if (_typeData != null && _currentPage * PageSize < _typeData.Rows.Count)
            {
                UpdateDataGridView(_typeData, _currentPage + 1);
            }
        }

        private void btnPrev_Click(object? sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                UpdateDataGridView(_typeData, _currentPage - 1);
            }
        }

        private void btnAdd_Click(object? sender, EventArgs e)
        {
            using var editForm = new EquipmentTypeEditForm();
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadTypes();
            }
        }

        private void btnUpdate_Click(object? sender, EventArgs e)
        {
            if (dgvTypes.SelectedRows.Count == 0)
            {
                ShowWarning("Vui lòng chọn một hàng để cập nhật.");
                return;
            }

            int typeId = Convert.ToInt32(dgvTypes.SelectedRows[0].Cells["MaLoai"].Value);
            using var editForm = new EquipmentTypeEditForm(typeId);
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                LoadTypes();
            }
        }

        private void btnDelete_Click(object? sender, EventArgs e)
        {
            if (dgvTypes.SelectedRows.Count == 0)
            {
                ShowWarning("Vui lòng chọn một hàng để xóa.");
                return;
            }

            string name = dgvTypes.SelectedRows[0].Cells["TenLoai"].Value?.ToString() ?? "";
            if (MessageBox.Show($"Bạn có chắc muốn xóa loại: '{name}'?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                int typeId = Convert.ToInt32(dgvTypes.SelectedRows[0].Cells["MaLoai"].Value);
                var parameters = new SqlParameter[] { new SqlParameter("@MaLoai", typeId) };
                DatabaseHelper.ExecuteNonQuery("sp_XoaLoaiCoSoVatChat", parameters);
                LoadTypes();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi xóa loại cơ sở vật chất", ex);
            }
        }

        private void DgvTypes_SelectionChanged(object? sender, EventArgs e)
        {
            if (_selectedSummaryLabel == null) return;

            if (dgvTypes.SelectedRows.Count > 0)
            {
                string name = dgvTypes.SelectedRows[0].Cells["TenLoai"].Value?.ToString() ?? "";
                string id = dgvTypes.SelectedRows[0].Cells["MaLoai"].Value?.ToString() ?? "";
                _selectedSummaryLabel.Text = $"Đang chọn: {name} (Mã: {id})";
            }
            else
            {
                _selectedSummaryLabel.Text = "Chưa chọn loại";
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
            LoadTypes();
            _currentPage = 1;
        }

        private void PerformSearch()
        {
            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadTypes();
                return;
            }

            try
            {
                _typeData = DatabaseHelper.ExecuteProcedure(
                    "sp_TimKiemLoaiCoSoVatChatTheoTen",
                    new SqlParameter("@TenLoai", searchTerm)
                );
                UpdateDataGridView(_typeData, 1);
                SetupColumnHeaders();
                ShowSearchResult($"loại cơ sở vật chất với từ khóa '{searchTerm}'", _typeData?.Rows.Count ?? 0);
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
            if (_typeData == null)
            {
                _pageInfoLabel.Text = string.Empty;
                btnPrev.Enabled = btnNext.Enabled = false;
                return;
            }

            int total = _typeData.Rows.Count;
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
            MessageBox.Show(message, "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}