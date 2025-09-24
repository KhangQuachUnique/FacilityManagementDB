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
        private const int pageSize = 20;

        public AreaForm()
        {
            InitializeComponent();
            LoadAreas();
        }

        private void LoadAreas()
        {
            dtAreas = DatabaseHelper.ExecuteProcedure("sp_GetAllAreas");
            dgvAreas.DataSource = GetPagedData(dtAreas, currentPage);
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
            SqlParameter[] parameters = {
                new SqlParameter("@AreaName", txtAreaName.Text)
            };
            DatabaseHelper.ExecuteNonQuery("sp_InsertArea", parameters);
            LoadAreas();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvAreas.SelectedRows.Count > 0)
            {
                int areaID = Convert.ToInt32(dgvAreas.SelectedRows[0].Cells["AreaID"].Value);
                SqlParameter[] parameters = {
                    new SqlParameter("@AreaID", areaID),
                    new SqlParameter("@AreaName", txtAreaName.Text)
                };
                DatabaseHelper.ExecuteNonQuery("sp_UpdateArea", parameters);
                LoadAreas();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAreas.SelectedRows.Count > 0)
            {
                int areaID = Convert.ToInt32(dgvAreas.SelectedRows[0].Cells["AreaID"].Value);
                SqlParameter[] parameters = { new SqlParameter("@AreaID", areaID) };
                DatabaseHelper.ExecuteNonQuery("sp_DeleteArea", parameters);
                LoadAreas();
            }
        }

        private void dgvAreas_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAreas.SelectedRows.Count > 0)
            {
                txtAreaName.Text = dgvAreas.SelectedRows[0].Cells["AreaName"].Value.ToString();
            }
        }
    }
}