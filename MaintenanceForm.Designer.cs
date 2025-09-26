// File: MaintenanceForm.Designer.cs

namespace FacilityManagementSystem
{
    partial class MaintenanceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvMaintenance = new DataGridView();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnClearSearch = new Button();
            lblSearch = new Label();
            cmbEquipment = new ComboBox();
            cmbEmployee = new ComboBox();
            dtpDate = new DateTimePicker();
            numCost = new NumericUpDown();
            txtDescription = new TextBox();
            lblEquipmentValue = new Label();
            lblEmployeeValue = new Label();
            lblDateValue = new Label();
            lblCostValue = new Label();
            lblDescriptionValue = new Label();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnNext = new Button();
            btnPrev = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            cmbFilterEquipment = new ComboBox();
            cmbFilterEmployee = new ComboBox();
            dtpStart = new DateTimePicker();
            dtpEnd = new DateTimePicker();
            btnFilter = new Button();
            btnResetFilter = new Button();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvMaintenance).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCost).BeginInit();
            SuspendLayout();
            // 
            // dgvMaintenance
            // 
            dgvMaintenance.BackgroundColor = SystemColors.Control;
            dgvMaintenance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMaintenance.Location = new Point(16, 53);
            dgvMaintenance.Margin = new Padding(4, 5, 4, 5);
            dgvMaintenance.Name = "dgvMaintenance";
            dgvMaintenance.RowHeadersWidth = 51;
            dgvMaintenance.Size = new Size(1013, 273);
            dgvMaintenance.TabIndex = 0;
            dgvMaintenance.SelectionChanged += dgvMaintenance_SelectionChanged;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(101, 15);
            txtSearch.Margin = new Padding(4, 5, 4, 5);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Nhập tên thiết bị hoặc nhân viên để tìm kiếm...";
            txtSearch.Size = new Size(300, 27);
            txtSearch.TabIndex = 26;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(409, 14);
            btnSearch.Margin = new Padding(4, 5, 4, 5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(80, 29);
            btnSearch.TabIndex = 27;
            btnSearch.Text = "Tìm kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnClearSearch
            // 
            btnClearSearch.Location = new Point(497, 14);
            btnClearSearch.Margin = new Padding(4, 5, 4, 5);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Size = new Size(100, 29);
            btnClearSearch.TabIndex = 28;
            btnClearSearch.Text = "Hủy tìm kiếm";
            btnClearSearch.UseVisualStyleBackColor = true;
            btnClearSearch.Click += btnClearSearch_Click;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Location = new Point(16, 18);
            lblSearch.Margin = new Padding(4, 0, 4, 0);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(73, 20);
            lblSearch.TabIndex = 25;
            lblSearch.Text = "Tìm kiếm:";
            // 
            // cmbEquipment
            // 
            cmbEquipment.FormattingEnabled = true;
            cmbEquipment.Location = new Point(133, 373);
            cmbEquipment.Margin = new Padding(4, 5, 4, 5);
            cmbEquipment.Name = "cmbEquipment";
            cmbEquipment.Size = new Size(199, 28);
            cmbEquipment.TabIndex = 1;
            cmbEquipment.Visible = false;
            // 
            // cmbEmployee
            // 
            cmbEmployee.FormattingEnabled = true;
            cmbEmployee.Location = new Point(133, 415);
            cmbEmployee.Margin = new Padding(4, 5, 4, 5);
            cmbEmployee.Name = "cmbEmployee";
            cmbEmployee.Size = new Size(199, 28);
            cmbEmployee.TabIndex = 2;
            cmbEmployee.Visible = false;
            // 
            // dtpDate
            // 
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(133, 457);
            dtpDate.Margin = new Padding(4, 5, 4, 5);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(199, 27);
            dtpDate.TabIndex = 3;
            dtpDate.Visible = false;
            // 
            // numCost
            // 
            numCost.DecimalPlaces = 2;
            numCost.Location = new Point(133, 497);
            numCost.Margin = new Padding(4, 5, 4, 5);
            numCost.Maximum = new decimal(new int[] { -1486618624, 232830643, 0, 0 });
            numCost.Name = "numCost";
            numCost.Size = new Size(200, 27);
            numCost.TabIndex = 4;
            numCost.Visible = false;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(133, 537);
            txtDescription.Margin = new Padding(4, 5, 4, 5);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(199, 90);
            txtDescription.TabIndex = 5;
            txtDescription.Visible = false;
            // 
            // lblEquipmentValue
            // 
            lblEquipmentValue.BorderStyle = BorderStyle.FixedSingle;
            lblEquipmentValue.Location = new Point(133, 373);
            lblEquipmentValue.Margin = new Padding(4, 0, 4, 0);
            lblEquipmentValue.Name = "lblEquipmentValue";
            lblEquipmentValue.Size = new Size(199, 30);
            lblEquipmentValue.TabIndex = 25;
            // 
            // lblEmployeeValue
            // 
            lblEmployeeValue.BorderStyle = BorderStyle.FixedSingle;
            lblEmployeeValue.Location = new Point(133, 415);
            lblEmployeeValue.Margin = new Padding(4, 0, 4, 0);
            lblEmployeeValue.Name = "lblEmployeeValue";
            lblEmployeeValue.Size = new Size(199, 30);
            lblEmployeeValue.TabIndex = 26;
            // 
            // lblDateValue
            // 
            lblDateValue.BorderStyle = BorderStyle.FixedSingle;
            lblDateValue.Location = new Point(133, 457);
            lblDateValue.Margin = new Padding(4, 0, 4, 0);
            lblDateValue.Name = "lblDateValue";
            lblDateValue.Size = new Size(199, 30);
            lblDateValue.TabIndex = 27;
            // 
            // lblCostValue
            // 
            lblCostValue.BorderStyle = BorderStyle.FixedSingle;
            lblCostValue.Location = new Point(133, 497);
            lblCostValue.Margin = new Padding(4, 0, 4, 0);
            lblCostValue.Name = "lblCostValue";
            lblCostValue.Size = new Size(199, 30);
            lblCostValue.TabIndex = 28;
            // 
            // lblDescriptionValue
            // 
            lblDescriptionValue.BorderStyle = BorderStyle.FixedSingle;
            lblDescriptionValue.Location = new Point(133, 537);
            lblDescriptionValue.Margin = new Padding(4, 0, 4, 0);
            lblDescriptionValue.Name = "lblDescriptionValue";
            lblDescriptionValue.Size = new Size(199, 91);
            lblDescriptionValue.TabIndex = 29;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(400, 373);
            btnAdd.Margin = new Padding(4, 5, 4, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 35);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(400, 418);
            btnUpdate.Margin = new Padding(4, 5, 4, 5);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(100, 35);
            btnUpdate.TabIndex = 7;
            btnUpdate.Text = "Cập Nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(400, 463);
            btnDelete.Margin = new Padding(4, 5, 4, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 35);
            btnDelete.TabIndex = 8;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(800, 373);
            btnNext.Margin = new Padding(4, 5, 4, 5);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(100, 35);
            btnNext.TabIndex = 9;
            btnNext.Text = "Tiếp";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(667, 373);
            btnPrev.Margin = new Padding(4, 5, 4, 5);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(100, 35);
            btnPrev.TabIndex = 10;
            btnPrev.Text = "Trước";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 378);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(108, 20);
            label1.TabIndex = 11;
            label1.Text = "Cơ Sở Vật Chất";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 420);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 12;
            label2.Text = "Nhân Viên";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 461);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(44, 20);
            label3.TabIndex = 13;
            label3.Text = "Ngày";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 501);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(54, 20);
            label4.TabIndex = 14;
            label4.Text = "Chi Phí";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 541);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(51, 20);
            label5.TabIndex = 15;
            label5.Text = "Mô Tả";
            // 
            // cmbFilterEquipment
            // 
            cmbFilterEquipment.FormattingEnabled = true;
            cmbFilterEquipment.Location = new Point(675, 486);
            cmbFilterEquipment.Margin = new Padding(4, 5, 4, 5);
            cmbFilterEquipment.Name = "cmbFilterEquipment";
            cmbFilterEquipment.Size = new Size(199, 28);
            cmbFilterEquipment.TabIndex = 16;
            // 
            // cmbFilterEmployee
            // 
            cmbFilterEmployee.FormattingEnabled = true;
            cmbFilterEmployee.Location = new Point(675, 528);
            cmbFilterEmployee.Margin = new Padding(4, 5, 4, 5);
            cmbFilterEmployee.Name = "cmbFilterEmployee";
            cmbFilterEmployee.Size = new Size(199, 28);
            cmbFilterEmployee.TabIndex = 17;
            // 
            // dtpStart
            // 
            dtpStart.Format = DateTimePickerFormat.Short;
            dtpStart.Location = new Point(675, 569);
            dtpStart.Margin = new Padding(4, 5, 4, 5);
            dtpStart.Name = "dtpStart";
            dtpStart.Size = new Size(199, 27);
            dtpStart.TabIndex = 18;
            // 
            // dtpEnd
            // 
            dtpEnd.Format = DateTimePickerFormat.Short;
            dtpEnd.Location = new Point(675, 609);
            dtpEnd.Margin = new Padding(4, 5, 4, 5);
            dtpEnd.Name = "dtpEnd";
            dtpEnd.Size = new Size(199, 27);
            dtpEnd.TabIndex = 19;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(896, 541);
            btnFilter.Margin = new Padding(4, 5, 4, 5);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(100, 35);
            btnFilter.TabIndex = 20;
            btnFilter.Text = "Lọc";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // btnResetFilter
            // 
            btnResetFilter.Location = new Point(896, 586);
            btnResetFilter.Margin = new Padding(4, 5, 4, 5);
            btnResetFilter.Name = "btnResetFilter";
            btnResetFilter.Size = new Size(100, 35);
            btnResetFilter.TabIndex = 28;
            btnResetFilter.Text = "Đặt Lại";
            btnResetFilter.UseVisualStyleBackColor = true;
            btnResetFilter.Click += btnResetFilter_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(528, 489);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(135, 20);
            label6.TabIndex = 21;
            label6.Text = "Lọc Cơ Sở Vật Chất";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(528, 530);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(104, 20);
            label7.TabIndex = 22;
            label7.Text = "Lọc Nhân Viên";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(528, 572);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(101, 20);
            label8.TabIndex = 23;
            label8.Text = "Ngày Bắt Đầu";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(528, 612);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(105, 20);
            label9.TabIndex = 24;
            label9.Text = "Ngày Kết Thúc";
            // 
            // MaintenanceForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 667);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(btnResetFilter);
            Controls.Add(btnFilter);
            Controls.Add(dtpEnd);
            Controls.Add(dtpStart);
            Controls.Add(cmbFilterEmployee);
            Controls.Add(cmbFilterEquipment);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnPrev);
            Controls.Add(btnNext);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(lblDescriptionValue);
            Controls.Add(lblCostValue);
            Controls.Add(lblDateValue);
            Controls.Add(lblEmployeeValue);
            Controls.Add(lblEquipmentValue);
            Controls.Add(txtDescription);
            Controls.Add(numCost);
            Controls.Add(dtpDate);
            Controls.Add(cmbEmployee);
            Controls.Add(cmbEquipment);
            Controls.Add(btnClearSearch);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(lblSearch);
            Controls.Add(dgvMaintenance);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MaintenanceForm";
            Text = "Quản Lý Bảo Trì";
            ((System.ComponentModel.ISupportInitialize)dgvMaintenance).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCost).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

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
    private System.Windows.Forms.Label lblEquipmentValue;
    private System.Windows.Forms.Label lblEmployeeValue;
    private System.Windows.Forms.Label lblDateValue;
    private System.Windows.Forms.Label lblCostValue;
    private System.Windows.Forms.Label lblDescriptionValue;
    }
}