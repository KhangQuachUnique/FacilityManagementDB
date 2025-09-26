// File: EquipmentTypeForm.cs

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace FacilityManagementSystem
{
    public partial class EquipmentTypeForm : Form
    {
        private DataTable? dtTypes;
        private int currentPage = 1;
        private const int pageSize = 15;

        private TableLayoutPanel? layout;
        private FlowLayoutPanel? topPanel;
        private FlowLayoutPanel? bottomPanel;
        private Label? lblPageInfo;

        public EquipmentTypeForm()
        {
            InitializeComponent();
            ConfigureUI();
            BuildBasicLayout();
            LoadTypes();
        }

        private void ConfigureUI()
        {
            this.BackColor = Color.WhiteSmoke;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.StartPosition = FormStartPosition.CenterScreen;

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

            foreach (var btn in new[] { btnAdd, btnUpdate, btnDelete, btnSearch, btnClearSearch, btnNext, btnPrev })
            {
                btn.Height = 32;
            }
            txtSearch.Height = 28;
            txtTypeName.Height = 28;
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

            bottomPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoSize = true,
                Padding = new Padding(10),
            };
            lblPageInfo = new Label { AutoSize = true, Margin = new Padding(10, 8, 10, 0) };
            bottomPanel.Controls.Add(label1);
            bottomPanel.Controls.Add(lblTypeNameValue);
            bottomPanel.Controls.Add(btnAdd);
            bottomPanel.Controls.Add(btnUpdate);
            bottomPanel.Controls.Add(btnDelete);
            bottomPanel.Controls.Add(btnPrev);
            bottomPanel.Controls.Add(btnNext);
            bottomPanel.Controls.Add(lblPageInfo);

            var grid = dgvTypes;
            this.Controls.Remove(dgvTypes);
            this.Controls.Remove(lblSearch);
            this.Controls.Remove(txtSearch);
            this.Controls.Remove(btnSearch);
            this.Controls.Remove(btnClearSearch);
            this.Controls.Remove(label1);
            this.Controls.Remove(lblTypeNameValue);
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

        private void LoadTypes()
        {
            dtTypes = DatabaseHelper.ExecuteProcedure("sp_LayTatCaLoaiCoSoVatChat");
            dgvTypes.DataSource = GetPagedData(dtTypes, currentPage);
            SetupColumnHeaders();
            UpdatePagingInfo();
        }

        private void SetupColumnHeaders()
        {
            try
            {
                if (dgvTypes.Columns.Count > 0)
                {
                    var colMa = dgvTypes.Columns["MaLoai"];
                    if (colMa != null)
                    {
                        colMa.HeaderText = "Mã Loại";
                        colMa.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                    }
                    
                    var colTen = dgvTypes.Columns["TenLoai"];
                    if (colTen != null)
                    {
                        colTen.HeaderText = "Tên Loại";
                        colTen.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cấu hình cột: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dtTypes != null && currentPage * pageSize < dtTypes.Rows.Count)
            {
                currentPage++;
                dgvTypes.DataSource = GetPagedData(dtTypes, currentPage);
                UpdatePagingInfo();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                dgvTypes.DataSource = GetPagedData(dtTypes, currentPage);
                UpdatePagingInfo();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new EquipmentTypeEditForm())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadTypes();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvTypes.SelectedRows.Count > 0)
            {
                int typeID = Convert.ToInt32(dgvTypes.SelectedRows[0].Cells["MaLoai"].Value);
                using (var editForm = new EquipmentTypeEditForm(typeID))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadTypes();
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
            if (dgvTypes.SelectedRows.Count > 0)
            {
                string name = dgvTypes.SelectedRows[0].Cells["TenLoai"].Value?.ToString() ?? "";
                var confirm = MessageBox.Show($"Bạn có chắc muốn xóa Loại: '{name}'?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                int typeID = Convert.ToInt32(dgvTypes.SelectedRows[0].Cells["MaLoai"].Value);
                SqlParameter[] parameters = { new SqlParameter("@MaLoai", typeID) };
                DatabaseHelper.ExecuteNonQuery("sp_XoaLoaiCoSoVatChat", parameters);
                LoadTypes();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hàng để xóa.", "Chưa Chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvTypes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTypes.SelectedRows.Count > 0)
            {
                lblTypeNameValue.Text = dgvTypes.SelectedRows[0].Cells["TenLoai"].Value?.ToString() ?? string.Empty;
            }
            else
            {
                lblTypeNameValue.Text = string.Empty;
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
            LoadTypes(); // Load lại tất cả dữ liệu
            currentPage = 1;
        }

        private void PerformSearch()
        {
            string searchTerm = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchTerm))
            {
                LoadTypes(); // Nếu không có từ khóa tìm kiếm, load tất cả
                return;
            }

            try
            {
                dtTypes = DatabaseHelper.SearchEquipmentTypeByName(searchTerm);
                dgvTypes.DataSource = GetPagedData(dtTypes, 1); // Reset về trang đầu
                SetupColumnHeaders();
                currentPage = 1;
                UpdatePagingInfo();

                if (dtTypes.Rows.Count == 0)
                {
                    MessageBox.Show($"Không tìm thấy loại cơ sở vật chất nào với từ khóa '{searchTerm}'.", 
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
            if (dtTypes == null)
            {
                if (lblPageInfo != null) lblPageInfo.Text = string.Empty;
                btnPrev.Enabled = btnNext.Enabled = false;
                return;
            }
            int total = dtTypes.Rows.Count;
            int totalPages = Math.Max(1, (int)Math.Ceiling(total / (double)pageSize));
            int start = total == 0 ? 0 : (currentPage - 1) * pageSize + 1;
            int end = Math.Min(currentPage * pageSize, total);
            if (lblPageInfo != null)
                lblPageInfo.Text = $"Hiển thị {start}-{end}/{total} (Trang {currentPage}/{totalPages})";

            btnPrev.Enabled = currentPage > 1;
            btnNext.Enabled = currentPage < totalPages;
        }
    }
}