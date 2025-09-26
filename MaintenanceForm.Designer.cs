namespace FacilityManagementSystem
{
    partial class MaintenanceForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvMaintenance = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cmbEquipment = new System.Windows.Forms.ComboBox();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.numCost = new System.Windows.Forms.NumericUpDown();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblEquipmentValue = new System.Windows.Forms.Label();
            this.lblEmployeeValue = new System.Windows.Forms.Label();
            this.lblDateValue = new System.Windows.Forms.Label();
            this.lblCostValue = new System.Windows.Forms.Label();
            this.lblDescriptionValue = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbFilterEquipment = new System.Windows.Forms.ComboBox();
            this.cmbFilterEmployee = new System.Windows.Forms.ComboBox();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.btnFilter = new System.Windows.Forms.Button();
            this.btnResetFilter = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCost)).BeginInit();
            this.SuspendLayout();

            this.dgvMaintenance.Name = "dgvMaintenance";
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Nhập tên thiết bị hoặc nhân viên để tìm kiếm...";
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.Name = "btnSearch";
            this.btnClearSearch.Text = "Hủy tìm kiếm";
            this.btnClearSearch.Name = "btnClearSearch";
            this.lblSearch.AutoSize = true;
            this.lblSearch.Text = "Tìm kiếm:";
            this.lblSearch.Margin = new System.Windows.Forms.Padding(5, 8, 5, 0);
            this.cmbEquipment.Name = "cmbEquipment";
            this.cmbEquipment.Visible = false;
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Visible = false;
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Visible = false;
            this.numCost.Name = "numCost";
            this.numCost.Visible = false;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Visible = false;
            this.lblEquipmentValue.Name = "lblEquipmentValue";
            this.lblEquipmentValue.Visible = false;
            this.lblEmployeeValue.Name = "lblEmployeeValue";
            this.lblEmployeeValue.Visible = false;
            this.lblDateValue.Name = "lblDateValue";
            this.lblDateValue.Visible = false;
            this.lblCostValue.Name = "lblCostValue";
            this.lblCostValue.Visible = false;
            this.lblDescriptionValue.Name = "lblDescriptionValue";
            this.lblDescriptionValue.Visible = false;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Name = "btnAdd";
            this.btnUpdate.Text = "Cập Nhật";
            this.btnUpdate.Name = "btnUpdate";
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Name = "btnDelete";
            this.btnNext.Text = "Tiếp";
            this.btnNext.Name = "btnNext";
            this.btnPrev.Text = "Trước";
            this.btnPrev.Name = "btnPrev";
            this.label1.AutoSize = true;
            this.label1.Name = "label1";
            this.label1.Visible = false;
            this.label2.AutoSize = true;
            this.label2.Name = "label2";
            this.label2.Visible = false;
            this.label3.AutoSize = true;
            this.label3.Name = "label3";
            this.label3.Visible = false;
            this.label4.AutoSize = true;
            this.label4.Name = "label4";
            this.label4.Visible = false;
            this.label5.AutoSize = true;
            this.label5.Name = "label5";
            this.label5.Visible = false;
            this.cmbFilterEquipment.Name = "cmbFilterEquipment";
            this.cmbFilterEmployee.Name = "cmbFilterEmployee";
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Format = DateTimePickerFormat.Short;
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Format = DateTimePickerFormat.Short;
            this.btnFilter.Text = "Lọc";
            this.btnFilter.Name = "btnFilter";
            this.btnResetFilter.Text = "Đặt Lại";
            this.btnResetFilter.Name = "btnResetFilter";
            this.label6.AutoSize = true;
            this.label6.Name = "label6";
            this.label6.Visible = false;
            this.label7.AutoSize = true;
            this.label7.Name = "label7";
            this.label7.Visible = false;
            this.label8.AutoSize = true;
            this.label8.Name = "label8";
            this.label8.Visible = false;
            this.label9.AutoSize = true;
            this.label9.Name = "label9";
            this.label9.Visible = false;

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Name = "MaintenanceForm";
            this.Text = "Quản Lý Bảo Trì";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCost)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView dgvMaintenance;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox cmbEquipment;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.NumericUpDown numCost;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblEquipmentValue;
        private System.Windows.Forms.Label lblEmployeeValue;
        private System.Windows.Forms.Label lblDateValue;
        private System.Windows.Forms.Label lblCostValue;
        private System.Windows.Forms.Label lblDescriptionValue;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbFilterEquipment;
        private System.Windows.Forms.ComboBox cmbFilterEmployee;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}