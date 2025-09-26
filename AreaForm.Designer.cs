// File: AreaForm.Designer.cs

namespace FacilityManagementSystem
{
    partial class AreaForm
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
            dgvAreas = new DataGridView();
            lblAreaNameValue = new Label();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnNext = new Button();
            btnPrev = new Button();
            label1 = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnClearSearch = new Button();
            lblSearch = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvAreas).BeginInit();
            SuspendLayout();
            // 
            // dgvAreas
            // 
            dgvAreas.BackgroundColor = SystemColors.Control;
            dgvAreas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAreas.Location = new Point(23, 80);
            dgvAreas.Margin = new Padding(3, 4, 3, 4);
            dgvAreas.Name = "dgvAreas";
            dgvAreas.RowHeadersWidth = 51;
            dgvAreas.Size = new Size(1051, 373);
            dgvAreas.TabIndex = 0;
            dgvAreas.SelectionChanged += dgvAreas_SelectionChanged;
            // 
            // lblAreaNameValue
            // 
            lblAreaNameValue.BorderStyle = BorderStyle.FixedSingle;
            lblAreaNameValue.Font = new Font("Segoe UI", 11F);
            lblAreaNameValue.Location = new Point(149, 481);
            lblAreaNameValue.Name = "lblAreaNameValue";
            lblAreaNameValue.Size = new Size(343, 35);
            lblAreaNameValue.TabIndex = 8;
            lblAreaNameValue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnAdd
            // 
            btnAdd.Font = new Font("Segoe UI", 11F);
            btnAdd.Location = new Point(514, 477);
            btnAdd.Margin = new Padding(3, 4, 3, 4);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(114, 43);
            btnAdd.TabIndex = 2;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Font = new Font("Segoe UI", 11F);
            btnUpdate.Location = new Point(640, 477);
            btnUpdate.Margin = new Padding(3, 4, 3, 4);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(114, 43);
            btnUpdate.TabIndex = 3;
            btnUpdate.Text = "Cập Nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.Font = new Font("Segoe UI", 11F);
            btnDelete.Location = new Point(766, 477);
            btnDelete.Margin = new Padding(3, 4, 3, 4);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(114, 43);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnNext
            // 
            btnNext.Font = new Font("Segoe UI", 11F);
            btnNext.Location = new Point(960, 17);
            btnNext.Margin = new Padding(3, 4, 3, 4);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(114, 43);
            btnNext.TabIndex = 5;
            btnNext.Text = "Tiếp";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // btnPrev
            // 
            btnPrev.Font = new Font("Segoe UI", 11F);
            btnPrev.Location = new Point(834, 17);
            btnPrev.Margin = new Padding(3, 4, 3, 4);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(114, 43);
            btnPrev.TabIndex = 6;
            btnPrev.Text = "Trước";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.Location = new Point(23, 484);
            label1.Name = "label1";
            label1.Size = new Size(120, 25);
            label1.TabIndex = 7;
            label1.Text = "Tên Khu Vực:";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 11F);
            txtSearch.Location = new Point(126, 23);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(342, 32);
            txtSearch.TabIndex = 11;
            txtSearch.KeyDown += txtSearch_KeyDown;
            // 
            // btnSearch
            // 
            btnSearch.Font = new Font("Segoe UI", 11F);
            btnSearch.Location = new Point(491, 20);
            btnSearch.Margin = new Padding(3, 4, 3, 4);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(114, 40);
            btnSearch.TabIndex = 12;
            btnSearch.Text = "Tìm Kiếm";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnClearSearch
            // 
            btnClearSearch.Font = new Font("Segoe UI", 11F);
            btnClearSearch.Location = new Point(617, 20);
            btnClearSearch.Margin = new Padding(3, 4, 3, 4);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Size = new Size(114, 40);
            btnClearSearch.TabIndex = 13;
            btnClearSearch.Text = "Xóa Tìm";
            btnClearSearch.UseVisualStyleBackColor = true;
            btnClearSearch.Click += btnClearSearch_Click;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 11F);
            lblSearch.Location = new Point(23, 27);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(94, 25);
            lblSearch.TabIndex = 10;
            lblSearch.Text = "Tìm Kiếm:";
            // 
            // AreaForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1097, 560);
            Controls.Add(btnClearSearch);
            Controls.Add(btnSearch);
            Controls.Add(txtSearch);
            Controls.Add(lblSearch);
            Controls.Add(label1);
            Controls.Add(lblAreaNameValue);
            Controls.Add(btnPrev);
            Controls.Add(btnNext);
            Controls.Add(btnDelete);
            Controls.Add(btnUpdate);
            Controls.Add(btnAdd);
            Controls.Add(dgvAreas);
            Font = new Font("Segoe UI", 9F);
            Margin = new Padding(3, 4, 3, 4);
            Name = "AreaForm";
            Text = "Quản Lý Khu Vực";
            ((System.ComponentModel.ISupportInitialize)dgvAreas).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAreas;
    private System.Windows.Forms.Label lblAreaNameValue;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClearSearch;
        private System.Windows.Forms.Label lblSearch;
    }
}