// File: ReportsForm.Designer.cs

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
            this.numMonth = new System.Windows.Forms.NumericUpDown();
            this.numYear = new System.Windows.Forms.NumericUpDown();
            this.btnMaintenanceCost = new System.Windows.Forms.Button();
            this.lblMaintenanceCost = new System.Windows.Forms.Label();
            this.cmbArea = new System.Windows.Forms.ComboBox();
            this.btnValueByArea = new System.Windows.Forms.Button();
            this.lblValueByArea = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.btnValueByType = new System.Windows.Forms.Button();
            this.lblValueByType = new System.Windows.Forms.Label();
            this.numDays = new System.Windows.Forms.NumericUpDown();
            this.btnNeedingMaintenance = new System.Windows.Forms.Button();
            this.dgvNeedingMaintenance = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNeedingMaintenance)).BeginInit();
            this.SuspendLayout();
            // 
            // numMonth
            // 
            this.numMonth.Location = new System.Drawing.Point(100, 12);
            this.numMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMonth.Name = "numMonth";
            this.numMonth.Size = new System.Drawing.Size(120, 20);
            this.numMonth.TabIndex = 0;
            this.numMonth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numYear
            // 
            this.numYear.Location = new System.Drawing.Point(100, 38);
            this.numYear.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numYear.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numYear.Name = "numYear";
            this.numYear.Size = new System.Drawing.Size(120, 20);
            this.numYear.TabIndex = 1;
            this.numYear.Value = new decimal(new int[] {
            2023,
            0,
            0,
            0});
            // 
            // btnMaintenanceCost
            // 
            this.btnMaintenanceCost.Location = new System.Drawing.Point(226, 12);
            this.btnMaintenanceCost.Name = "btnMaintenanceCost";
            this.btnMaintenanceCost.Size = new System.Drawing.Size(150, 23);
            this.btnMaintenanceCost.TabIndex = 2;
            this.btnMaintenanceCost.Text = "Get Maintenance Cost";
            this.btnMaintenanceCost.UseVisualStyleBackColor = true;
            this.btnMaintenanceCost.Click += new System.EventHandler(this.btnMaintenanceCost_Click);
            // 
            // lblMaintenanceCost
            // 
            this.lblMaintenanceCost.AutoSize = true;
            this.lblMaintenanceCost.Location = new System.Drawing.Point(382, 17);
            this.lblMaintenanceCost.Name = "lblMaintenanceCost";
            this.lblMaintenanceCost.Size = new System.Drawing.Size(0, 13);
            this.lblMaintenanceCost.TabIndex = 3;
            // 
            // cmbArea
            // 
            this.cmbArea.FormattingEnabled = true;
            this.cmbArea.Location = new System.Drawing.Point(100, 64);
            this.cmbArea.Name = "cmbArea";
            this.cmbArea.Size = new System.Drawing.Size(120, 21);
            this.cmbArea.TabIndex = 4;
            // 
            // btnValueByArea
            // 
            this.btnValueByArea.Location = new System.Drawing.Point(226, 64);
            this.btnValueByArea.Name = "btnValueByArea";
            this.btnValueByArea.Size = new System.Drawing.Size(150, 23);
            this.btnValueByArea.TabIndex = 5;
            this.btnValueByArea.Text = "Get Value by Area";
            this.btnValueByArea.UseVisualStyleBackColor = true;
            this.btnValueByArea.Click += new System.EventHandler(this.btnValueByArea_Click);
            // 
            // lblValueByArea
            // 
            this.lblValueByArea.AutoSize = true;
            this.lblValueByArea.Location = new System.Drawing.Point(382, 68);
            this.lblValueByArea.Name = "lblValueByArea";
            this.lblValueByArea.Size = new System.Drawing.Size(0, 13);
            this.lblValueByArea.TabIndex = 6;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(100, 91);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(120, 21);
            this.cmbType.TabIndex = 7;
            // 
            // btnValueByType
            // 
            this.btnValueByType.Location = new System.Drawing.Point(226, 91);
            this.btnValueByType.Name = "btnValueByType";
            this.btnValueByType.Size = new System.Drawing.Size(150, 23);
            this.btnValueByType.TabIndex = 8;
            this.btnValueByType.Text = "Get Value by Type";
            this.btnValueByType.UseVisualStyleBackColor = true;
            this.btnValueByType.Click += new System.EventHandler(this.btnValueByType_Click);
            // 
            // lblValueByType
            // 
            this.lblValueByType.AutoSize = true;
            this.lblValueByType.Location = new System.Drawing.Point(382, 95);
            this.lblValueByType.Name = "lblValueByType";
            this.lblValueByType.Size = new System.Drawing.Size(0, 13);
            this.lblValueByType.TabIndex = 9;
            // 
            // numDays
            // 
            this.numDays.Location = new System.Drawing.Point(100, 118);
            this.numDays.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numDays.Name = "numDays";
            this.numDays.Size = new System.Drawing.Size(120, 20);
            this.numDays.TabIndex = 10;
            this.numDays.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // btnNeedingMaintenance
            // 
            this.btnNeedingMaintenance.Location = new System.Drawing.Point(226, 118);
            this.btnNeedingMaintenance.Name = "btnNeedingMaintenance";
            this.btnNeedingMaintenance.Size = new System.Drawing.Size(150, 23);
            this.btnNeedingMaintenance.TabIndex = 11;
            this.btnNeedingMaintenance.Text = "Get Needing Maintenance";
            this.btnNeedingMaintenance.UseVisualStyleBackColor = true;
            this.btnNeedingMaintenance.Click += new System.EventHandler(this.btnNeedingMaintenance_Click);
            // 
            // dgvNeedingMaintenance
            // 
            this.dgvNeedingMaintenance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNeedingMaintenance.Location = new System.Drawing.Point(12, 147);
            this.dgvNeedingMaintenance.Name = "dgvNeedingMaintenance";
            this.dgvNeedingMaintenance.Size = new System.Drawing.Size(760, 200);
            this.dgvNeedingMaintenance.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Month";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Year";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Area";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Type";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Days Threshold";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Equipment Needing Maint:";
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvNeedingMaintenance);
            this.Controls.Add(this.btnNeedingMaintenance);
            this.Controls.Add(this.numDays);
            this.Controls.Add(this.lblValueByType);
            this.Controls.Add(this.btnValueByType);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.lblValueByArea);
            this.Controls.Add(this.btnValueByArea);
            this.Controls.Add(this.cmbArea);
            this.Controls.Add(this.lblMaintenanceCost);
            this.Controls.Add(this.btnMaintenanceCost);
            this.Controls.Add(this.numYear);
            this.Controls.Add(this.numMonth);
            this.Name = "ReportsForm";
            this.Text = "Reports";
            ((System.ComponentModel.ISupportInitialize)(this.numMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNeedingMaintenance)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numMonth;
        private System.Windows.Forms.NumericUpDown numYear;
        private System.Windows.Forms.Button btnMaintenanceCost;
        private System.Windows.Forms.Label lblMaintenanceCost;
        private System.Windows.Forms.ComboBox cmbArea;
        private System.Windows.Forms.Button btnValueByArea;
        private System.Windows.Forms.Label lblValueByArea;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Button btnValueByType;
        private System.Windows.Forms.Label lblValueByType;
        private System.Windows.Forms.NumericUpDown numDays;
        private System.Windows.Forms.Button btnNeedingMaintenance;
        private System.Windows.Forms.DataGridView dgvNeedingMaintenance;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}