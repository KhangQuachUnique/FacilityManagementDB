namespace FacilityManagementSystem
{
    partial class EquipmentForm
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
            dgvEquipment = new DataGridView();
            lblName = new Label();
            lblType = new Label();
            lblArea = new Label();
            lblStatus = new Label();
            lblQuantity = new Label();
            lblPrice = new Label();
            lblLastMaintenance = new Label();
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
            label6 = new Label();
            label7 = new Label();
            cmbFilterArea = new ComboBox();
            cmbFilterType = new ComboBox();
            cmbFilterStatus = new ComboBox();
            btnFilter = new Button();
            btnResetFilter = new Button();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvEquipment).BeginInit();
            SuspendLayout();
            // 
            // dgvEquipment
            // 
            dgvEquipment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEquipment.Location = new Point(16, 18);
            dgvEquipment.Margin = new Padding(4, 5, 4, 5);
            dgvEquipment.Name = "dgvEquipment";
            dgvEquipment.RowHeadersWidth = 51;
            dgvEquipment.Size = new Size(1013, 308);
            dgvEquipment.TabIndex = 0;
            dgvEquipment.SelectionChanged += dgvEquipment_SelectionChanged;
            // 
            // lblName
            // 
            lblName.BorderStyle = BorderStyle.FixedSingle;
            lblName.Location = new Point(133, 338);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(199, 30);
            lblName.TabIndex = 1;
            // 
            // lblType
            // 
            lblType.BorderStyle = BorderStyle.FixedSingle;
            lblType.Location = new Point(133, 378);
            lblType.Margin = new Padding(4, 0, 4, 0);
            lblType.Name = "lblType";
            lblType.Size = new Size(199, 30);
            lblType.TabIndex = 2;
            // 
            // lblArea
            // 
            lblArea.BorderStyle = BorderStyle.FixedSingle;
            lblArea.Location = new Point(133, 420);
            lblArea.Margin = new Padding(4, 0, 4, 0);
            lblArea.Name = "lblArea";
            lblArea.Size = new Size(199, 30);
            lblArea.TabIndex = 3;
            // 
            // lblStatus
            // 
            lblStatus.BorderStyle = BorderStyle.FixedSingle;
            lblStatus.Location = new Point(133, 462);
            lblStatus.Margin = new Padding(4, 0, 4, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(199, 30);
            lblStatus.TabIndex = 4;
            // 
            // lblQuantity
            // 
            lblQuantity.BorderStyle = BorderStyle.FixedSingle;
            lblQuantity.Location = new Point(133, 503);
            lblQuantity.Margin = new Padding(4, 0, 4, 0);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(199, 30);
            lblQuantity.TabIndex = 5;
            // 
            // lblPrice
            // 
            lblPrice.BorderStyle = BorderStyle.FixedSingle;
            lblPrice.Location = new Point(133, 543);
            lblPrice.Margin = new Padding(4, 0, 4, 0);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(199, 30);
            lblPrice.TabIndex = 6;
            // 
            // lblLastMaintenance
            // 
            lblLastMaintenance.BorderStyle = BorderStyle.FixedSingle;
            lblLastMaintenance.Location = new Point(133, 583);
            lblLastMaintenance.Margin = new Padding(4, 0, 4, 0);
            lblLastMaintenance.Name = "lblLastMaintenance";
            lblLastMaintenance.Size = new Size(199, 30);
            lblLastMaintenance.TabIndex = 7;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(400, 338);
            btnAdd.Margin = new Padding(4, 5, 4, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 35);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(400, 383);
            btnUpdate.Margin = new Padding(4, 5, 4, 5);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(100, 35);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Cập Nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(400, 428);
            btnDelete.Margin = new Padding(4, 5, 4, 5);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 35);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(800, 338);
            btnNext.Margin = new Padding(4, 5, 4, 5);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(100, 35);
            btnNext.TabIndex = 11;
            btnNext.Text = "Tiếp";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnPrev
            // 
            btnPrev.Location = new Point(667, 338);
            btnPrev.Margin = new Padding(4, 5, 4, 5);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(100, 35);
            btnPrev.TabIndex = 12;
            btnPrev.Text = "Trước";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 343);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(49, 20);
            label1.TabIndex = 13;
            label1.Text = "Tên";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 383);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(40, 20);
            label2.TabIndex = 14;
            label2.Text = "Loại";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 425);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(40, 20);
            label3.TabIndex = 15;
            label3.Text = "Khu Vực";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 466);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(49, 20);
            label4.TabIndex = 16;
            label4.Text = "Trạng Thái";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(16, 506);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(65, 20);
            label5.TabIndex = 17;
            label5.Text = "Số Lượng";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(16, 546);
            label6.Margin = new Padding(4, 0, 4, 0);
            label6.Name = "label6";
            label6.Size = new Size(41, 20);
            label6.TabIndex = 18;
            label6.Text = "Giá";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(16, 586);
            label7.Margin = new Padding(4, 0, 4, 0);
            label7.Name = "label7";
            label7.Size = new Size(124, 20);
            label7.TabIndex = 19;
            label7.Text = "Bảo Trì Cuối";
            // 
            // cmbFilterArea
            // 
            cmbFilterArea.FormattingEnabled = true;
            cmbFilterArea.Location = new Point(675, 434);
            cmbFilterArea.Margin = new Padding(4, 5, 4, 5);
            cmbFilterArea.Name = "cmbFilterArea";
            cmbFilterArea.Size = new Size(199, 28);
            cmbFilterArea.TabIndex = 20;
            // 
            // cmbFilterType
            // 
            cmbFilterType.FormattingEnabled = true;
            cmbFilterType.Location = new Point(675, 480);
            cmbFilterType.Margin = new Padding(4, 5, 4, 5);
            cmbFilterType.Name = "cmbFilterType";
            cmbFilterType.Size = new Size(199, 28);
            cmbFilterType.TabIndex = 21;
            // 
            // cmbFilterStatus
            // 
            cmbFilterStatus.FormattingEnabled = true;
            cmbFilterStatus.Location = new Point(675, 526);
            cmbFilterStatus.Margin = new Padding(4, 5, 4, 5);
            cmbFilterStatus.Name = "cmbFilterStatus";
            cmbFilterStatus.Size = new Size(199, 28);
            cmbFilterStatus.TabIndex = 22;
            // 
            // btnFilter
            // 
            btnFilter.Location = new Point(901, 476);
            btnFilter.Margin = new Padding(4, 5, 4, 5);
            btnFilter.Name = "btnFilter";
            btnFilter.Size = new Size(100, 35);
            btnFilter.TabIndex = 23;
            btnFilter.Text = "Lọc";
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += btnFilter_Click;
            // 
            // btnResetFilter
            // 
            btnResetFilter.Location = new Point(901, 521);
            btnResetFilter.Margin = new Padding(4, 5, 4, 5);
            btnResetFilter.Name = "btnResetFilter";
            btnResetFilter.Size = new Size(100, 35);
            btnResetFilter.TabIndex = 27;
            btnResetFilter.Text = "Đặt Lại";
            btnResetFilter.UseVisualStyleBackColor = true;
            btnResetFilter.Click += btnResetFilter_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(568, 440);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(97, 20);
            label8.TabIndex = 24;
            label8.Text = "Lọc theo Khu Vực";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(568, 486);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(97, 20);
            label9.TabIndex = 25;
            label9.Text = "Lọc theo Loại";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(568, 532);
            label10.Margin = new Padding(4, 0, 4, 0);
            label10.Name = "label10";
            label10.Size = new Size(106, 20);
            label10.TabIndex = 26;
            label10.Text = "Lọc theo Trạng Thái";
            // 
            // EquipmentForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1045, 642);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(btnResetFilter);
            Controls.Add(btnFilter);
            Controls.Add(cmbFilterStatus);
            Controls.Add(cmbFilterType);
            Controls.Add(cmbFilterArea);
            Controls.Add(label7);
            Controls.Add(label6);
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
            Controls.Add(lblLastMaintenance);
            Controls.Add(lblPrice);
            Controls.Add(lblQuantity);
            Controls.Add(lblStatus);
            Controls.Add(lblArea);
            Controls.Add(lblType);
            Controls.Add(lblName);
            Controls.Add(dgvEquipment);
            Margin = new Padding(4, 5, 4, 5);
            Name = "EquipmentForm";
            Text = "Quản Lý Cơ Sở Vật Chất";
            ((System.ComponentModel.ISupportInitialize)dgvEquipment).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEquipment;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblArea;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblLastMaintenance;
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
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbFilterArea;
        private System.Windows.Forms.ComboBox cmbFilterType;
        private System.Windows.Forms.ComboBox cmbFilterStatus;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnResetFilter;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}