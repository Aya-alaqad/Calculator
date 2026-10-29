using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_Calculator
{
    public partial class Form1 : Form
    { 
        private void restoreFocusToTxtb()
        {
            txtb_screen.Focus();
            txtb_screen.SelectionStart = txtb_screen.Text.Length;
            txtb_screen.SelectionLength = 0;
        }
        public Form1()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((keyData <= Keys.NumPad9 && keyData >= Keys.NumPad0) || (keyData <= Keys.D9 && keyData >= Keys.D0) || 
                keyData == Keys.Add || keyData == Keys.Subtract || keyData == Keys.Multiply || keyData == Keys.Divide 
                || keyData == Keys.OemMinus ||keyData==Keys.Decimal|| keyData == (Keys.Shift | Keys.D8) || keyData == (Keys.Shift | Keys.D9)  || 
                keyData == (Keys.Shift | Keys.D0) || keyData == (Keys.Shift | Keys.D5) || keyData == (Keys.Shift|Keys.Oemplus))
                return base.ProcessCmdKey(ref msg, keyData);

            else if(keyData== Keys.Enter)
            {
                showResult();             
            }
            else if(keyData==Keys.Back)
            {
                delete();    
            }

            return true;
        }

       private void delete()
        {
            if (txtb_screen.Text.Length == 0)
                return;
            if(txtb_screen.Text=="NaN")
            {
                txtb_screen.Text = "";
                return;
            }
            StringBuilder sb;
            sb = new StringBuilder(txtb_screen.Text);
            sb.Length--;
            txtb_screen.Text = sb.ToString();
            restoreFocusToTxtb();
        }

        private void showResult()
        {
            txtb_screen.Text = clsCalculatorCore.calculateReslut(txtb_screen.Text).ToString();
            restoreFocusToTxtb();
        }
        private void btn_equal_Click(object sender, EventArgs e)
        {
            showResult();
        }
        
        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            txtb_screen.Text += btn.Tag.ToString();
            restoreFocusToTxtb();
        }

        private void btn_c_Click(object sender, EventArgs e)
        {
            txtb_screen .Text= "";
            restoreFocusToTxtb();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            delete();
        }
        
        private void Form1_Shown(object sender, EventArgs e)
        {
            restoreFocusToTxtb();
        }
    }
}
