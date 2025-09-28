using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace FacilityManagementSystem
{
    public partial class ReportsForm : Form
    {
        private const int PageSize = 15;
        private DataTable? _maintenanceCostData;
        private DataTable? _assetValueData;
        private DataTable? _maintenanceNeededData;
        private DataTable? _overBudgetData; // thiết bị có chi phí bảo trì > 50% giá trị
        private int _pageMaintenanceCost = 1;
        private int _pageAssetValue = 1;
        private int _pageMaintenanceNeeded = 1;
        private int _pageOverBudget = 1;

        private readonly FlowLayoutPanel _pagerMaintenanceCost;
        private readonly FlowLayoutPanel _pagerAssetValue;
        private readonly FlowLayoutPanel _pagerMaintenanceNeeded;
        private readonly FlowLayoutPanel _pagerOverBudget;
        private readonly Label _lblPageInfoMaintenanceCost;
        private readonly Label _lblPageInfoAssetValue;
        private readonly Label _lblPageInfoMaintenanceNeeded;
        private readonly Label _lblPageInfoOverBudget;
        private readonly Button _btnPrevMaintenanceCost;
        private readonly Button _btnNextMaintenanceCost;
        private readonly Button _btnPrevAssetValue;
        private readonly Button _btnNextAssetValue;
        private readonly Button _btnPrevMaintenanceNeeded;
        private readonly Button _btnNextMaintenanceNeeded;
        private readonly Button _btnPrevOverBudget;
        private readonly Button _btnNextOverBudget;

        // Controls for new report tab (avoid touching Designer)
        private readonly TabPage _tabOverBudget = new TabPage { Name = "tabOverBudget", Text = "🚨 Thiết Bị Vượt 50% Chi Phí" };
        private readonly DataGridView _dgvOverBudget = new DataGridView { Name = "dgvOverBudget" };
        private readonly Button _btnRefreshOverBudget = new Button { Name = "btnRefreshOverBudget", Text = "Làm Mới Dữ Liệu" };
        private readonly Label _lblOverBudgetCount = new Label { Name = "lblOverBudgetCount", Text = "Tổng số thiết bị vượt ngưỡng: 0" };

        public ReportsForm()
        {
            InitializeComponent();
            _pagerMaintenanceCost = new FlowLayoutPanel();
            _pagerAssetValue = new FlowLayoutPanel();
            _pagerMaintenanceNeeded = new FlowLayoutPanel();
            _pagerOverBudget = new FlowLayoutPanel();
            _lblPageInfoMaintenanceCost = new Label { AutoSize = true, Margin = new Padding(10, 8, 10, 0) };
            _lblPageInfoAssetValue = new Label { AutoSize = true, Margin = new Padding(10, 8, 10, 0) };
            _lblPageInfoMaintenanceNeeded = new Label { AutoSize = true, Margin = new Padding(10, 8, 10, 0) };
            _lblPageInfoOverBudget = new Label { AutoSize = true, Margin = new Padding(10, 8, 10, 0) };
            _btnPrevMaintenanceCost = new Button { Text = "Trước", Height = 32, Width = 80 };
            _btnNextMaintenanceCost = new Button { Text = "Tiếp", Height = 32, Width = 80 };
            _btnPrevAssetValue = new Button { Text = "Trước", Height = 32, Width = 80 };
            _btnNextAssetValue = new Button { Text = "Tiếp", Height = 32, Width = 80 };
            _btnPrevMaintenanceNeeded = new Button { Text = "Trước", Height = 32, Width = 80 };
            _btnNextMaintenanceNeeded = new Button { Text = "Tiếp", Height = 32, Width = 80 };
            _btnPrevOverBudget = new Button { Text = "Trước", Height = 32, Width = 80 };
            _btnNextOverBudget = new Button { Text = "Tiếp", Height = 32, Width = 80 };

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
            Text = "Báo Cáo Quản Lý Cơ Sở Vật Chất";

            ConfigureTabControl();
            ConfigureDataGridViews();
            ConfigureButtons();
            ConfigureComboBoxes();
            ConfigureLabels();
        }

        private void ConfigureTabControl()
        {
            tabReports.Dock = DockStyle.Fill;
            tabReports.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
        }

        private void ConfigureDataGridViews()
        {
            foreach (var dgv in new[] { dgvMaintenanceCost, dgvAssetValue, dgvMaintenanceNeeded, _dgvOverBudget })
            {
                dgv.BackgroundColor = Color.White;
                dgv.BorderStyle = BorderStyle.Fixed3D;
                dgv.ReadOnly = true;
                dgv.AllowUserToAddRows = false;
                dgv.AllowUserToDeleteRows = false;
                dgv.MultiSelect = false;
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgv.RowTemplate.Height = 28;
                dgv.RowHeadersVisible = false;
                dgv.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
                dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
                dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
                dgv.ColumnHeadersHeight = 35;
                dgv.GridColor = Color.LightGray;
            }
        }

        private void ConfigureButtons()
        {
            foreach (var button in new[] { btnViewMaintenanceCost, btnRefreshStatus, btnViewAssetValue, btnRefreshMaintenanceNeeded,
                                          _btnPrevMaintenanceCost, _btnNextMaintenanceCost, _btnPrevAssetValue, _btnNextAssetValue,
                                          _btnPrevMaintenanceNeeded, _btnNextMaintenanceNeeded,
                                          _btnPrevOverBudget, _btnNextOverBudget, _btnRefreshOverBudget })
            {
                button.Height = 32;
                button.Width = 80;
                button.FlatStyle = FlatStyle.Flat;
                button.BackColor = Color.FromArgb(0, 120, 215);
                button.ForeColor = Color.White;
                button.Margin = new Padding(5);
            }
            _btnRefreshOverBudget.Width = 140;
        }

        private void ConfigureComboBoxes()
        {
            foreach (var cmb in new[] { cmbMonth, cmbYear, cmbArea })
            {
                cmb.Height = 28;
                cmb.Width = 120;
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                cmb.Margin = new Padding(5);
            }
        }

        private void ConfigureLabels()
        {
            foreach (var label in new[] { label1, label2, label3, label4, label5, label6, label7,
                                         lblTotalCost, lblAreaTotalValue, lblMaintenanceCount,
                                         lblActive, lblMaintenance, lblBroken, lblStopped, _lblOverBudgetCount })
            {
                label.AutoSize = true;
                label.Margin = new Padding(5, 8, 5, 0);
            }

            lblTotalCost.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalCost.ForeColor = Color.DarkBlue;
            lblAreaTotalValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAreaTotalValue.ForeColor = Color.DarkGoldenrod;
            lblMaintenanceCount.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblMaintenanceCount.ForeColor = Color.DarkRed;

            lblActive.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblActive.ForeColor = Color.Green;
            lblMaintenance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblMaintenance.ForeColor = Color.Orange;
            lblBroken.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblBroken.ForeColor = Color.Red;
            lblStopped.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblStopped.ForeColor = Color.DarkRed;

            label3.Text = "✅ Hoạt Động:";
            label4.Text = "🔧 Đang Bảo Trì:";
            label5.Text = "💥 Hỏng:";
            label6.Text = "🔴 Ngừng Hoạt Động:";
            label7.Text = "Khu Vực:";
            label1.Text = "Tháng:";
            label2.Text = "Năm:";

            _lblOverBudgetCount.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _lblOverBudgetCount.ForeColor = Color.Firebrick;
        }

        private void BuildLayout()
        {
            ConfigureTabLayout(tabMaintenanceCost, _pagerMaintenanceCost, new Control[] { label1, cmbMonth, label2, cmbYear, btnViewMaintenanceCost, lblTotalCost }, dgvMaintenanceCost);
            ConfigureTabLayout(tabAssetValue, _pagerAssetValue, new Control[] { label7, cmbArea, btnViewAssetValue, lblAreaTotalValue }, dgvAssetValue);
            ConfigureTabLayout(tabMaintenanceNeeded, _pagerMaintenanceNeeded, new Control[] { btnRefreshMaintenanceNeeded, lblMaintenanceCount }, dgvMaintenanceNeeded);
            ConfigureTabLayout(_tabOverBudget, _pagerOverBudget, new Control[] { _btnRefreshOverBudget, _lblOverBudgetCount }, _dgvOverBudget);
            ConfigureEquipmentStatusTab();
            tabReports.Controls.AddRange(new[] { tabMaintenanceCost, tabEquipmentStatus, tabAssetValue, tabMaintenanceNeeded, _tabOverBudget });
            Controls.Clear();
            Controls.Add(tabReports);
        }

        private void ConfigureTabLayout(TabPage tab, FlowLayoutPanel pager, Control[] controls, DataGridView dgv)
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(10)
            };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var controlPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Padding = new Padding(5)
            };
            controlPanel.Controls.AddRange(controls);

            // Configure DataGridView to fill the cell
            dgv.Dock = DockStyle.Fill; // Ensure DataGridView fills the cell
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Columns fill the DataGridView width
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; // Prevent rows from auto-sizing vertically
            dgv.ScrollBars = ScrollBars.Both; // Enable both scrollbars if needed

            pager.Dock = DockStyle.Fill;
            pager.FlowDirection = FlowDirection.LeftToRight;
            pager.AutoSize = true;
            pager.Padding = new Padding(5);
            if (tab == tabMaintenanceCost)
                pager.Controls.AddRange(new Control[] { _btnPrevMaintenanceCost, _btnNextMaintenanceCost, _lblPageInfoMaintenanceCost });
            else if (tab == tabAssetValue)
                pager.Controls.AddRange(new Control[] { _btnPrevAssetValue, _btnNextAssetValue, _lblPageInfoAssetValue });
            else if (tab == tabMaintenanceNeeded)
                pager.Controls.AddRange(new Control[] { _btnPrevMaintenanceNeeded, _btnNextMaintenanceNeeded, _lblPageInfoMaintenanceNeeded });

            tab.Controls.Clear();
            layout.Controls.Add(controlPanel, 0, 0);
            layout.Controls.Add(dgv, 0, 1);
            layout.Controls.Add(pager, 0, 2);
            tab.Controls.Add(layout);
        }

        private void ConfigureEquipmentStatusTab()
        {
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Padding = new Padding(10)
            };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            var controlPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Padding = new Padding(5),
                WrapContents = false
            };
            var statusPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 4,
                AutoSize = true,
                Padding = new Padding(5)
            };
            statusPanel.Controls.AddRange(new[]
            {
                label3, lblActive,
                label4, lblMaintenance,
                label5, lblBroken,
                label6, lblStopped
            });
            statusPanel.SetColumn(label3, 0); statusPanel.SetRow(label3, 0);
            statusPanel.SetColumn(lblActive, 1); statusPanel.SetRow(lblActive, 0);
            statusPanel.SetColumn(label4, 0); statusPanel.SetRow(label4, 1);
            statusPanel.SetColumn(lblMaintenance, 1); statusPanel.SetRow(lblMaintenance, 1);
            statusPanel.SetColumn(label5, 0); statusPanel.SetRow(label5, 2);
            statusPanel.SetColumn(lblBroken, 1); statusPanel.SetRow(lblBroken, 2);
            statusPanel.SetColumn(label6, 0); statusPanel.SetRow(label6, 3);
            statusPanel.SetColumn(lblStopped, 1); statusPanel.SetRow(lblStopped, 3);

            controlPanel.Controls.AddRange(new Control[] { statusPanel, btnRefreshStatus });

            tabEquipmentStatus.Controls.Clear();
            layout.Controls.Add(controlPanel, 0, 0);
            tabEquipmentStatus.Controls.Add(layout);
        }

        private void RegisterEventHandlers()
        {
            Load += ReportsForm_Load;
            btnViewMaintenanceCost.Click += btnViewMaintenanceCost_Click;
            btnRefreshStatus.Click += btnRefreshStatus_Click;
            btnViewAssetValue.Click += btnViewAssetValue_Click;
            btnRefreshMaintenanceNeeded.Click += btnRefreshMaintenanceNeeded_Click;
            _btnRefreshOverBudget.Click += (s, e) => LoadOverBudgetReport();
            _btnPrevMaintenanceCost.Click += (s, e) => { if (_pageMaintenanceCost > 1) { _pageMaintenanceCost--; BindMaintenanceCostPaged(); } };
            _btnNextMaintenanceCost.Click += (s, e) => { if (_maintenanceCostData != null && _pageMaintenanceCost * PageSize < _maintenanceCostData.Rows.Count) { _pageMaintenanceCost++; BindMaintenanceCostPaged(); } };
            _btnPrevAssetValue.Click += (s, e) => { if (_pageAssetValue > 1) { _pageAssetValue--; BindAssetValuePaged(); } };
            _btnNextAssetValue.Click += (s, e) => { if (_assetValueData != null && _pageAssetValue * PageSize < _assetValueData.Rows.Count) { _pageAssetValue++; BindAssetValuePaged(); } };
            _btnPrevMaintenanceNeeded.Click += (s, e) => { if (_pageMaintenanceNeeded > 1) { _pageMaintenanceNeeded--; BindMaintenanceNeededPaged(); } };
            _btnNextMaintenanceNeeded.Click += (s, e) => { if (_maintenanceNeededData != null && _pageMaintenanceNeeded * PageSize < _maintenanceNeededData.Rows.Count) { _pageMaintenanceNeeded++; BindMaintenanceNeededPaged(); } };
            _btnPrevOverBudget.Click += (s, e) => { if (_pageOverBudget > 1) { _pageOverBudget--; BindOverBudgetPaged(); } };
            _btnNextOverBudget.Click += (s, e) => { if (_overBudgetData != null && _pageOverBudget * PageSize < _overBudgetData.Rows.Count) { _pageOverBudget++; BindOverBudgetPaged(); } };
        }

        private void ReportsForm_Load(object? sender, EventArgs e)
        {
            LoadInitialData();
        }

        private void LoadInitialData()
        {
            try
            {
                LoadComboBoxData();
                LoadEquipmentStatusReport();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi khởi tạo báo cáo", ex);
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
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;
        }

        private void LoadYearComboBox()
        {
            cmbYear.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int i = currentYear - 5; i <= currentYear + 1; i++)
            {
                cmbYear.Items.Add(i);
            }
            cmbYear.SelectedItem = currentYear;
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
                    ShowWarning("Không có dữ liệu khu vực.");
                    cmbArea.Items.Add("Không có dữ liệu khu vực");
                    cmbArea.SelectedIndex = 0;
                    cmbArea.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải danh sách khu vực", ex);
                cmbArea.Items.Add("Lỗi tải dữ liệu");
                cmbArea.SelectedIndex = 0;
                cmbArea.Enabled = false;
            }
        }
        #endregion

        #region 5. Báo Cáo Thiết Bị Có Chi Phí Bảo Trì > 50% Giá Trị
        private void LoadOverBudgetReport()
        {
            try
            {
                _overBudgetData = DatabaseHelper.ExecuteProcedure("sp_BaoCaoThietBiChiPhiBaoTriVuot50PhanTram");
                _pageOverBudget = 1;
                BindOverBudgetPaged();
                SetupOverBudgetHeaders();
                UpdateOverBudgetCount();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải báo cáo thiết bị vượt 50% chi phí", ex);
                _lblOverBudgetCount.Text = "Tổng số thiết bị vượt ngưỡng: 0";
            }
        }

        private void BindOverBudgetPaged()
        {
            _dgvOverBudget.DataSource = GetPagedData(_overBudgetData, _pageOverBudget);
            UpdatePagerInfo(_lblPageInfoOverBudget, _btnPrevOverBudget, _btnNextOverBudget, _overBudgetData, _pageOverBudget);
        }

        private void SetupOverBudgetHeaders()
        {
            if (_dgvOverBudget.Columns.Count == 0) return;

            ConfigureColumn(_dgvOverBudget, "TenCoSoVatChat", "Tên Thiết Bị", 200);
            ConfigureColumn(_dgvOverBudget, "TenKhuVuc", "Khu Vực", 120);
            ConfigureColumn(_dgvOverBudget, "TenLoai", "Loại Thiết Bị", 150);
            ConfigureColumn(_dgvOverBudget, "TrangThai", "Trạng Thái", 120);
            ConfigureColumn(_dgvOverBudget, "Gia", "Giá Trị (VND)", 130, "N0", DataGridViewContentAlignment.MiddleRight);
            ConfigureColumn(_dgvOverBudget, "TongChiPhiBaoTri", "Tổng Chi Phí Bảo Trì (VND)", 160, "N0", DataGridViewContentAlignment.MiddleRight);
            ConfigureColumn(_dgvOverBudget, "PhanTramChiPhi", "Phần Trăm Chi Phí (%)", 160, "N2", DataGridViewContentAlignment.MiddleRight);
            ConfigureColumn(_dgvOverBudget, "SoLanBaoTri", "Số Lần Bảo Trì", 120, null, DataGridViewContentAlignment.MiddleCenter);
            ConfigureColumn(_dgvOverBudget, "LanBaoTriGanNhat", "Bảo Trì Gần Nhất", 140, "dd/MM/yyyy");
            // Optional: hide MaCoSoVatChat column if present
            if (_dgvOverBudget.Columns["MaCoSoVatChat"] is DataGridViewColumn col)
                col.Visible = false;
        }

        private void UpdateOverBudgetCount()
        {
            int count = _overBudgetData?.Rows.Count ?? 0;
            _lblOverBudgetCount.Text = $"Tổng số thiết bị vượt ngưỡng: {count}";
        }
        #endregion

        #region 1. Báo Cáo Chi Phí Bảo Trì Theo Tháng
        private void btnViewMaintenanceCost_Click(object? sender, EventArgs e)
        {
            if (cmbMonth.SelectedItem == null || cmbYear.SelectedItem == null)
            {
                ShowWarning("Vui lòng chọn tháng và năm!");
                return;
            }
            LoadMaintenanceCostReport();
        }

        private void LoadMaintenanceCostReport()
        {
            try
            {
                int selectedMonth = ((dynamic?)cmbMonth.SelectedItem)?.Value ?? 0;
                int selectedYear = (int?)cmbYear.SelectedItem ?? 0;

                var parameters = new[]
                {
                    new SqlParameter("@Thang", selectedMonth),
                    new SqlParameter("@Nam", selectedYear)
                };

                _maintenanceCostData = DatabaseHelper.ExecuteProcedure("sp_BaoCaoChiPhiBaoTriTheoThang", parameters);
                _pageMaintenanceCost = 1;
                BindMaintenanceCostPaged();
                SetupMaintenanceCostHeaders();
                CalculateTotalMaintenanceCost();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải báo cáo chi phí bảo trì", ex);
            }
        }

        private void BindMaintenanceCostPaged()
        {
            dgvMaintenanceCost.DataSource = GetPagedData(_maintenanceCostData, _pageMaintenanceCost);
            UpdatePagerInfo(_lblPageInfoMaintenanceCost, _btnPrevMaintenanceCost, _btnNextMaintenanceCost, _maintenanceCostData, _pageMaintenanceCost);
        }

        private void SetupMaintenanceCostHeaders()
        {
            if (dgvMaintenanceCost.Columns.Count == 0) return;

            ConfigureColumn(dgvMaintenanceCost, "TenCoSoVatChat", "Tên Thiết Bị", 200);
            ConfigureColumn(dgvMaintenanceCost, "TenKhuVuc", "Khu Vực", 120);
            ConfigureColumn(dgvMaintenanceCost, "NgayBaoTri", "Ngày Bảo Trì", 100, "dd/MM/yyyy");
            ConfigureColumn(dgvMaintenanceCost, "ChiPhi", "Chi Phí (VND)", 120, "N0", DataGridViewContentAlignment.MiddleRight);
            ConfigureColumn(dgvMaintenanceCost, "TenNhanVien", "Nhân Viên", 150);
            ConfigureColumn(dgvMaintenanceCost, "MoTa", "Mô Tả", 250);
        }

        private void CalculateTotalMaintenanceCost()
        {
            decimal totalCost = _maintenanceCostData?.Rows.Cast<DataRow>()
                .Where(row => row["ChiPhi"] != DBNull.Value)
                .Sum(row => Convert.ToDecimal(row["ChiPhi"])) ?? 0;
            lblTotalCost.Text = $"Tổng chi phí: {totalCost:N0} đ";
        }
        #endregion

        #region 2. Báo Cáo Thiết Bị Theo Trạng Thái
        private void btnRefreshStatus_Click(object? sender, EventArgs e)
        {
            LoadEquipmentStatusReport();
        }

        private void LoadEquipmentStatusReport()
        {
            try
            {
                var dtStatus = DatabaseHelper.ExecuteProcedure("sp_BaoCaoThietBiTheoTrangThai");
                int activeCount = 0, maintenanceCount = 0, brokenCount = 0, stoppedCount = 0;

                if (dtStatus != null && dtStatus.Rows.Count > 0)
                {
                    foreach (DataRow row in dtStatus.Rows)
                    {
                        string status = row["TrangThai"]?.ToString() ?? "";
                        int count = row["SoLuong"] != DBNull.Value ? Convert.ToInt32(row["SoLuong"]) : 0;
                        switch (status)
                        {
                            case "Hoạt Động": activeCount = count; break;
                            case "Đang Bảo Trì": maintenanceCount = count; break;
                            case "Hỏng": brokenCount = count; break;
                            case "Ngừng Hoạt Động": stoppedCount = count; break;
                        }
                    }
                }

                lblActive.Text = $"{activeCount} thiết bị";
                lblMaintenance.Text = $"{maintenanceCount} thiết bị";
                lblBroken.Text = $"{brokenCount} thiết bị";
                lblStopped.Text = $"{stoppedCount} thiết bị";
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải báo cáo trạng thái thiết bị", ex);
                lblActive.Text = lblMaintenance.Text = lblBroken.Text = lblStopped.Text = "0 thiết bị";
            }
        }
        #endregion

        #region 3. Báo Cáo Giá Trị Tài Sản Theo Khu Vực
        private void btnViewAssetValue_Click(object? sender, EventArgs e)
        {
            if (cmbArea.SelectedValue == null)
            {
                ShowWarning("Vui lòng chọn khu vực!");
                return;
            }
            LoadAssetValueReport();
        }

        private void LoadAssetValueReport()
        {
            try
            {
                int selectedAreaId = (int?)cmbArea.SelectedValue ?? 0;
                var parameters = new[] { new SqlParameter("@MaKhuVuc", selectedAreaId) };

                _assetValueData = DatabaseHelper.ExecuteProcedure("sp_BaoCaoGiaTriTaiSanTheoKhuVuc", parameters);
                _pageAssetValue = 1;
                BindAssetValuePaged();
                SetupAssetValueHeaders();
                CalculateTotalAssetValue();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải báo cáo giá trị tài sản", ex);
            }
        }

        private void BindAssetValuePaged()
        {
            dgvAssetValue.DataSource = GetPagedData(_assetValueData, _pageAssetValue);
            UpdatePagerInfo(_lblPageInfoAssetValue, _btnPrevAssetValue, _btnNextAssetValue, _assetValueData, _pageAssetValue);
        }

        private void SetupAssetValueHeaders()
        {
            if (dgvAssetValue.Columns.Count == 0) return;

            ConfigureColumn(dgvAssetValue, "TenCoSoVatChat", "Tên Thiết Bị", 250);
            ConfigureColumn(dgvAssetValue, "TenLoai", "Loại Thiết Bị", 150);
            ConfigureColumn(dgvAssetValue, "TrangThai", "Trạng Thái", 120);
            ConfigureColumn(dgvAssetValue, "Gia", "Giá Trị (VND)", 150, "N0", DataGridViewContentAlignment.MiddleRight);
        }

        private void CalculateTotalAssetValue()
        {
            decimal totalValue = _assetValueData?.Rows.Cast<DataRow>()
                .Where(row => row["Gia"] != DBNull.Value)
                .Sum(row => Convert.ToDecimal(row["Gia"])) ?? 0;
            lblAreaTotalValue.Text = $"Tổng giá trị: {totalValue:N0} đ";
        }
        #endregion

        #region 4. Báo Cáo Thiết Bị Cần Bảo Trì
        private void btnRefreshMaintenanceNeeded_Click(object? sender, EventArgs e)
        {
            LoadMaintenanceNeededReport();
        }

        private void LoadMaintenanceNeededReport()
        {
            try
            {
                _maintenanceNeededData = DatabaseHelper.ExecuteProcedure("sp_BaoCaoThietBiCanBaoTri");
                _pageMaintenanceNeeded = 1;
                BindMaintenanceNeededPaged();
                SetupMaintenanceNeededHeaders();
                UpdateMaintenanceNeededCount();
            }
            catch (Exception ex)
            {
                ShowError("Lỗi khi tải báo cáo thiết bị cần bảo trì", ex);
                lblMaintenanceCount.Text = "Tổng số thiết bị cần bảo trì: 0";
            }
        }

        private void BindMaintenanceNeededPaged()
        {
            dgvMaintenanceNeeded.DataSource = GetPagedData(_maintenanceNeededData, _pageMaintenanceNeeded);
            UpdatePagerInfo(_lblPageInfoMaintenanceNeeded, _btnPrevMaintenanceNeeded, _btnNextMaintenanceNeeded, _maintenanceNeededData, _pageMaintenanceNeeded);
        }

        private void SetupMaintenanceNeededHeaders()
        {
            if (dgvMaintenanceNeeded.Columns.Count == 0) return;

            ConfigureColumn(dgvMaintenanceNeeded, "TenCoSoVatChat", "Tên Thiết Bị", 200);
            ConfigureColumn(dgvMaintenanceNeeded, "TenKhuVuc", "Khu Vực", 120);
            ConfigureColumn(dgvMaintenanceNeeded, "TenLoai", "Loại Thiết Bị", 150);
            ConfigureColumn(dgvMaintenanceNeeded, "TrangThai", "Trạng Thái", 120);
            ConfigureColumn(dgvMaintenanceNeeded, "Gia", "Giá Trị (VND)", 120, "N0", DataGridViewContentAlignment.MiddleRight);
            ConfigureColumn(dgvMaintenanceNeeded, "NgayBaoTri", "Ngày Bảo Trì Gần Nhất", 130, "dd/MM/yyyy");
            ConfigureColumn(dgvMaintenanceNeeded, "TrangThaiBaoTri", "Trạng Thái Bảo Trì", 130);
            if (dgvMaintenanceNeeded.Columns["ThuTuUuTien"] is DataGridViewColumn col)
                col.Visible = false;
        }
        #endregion

        private DataTable GetPagedData(DataTable? data, int page)
        {
            if (data == null) return new DataTable();
            var paged = data.Clone();
            int start = (page - 1) * PageSize;
            for (int i = start; i < start + PageSize && i < data.Rows.Count; i++)
                paged.ImportRow(data.Rows[i]);
            return paged;
        }

        private void UpdatePagerInfo(Label label, Button prev, Button next, DataTable? data, int page)
        {
            int total = data?.Rows.Count ?? 0;
            int totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)PageSize));
            int start = total == 0 ? 0 : (page - 1) * PageSize + 1;
            int end = Math.Min(page * PageSize, total);
            label.Text = $"Hiển thị {start}-{end}/{total} (Trang {page}/{totalPages})";
            prev.Enabled = page > 1;
            next.Enabled = page < totalPages;
        }

        private void ConfigureColumn(DataGridView dgv, string columnName, string headerText, int width, string? format = null, DataGridViewContentAlignment? alignment = null)
        {
            if (dgv.Columns[columnName] is DataGridViewColumn column)
            {
                column.HeaderText = headerText;
                column.Width = width;
                if (!string.IsNullOrEmpty(format))
                    column.DefaultCellStyle.Format = format;
                if (alignment.HasValue)
                    column.DefaultCellStyle.Alignment = alignment.Value;
            }
        }

        private void UpdateMaintenanceNeededCount()
        {
            int count = _maintenanceNeededData?.Rows.Count ?? 0;
            lblMaintenanceCount.Text = $"Tổng số thiết bị cần bảo trì: {count}";
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