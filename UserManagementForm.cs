// File: UserManagementForm.cs

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class UserManagementForm : Form
    {
        private DataTable dtUsers;
        private int currentPage = 1;
        private const int pageSize = 20;

        public UserManagementForm()
        {
            InitializeComponent();
            LoadUsers();
            LoadRoles();
        }

        private void LoadUsers()
        {
            dtUsers = DatabaseHelper.ExecuteProcedure("sp_GetAllUsers");
            dgvUsers.DataSource = GetPagedData(dtUsers, currentPage);
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
            if (currentPage * pageSize < dtUsers.Rows.Count)
            {
                currentPage++;
                dgvUsers.DataSource = GetPagedData(dtUsers, currentPage);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                dgvUsers.DataSource = GetPagedData(dtUsers, currentPage);
            }
        }

        private void LoadRoles()
        {
            var dtRoles = DatabaseHelper.ExecuteProcedure("sp_GetAllRoles");
            cmbRole.DataSource = dtRoles;
            cmbRole.DisplayMember = "RoleName";
            cmbRole.ValueMember = "RoleID";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Username", txtUsername.Text),
                new SqlParameter("@PasswordHash", txtPassword.Text), // Hash in real app
                new SqlParameter("@RoleID", cmbRole.SelectedValue),
                new SqlParameter("@IsActive", chkActive.Checked ? 1 : 0)
            };
            DatabaseHelper.ExecuteNonQuery("sp_InsertUser", parameters);
            LoadUsers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                int userID = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);
                SqlParameter[] parameters = {
                    new SqlParameter("@UserID", userID),
                    new SqlParameter("@Username", txtUsername.Text),
                    new SqlParameter("@PasswordHash", txtPassword.Text),
                    new SqlParameter("@RoleID", cmbRole.SelectedValue),
                    new SqlParameter("@IsActive", chkActive.Checked ? 1 : 0)
                };
                DatabaseHelper.ExecuteNonQuery("sp_UpdateUser", parameters);
                LoadUsers();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                int userID = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["UserID"].Value);
                SqlParameter[] parameters = { new SqlParameter("@UserID", userID) };
                DatabaseHelper.ExecuteNonQuery("sp_DeleteUser", parameters);
                LoadUsers();
            }
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count > 0)
            {
                txtUsername.Text = dgvUsers.SelectedRows[0].Cells["Username"].Value.ToString();
                txtPassword.Text = dgvUsers.SelectedRows[0].Cells["PasswordHash"].Value.ToString();
                cmbRole.SelectedValue = dgvUsers.SelectedRows[0].Cells["RoleID"].Value;
                chkActive.Checked = Convert.ToBoolean(dgvUsers.SelectedRows[0].Cells["IsActive"].Value);
            }
        }
    }
}