// File: RoleManagementForm.cs

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class RoleManagementForm : Form
    {
        private DataTable dtRoles;
        private int currentPage = 1;
        private const int pageSize = 20;

        public RoleManagementForm()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void LoadRoles()
        {
            dtRoles = DatabaseHelper.ExecuteProcedure("sp_GetAllRoles");
            dgvRoles.DataSource = GetPagedData(dtRoles, currentPage);
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
            if (currentPage * pageSize < dtRoles.Rows.Count)
            {
                currentPage++;
                dgvRoles.DataSource = GetPagedData(dtRoles, currentPage);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                dgvRoles.DataSource = GetPagedData(dtRoles, currentPage);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@RoleName", txtRoleName.Text)
            };
            DatabaseHelper.ExecuteNonQuery("sp_InsertRole", parameters);
            LoadRoles();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count > 0)
            {
                int roleID = Convert.ToInt32(dgvRoles.SelectedRows[0].Cells["RoleID"].Value);
                SqlParameter[] parameters = {
                    new SqlParameter("@RoleID", roleID),
                    new SqlParameter("@RoleName", txtRoleName.Text)
                };
                DatabaseHelper.ExecuteNonQuery("sp_UpdateRole", parameters);
                LoadRoles();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count > 0)
            {
                int roleID = Convert.ToInt32(dgvRoles.SelectedRows[0].Cells["RoleID"].Value);
                SqlParameter[] parameters = { new SqlParameter("@RoleID", roleID) };
                DatabaseHelper.ExecuteNonQuery("sp_DeleteRole", parameters);
                LoadRoles();
            }
        }

        private void dgvRoles_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count > 0)
            {
                txtRoleName.Text = dgvRoles.SelectedRows[0].Cells["RoleName"].Value.ToString();
            }
        }
    }
}