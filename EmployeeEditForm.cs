using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class EmployeeEditForm : Form
    {
        private int? employeeID;

        public EmployeeEditForm(int? employeeID = null)
        {
            InitializeComponent();
            this.employeeID = employeeID;
            LoadAreas();
            if (employeeID.HasValue)
            {
                LoadEmployeeData(employeeID.Value);
                this.Text = "Edit Employee";
                btnSave.Text = "Update";
            }
            else
            {
                this.Text = "Add Employee";
                btnSave.Text = "Add";
            }
        }

        private void LoadAreas()
        {
            var dtAreas = DatabaseHelper.ExecuteProcedure("sp_GetAllAreas");
            cmbArea.DataSource = dtAreas;
            cmbArea.DisplayMember = "AreaName";
            cmbArea.ValueMember = "AreaID";
            cmbArea.SelectedIndex = -1;
        }

        private void LoadEmployeeData(int employeeID)
        {
            SqlParameter[] parameters = { new SqlParameter("@EmployeeID", employeeID) };
            var dt = DatabaseHelper.ExecuteProcedure("sp_GetEmployeeByID", parameters);
            if (dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                txtName.Text = row["Name"].ToString();
                txtPosition.Text = row["Position"].ToString();
                if (row["AreaID"] != DBNull.Value)
                {
                    cmbArea.SelectedValue = row["AreaID"];
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                MessageBox.Show("Please enter a position.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SqlParameter[] parameters = {
                new SqlParameter("@Name", txtName.Text),
                new SqlParameter("@Position", txtPosition.Text),
                new SqlParameter("@AreaID", cmbArea.SelectedValue ?? (object)DBNull.Value)
            };

            if (employeeID.HasValue)
            {
                Array.Resize(ref parameters, parameters.Length + 1);
                parameters[^1] = new SqlParameter("@EmployeeID", employeeID.Value);
                DatabaseHelper.ExecuteNonQuery("sp_UpdateEmployee", parameters);
            }
            else
            {
                DatabaseHelper.ExecuteNonQuery("sp_InsertEmployee", parameters);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
