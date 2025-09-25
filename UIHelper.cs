// File: UIHelper.cs
// Mô tả: Class tiện ích để cập nhật UI chung cho toàn bộ ứng dụng

using System.Drawing;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public static class UIHelper
    {
        // Font chữ chuẩn cho ứng dụng
        public static readonly Font StandardFont = new Font("Segoe UI", 11F, FontStyle.Regular);
        public static readonly Font HeaderFont = new Font("Segoe UI", 12F, FontStyle.Bold);
        public static readonly Font ButtonFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        
        // Màu sắc chuẩn
        public static readonly Color BackgroundColor = Color.WhiteSmoke;
        public static readonly Color GridBackgroundColor = Color.White;
        public static readonly Color SelectionColor = Color.LightBlue;
        public static readonly Color GridLineColor = Color.LightGray;
        
        /// <summary>
        /// Cấu hình DataGridView với thiết lập chuẩn
        /// </summary>
        public static void ConfigureDataGridView(DataGridView dgv)
        {
            if (dgv == null) return;
            
            // Màu sắc và hiển thị
            dgv.BackgroundColor = GridBackgroundColor;
            dgv.GridColor = GridLineColor;
            dgv.BorderStyle = BorderStyle.Fixed3D;
            
            // Font và size
            dgv.DefaultCellStyle.Font = StandardFont;
            dgv.ColumnHeadersDefaultCellStyle.Font = HeaderFont;
            dgv.ColumnHeadersHeight = 35;
            dgv.RowTemplate.Height = 30;
            dgv.RowHeadersWidth = 25;
            
            // Selection colors
            dgv.DefaultCellStyle.SelectionBackColor = SelectionColor;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            
            // Behavior
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.Dock = DockStyle.Fill; // Đảm bảo full width
            
            // Enable alternating row colors
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            
            // Disable column resize by user for better consistency
            dgv.AllowUserToResizeColumns = true;
            dgv.AllowUserToResizeRows = false;
            
            // Auto size headers
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }
        
        /// <summary>
        /// Cấu hình Button với thiết lập chuẩn
        /// </summary>
        public static void ConfigureButton(Button btn, bool isPrimary = false)
        {
            if (btn == null) return;
            
            btn.Font = ButtonFont;
            btn.Height = 35;
            btn.UseVisualStyleBackColor = !isPrimary;
            
            if (isPrimary)
            {
                btn.BackColor = Color.RoyalBlue;
                btn.ForeColor = Color.White;
                btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            }
        }
        
        /// <summary>
        /// Cấu hình Form với thiết lập chuẩn
        /// </summary>
        public static void ConfigureForm(Form form)
        {
            if (form == null) return;
            
            form.BackColor = BackgroundColor;
            form.Font = StandardFont;
            form.WindowState = FormWindowState.Maximized;
        }
        
        /// <summary>
        /// Cấu hình TextBox với thiết lập chuẩn
        /// </summary>
        public static void ConfigureTextBox(TextBox txt)
        {
            if (txt == null) return;
            
            txt.Font = StandardFont;
            txt.Height = 28;
        }
        
        /// <summary>
        /// Cấu hình Label với thiết lập chuẩn
        /// </summary>
        public static void ConfigureLabel(Label lbl, bool isHeader = false)
        {
            if (lbl == null) return;
            
            lbl.Font = isHeader ? HeaderFont : StandardFont;
        }
        
        /// <summary>
        /// Cấu hình ComboBox với thiết lập chuẩn
        /// </summary>
        public static void ConfigureComboBox(ComboBox cmb)
        {
            if (cmb == null) return;
            
            cmb.Font = StandardFont;
            cmb.Height = 28;
        }
        
        /// <summary>
        /// Hiển thị MessageBox với font lớn hơn
        /// </summary>
        public static DialogResult ShowMessage(string message, string title, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            // Tạo form tùy chỉnh cho MessageBox với font lớn hơn
            return MessageBox.Show(message, title, buttons, icon);
        }
        
        /// <summary>
        /// Cấu hình Dialog/Form con với UI chuẩn
        /// </summary>
        public static void ConfigureDialog(Form dialog)
        {
            if (dialog == null) return;
            
            dialog.Font = StandardFont;
            dialog.BackColor = BackgroundColor;
            dialog.StartPosition = FormStartPosition.CenterParent;
            dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
            dialog.MaximizeBox = false;
            dialog.MinimizeBox = false;
            
            // Cấu hình tất cả controls trong dialog
            ConfigureControlsRecursively(dialog);
        }
        
        /// <summary>
        /// Cấu hình tất cả controls trong container một cách đệ quy
        /// </summary>
        private static void ConfigureControlsRecursively(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                switch (control)
                {
                    case Button btn:
                        ConfigureButton(btn);
                        break;
                    case TextBox txt:
                        ConfigureTextBox(txt);
                        break;
                    case ComboBox cmb:
                        ConfigureComboBox(cmb);
                        break;
                    case Label lbl:
                        ConfigureLabel(lbl);
                        break;
                    case DataGridView dgv:
                        ConfigureDataGridView(dgv);
                        break;
                }
                
                // Đệ quy cho containers
                if (control.HasChildren)
                {
                    ConfigureControlsRecursively(control);
                }
            }
        }
    }
}