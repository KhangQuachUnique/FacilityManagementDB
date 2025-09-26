namespace FacilityManagementSystem
{
    partial class ReportsForm
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
            this.tabReports = new System.Windows.Forms.TabControl();
            this.tabMaintenanceCost = new System.Windows.Forms.TabPage();
            this.tabEquipmentStatus = new System.Windows.Forms.TabPage();
            this.tabAssetValue = new System.Windows.Forms.TabPage();
            this.tabMaintenanceNeeded = new System.Windows.Forms.TabPage();
            this.dgvMaintenanceCost = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbMonth = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbYear = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnViewMaintenanceCost = new System.Windows.Forms.Button();
            this.lblTotalCost = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblActive = new System.Windows.Forms.Label();
            this.lblMaintenance = new System.Windows.Forms.Label();
            this.lblBroken = new System.Windows.Forms.Label();
            this.lblStopped = new System.Windows.Forms.Label();
            this.btnRefreshStatus = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvAssetValue = new System.Windows.Forms.DataGridView();
            this.cmbArea = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnViewAssetValue = new System.Windows.Forms.Button();
            this.lblAreaTotalValue = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.dgvMaintenanceNeeded = new System.Windows.Forms.DataGridView();
            this.btnRefreshMaintenanceNeeded = new System.Windows.Forms.Button();
            this.lblMaintenanceCount = new System.Windows.Forms.Label();

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

            this.tabReports.Name = "tabReports";
            this.tabReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMaintenanceCost.Name = "tabMaintenanceCost";
            this.tabMaintenanceCost.Text = "📊 Chi Phí Bảo Trì";
            this.tabEquipmentStatus.Name = "tabEquipmentStatus";
            this.tabEquipmentStatus.Text = "🔧 Thiết Bị Theo Trạng Thái";
            this.tabAssetValue.Name = "tabAssetValue";
            this.tabAssetValue.Text = "💰 Giá Trị Tài Sản";
            this.tabMaintenanceNeeded.Name = "tabMaintenanceNeeded";
            this.tabMaintenanceNeeded.Text = "⚠️ Cần Bảo Trì";

            this.dgvMaintenanceCost.Name = "dgvMaintenanceCost";
            this.cmbMonth.Name = "cmbMonth";
            this.label1.Name = "label1";
            this.label1.AutoSize = true;
            this.cmbYear.Name = "cmbYear";
            this.label2.Name = "label2";
            this.label2.AutoSize = true;
            this.btnViewMaintenanceCost.Name = "btnViewMaintenanceCost";
            this.btnViewMaintenanceCost.Text = "Xem Báo Cáo";
            this.lblTotalCost.Name = "lblTotalCost";
            this.lblTotalCost.Text = "Tổng chi phí: 0 đ";

            this.label3.Name = "label3";
            this.label3.AutoSize = true;
            this.label4.Name = "label4";
            this.label4.AutoSize = true;
            this.label5.Name = "label5";
            this.label5.AutoSize = true;
            this.label6.Name = "label6";
            this.label6.AutoSize = true;
            this.lblActive.Name = "lblActive";
            this.lblActive.Text = "0 thiết bị";
            this.lblMaintenance.Name = "lblMaintenance";
            this.lblMaintenance.Text = "0 thiết bị";
            this.lblBroken.Name = "lblBroken";
            this.lblBroken.Text = "0 thiết bị";
            this.lblStopped.Name = "lblStopped";
            this.lblStopped.Text = "0 thiết bị";
            this.btnRefreshStatus.Name = "btnRefreshStatus";
            this.btnRefreshStatus.Text = "Làm Mới Dữ Liệu";

            this.dgvAssetValue.Name = "dgvAssetValue";
            this.cmbArea.Name = "cmbArea";
            this.label7.Name = "label7";
            this.label7.AutoSize = true;
            this.btnViewAssetValue.Name = "btnViewAssetValue";
            this.btnViewAssetValue.Text = "Xem Báo Cáo";
            this.lblAreaTotalValue.Name = "lblAreaTotalValue";
            this.lblAreaTotalValue.Text = "Tổng giá trị: 0 đ";

            this.dgvMaintenanceNeeded.Name = "dgvMaintenanceNeeded";
            this.btnRefreshMaintenanceNeeded.Name = "btnRefreshMaintenanceNeeded";
            this.btnRefreshMaintenanceNeeded.Text = "Làm Mới Dữ Liệu";
            this.lblMaintenanceCount.Name = "lblMaintenanceCount";
            this.lblMaintenanceCount.Text = "Tổng số thiết bị cần bảo trì: 0";

            this.panel1.Name = "panel1";
            this.panel2.Name = "panel2";
            this.panel3.Name = "panel3";
            this.panel4.Name = "panel4";

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Name = "ReportsForm";
            this.Text = "Báo Cáo Quản Lý Cơ Sở Vật Chất";

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

        private System.Windows.Forms.TabControl tabReports;
        private System.Windows.Forms.TabPage tabMaintenanceCost;
        private System.Windows.Forms.TabPage tabEquipmentStatus;
        private System.Windows.Forms.TabPage tabAssetValue;
        private System.Windows.Forms.TabPage tabMaintenanceNeeded;
        private System.Windows.Forms.DataGridView dgvMaintenanceCost;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbMonth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnViewMaintenanceCost;
        private System.Windows.Forms.Label lblTotalCost;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblActive;
        private System.Windows.Forms.Label lblMaintenance;
        private System.Windows.Forms.Label lblBroken;
        private System.Windows.Forms.Label lblStopped;
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