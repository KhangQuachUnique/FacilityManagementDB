// File: AreaForm.cs

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class AreaForm : Form
    {
        private DataTable dtAreas;
        private int currentPage = 1;
        private const int pageSize = 50;

        public AreaForm()
        {
            InitializeComponent();
            LoadAreas();
        }

        private void LoadAreas()
        {
            dtAreas = DatabaseHelper.ExecuteProcedure("sp_LayTatCaKhuVuc");
            dgvAreas.DataSource = GetPagedData(dtAreas, currentPage);
            SetupColumnHeaders();
        }

        private void SetupColumnHeaders()
        {
            if (dgvAreas.Columns.Count > 0)
            {
                var colMa = dgvAreas.Columns["MaKhuVuc"];
                if (colMa != null)
                {
                    colMa.HeaderText = "Mã Khu Vực";
                    colMa.Width = 80;
                }
                
                var colTen = dgvAreas.Columns["TenKhuVuc"];
                if (colTen != null)
                {
                    colTen.HeaderText = "Tên Khu Vực";
                    colTen.Width = 250;
                }
            }
        }

        private DataTable GetPagedData(DataTable dt, int page)
        {
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
            if (currentPage * pageSize < dtAreas.Rows.Count)
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
    }
}