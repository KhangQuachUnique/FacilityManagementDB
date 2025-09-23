// File: EquipmentTypeForm.cs

using System;
using System.Data;
using System.Data.SqlClient;
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
            dtTypes = DatabaseHelper.ExecuteProcedure("sp_GetAllEquipmentTypes");
            dgvTypes.DataSource = GetPagedData(dtTypes, currentPage);
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
            SqlParameter[] parameters = {
                new SqlParameter("@TypeName", txtTypeName.Text)
            };
            DatabaseHelper.ExecuteNonQuery("sp_InsertEquipmentType", parameters);
            LoadTypes();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvTypes.SelectedRows.Count > 0)
            {
                int typeID = Convert.ToInt32(dgvTypes.SelectedRows[0].Cells["TypeID"].Value);
                SqlParameter[] parameters = {
                    new SqlParameter("@TypeID", typeID),
                    new SqlParameter("@TypeName", txtTypeName.Text)
                };
                DatabaseHelper.ExecuteNonQuery("sp_UpdateEquipmentType", parameters);
                LoadTypes();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvTypes.SelectedRows.Count > 0)
            {
                int typeID = Convert.ToInt32(dgvTypes.SelectedRows[0].Cells["TypeID"].Value);
                SqlParameter[] parameters = { new SqlParameter("@TypeID", typeID) };
                DatabaseHelper.ExecuteNonQuery("sp_DeleteEquipmentType", parameters);
                LoadTypes();
            }
        }

        private void dgvTypes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTypes.SelectedRows.Count > 0)
            {
                txtTypeName.Text = dgvTypes.SelectedRows[0].Cells["TypeName"].Value.ToString();
            }
        }
    }
}