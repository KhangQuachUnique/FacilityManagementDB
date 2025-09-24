// File: ReportsForm.cs

using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class ReportsForm : Form
    {
        public ReportsForm()
        {
            InitializeComponent();
            LoadAreas();
            LoadTypes();
        }

        private void LoadAreas()
        {
            var dtAreas = DatabaseHelper.ExecuteProcedure("sp_GetAllAreas");
            cmbArea.DataSource = dtAreas;
            cmbArea.DisplayMember = "AreaName";
            cmbArea.ValueMember = "AreaID";
        }

        private void LoadTypes()
        {
            var dtTypes = DatabaseHelper.ExecuteProcedure("sp_GetAllEquipmentTypes");
            cmbType.DataSource = dtTypes;
            cmbType.DisplayMember = "TypeName";
            cmbType.ValueMember = "TypeID";
        }

        private void btnMaintenanceCost_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@Month", numMonth.Value),
                new SqlParameter("@Year", numYear.Value)
            };
            object result = DatabaseHelper.ExecuteScalar("sp_ReportMaintenanceCostByMonthYear", parameters);
            lblMaintenanceCost.Text = "Total Maintenance Cost: " + (result ?? "0").ToString();
        }

        private void btnValueByArea_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@AreaID", cmbArea.SelectedValue)
            };
            object result = DatabaseHelper.ExecuteScalar("sp_ReportEquipmentValueByArea", parameters);
            lblValueByArea.Text = "Total Value by Area: " + (result ?? "0").ToString();
        }

        private void btnValueByType_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@TypeID", cmbType.SelectedValue)
            };
            object result = DatabaseHelper.ExecuteScalar("sp_ReportEquipmentValueByType", parameters);
            lblValueByType.Text = "Total Value by Type: " + (result ?? "0").ToString();
        }

        private void btnNeedingMaintenance_Click(object sender, EventArgs e)
        {
            SqlParameter[] parameters = {
                new SqlParameter("@DaysThreshold", numDays.Value)
            };
            DataTable dt = DatabaseHelper.ExecuteProcedure("sp_ReportEquipmentNeedingMaintenance", parameters);
            dgvNeedingMaintenance.DataSource = dt;
        }
    }
}