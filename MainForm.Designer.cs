// File: MainForm.Designer.cs

namespace FacilityManagementSystem
{
    partial class MainForm
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
            // this.btnUserManagement = new System.Windows.Forms.Button();
            // this.btnRoleManagement = new System.Windows.Forms.Button();
            // this.btnEmployee = new System.Windows.Forms.Button();
            this.btnArea = new System.Windows.Forms.Button();
            this.btnEquipmentType = new System.Windows.Forms.Button();
            this.btnEquipment = new System.Windows.Forms.Button();
            this.btnMaintenance = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // // 
            // // btnUserManagement
            // // 
            // this.btnUserManagement.Location = new System.Drawing.Point(12, 12);
            // this.btnUserManagement.Name = "btnUserManagement";
            // this.btnUserManagement.Size = new System.Drawing.Size(150, 23);
            // this.btnUserManagement.TabIndex = 0;
            // this.btnUserManagement.Text = "User Management";
            // this.btnUserManagement.UseVisualStyleBackColor = true;
            // this.btnUserManagement.Click += new System.EventHandler(this.btnUserManagement_Click);
            // // 
            // // btnRoleManagement
            // // 
            // this.btnRoleManagement.Location = new System.Drawing.Point(12, 41);
            // this.btnRoleManagement.Name = "btnRoleManagement";
            // this.btnRoleManagement.Size = new System.Drawing.Size(150, 23);
            // this.btnRoleManagement.TabIndex = 1;
            // this.btnRoleManagement.Text = "Role Management";
            // this.btnRoleManagement.UseVisualStyleBackColor = true;
            // this.btnRoleManagement.Click += new System.EventHandler(this.btnRoleManagement_Click);
            // // 
            // // btnEmployee
            // // 
            // this.btnEmployee.Location = new System.Drawing.Point(12, 70);
            // this.btnEmployee.Name = "btnEmployee";
            // this.btnEmployee.Size = new System.Drawing.Size(150, 23);
            // this.btnEmployee.TabIndex = 2;
            // this.btnEmployee.Text = "Employee Management";
            // this.btnEmployee.UseVisualStyleBackColor = true;
            // this.btnEmployee.Click += new System.EventHandler(this.btnEmployee_Click);
            // 
            // btnArea
            // 
            this.btnArea.Location = new System.Drawing.Point(12, 12);
            this.btnArea.Name = "btnArea";
            this.btnArea.Size = new System.Drawing.Size(150, 23);
            this.btnArea.TabIndex = 3;
            this.btnArea.Text = "Area Management";
            this.btnArea.UseVisualStyleBackColor = true;
            this.btnArea.Click += new System.EventHandler(this.btnArea_Click);
            // 
            // btnEquipmentType
            // 
            this.btnEquipmentType.Location = new System.Drawing.Point(12, 41);
            this.btnEquipmentType.Name = "btnEquipmentType";
            this.btnEquipmentType.Size = new System.Drawing.Size(150, 23);
            this.btnEquipmentType.TabIndex = 4;
            this.btnEquipmentType.Text = "Equipment Type Management";
            this.btnEquipmentType.UseVisualStyleBackColor = true;
            this.btnEquipmentType.Click += new System.EventHandler(this.btnEquipmentType_Click);
            // 
            // btnEquipment
            // 
            this.btnEquipment.Location = new System.Drawing.Point(12, 70);
            this.btnEquipment.Name = "btnEquipment";
            this.btnEquipment.Size = new System.Drawing.Size(150, 23);
            this.btnEquipment.TabIndex = 5;
            this.btnEquipment.Text = "Equipment Management";
            this.btnEquipment.UseVisualStyleBackColor = true;
            this.btnEquipment.Click += new System.EventHandler(this.btnEquipment_Click);
            // 
            // btnMaintenance
            // 
            this.btnMaintenance.Location = new System.Drawing.Point(12, 99);
            this.btnMaintenance.Name = "btnMaintenance";
            this.btnMaintenance.Size = new System.Drawing.Size(150, 23);
            this.btnMaintenance.TabIndex = 6;
            this.btnMaintenance.Text = "Maintenance Management";
            this.btnMaintenance.UseVisualStyleBackColor = true;
            this.btnMaintenance.Click += new System.EventHandler(this.btnMaintenance_Click);
            // 
            // btnReports
            // 
            this.btnReports.Location = new System.Drawing.Point(12, 128);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(150, 23);
            this.btnReports.TabIndex = 7;
            this.btnReports.Text = "Reports";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(174, 261);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnMaintenance);
            this.Controls.Add(this.btnEquipment);
            this.Controls.Add(this.btnEquipmentType);
            this.Controls.Add(this.btnArea);
            this.Controls.Add(this.btnEmployee);
            this.Controls.Add(this.btnRoleManagement);
            this.Controls.Add(this.btnUserManagement);
            this.Name = "MainForm";
            this.Text = "Main Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnUserManagement;
        private System.Windows.Forms.Button btnRoleManagement;
        private System.Windows.Forms.Button btnEmployee;
        private System.Windows.Forms.Button btnArea;
        private System.Windows.Forms.Button btnEquipmentType;
        private System.Windows.Forms.Button btnEquipment;
        private System.Windows.Forms.Button btnMaintenance;
        private System.Windows.Forms.Button btnReports;
    }
}