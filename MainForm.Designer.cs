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
            this.btnArea.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.btnArea.Location = new System.Drawing.Point(12, 12);
            this.btnArea.Name = "btnArea";
            this.btnArea.Size = new System.Drawing.Size(180, 35);
            this.btnArea.TabIndex = 3;
            this.btnArea.Text = "🏢 Quản Lý Khu Vực";
            this.btnArea.UseVisualStyleBackColor = true;
            this.btnArea.Click += new System.EventHandler(this.btnArea_Click);
            // 
            // btnEquipmentType
            // 
            this.btnEquipmentType.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.btnEquipmentType.Location = new System.Drawing.Point(12, 55);
            this.btnEquipmentType.Name = "btnEquipmentType";
            this.btnEquipmentType.Size = new System.Drawing.Size(180, 35);
            this.btnEquipmentType.TabIndex = 4;
            this.btnEquipmentType.Text = "🏷️ Quản Lý Loại Cơ sở vật chất";
            this.btnEquipmentType.UseVisualStyleBackColor = true;
            this.btnEquipmentType.Click += new System.EventHandler(this.btnEquipmentType_Click);
            // 
            // btnEquipment
            // 
            this.btnEquipment.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.btnEquipment.Location = new System.Drawing.Point(12, 98);
            this.btnEquipment.Name = "btnEquipment";
            this.btnEquipment.Size = new System.Drawing.Size(180, 35);
            this.btnEquipment.TabIndex = 5;
            this.btnEquipment.Text = "⚙️ Quản Lý Cơ Sở Vật Chất";
            this.btnEquipment.UseVisualStyleBackColor = true;
            this.btnEquipment.Click += new System.EventHandler(this.btnEquipment_Click);
            // 
            // btnMaintenance
            // 
            this.btnMaintenance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular);
            this.btnMaintenance.Location = new System.Drawing.Point(12, 141);
            this.btnMaintenance.Name = "btnMaintenance";
            this.btnMaintenance.Size = new System.Drawing.Size(180, 35);
            this.btnMaintenance.TabIndex = 6;
            this.btnMaintenance.Text = "🔧 Quản Lý Bảo Trì";
            this.btnMaintenance.UseVisualStyleBackColor = true;
            this.btnMaintenance.Click += new System.EventHandler(this.btnMaintenance_Click);
            // 
            // btnReports
            // 
            this.btnReports.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnReports.Location = new System.Drawing.Point(12, 184);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(180, 35);
            this.btnReports.TabIndex = 7;
            this.btnReports.Text = "📊 Báo Cáo";
            this.btnReports.UseVisualStyleBackColor = true;
            this.btnReports.Click += new System.EventHandler(this.btnReports_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(210, 300);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
            this.Controls.Add(this.btnReports);
            this.Controls.Add(this.btnMaintenance);
            this.Controls.Add(this.btnEquipment);
            this.Controls.Add(this.btnEquipmentType);
            this.Controls.Add(this.btnArea);
            this.Name = "MainForm";
            this.Text = "Menu Chính";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnArea;
        private System.Windows.Forms.Button btnEquipmentType;
        private System.Windows.Forms.Button btnEquipment;
        private System.Windows.Forms.Button btnMaintenance;
        private System.Windows.Forms.Button btnReports;
    }
}