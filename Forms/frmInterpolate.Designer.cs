namespace MeteoInfo.Forms
{
    partial class frmInterpolate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInterpolate));
            this.label1 = new System.Windows.Forms.Label();
            this.CB_Method = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TB_YNum = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.TB_XNum = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.TB_YSize = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TB_XSize = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TB_MaxY = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_MinY = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TB_MaxX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_MinX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TB_UndefData = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TB_MinNum = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TB_Radius = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.B_OK = new System.Windows.Forms.Button();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // CB_Method
            // 
            this.CB_Method.AccessibleDescription = null;
            this.CB_Method.AccessibleName = null;
            resources.ApplyResources(this.CB_Method, "CB_Method");
            this.CB_Method.BackgroundImage = null;
            this.CB_Method.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Method.Font = null;
            this.CB_Method.FormattingEnabled = true;
            this.CB_Method.Name = "CB_Method";
            this.CB_Method.SelectedIndexChanged += new System.EventHandler(this.CB_Method_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = null;
            this.groupBox1.Controls.Add(this.TB_YNum);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.TB_XNum);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.TB_YSize);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.TB_XSize);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.TB_MaxY);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.TB_MinY);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.TB_MaxX);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.TB_MinX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // TB_YNum
            // 
            this.TB_YNum.AccessibleDescription = null;
            this.TB_YNum.AccessibleName = null;
            resources.ApplyResources(this.TB_YNum, "TB_YNum");
            this.TB_YNum.BackgroundImage = null;
            this.TB_YNum.Font = null;
            this.TB_YNum.Name = "TB_YNum";
            this.TB_YNum.Validated += new System.EventHandler(this.TB_YNum_Validated);
            // 
            // label11
            // 
            this.label11.AccessibleDescription = null;
            this.label11.AccessibleName = null;
            resources.ApplyResources(this.label11, "label11");
            this.label11.Font = null;
            this.label11.Name = "label11";
            // 
            // TB_XNum
            // 
            this.TB_XNum.AccessibleDescription = null;
            this.TB_XNum.AccessibleName = null;
            resources.ApplyResources(this.TB_XNum, "TB_XNum");
            this.TB_XNum.BackgroundImage = null;
            this.TB_XNum.Font = null;
            this.TB_XNum.Name = "TB_XNum";
            this.TB_XNum.Validated += new System.EventHandler(this.TB_XNum_Validated);
            // 
            // label12
            // 
            this.label12.AccessibleDescription = null;
            this.label12.AccessibleName = null;
            resources.ApplyResources(this.label12, "label12");
            this.label12.Font = null;
            this.label12.Name = "label12";
            // 
            // TB_YSize
            // 
            this.TB_YSize.AccessibleDescription = null;
            this.TB_YSize.AccessibleName = null;
            resources.ApplyResources(this.TB_YSize, "TB_YSize");
            this.TB_YSize.BackgroundImage = null;
            this.TB_YSize.Font = null;
            this.TB_YSize.Name = "TB_YSize";
            this.TB_YSize.Validated += new System.EventHandler(this.TB_YSize_Validated);
            // 
            // label10
            // 
            this.label10.AccessibleDescription = null;
            this.label10.AccessibleName = null;
            resources.ApplyResources(this.label10, "label10");
            this.label10.Font = null;
            this.label10.Name = "label10";
            // 
            // TB_XSize
            // 
            this.TB_XSize.AccessibleDescription = null;
            this.TB_XSize.AccessibleName = null;
            resources.ApplyResources(this.TB_XSize, "TB_XSize");
            this.TB_XSize.BackgroundImage = null;
            this.TB_XSize.Font = null;
            this.TB_XSize.Name = "TB_XSize";
            this.TB_XSize.Validated += new System.EventHandler(this.TB_XSize_Validated);
            // 
            // label9
            // 
            this.label9.AccessibleDescription = null;
            this.label9.AccessibleName = null;
            resources.ApplyResources(this.label9, "label9");
            this.label9.Font = null;
            this.label9.Name = "label9";
            // 
            // TB_MaxY
            // 
            this.TB_MaxY.AccessibleDescription = null;
            this.TB_MaxY.AccessibleName = null;
            resources.ApplyResources(this.TB_MaxY, "TB_MaxY");
            this.TB_MaxY.BackgroundImage = null;
            this.TB_MaxY.Font = null;
            this.TB_MaxY.Name = "TB_MaxY";
            this.TB_MaxY.Validated += new System.EventHandler(this.TB_MaxY_Validated);
            // 
            // label5
            // 
            this.label5.AccessibleDescription = null;
            this.label5.AccessibleName = null;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Font = null;
            this.label5.Name = "label5";
            // 
            // TB_MinY
            // 
            this.TB_MinY.AccessibleDescription = null;
            this.TB_MinY.AccessibleName = null;
            resources.ApplyResources(this.TB_MinY, "TB_MinY");
            this.TB_MinY.BackgroundImage = null;
            this.TB_MinY.Font = null;
            this.TB_MinY.Name = "TB_MinY";
            this.TB_MinY.Validated += new System.EventHandler(this.TB_MinY_Validated);
            // 
            // label4
            // 
            this.label4.AccessibleDescription = null;
            this.label4.AccessibleName = null;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Font = null;
            this.label4.Name = "label4";
            // 
            // TB_MaxX
            // 
            this.TB_MaxX.AccessibleDescription = null;
            this.TB_MaxX.AccessibleName = null;
            resources.ApplyResources(this.TB_MaxX, "TB_MaxX");
            this.TB_MaxX.BackgroundImage = null;
            this.TB_MaxX.Font = null;
            this.TB_MaxX.Name = "TB_MaxX";
            this.TB_MaxX.Validated += new System.EventHandler(this.TB_MaxX_Validated);
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Font = null;
            this.label3.Name = "label3";
            // 
            // TB_MinX
            // 
            this.TB_MinX.AccessibleDescription = null;
            this.TB_MinX.AccessibleName = null;
            resources.ApplyResources(this.TB_MinX, "TB_MinX");
            this.TB_MinX.BackgroundImage = null;
            this.TB_MinX.Font = null;
            this.TB_MinX.Name = "TB_MinX";
            this.TB_MinX.Validated += new System.EventHandler(this.TB_MinX_Validated);
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            // 
            // groupBox2
            // 
            this.groupBox2.AccessibleDescription = null;
            this.groupBox2.AccessibleName = null;
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.BackgroundImage = null;
            this.groupBox2.Controls.Add(this.TB_UndefData);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.TB_MinNum);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.TB_Radius);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.CB_Method);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Font = null;
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // TB_UndefData
            // 
            this.TB_UndefData.AccessibleDescription = null;
            this.TB_UndefData.AccessibleName = null;
            resources.ApplyResources(this.TB_UndefData, "TB_UndefData");
            this.TB_UndefData.BackgroundImage = null;
            this.TB_UndefData.Font = null;
            this.TB_UndefData.Name = "TB_UndefData";
            // 
            // label8
            // 
            this.label8.AccessibleDescription = null;
            this.label8.AccessibleName = null;
            resources.ApplyResources(this.label8, "label8");
            this.label8.Font = null;
            this.label8.Name = "label8";
            // 
            // TB_MinNum
            // 
            this.TB_MinNum.AccessibleDescription = null;
            this.TB_MinNum.AccessibleName = null;
            resources.ApplyResources(this.TB_MinNum, "TB_MinNum");
            this.TB_MinNum.BackgroundImage = null;
            this.TB_MinNum.Font = null;
            this.TB_MinNum.Name = "TB_MinNum";
            // 
            // label7
            // 
            this.label7.AccessibleDescription = null;
            this.label7.AccessibleName = null;
            resources.ApplyResources(this.label7, "label7");
            this.label7.Font = null;
            this.label7.Name = "label7";
            // 
            // TB_Radius
            // 
            this.TB_Radius.AccessibleDescription = null;
            this.TB_Radius.AccessibleName = null;
            resources.ApplyResources(this.TB_Radius, "TB_Radius");
            this.TB_Radius.BackgroundImage = null;
            this.TB_Radius.Font = null;
            this.TB_Radius.Name = "TB_Radius";
            // 
            // label6
            // 
            this.label6.AccessibleDescription = null;
            this.label6.AccessibleName = null;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Font = null;
            this.label6.Name = "label6";
            // 
            // B_OK
            // 
            this.B_OK.AccessibleDescription = null;
            this.B_OK.AccessibleName = null;
            resources.ApplyResources(this.B_OK, "B_OK");
            this.B_OK.BackgroundImage = null;
            this.B_OK.Font = null;
            this.B_OK.Name = "B_OK";
            this.B_OK.UseVisualStyleBackColor = true;
            this.B_OK.Click += new System.EventHandler(this.B_OK_Click);
            // 
            // B_Cancel
            // 
            this.B_Cancel.AccessibleDescription = null;
            this.B_Cancel.AccessibleName = null;
            resources.ApplyResources(this.B_Cancel, "B_Cancel");
            this.B_Cancel.BackgroundImage = null;
            this.B_Cancel.Font = null;
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // frmInterpolate
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.B_Cancel);
            this.Controls.Add(this.B_OK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = null;
            this.Name = "frmInterpolate";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmInterpolate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CB_Method;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TB_MaxY;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TB_MinY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TB_MaxX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TB_MinX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TB_Radius;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TB_MinNum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TB_UndefData;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TB_YNum;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox TB_XNum;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox TB_YSize;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TB_XSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button B_OK;
        private System.Windows.Forms.Button B_Cancel;
    }
}