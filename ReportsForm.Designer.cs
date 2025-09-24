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
            numMonth = new NumericUpDown();
            numYear = new NumericUpDown();
            btnMaintenanceCost = new Button();
            lblMaintenanceCost = new Label();
            cmbArea = new ComboBox();
            btnValueByArea = new Button();
            lblValueByArea = new Label();
            cmbType = new ComboBox();
            btnValueByType = new Button();
            lblValueByType = new Label();
            numDays = new NumericUpDown();
            btnNeedingMaintenance = new Button();
            dgvNeedingMaintenance = new DataGridView();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)numMonth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numYear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDays).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvNeedingMaintenance).BeginInit();
            SuspendLayout();
            // 
            // numMonth
            // 
            numMonth.Location = new Point(133, 18);
            numMonth.Margin = new Padding(4, 5, 4, 5);
            numMonth.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            numMonth.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numMonth.Name = "numMonth";
            numMonth.Size = new Size(160, 27);
            numMonth.TabIndex = 0;
            numMonth.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // numYear
            // 
            numYear.Location = new Point(133, 58);
            numYear.Margin = new Padding(4, 5, 4, 5);
            numYear.Maximum = new decimal(new int[] { 2050, 0, 0, 0 });
            numYear.Minimum = new decimal(new int[] { 2020, 0, 0, 0 });
            numYear.Name = "numYear";
            numYear.Size = new Size(160, 27);
            numYear.TabIndex = 1;
            numYear.Value = new decimal(new int[] { 2025, 0, 0, 0 });
            // 
            // btnMaintenanceCost
            // 
            btnMaintenanceCost.Location = new Point(301, 15);
            btnMaintenanceCost.Margin = new Padding(4, 5, 4, 5);
            btnMaintenanceCost.Name = "btnMaintenanceCost";
            btnMaintenanceCost.Size = new Size(200, 35);
            btnMaintenanceCost.TabIndex = 2;
            btnMaintenanceCost.Text = "Xem Chi Phí Bảo Trì";
            btnMaintenanceCost.UseVisualStyleBackColor = true;
            btnMaintenanceCost.Click += btnMaintenanceCost_Click;
            // 
            // lblMaintenanceCost
            // 
            lblMaintenanceCost.AutoSize = true;
            lblMaintenanceCost.Location = new Point(509, 26);
            lblMaintenanceCost.Margin = new Padding(4, 0, 4, 0);
            lblMaintenanceCost.Name = "lblMaintenanceCost";
            lblMaintenanceCost.Size = new Size(0, 20);
            lblMaintenanceCost.TabIndex = 3;
            // 
            // cmbArea
            // 
            cmbArea.FormattingEnabled = true;
            cmbArea.Location = new Point(133, 98);
            cmbArea.Margin = new Padding(4, 5, 4, 5);
            cmbArea.Name = "cmbArea";
            cmbArea.Size = new Size(159, 28);
            cmbArea.TabIndex = 4;
            // 
            // btnValueByArea
            // 
            btnValueByArea.Location = new Point(301, 94);
            btnValueByArea.Margin = new Padding(4, 5, 4, 5);
            btnValueByArea.Name = "btnValueByArea";
            btnValueByArea.Size = new Size(200, 35);
            btnValueByArea.TabIndex = 5;
            btnValueByArea.Text = "Xem Giá Trị theo Khu Vực";
            btnValueByArea.UseVisualStyleBackColor = true;
            btnValueByArea.Click += btnValueByArea_Click;
            // 
            // lblValueByArea
            // 
            lblValueByArea.AutoSize = true;
            lblValueByArea.Location = new Point(509, 105);
            lblValueByArea.Margin = new Padding(4, 0, 4, 0);
            lblValueByArea.Name = "lblValueByArea";
            lblValueByArea.Size = new Size(0, 20);
            lblValueByArea.TabIndex = 6;
            // 
            // cmbType
            // 
            cmbType.FormattingEnabled = true;
            cmbType.Location = new Point(133, 140);
            cmbType.Margin = new Padding(4, 5, 4, 5);
            cmbType.Name = "cmbType";
            cmbType.Size = new Size(159, 28);
            cmbType.TabIndex = 7;
            // 
            // btnValueByType
            // 
            btnValueByType.Location = new Point(301, 137);
            btnValueByType.Margin = new Padding(4, 5, 4, 5);
            btnValueByType.Name = "btnValueByType";
            btnValueByType.Size = new Size(200, 35);
            btnValueByType.TabIndex = 8;
            btnValueByType.Text = "Xem Giá Trị theo Loại";
            btnValueByType.UseVisualStyleBackColor = true;
            btnValueByType.Click += btnValueByType_Click;
            // 
            // lblValueByType
            // 
            lblValueByType.AutoSize = true;
            lblValueByType.Location = new Point(509, 146);
            lblValueByType.Margin = new Padding(4, 0, 4, 0);
            lblValueByType.Name = "lblValueByType";
            lblValueByType.Size = new Size(0, 20);
            lblValueByType.TabIndex = 9;
            // 
            // numDays
            // 
            numDays.Location = new Point(658, 23);
            numDays.Margin = new Padding(4, 5, 4, 5);
            numDays.Maximum = new decimal(new int[] { 1095, 0, 0, 0 }); // 3 năm
            numDays.Name = "numDays";
            numDays.Size = new Size(160, 27);
            numDays.TabIndex = 10;
            numDays.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // btnNeedingMaintenance
            // 
            btnNeedingMaintenance.Location = new Point(829, 18);
            btnNeedingMaintenance.Margin = new Padding(4, 5, 4, 5);
            btnNeedingMaintenance.Name = "btnNeedingMaintenance";
            btnNeedingMaintenance.Size = new Size(200, 35);
            btnNeedingMaintenance.TabIndex = 11;
            btnNeedingMaintenance.Text = "Xem Cần Bảo Trì";
            btnNeedingMaintenance.UseVisualStyleBackColor = true;
            btnNeedingMaintenance.Click += btnNeedingMaintenance_Click;
            // 
            // dgvNeedingMaintenance
            // 
            dgvNeedingMaintenance.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNeedingMaintenance.Location = new Point(16, 226);
            dgvNeedingMaintenance.Margin = new Padding(4, 5, 4, 5);
            dgvNeedingMaintenance.Name = "dgvNeedingMaintenance";
            dgvNeedingMaintenance.RowHeadersWidth = 51;
            dgvNeedingMaintenance.Size = new Size(1013, 308);
            dgvNeedingMaintenance.TabIndex = 12;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 22);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(52, 20);
            label1.TabIndex = 13;
            label1.Text = "Tháng";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 62);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(37, 20);
            label2.TabIndex = 14;
            label2.Text = "Năm";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 105);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(40, 20);
            label3.TabIndex = 15;
            label3.Text = "Khu Vực";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 146);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(40, 20);
            label4.TabIndex = 16;
            label4.Text = "Loại";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(541, 26);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(110, 20);
            label5.TabIndex = 17;
            label5.Text = "Ngưỡng Ngày";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 10F);
            label6.Location = new Point(16, 198);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(216, 23);
            label6.TabIndex = 18;
            label6.Text = "Cơ Sở Vật Chất Cần Bảo Trì:";
            // 
            // ReportsForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 555);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dgvNeedingMaintenance);
            Controls.Add(btnNeedingMaintenance);
            Controls.Add(numDays);
            Controls.Add(lblValueByType);
            Controls.Add(btnValueByType);
            Controls.Add(cmbType);
            Controls.Add(lblValueByArea);
            Controls.Add(btnValueByArea);
            Controls.Add(cmbArea);
            Controls.Add(lblMaintenanceCost);
            Controls.Add(btnMaintenanceCost);
            Controls.Add(numYear);
            Controls.Add(numMonth);
            Margin = new Padding(4, 5, 4, 5);
            Name = "ReportsForm";
            Text = "Báo Cáo";
            ((System.ComponentModel.ISupportInitialize)numMonth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDays).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvNeedingMaintenance).EndInit();
            ResumeLayout(false);
            PerformLayout();

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