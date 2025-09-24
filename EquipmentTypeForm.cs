// File: EquipmentTypeForm.cs

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class EquipmentTypeForm : Form
    {
        private DataTable dtTypes;
        private int currentPage = 1;
        private const int pageSize = 20;

        public EquipmentTypeForm()
        {
            InitializeComponent();
            LoadTypes();
        }

        private void LoadTypes()
        {
            dtTypes = DatabaseHelper.ExecuteProcedure("sp_LayTatCaLoaiCoSoVatChat");
            dgvTypes.DataSource = GetPagedData(dtTypes, currentPage);
            SetupColumnHeaders();
        }

        private void SetupColumnHeaders()
        {
            if (dgvTypes.Columns.Count > 0)
            {
                var colMa = dgvTypes.Columns["MaLoai"];
                if (colMa != null)
                {
                    colMa.HeaderText = "Mã Loại";
                    colMa.Width = 80;
                }
                
                var colTen = dgvTypes.Columns["TenLoai"];
                if (colTen != null)
                {
                    colTen.HeaderText = "Tên Loại";
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
            if (currentPage * pageSize < dtTypes.Rows.Count)
            {
                currentPage++;
                dgvTypes.DataSource = GetPagedData(dtTypes, currentPage);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                dgvTypes.DataSource = GetPagedData(dtTypes, currentPage);
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
                int typeID = Convert.ToInt32(dgvTypes.SelectedRows[0].Cells["MaLoai"].Value);
                SqlParameter[] parameters = { new SqlParameter("@MaLoai", typeID) };
                DatabaseHelper.ExecuteNonQuery("sp_XoaLoaiCoSoVatChat", parameters);
                LoadTypes();
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
    }
}