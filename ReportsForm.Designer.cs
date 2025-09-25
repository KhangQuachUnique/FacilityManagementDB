namespace FacilityManagementSystem
{
    partial class ReportsForm
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
            this.tabReports = new System.Windows.Forms.TabControl();
            this.tabMaintenanceCost = new System.Windows.Forms.TabPage();
            this.dgvMaintenanceCost = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotalCost = new System.Windows.Forms.Label();
            this.btnViewMaintenanceCost = new System.Windows.Forms.Button();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabEquipmentStatus = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnRefreshStatus = new System.Windows.Forms.Button();
            this.lblStopped = new System.Windows.Forms.Label();
            this.lblBroken = new System.Windows.Forms.Label();
            this.lblMaintenance = new System.Windows.Forms.Label();
            this.lblActive = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabAssetValue = new System.Windows.Forms.TabPage();
            this.dgvAssetValue = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblAreaTotalValue = new System.Windows.Forms.Label();
            this.btnViewAssetValue = new System.Windows.Forms.Button();
            this.cmbArea = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabMaintenanceNeeded = new System.Windows.Forms.TabPage();
            this.dgvMaintenanceNeeded = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblMaintenanceCount = new System.Windows.Forms.Label();
            this.btnRefreshMaintenanceNeeded = new System.Windows.Forms.Button();
            this.tabReports.SuspendLayout();
            this.tabMaintenanceCost.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenanceCost)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabEquipmentStatus.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabAssetValue.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssetValue)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabMaintenanceNeeded.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenanceNeeded)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabReports
            // 
            this.tabReports.Controls.Add(this.tabMaintenanceCost);
            this.tabReports.Controls.Add(this.tabEquipmentStatus);
            this.tabReports.Controls.Add(this.tabAssetValue);
            this.tabReports.Controls.Add(this.tabMaintenanceNeeded);
            this.tabReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabReports.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabReports.Location = new System.Drawing.Point(0, 0);
            this.tabReports.Name = "tabReports";
            this.tabReports.SelectedIndex = 0;
            this.tabReports.Size = new System.Drawing.Size(800, 600);
            this.tabReports.TabIndex = 0;
            // 
            // tabMaintenanceCost
            // 
            this.tabMaintenanceCost.Controls.Add(this.dgvMaintenanceCost);
            this.tabMaintenanceCost.Controls.Add(this.panel1);
            this.tabMaintenanceCost.Location = new System.Drawing.Point(4, 28);
            this.tabMaintenanceCost.Name = "tabMaintenanceCost";
            this.tabMaintenanceCost.Padding = new System.Windows.Forms.Padding(3);
            this.tabMaintenanceCost.Size = new System.Drawing.Size(792, 568);
            this.tabMaintenanceCost.TabIndex = 0;
            this.tabMaintenanceCost.Text = "📊 Chi Phí Bảo Trì";
            this.tabMaintenanceCost.UseVisualStyleBackColor = true;
            // 
            // dgvMaintenanceCost
            // 
            this.dgvMaintenanceCost.AllowUserToAddRows = false;
            this.dgvMaintenanceCost.AllowUserToDeleteRows = false;
            this.dgvMaintenanceCost.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMaintenanceCost.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMaintenanceCost.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaintenanceCost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaintenanceCost.Location = new System.Drawing.Point(3, 83);
            this.dgvMaintenanceCost.MultiSelect = false;
            this.dgvMaintenanceCost.Name = "dgvMaintenanceCost";
            this.dgvMaintenanceCost.ReadOnly = true;
            this.dgvMaintenanceCost.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaintenanceCost.Size = new System.Drawing.Size(786, 482);
            this.dgvMaintenanceCost.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightBlue;
            this.panel1.Controls.Add(this.lblTotalCost);
            this.panel1.Controls.Add(this.btnViewMaintenanceCost);
            this.panel1.Controls.Add(this.cmbYear);
            this.panel1.Controls.Add(this.cmbMonth);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 80);
            this.panel1.TabIndex = 0;
            // 
            // lblTotalCost
            // 
            this.lblTotalCost.AutoSize = true;
            this.lblTotalCost.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTotalCost.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTotalCost.Location = new System.Drawing.Point(400, 45);
            this.lblTotalCost.Name = "lblTotalCost";
            this.lblTotalCost.Size = new System.Drawing.Size(141, 21);
            this.lblTotalCost.TabIndex = 5;
            this.lblTotalCost.Text = "Tổng chi phí: 0 đ";
            // 
            // btnViewMaintenanceCost
            // 
            this.btnViewMaintenanceCost.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnViewMaintenanceCost.ForeColor = System.Drawing.Color.White;
            this.btnViewMaintenanceCost.Location = new System.Drawing.Point(280, 10);
            this.btnViewMaintenanceCost.Name = "btnViewMaintenanceCost";
            this.btnViewMaintenanceCost.Size = new System.Drawing.Size(100, 30);
            this.btnViewMaintenanceCost.TabIndex = 4;
            this.btnViewMaintenanceCost.Text = "Xem Báo Cáo";
            this.btnViewMaintenanceCost.UseVisualStyleBackColor = false;
            this.btnViewMaintenanceCost.Click += new System.EventHandler(this.btnViewMaintenanceCost_Click);
            // 
            // cmbYear
            // 
            this.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbYear.Location = new System.Drawing.Point(180, 13);
            this.cmbYear.Name = "cmbYear";
            this.cmbYear.Size = new System.Drawing.Size(80, 25);
            this.cmbYear.TabIndex = 3;
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.Location = new System.Drawing.Point(60, 13);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.Size = new System.Drawing.Size(70, 25);
            this.cmbMonth.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Năm:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tháng:";
            // 
            // tabEquipmentStatus
            // 
            this.tabEquipmentStatus.Controls.Add(this.panel2);
            this.tabEquipmentStatus.Location = new System.Drawing.Point(4, 28);
            this.tabEquipmentStatus.Name = "tabEquipmentStatus";
            this.tabEquipmentStatus.Padding = new System.Windows.Forms.Padding(3);
            this.tabEquipmentStatus.Size = new System.Drawing.Size(792, 568);
            this.tabEquipmentStatus.TabIndex = 1;
            this.tabEquipmentStatus.Text = "🔧 Thiết Bị Theo Trạng Thái";
            this.tabEquipmentStatus.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGreen;
            this.panel2.Controls.Add(this.btnRefreshStatus);
            this.panel2.Controls.Add(this.lblStopped);
            this.panel2.Controls.Add(this.lblBroken);
            this.panel2.Controls.Add(this.lblMaintenance);
            this.panel2.Controls.Add(this.lblActive);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(786, 562);
            this.panel2.TabIndex = 0;
            // 
            // btnRefreshStatus
            // 
            this.btnRefreshStatus.BackColor = System.Drawing.Color.Green;
            this.btnRefreshStatus.ForeColor = System.Drawing.Color.White;
            this.btnRefreshStatus.Location = new System.Drawing.Point(50, 300);
            this.btnRefreshStatus.Name = "btnRefreshStatus";
            this.btnRefreshStatus.Size = new System.Drawing.Size(120, 35);
            this.btnRefreshStatus.TabIndex = 8;
            this.btnRefreshStatus.Text = "Làm Mới Dữ Liệu";
            this.btnRefreshStatus.UseVisualStyleBackColor = false;
            this.btnRefreshStatus.Click += new System.EventHandler(this.btnRefreshStatus_Click);
            // 
            // lblStopped
            // 
            this.lblStopped.AutoSize = true;
            this.lblStopped.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblStopped.ForeColor = System.Drawing.Color.DarkRed;
            this.lblStopped.Location = new System.Drawing.Point(250, 230);
            this.lblStopped.Name = "lblStopped";
            this.lblStopped.Size = new System.Drawing.Size(141, 30);
            this.lblStopped.TabIndex = 7;
            this.lblStopped.Text = "0 thiết bị";
            // 
            // lblBroken
            // 
            this.lblBroken.AutoSize = true;
            this.lblBroken.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblBroken.ForeColor = System.Drawing.Color.Red;
            this.lblBroken.Location = new System.Drawing.Point(250, 170);
            this.lblBroken.Name = "lblBroken";
            this.lblBroken.Size = new System.Drawing.Size(141, 30);
            this.lblBroken.TabIndex = 6;
            this.lblBroken.Text = "0 thiết bị";
            // 
            // lblMaintenance
            // 
            this.lblMaintenance.AutoSize = true;
            this.lblMaintenance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblMaintenance.ForeColor = System.Drawing.Color.Orange;
            this.lblMaintenance.Location = new System.Drawing.Point(250, 110);
            this.lblMaintenance.Name = "lblMaintenance";
            this.lblMaintenance.Size = new System.Drawing.Size(141, 30);
            this.lblMaintenance.TabIndex = 5;
            this.lblMaintenance.Text = "0 thiết bị";
            // 
            // lblActive
            // 
            this.lblActive.AutoSize = true;
            this.lblActive.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblActive.ForeColor = System.Drawing.Color.Green;
            this.lblActive.Location = new System.Drawing.Point(250, 50);
            this.lblActive.Name = "lblActive";
            this.lblActive.Size = new System.Drawing.Size(141, 30);
            this.lblActive.TabIndex = 4;
            this.lblActive.Text = "0 thiết bị";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(50, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(184, 25);
            this.label6.TabIndex = 3;
            this.label6.Text = "🔴 Ngừng Hoạt Động:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(50, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 25);
            this.label5.TabIndex = 2;
            this.label5.Text = "💥 Hỏng:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(50, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "🔧 Đang Bảo Trì:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(50, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 25);
            this.label3.TabIndex = 0;
            this.label3.Text = "✅ Hoạt Động:";
            // 
            // tabAssetValue
            // 
            this.tabAssetValue.Controls.Add(this.dgvAssetValue);
            this.tabAssetValue.Controls.Add(this.panel3);
            this.tabAssetValue.Location = new System.Drawing.Point(4, 28);
            this.tabAssetValue.Name = "tabAssetValue";
            this.tabAssetValue.Padding = new System.Windows.Forms.Padding(3);
            this.tabAssetValue.Size = new System.Drawing.Size(792, 568);
            this.tabAssetValue.TabIndex = 2;
            this.tabAssetValue.Text = "💰 Giá Trị Tài Sản";
            this.tabAssetValue.UseVisualStyleBackColor = true;
            // 
            // dgvAssetValue
            // 
            this.dgvAssetValue.AllowUserToAddRows = false;
            this.dgvAssetValue.AllowUserToDeleteRows = false;
            this.dgvAssetValue.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAssetValue.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvAssetValue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAssetValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAssetValue.Location = new System.Drawing.Point(3, 83);
            this.dgvAssetValue.MultiSelect = false;
            this.dgvAssetValue.Name = "dgvAssetValue";
            this.dgvAssetValue.ReadOnly = true;
            this.dgvAssetValue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAssetValue.Size = new System.Drawing.Size(786, 482);
            this.dgvAssetValue.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.panel3.Controls.Add(this.lblAreaTotalValue);
            this.panel3.Controls.Add(this.btnViewAssetValue);
            this.panel3.Controls.Add(this.cmbArea);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(786, 80);
            this.panel3.TabIndex = 0;
            // 
            // lblAreaTotalValue
            // 
            this.lblAreaTotalValue.AutoSize = true;
            this.lblAreaTotalValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAreaTotalValue.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.lblAreaTotalValue.Location = new System.Drawing.Point(350, 45);
            this.lblAreaTotalValue.Name = "lblAreaTotalValue";
            this.lblAreaTotalValue.Size = new System.Drawing.Size(154, 21);
            this.lblAreaTotalValue.TabIndex = 3;
            this.lblAreaTotalValue.Text = "Tổng giá trị: 0 đ";
            // 
            // btnViewAssetValue
            // 
            this.btnViewAssetValue.BackColor = System.Drawing.Color.Goldenrod;
            this.btnViewAssetValue.ForeColor = System.Drawing.Color.White;
            this.btnViewAssetValue.Location = new System.Drawing.Point(230, 10);
            this.btnViewAssetValue.Name = "btnViewAssetValue";
            this.btnViewAssetValue.Size = new System.Drawing.Size(100, 30);
            this.btnViewAssetValue.TabIndex = 2;
            this.btnViewAssetValue.Text = "Xem Báo Cáo";
            this.btnViewAssetValue.UseVisualStyleBackColor = false;
            this.btnViewAssetValue.Click += new System.EventHandler(this.btnViewAssetValue_Click);
            // 
            // cmbArea
            // 
            this.cmbArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbArea.Location = new System.Drawing.Point(80, 13);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(140, 25);
            this.cmbArea.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "Khu Vực:";
            // 
            // tabMaintenanceNeeded
            // 
            this.tabMaintenanceNeeded.Controls.Add(this.dgvMaintenanceNeeded);
            this.tabMaintenanceNeeded.Controls.Add(this.panel4);
            this.tabMaintenanceNeeded.Location = new System.Drawing.Point(4, 28);
            this.tabMaintenanceNeeded.Name = "tabMaintenanceNeeded";
            this.tabMaintenanceNeeded.Padding = new System.Windows.Forms.Padding(3);
            this.tabMaintenanceNeeded.Size = new System.Drawing.Size(792, 568);
            this.tabMaintenanceNeeded.TabIndex = 3;
            this.tabMaintenanceNeeded.Text = "⚠️ Cần Bảo Trì";
            this.tabMaintenanceNeeded.UseVisualStyleBackColor = true;
            // 
            // dgvMaintenanceNeeded
            // 
            this.dgvMaintenanceNeeded.AllowUserToAddRows = false;
            this.dgvMaintenanceNeeded.AllowUserToDeleteRows = false;
            this.dgvMaintenanceNeeded.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMaintenanceNeeded.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMaintenanceNeeded.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaintenanceNeeded.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaintenanceNeeded.Location = new System.Drawing.Point(3, 83);
            this.dgvMaintenanceNeeded.MultiSelect = false;
            this.dgvMaintenanceNeeded.Name = "dgvMaintenanceNeeded";
            this.dgvMaintenanceNeeded.ReadOnly = true;
            this.dgvMaintenanceNeeded.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMaintenanceNeeded.Size = new System.Drawing.Size(786, 482);
            this.dgvMaintenanceNeeded.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LightCoral;
            this.panel4.Controls.Add(this.lblMaintenanceCount);
            this.panel4.Controls.Add(this.btnRefreshMaintenanceNeeded);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(786, 80);
            this.panel4.TabIndex = 0;
            // 
            // lblMaintenanceCount
            // 
            this.lblMaintenanceCount.AutoSize = true;
            this.lblMaintenanceCount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblMaintenanceCount.ForeColor = System.Drawing.Color.DarkRed;
            this.lblMaintenanceCount.Location = new System.Drawing.Point(150, 45);
            this.lblMaintenanceCount.Name = "lblMaintenanceCount";
            this.lblMaintenanceCount.Size = new System.Drawing.Size(236, 21);
            this.lblMaintenanceCount.TabIndex = 1;
            this.lblMaintenanceCount.Text = "Tổng số thiết bị cần bảo trì: 0";
            // 
            // btnRefreshMaintenanceNeeded
            // 
            this.btnRefreshMaintenanceNeeded.BackColor = System.Drawing.Color.Crimson;
            this.btnRefreshMaintenanceNeeded.ForeColor = System.Drawing.Color.White;
            this.btnRefreshMaintenanceNeeded.Location = new System.Drawing.Point(20, 15);
            this.btnRefreshMaintenanceNeeded.Name = "btnRefreshMaintenanceNeeded";
            this.btnRefreshMaintenanceNeeded.Size = new System.Drawing.Size(120, 35);
            this.btnRefreshMaintenanceNeeded.TabIndex = 0;
            this.btnRefreshMaintenanceNeeded.Text = "Làm Mới Dữ Liệu";
            this.btnRefreshMaintenanceNeeded.UseVisualStyleBackColor = false;
            this.btnRefreshMaintenanceNeeded.Click += new System.EventHandler(this.btnRefreshMaintenanceNeeded_Click);
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.tabReports);
            this.Name = "ReportsForm";
            this.Text = "📊 Báo Cáo Quản Lý Cơ Sở Vật Chất";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ReportsForm_Load);
            this.tabReports.ResumeLayout(false);
            this.tabMaintenanceCost.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenanceCost)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabEquipmentStatus.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabAssetValue.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAssetValue)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabMaintenanceNeeded.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaintenanceNeeded)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabReports;
        private System.Windows.Forms.TabPage tabMaintenanceCost;
        private System.Windows.Forms.TabPage tabEquipmentStatus;
        private System.Windows.Forms.TabPage tabAssetValue;
        private System.Windows.Forms.TabPage tabMaintenanceNeeded;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvMaintenanceCost;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Button btnViewMaintenanceCost;
        private System.Windows.Forms.Label lblTotalCost;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblStopped;
        private System.Windows.Forms.Label lblBroken;
        private System.Windows.Forms.Label lblMaintenance;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.Button btnRefreshStatus;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvAssetValue;
        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnViewAssetValue;
        private System.Windows.Forms.Label lblAreaTotalValue;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dgvMaintenanceNeeded;
        private System.Windows.Forms.Button btnRefreshMaintenanceNeeded;
        private System.Windows.Forms.Label lblMaintenanceCount;
    }
}