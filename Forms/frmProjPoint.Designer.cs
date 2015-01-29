namespace MeteoInfo.Forms
{
    partial class frmProjPoint
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
            this.label1 = new System.Windows.Forms.Label();
            this.TB_FromProj = new System.Windows.Forms.TextBox();
            this.TB_ToProj = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.B_Reserve = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_FromX = new System.Windows.Forms.TextBox();
            this.TB_FromY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TB_ToY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_ToX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.B_Projection = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "From Projection:";
            // 
            // TB_FromProj
            // 
            this.TB_FromProj.Location = new System.Drawing.Point(153, 18);
            this.TB_FromProj.Name = "TB_FromProj";
            this.TB_FromProj.Size = new System.Drawing.Size(338, 25);
            this.TB_FromProj.TabIndex = 1;
            // 
            // TB_ToProj
            // 
            this.TB_ToProj.Location = new System.Drawing.Point(153, 58);
            this.TB_ToProj.Name = "TB_ToProj";
            this.TB_ToProj.Size = new System.Drawing.Size(338, 25);
            this.TB_ToProj.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "To Projection:";
            // 
            // B_Reserve
            // 
            this.B_Reserve.Location = new System.Drawing.Point(501, 36);
            this.B_Reserve.Name = "B_Reserve";
            this.B_Reserve.Size = new System.Drawing.Size(92, 27);
            this.B_Reserve.TabIndex = 4;
            this.B_Reserve.Text = "Reverse";
            this.B_Reserve.UseVisualStyleBackColor = true;
            this.B_Reserve.Click += new System.EventHandler(this.B_Reserve_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TB_FromY);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TB_FromX);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(19, 126);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(213, 93);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "From Point";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "X:";
            // 
            // TB_FromX
            // 
            this.TB_FromX.Location = new System.Drawing.Point(35, 25);
            this.TB_FromX.Name = "TB_FromX";
            this.TB_FromX.Size = new System.Drawing.Size(172, 25);
            this.TB_FromX.TabIndex = 1;
            // 
            // TB_FromY
            // 
            this.TB_FromY.Location = new System.Drawing.Point(35, 56);
            this.TB_FromY.Name = "TB_FromY";
            this.TB_FromY.Size = new System.Drawing.Size(172, 25);
            this.TB_FromY.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Y:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TB_ToY);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.TB_ToX);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(376, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(213, 93);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "To Point";
            // 
            // TB_ToY
            // 
            this.TB_ToY.Location = new System.Drawing.Point(35, 56);
            this.TB_ToY.Name = "TB_ToY";
            this.TB_ToY.Size = new System.Drawing.Size(172, 25);
            this.TB_ToY.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 15);
            this.label5.TabIndex = 2;
            this.label5.Text = "Y:";
            // 
            // TB_ToX
            // 
            this.TB_ToX.Location = new System.Drawing.Point(35, 25);
            this.TB_ToX.Name = "TB_ToX";
            this.TB_ToX.Size = new System.Drawing.Size(172, 25);
            this.TB_ToX.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "X:";
            // 
            // B_Projection
            // 
            this.B_Projection.Location = new System.Drawing.Point(253, 164);
            this.B_Projection.Name = "B_Projection";
            this.B_Projection.Size = new System.Drawing.Size(102, 27);
            this.B_Projection.TabIndex = 7;
            this.B_Projection.Text = "Projection";
            this.B_Projection.UseVisualStyleBackColor = true;
            this.B_Projection.Click += new System.EventHandler(this.B_Projection_Click);
            // 
            // frmProjPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 234);
            this.Controls.Add(this.B_Projection);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.B_Reserve);
            this.Controls.Add(this.TB_ToProj);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TB_FromProj);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProjPoint";
            this.Text = "Point Projection";
            this.Load += new System.EventHandler(this.frmProjPoint_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TB_FromProj;
        private System.Windows.Forms.TextBox TB_ToProj;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button B_Reserve;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TB_FromY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_FromX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TB_ToY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_ToX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button B_Projection;
    }
}