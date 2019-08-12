using System.Windows.Forms;

namespace WindowsForms
{
    public static class BaseForm
    {
        public static void SetForm(Form frm, FormBorderStyle formBorderStyle = FormBorderStyle.FixedSingle, FormWindowState formWindowState = FormWindowState.Normal,
                                    FormStartPosition formStartPosition = FormStartPosition.CenterScreen)
        {
            frm.ControlBox = false;
            frm.FormBorderStyle = formBorderStyle;
            frm.WindowState = formWindowState;
            frm.StartPosition = formStartPosition;
        }

        /// <summary>
        /// Return true if you click on Yes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool Yes_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool isYes = true;
            // Display a MsgBox asking the user to save changes or abort.
            if (MessageBox.Show("Do you want to close application?", "Close",
               MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                // Cancel the Closing event from closing the form.
                e.Cancel = true;
                isYes = false;
                // Call method to save file...
            }
            return isYes;
        }

        public static DialogResult Ask_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to close application?", "Close", MessageBoxButtons.YesNoCancel);
            // Display a MsgBox asking the user to save changes or abort.
            if (result == DialogResult.Yes)
            {
                // Cancel the Closing event from closing the form.
                //e.Cancel = true;
                result = DialogResult.Yes;
                // Call method to save file...
            }
            if (result == DialogResult.No)
            {
                //e.Cancel = false;
                result = DialogResult.No;
            }
            if (result == DialogResult.Cancel)
            {
                result = DialogResult.Cancel;
            }
            return result;
        }
    }
}