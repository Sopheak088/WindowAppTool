﻿namespace WindowsForms.Control
{
    partial class MoneyTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtBox
            // 
            this.txtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox.Location = new System.Drawing.Point(0, 0);
            this.txtBox.Name = "txtBox";
            this.txtBox.Size = new System.Drawing.Size(164, 20);
            this.txtBox.TabIndex = 0;
            this.txtBox.Enter += new System.EventHandler(this.TxtBox_Enter);
            this.txtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBox_KeyPress);
            this.txtBox.Leave += new System.EventHandler(this.TxtBox_Leave);
            // 
            // MoneyTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtBox);
            this.Name = "MoneyTextBox";
            this.Size = new System.Drawing.Size(164, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBox;
    }
}
