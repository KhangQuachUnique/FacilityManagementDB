// File: AreaForm.cs

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace FacilityManagementSystem
{
    public partial class AreaForm : Form
    {
        private DataTable? dtAreas;
        private int currentPage = 1;
        private const int pageSize = 15;

        // Runtime-only UI elements
        private TableLayoutPanel? layout;
        private FlowLayoutPanel? topPanel;
        private FlowLayoutPanel? bottomPanel;
        private Label? lblPageInfo;

        public AreaForm()
        {
            InitializeComponent();
            ConfigureUI();
            BuildBasicLayout();
            LoadAreas();
        }
        
        private void ConfigureUI()
        {
            // Form basics
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.StartPosition = FormStartPosition.CenterScreen;

            // DataGridView basic config
            dgvAreas.BackgroundColor = Color.White;
            dgvAreas.BorderStyle = BorderStyle.Fixed3D;
            dgvAreas.ReadOnly = true;
            dgvAreas.AllowUserToAddRows = false;
            dgvAreas.AllowUserToDeleteRows = false;
            dgvAreas.MultiSelect = false;
            dgvAreas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAreas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAreas.RowTemplate.Height = 28;
            dgvAreas.Dock = DockStyle.Fill;
            dgvAreas.RowHeadersVisible = false;

            // Buttons basic size
            foreach (var btn in new[] { btnAdd, btnUpdate, btnDelete, btnSearch, btnClearSearch, btnNext, btnPrev })
            {
                btn.Height = 32;
            }
            // Inputs
            txtSearch.Height = 28;
            txtAreaName.Height = 28;
        }

        private void BuildBasicLayout()
        {
            // Create layout container: Top (auto), Middle grid (fill), Bottom (auto)
            layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
            };
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            // Top panel for search
            topPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoSize = true,
                Padding = new Padding(10),
            };

            // Move existing search controls to top panel
            topPanel.Controls.Add(lblSearch);
            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(btnSearch);
            topPanel.Controls.Add(btnClearSearch);

            // Bottom panel for actions and paging
            bottomPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoSize = true,
                Padding = new Padding(10),
            };

            lblPageInfo = new Label
            {
                AutoSize = true,
                Margin = new Padding(10, 8, 10, 0),
            };

            // Move action/info controls to bottom panel
            bottomPanel.Controls.Add(label1);
            bottomPanel.Controls.Add(lblAreaNameValue);
            bottomPanel.Controls.Add(btnAdd);
            bottomPanel.Controls.Add(btnUpdate);
            bottomPanel.Controls.Add(btnDelete);
            bottomPanel.Controls.Add(btnPrev);
            bottomPanel.Controls.Add(btnNext);
            bottomPanel.Controls.Add(lblPageInfo);

            // Clear current root Controls order and rebuild
            var grid = dgvAreas;
            this.Controls.Remove(dgvAreas);
            this.Controls.Remove(lblSearch);
            this.Controls.Remove(txtSearch);
            this.Controls.Remove(btnSearch);
            this.Controls.Remove(btnClearSearch);
            this.Controls.Remove(label1);
            this.Controls.Remove(lblAreaNameValue);
            this.Controls.Remove(btnAdd);
            this.Controls.Remove(btnUpdate);
            this.Controls.Remove(btnDelete);
            this.Controls.Remove(btnPrev);
            this.Controls.Remove(btnNext);

            layout.Controls.Add(topPanel, 0, 0);
            layout.Controls.Add(grid, 0, 1);
            layout.Controls.Add(bottomPanel, 0, 2);
            this.Controls.Add(layout);
        }

        private void LoadAreas()
        {
            try
            {
                dtAreas = DatabaseHelper.ExecuteProcedure("sp_LayTatCaKhuVuc");
                if (dtAreas != null)
                {
                    dgvAreas.DataSource = GetPagedData(dtAreas, currentPage);
                    SetupColumnHeaders();
                    UpdatePagingInfo();
                }
                else
                {
                    dgvAreas.DataSource = null;
                    MessageBox.Show("Không thể tải dữ liệu khu vực. Vui lòng kiểm tra kết nối database.", 
                                   "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu khu vực: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgvAreas.DataSource = null;
            }
        }

        private void SetupColumnHeaders()
        {
            try
            {
                if (dgvAreas?.Columns != null && dgvAreas.Columns.Count > 0)
                {
                    var colMa = dgvAreas.Columns["MaKhuVuc"];
                    if (colMa != null)
                    {
                        colMa.HeaderText = "Mã Khu Vực";
                        colMa.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        colMa.Width = 100;
                    }
                    
                    var colTen = dgvAreas.Columns["TenKhuVuc"];
                    if (colTen != null)
                    {
                        colTen.HeaderText = "Tên Khu Vực";
                        colTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cấu hình column headers: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (dtAreas != null && currentPage * pageSize < dtAreas.Rows.Count)
            {
                currentPage++;
                dgvAreas.DataSource = GetPagedData(dtAreas, currentPage);
                UpdatePagingInfo();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                dgvAreas.DataSource = GetPagedData(dtAreas, currentPage);
                UpdatePagingInfo();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new AreaEditForm())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadAreas();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvAreas.SelectedRows.Count > 0)
            {
                int areaID = Convert.ToInt32(dgvAreas.SelectedRows[0].Cells["MaKhuVuc"].Value);
                using (var editForm = new AreaEditForm(areaID))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadAreas();
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
            if (dgvAreas.SelectedRows.Count > 0)
            {
                string name = dgvAreas.SelectedRows[0].Cells["TenKhuVuc"].Value?.ToString() ?? "";
                var confirm = MessageBox.Show($"Bạn có chắc muốn xóa Khu Vực: '{name}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                int areaID = Convert.ToInt32(dgvAreas.SelectedRows[0].Cells["MaKhuVuc"].Value);
                SqlParameter[] parameters = { new SqlParameter("@MaKhuVuc", areaID) };
                DatabaseHelper.ExecuteNonQuery("sp_XoaKhuVuc", parameters);
                LoadAreas();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.", "Chưa Chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvAreas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAreas.SelectedRows.Count > 0)
            {
                lblAreaNameValue.Text = dgvAreas.SelectedRows[0].Cells["TenKhuVuc"].Value?.ToString() ?? string.Empty;
            }
            else
            {
                lblAreaNameValue.Text = string.Empty;
            }
        }

        // ============================================
        // CÁC PHƯƠNG THỨC TÌM KIẾM
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
            LoadAreas(); // Load lại tất cả dữ liệu
            currentPage = 1;
        }

        private void PerformSearch()
        {
            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadAreas(); // Nếu không có từ khóa tìm kiếm, load tất cả
                return;
            }

            try
            {
                dtAreas = DatabaseHelper.SearchAreaByName(searchTerm);
                dgvAreas.DataSource = GetPagedData(dtAreas, 1); // Reset về trang đầu
                SetupColumnHeaders();
                currentPage = 1;
                UpdatePagingInfo();

                if (dtAreas.Rows.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy khu vực nào với từ khóa '{searchTerm}'.", 
                                    "Không Tìm Thấy", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdatePagingInfo()
        {
            if (dtAreas == null)
            {
                if (lblPageInfo != null) lblPageInfo.Text = "";
                btnPrev.Enabled = btnNext.Enabled = false;
                return;
            }
            int total = dtAreas.Rows.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
            if (lblPageInfo != null)
            {
                int start = total == 0 ? 0 : (currentPage - 1) * pageSize + 1;
                int end = Math.Min(currentPage * pageSize, total);
                lblPageInfo.Text = $"Hiển thị {start}-{end}/{total} (Trang {currentPage}/{totalPages})";
            }
            btnPrev.Enabled = currentPage > 1;
            btnNext.Enabled = currentPage < totalPages;
        }
    }
}