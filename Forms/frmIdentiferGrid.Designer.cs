namespace MeteoInfo.Forms
{
    partial class frmIdentiferGrid
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
            this.Lab_I = new System.Windows.Forms.Label();
            this.Lab_J = new System.Windows.Forms.Label();
            this.Lab_CellValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lab_I
            // 
            this.Lab_I.AutoSize = true;
            this.Lab_I.Location = new System.Drawing.Point(25, 22);
            this.Lab_I.Name = "Lab_I";
            this.Lab_I.Size = new System.Drawing.Size(23, 15);
            this.Lab_I.TabIndex = 0;
            this.Lab_I.Text = "I=";
            // 
            // Lab_J
            // 
            this.Lab_J.AutoSize = true;
            this.Lab_J.Location = new System.Drawing.Point(120, 22);
            this.Lab_J.Name = "Lab_J";
            this.Lab_J.Size = new System.Drawing.Size(23, 15);
            this.Lab_J.TabIndex = 1;
            this.Lab_J.Text = "J=";
            // 
            // Lab_CellValue
            // 
            this.Lab_CellValue.AutoSize = true;
            this.Lab_CellValue.Location = new System.Drawing.Point(25, 68);
            this.Lab_CellValue.Name = "Lab_CellValue";
            this.Lab_CellValue.Size = new System.Drawing.Size(95, 15);
            this.Lab_CellValue.TabIndex = 2;
            this.Lab_CellValue.Text = "Cell Value:";
            // 
            // frmIdentiferGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 111);
            this.Controls.Add(this.Lab_CellValue);
            this.Controls.Add(this.Lab_J);
            this.Controls.Add(this.Lab_I);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmIdentiferGrid";
            this.Text = "Identifer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Lab_I;
        internal System.Windows.Forms.Label Lab_J;
        internal System.Windows.Forms.Label Lab_CellValue;
    }
}