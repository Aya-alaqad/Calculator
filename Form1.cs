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
        private bool isErrorDisplayed = false;
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

        private void clearError()
        {
            if (isErrorDisplayed)
            {
                txtb_screen.Clear();
            }
            isErrorDisplayed = false;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if ((keyData <= Keys.NumPad9 && keyData >= Keys.NumPad0) || (keyData <= Keys.D9 && keyData >= Keys.D0) ||
                keyData == Keys.Add || keyData == Keys.Subtract || keyData == Keys.Multiply || keyData == Keys.Divide
                || keyData == Keys.OemMinus || keyData == Keys.Decimal || keyData == (Keys.Shift | Keys.D8) || keyData == (Keys.Shift | Keys.D9) ||
                keyData == (Keys.Shift | Keys.D0) || keyData == (Keys.Shift | Keys.D5) || keyData == (Keys.Shift | Keys.Oemplus))
            {
                clearError();
                return base.ProcessCmdKey(ref msg, keyData);
            }

            else if (keyData == Keys.Enter)
            {
                showResult();
            }
            else if (keyData == Keys.Back)
            {
                delete();
            }

            return true;
        }

       private void delete()
       {
            clearError();
            if(txtb_screen.Text.Length!=0)
            {
                txtb_screen.Text = txtb_screen.Text.Remove(txtb_screen.Text.Length - 1);
            }
            restoreFocusToTxtb();
       }

        private void displayResult(clsCalculatorCore.enCalculatorError error, float res)
        {
            switch(error)
            {
                case clsCalculatorCore.enCalculatorError.None:
                    txtb_screen.Text = res.ToString();
                    break;
                case clsCalculatorCore.enCalculatorError.syntaxError:
                    txtb_screen.Text = "Syntax Error";
                    isErrorDisplayed = true;
                    break;
                case clsCalculatorCore.enCalculatorError.zeroDivision:
                    txtb_screen.Text = "Zero Division Error";
                    isErrorDisplayed = true;
                    break;
                case clsCalculatorCore.enCalculatorError.emptyExpression:
                    break;
            }
        }
        private void showResult()
        {
            float res=0;
            clsCalculatorCore.enCalculatorError error= clsCalculatorCore.calculateReslut(txtb_screen.Text,ref res);
            displayResult(error, res);
            restoreFocusToTxtb();
        }
        private void btn_equal_Click(object sender, EventArgs e)
        {
            showResult();
        }
        
        private void button_Click(object sender, EventArgs e)
        {
            clearError();
            Button btn = (Button)sender;
            txtb_screen.Text += btn.Tag.ToString();
            restoreFocusToTxtb();
        }

        private void btn_c_Click(object sender, EventArgs e)
        {
            txtb_screen .Text= "";
            isErrorDisplayed = false;
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
