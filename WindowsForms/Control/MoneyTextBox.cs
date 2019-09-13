using System;
using System.Windows.Forms;
using System.Globalization;

namespace WindowsForms.Control
{
    public partial class MoneyTextBox : UserControl
    {
        public MoneyTextBox()
        {
            InitializeComponent();
        }

        public string GetText()
        {
            string getString = string.Empty;
            if (!string.IsNullOrEmpty(txtBox.Text) ||
                !string.IsNullOrWhiteSpace(txtBox.Text))
            {
                var s = decimal.Parse(txtBox.Text, NumberStyles.Currency);
                getString = s.ToString();
            }
            return getString;
        }

        public void SetText(string text)
        {
            if (!string.IsNullOrWhiteSpace(text))
                txtBox.Text = string.Format("{0:C}", decimal.Parse(text));
        }

        private void TxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            decimal x;
            char ch = e.KeyChar;
            if (ch == (char)8) //backspace
                e.Handled = false;
            else if (!char.IsDigit(ch) && ch != '.' ||
                    !Decimal.TryParse(txtBox.Text + ch, out x))
                e.Handled = true;
        }

        private void TxtBox_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBox.Text) ||
                   !string.IsNullOrWhiteSpace(txtBox.Text))
                txtBox.Text = string.Format("{0:C}", decimal.Parse(txtBox.Text));
        }

        private void TxtBox_Enter(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBox.Text) ||
                   !string.IsNullOrWhiteSpace(txtBox.Text))
            {
                var s = decimal.Parse(txtBox.Text, NumberStyles.Currency);
                txtBox.Text = s.ToString();
            }
        }
    }
}