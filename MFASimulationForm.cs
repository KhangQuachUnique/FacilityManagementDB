// File: MFASimulationForm.cs

using System;
using System.Windows.Forms;

namespace FacilityManagementSystem
{
    public partial class MFASimulationForm : Form
    {
        private string mfaCode = "123456"; // Simulated code

        public MFASimulationForm()
        {
            InitializeComponent();
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (txtMFA.Text == mfaCode)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid MFA code.");
            }
        }
    }
}