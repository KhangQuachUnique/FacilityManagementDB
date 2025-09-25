// File: AreaForm.cs

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class AreaForm : Form
    {
        private DataTable? dtAreas;
        private int currentPage = 1;
        private const int pageSize = 50;

        public AreaForm()
        {
            InitializeComponent();
            ConfigureUI();
            LoadAreas();
        }
        
        private void ConfigureUI()
        {
            UIHelper.ConfigureForm(this);
            UIHelper.ConfigureDataGridView(dgvAreas);
            UIHelper.ConfigureButton(btnAdd, true);
            UIHelper.ConfigureButton(btnUpdate);
            UIHelper.ConfigureButton(btnDelete);
            UIHelper.ConfigureButton(btnSearch);
            UIHelper.ConfigureButton(btnClearSearch);
            UIHelper.ConfigureButton(btnNext);
            UIHelper.ConfigureButton(btnPrev);
            UIHelper.ConfigureTextBox(txtAreaName);
            UIHelper.ConfigureTextBox(txtSearch);
            UIHelper.ConfigureLabel(label1, true);
            UIHelper.ConfigureLabel(lblSearch);
            UIHelper.ConfigureLabel(lblAreaNameValue);
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
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                dgvAreas.DataSource = GetPagedData(dtAreas, currentPage);
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
                int areaID = Convert.ToInt32(dgvAreas.SelectedRows[0].Cells["MaKhuVuc"].Value);
                SqlParameter[] parameters = { new SqlParameter("@MaKhuVuc", areaID) };
                DatabaseHelper.ExecuteNonQuery("sp_XoaKhuVuc", parameters);
                LoadAreas();
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
    }
}