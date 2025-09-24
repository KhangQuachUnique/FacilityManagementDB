// File: EquipmentTypeForm.Designer.cs

namespace FacilityManagementSystem
{
    partial class EquipmentTypeForm
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
            this.dgvTypes = new System.Windows.Forms.DataGridView();
            this.txtTypeName = new System.Windows.Forms.TextBox();
            this.lblTypeNameValue = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTypes
            // 
            this.dgvTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTypes.Location = new System.Drawing.Point(12, 12);
            this.dgvTypes.Name = "dgvTypes";
            this.dgvTypes.Size = new System.Drawing.Size(760, 200);
            this.dgvTypes.TabIndex = 0;
            this.dgvTypes.SelectionChanged += new System.EventHandler(this.dgvTypes_SelectionChanged);
            // 
            // txtTypeName (hidden, replaced by label)
            // 
            this.txtTypeName.Location = new System.Drawing.Point(100, 220);
            this.txtTypeName.Name = "txtTypeName";
            this.txtTypeName.Size = new System.Drawing.Size(150, 20);
            this.txtTypeName.TabIndex = 1;
            this.txtTypeName.Visible = false;

            // 
            // lblTypeNameValue
            // 
            this.lblTypeNameValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTypeNameValue.Location = new System.Drawing.Point(100, 220);
            this.lblTypeNameValue.Name = "lblTypeNameValue";
            this.lblTypeNameValue.Size = new System.Drawing.Size(150, 20);
            this.lblTypeNameValue.TabIndex = 8;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(300, 220);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(300, 249);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(300, 278);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(600, 220);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(500, 220);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 6;
            this.btnPrev.Text = "Prev";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Type Name";
            // 
            // EquipmentTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 341);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTypeNameValue);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtTypeName);
            this.Controls.Add(this.dgvTypes);
            this.Name = "EquipmentTypeForm";
            this.Text = "Equipment Type Management";
            ((System.ComponentModel.ISupportInitialize)(this.dgvTypes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTypes;
        private System.Windows.Forms.TextBox txtTypeName;
    private System.Windows.Forms.Label lblTypeNameValue;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Label label1;
    }
}