namespace FacilityManagementSystem
{
    partial class AreaForm
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
            this.dgvAreas = new System.Windows.Forms.DataGridView();
            this.lblAreaNameValue = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnClearSearch = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAreas)).BeginInit();
            this.SuspendLayout();

            this.dgvAreas.Name = "dgvAreas";
            this.lblAreaNameValue.Name = "lblAreaNameValue";
            this.lblAreaNameValue.Visible = false;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Text = "Thêm";
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Text = "Cập Nhật";
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Text = "Xóa";
            this.btnNext.Name = "btnNext";
            this.btnNext.Text = "Tiếp";
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Text = "Trước";
            this.label1.Name = "label1";
            this.label1.AutoSize = true;
            this.label1.Text = "Tên Khu Vực:";
            this.label1.Visible = false;
            this.txtSearch.Name = "txtSearch";
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Text = "Tìm Kiếm";
            this.btnClearSearch.Name = "btnClearSearch";
            this.btnClearSearch.Text = "Xóa Tìm";
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.AutoSize = true;

            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Name = "AreaForm";
            this.Text = "Quản Lý Khu Vực";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAreas)).EndInit();
            this.ResumeLayout(false);
        }

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