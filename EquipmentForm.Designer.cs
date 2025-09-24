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
            this.dgvEquipment = new System.Windows.Forms.DataGridView();
            this.lblName = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblArea = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblLastMaintenance = new System.Windows.Forms.Label();
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
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbFilterArea = new System.Windows.Forms.ComboBox();
            this.cmbFilterType = new System.Windows.Forms.ComboBox();
            this.cmbFilterStatus = new System.Windows.Forms.ComboBox();
            this.btnFilter = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipment)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEquipment
            // 
            this.dgvEquipment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquipment.Location = new System.Drawing.Point(12, 12);
            this.dgvEquipment.Name = "dgvEquipment";
            this.dgvEquipment.Size = new System.Drawing.Size(760, 200);
            this.dgvEquipment.TabIndex = 0;
            this.dgvEquipment.SelectionChanged += new System.EventHandler(this.dgvEquipment_SelectionChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = false;
            this.lblName.Location = new System.Drawing.Point(100, 220);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(150, 20);
            this.lblName.TabIndex = 1;
            this.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblType
            // 
            this.lblType.AutoSize = false;
            this.lblType.Location = new System.Drawing.Point(100, 246);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(150, 20);
            this.lblType.TabIndex = 2;
            this.lblType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblArea
            // 
            this.lblArea.AutoSize = false;
            this.lblArea.Location = new System.Drawing.Point(100, 273);
            this.lblArea.Name = "lblArea";
            this.lblArea.Size = new System.Drawing.Size(150, 20);
            this.lblArea.TabIndex = 3;
            this.lblArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.Location = new System.Drawing.Point(100, 300);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(150, 20);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = false;
            this.lblQuantity.Location = new System.Drawing.Point(100, 327);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(150, 20);
            this.lblQuantity.TabIndex = 5;
            this.lblQuantity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = false;
            this.lblPrice.Location = new System.Drawing.Point(100, 353);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(150, 20);
            this.lblPrice.TabIndex = 6;
            this.lblPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // lblLastMaintenance
            // 
            this.lblLastMaintenance.AutoSize = false;
            this.lblLastMaintenance.Location = new System.Drawing.Point(100, 379);
            this.lblLastMaintenance.Name = "lblLastMaintenance";
            this.lblLastMaintenance.Size = new System.Drawing.Size(150, 20);
            this.lblLastMaintenance.TabIndex = 7;
            this.lblLastMaintenance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(300, 220);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(300, 249);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 9;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(300, 278);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(600, 220);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 11;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(500, 220);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 12;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Type";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 276);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Area";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Status";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 329);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "Quantity";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 355);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(34, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Price";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 381);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Last Maintenance";
            // 
            // cmbFilterArea
            // 
            this.cmbFilterArea.FormattingEnabled = true;
            this.cmbFilterArea.Location = new System.Drawing.Point(530, 286);
            this.cmbFilterArea.Name = "cmbFilterArea";
            this.cmbFilterArea.Size = new System.Drawing.Size(150, 21);
            this.cmbFilterArea.TabIndex = 20;
            // 
            // cmbFilterType
            // 
            this.cmbFilterType.FormattingEnabled = true;
            this.cmbFilterType.Location = new System.Drawing.Point(530, 316);
            this.cmbFilterType.Name = "cmbFilterType";
            this.cmbFilterType.Size = new System.Drawing.Size(150, 21);
            this.cmbFilterType.TabIndex = 21;
            // 
            // cmbFilterStatus
            // 
            this.cmbFilterStatus.FormattingEnabled = true;
            this.cmbFilterStatus.Location = new System.Drawing.Point(530, 346);
            this.cmbFilterStatus.Name = "cmbFilterStatus";
            this.cmbFilterStatus.Size = new System.Drawing.Size(150, 21);
            this.cmbFilterStatus.TabIndex = 22;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(680, 316);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 23;
            this.btnFilter.Text = "Filter";
            this.btnFilter.UseVisualStyleBackColor = true;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(450, 290);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Filter by Area";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(450, 320);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 25;
            this.label9.Text = "Filter by Type";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(450, 350);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Filter by Status";
            // 
            // EquipmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 417);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.cmbFilterStatus);
            this.Controls.Add(this.cmbFilterType);
            this.Controls.Add(this.cmbFilterArea);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lblLastMaintenance);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblArea);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.dgvEquipment);
            this.Name = "EquipmentForm";
            this.Text = "Equipment Management";
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
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
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}