namespace MeteoInfo.Forms
{
	partial class frmTextEditor
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
			if (disposing && (components != null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTextEditor));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.fileTabs = new System.Windows.Forms.TabControl();
            this.TB_Output = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.TSMI_File = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_NewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_SaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_CloseFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.TSMI_Exit = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_EditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFindAgain = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFindAgainReverse = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuToggleBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGoToNextBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGoToPrevBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSplitTextArea = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuShowSpacesTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShowNewlines = new System.Windows.Forms.ToolStripMenuItem();
            this.menuShowLineNumbers = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHighlightCurrentRow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBracketMatchingStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEnableVirtualSpace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuSetTabSize = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSetFont = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TSB_NewFile = new System.Windows.Forms.ToolStripButton();
            this.TSB_OpenFile = new System.Windows.Forms.ToolStripButton();
            this.TSB_CloseFile = new System.Windows.Forms.ToolStripButton();
            this.TSB_SaveFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_Undo = new System.Windows.Forms.ToolStripButton();
            this.TSB_Redo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.TSB_RunScript = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.fileTabs);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TB_Output);
            // 
            // fileTabs
            // 
            resources.ApplyResources(this.fileTabs, "fileTabs");
            this.fileTabs.Name = "fileTabs";
            this.fileTabs.SelectedIndex = 0;
            this.fileTabs.TabStop = false;
            // 
            // TB_Output
            // 
            this.TB_Output.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.TB_Output, "TB_Output");
            this.TB_Output.Name = "TB_Output";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_File,
            this.TSMI_Edit,
            this.optionsToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // TSMI_File
            // 
            this.TSMI_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_NewFile,
            this.TSMI_OpenFile,
            this.TSMI_SaveFile,
            this.TSMI_SaveAs,
            this.TSMI_CloseFile,
            this.toolStripSeparator1,
            this.TSMI_Exit});
            this.TSMI_File.Name = "TSMI_File";
            resources.ApplyResources(this.TSMI_File, "TSMI_File");
            // 
            // TSMI_NewFile
            // 
            resources.ApplyResources(this.TSMI_NewFile, "TSMI_NewFile");
            this.TSMI_NewFile.Name = "TSMI_NewFile";
            this.TSMI_NewFile.Click += new System.EventHandler(this.menuFileNew_Click);
            // 
            // TSMI_OpenFile
            // 
            resources.ApplyResources(this.TSMI_OpenFile, "TSMI_OpenFile");
            this.TSMI_OpenFile.Name = "TSMI_OpenFile";
            this.TSMI_OpenFile.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // TSMI_SaveFile
            // 
            resources.ApplyResources(this.TSMI_SaveFile, "TSMI_SaveFile");
            this.TSMI_SaveFile.Name = "TSMI_SaveFile";
            this.TSMI_SaveFile.Click += new System.EventHandler(this.menuFileSave_Click);
            // 
            // TSMI_SaveAs
            // 
            this.TSMI_SaveAs.Name = "TSMI_SaveAs";
            resources.ApplyResources(this.TSMI_SaveAs, "TSMI_SaveAs");
            this.TSMI_SaveAs.Click += new System.EventHandler(this.menuFileSaveAs_Click);
            // 
            // TSMI_CloseFile
            // 
            resources.ApplyResources(this.TSMI_CloseFile, "TSMI_CloseFile");
            this.TSMI_CloseFile.Name = "TSMI_CloseFile";
            this.TSMI_CloseFile.Click += new System.EventHandler(this.menuFileClose_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // TSMI_Exit
            // 
            this.TSMI_Exit.Name = "TSMI_Exit";
            resources.ApplyResources(this.TSMI_Exit, "TSMI_Exit");
            this.TSMI_Exit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // TSMI_Edit
            // 
            this.TSMI_Edit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_EditCut,
            this.menuEditCopy,
            this.menuEditPaste,
            this.menuEditDelete,
            this.toolStripSeparator2,
            this.menuEditFind,
            this.menuEditReplace,
            this.menuFindAgain,
            this.menuFindAgainReverse,
            this.toolStripSeparator5,
            this.menuToggleBookmark,
            this.menuGoToNextBookmark,
            this.menuGoToPrevBookmark});
            this.TSMI_Edit.Name = "TSMI_Edit";
            resources.ApplyResources(this.TSMI_Edit, "TSMI_Edit");
            // 
            // TSMI_EditCut
            // 
            resources.ApplyResources(this.TSMI_EditCut, "TSMI_EditCut");
            this.TSMI_EditCut.Name = "TSMI_EditCut";
            this.TSMI_EditCut.Click += new System.EventHandler(this.menuEditCut_Click);
            // 
            // menuEditCopy
            // 
            resources.ApplyResources(this.menuEditCopy, "menuEditCopy");
            this.menuEditCopy.Name = "menuEditCopy";
            this.menuEditCopy.Click += new System.EventHandler(this.menuEditCopy_Click);
            // 
            // menuEditPaste
            // 
            resources.ApplyResources(this.menuEditPaste, "menuEditPaste");
            this.menuEditPaste.Name = "menuEditPaste";
            this.menuEditPaste.Click += new System.EventHandler(this.menuEditPaste_Click);
            // 
            // menuEditDelete
            // 
            resources.ApplyResources(this.menuEditDelete, "menuEditDelete");
            this.menuEditDelete.Name = "menuEditDelete";
            this.menuEditDelete.Click += new System.EventHandler(this.menuEditDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // menuEditFind
            // 
            resources.ApplyResources(this.menuEditFind, "menuEditFind");
            this.menuEditFind.Name = "menuEditFind";
            this.menuEditFind.Click += new System.EventHandler(this.menuEditFind_Click);
            // 
            // menuEditReplace
            // 
            this.menuEditReplace.Name = "menuEditReplace";
            resources.ApplyResources(this.menuEditReplace, "menuEditReplace");
            this.menuEditReplace.Click += new System.EventHandler(this.menuEditReplace_Click);
            // 
            // menuFindAgain
            // 
            resources.ApplyResources(this.menuFindAgain, "menuFindAgain");
            this.menuFindAgain.Name = "menuFindAgain";
            this.menuFindAgain.Click += new System.EventHandler(this.menuFindAgain_Click);
            // 
            // menuFindAgainReverse
            // 
            this.menuFindAgainReverse.Name = "menuFindAgainReverse";
            resources.ApplyResources(this.menuFindAgainReverse, "menuFindAgainReverse");
            this.menuFindAgainReverse.Click += new System.EventHandler(this.menuFindAgainReverse_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // menuToggleBookmark
            // 
            this.menuToggleBookmark.Name = "menuToggleBookmark";
            resources.ApplyResources(this.menuToggleBookmark, "menuToggleBookmark");
            this.menuToggleBookmark.Click += new System.EventHandler(this.menuToggleBookmark_Click);
            // 
            // menuGoToNextBookmark
            // 
            this.menuGoToNextBookmark.Name = "menuGoToNextBookmark";
            resources.ApplyResources(this.menuGoToNextBookmark, "menuGoToNextBookmark");
            this.menuGoToNextBookmark.Click += new System.EventHandler(this.menuGoToNextBookmark_Click);
            // 
            // menuGoToPrevBookmark
            // 
            this.menuGoToPrevBookmark.Name = "menuGoToPrevBookmark";
            resources.ApplyResources(this.menuGoToPrevBookmark, "menuGoToPrevBookmark");
            this.menuGoToPrevBookmark.Click += new System.EventHandler(this.menuGoToPrevBookmark_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSplitTextArea,
            this.toolStripSeparator3,
            this.menuShowSpacesTabs,
            this.menuShowNewlines,
            this.menuShowLineNumbers,
            this.menuHighlightCurrentRow,
            this.menuBracketMatchingStyle,
            this.menuEnableVirtualSpace,
            this.toolStripSeparator4,
            this.menuSetTabSize,
            this.menuSetFont});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            // 
            // menuSplitTextArea
            // 
            resources.ApplyResources(this.menuSplitTextArea, "menuSplitTextArea");
            this.menuSplitTextArea.Name = "menuSplitTextArea";
            this.menuSplitTextArea.Click += new System.EventHandler(this.menuSplitTextArea_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // menuShowSpacesTabs
            // 
            this.menuShowSpacesTabs.Name = "menuShowSpacesTabs";
            resources.ApplyResources(this.menuShowSpacesTabs, "menuShowSpacesTabs");
            this.menuShowSpacesTabs.Click += new System.EventHandler(this.menuShowSpaces_Click);
            // 
            // menuShowNewlines
            // 
            this.menuShowNewlines.Name = "menuShowNewlines";
            resources.ApplyResources(this.menuShowNewlines, "menuShowNewlines");
            this.menuShowNewlines.Click += new System.EventHandler(this.menuShowNewlines_Click);
            // 
            // menuShowLineNumbers
            // 
            this.menuShowLineNumbers.Name = "menuShowLineNumbers";
            resources.ApplyResources(this.menuShowLineNumbers, "menuShowLineNumbers");
            this.menuShowLineNumbers.Click += new System.EventHandler(this.menuShowLineNumbers_Click);
            // 
            // menuHighlightCurrentRow
            // 
            resources.ApplyResources(this.menuHighlightCurrentRow, "menuHighlightCurrentRow");
            this.menuHighlightCurrentRow.Name = "menuHighlightCurrentRow";
            this.menuHighlightCurrentRow.Click += new System.EventHandler(this.menuHighlightCurrentRow_Click);
            // 
            // menuBracketMatchingStyle
            // 
            this.menuBracketMatchingStyle.Name = "menuBracketMatchingStyle";
            resources.ApplyResources(this.menuBracketMatchingStyle, "menuBracketMatchingStyle");
            this.menuBracketMatchingStyle.Click += new System.EventHandler(this.menuBracketMatchingStyle_Click);
            // 
            // menuEnableVirtualSpace
            // 
            this.menuEnableVirtualSpace.Name = "menuEnableVirtualSpace";
            resources.ApplyResources(this.menuEnableVirtualSpace, "menuEnableVirtualSpace");
            this.menuEnableVirtualSpace.Click += new System.EventHandler(this.menuEnableVirtualSpace_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // menuSetTabSize
            // 
            this.menuSetTabSize.Name = "menuSetTabSize";
            resources.ApplyResources(this.menuSetTabSize, "menuSetTabSize");
            this.menuSetTabSize.Click += new System.EventHandler(this.menuSetTabSize_Click);
            // 
            // menuSetFont
            // 
            resources.ApplyResources(this.menuSetFont, "menuSetFont");
            this.menuSetFont.Name = "menuSetFont";
            this.menuSetFont.Click += new System.EventHandler(this.menuSetFont_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.Multiselect = true;
            // 
            // fontDialog
            // 
            this.fontDialog.AllowVerticalFonts = false;
            this.fontDialog.ShowEffects = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSB_NewFile,
            this.TSB_OpenFile,
            this.TSB_CloseFile,
            this.TSB_SaveFile,
            this.toolStripSeparator6,
            this.TSB_Undo,
            this.TSB_Redo,
            this.toolStripSeparator7,
            this.TSB_RunScript});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // TSB_NewFile
            // 
            this.TSB_NewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_NewFile, "TSB_NewFile");
            this.TSB_NewFile.Name = "TSB_NewFile";
            this.TSB_NewFile.Click += new System.EventHandler(this.TSB_NewFile_Click);
            // 
            // TSB_OpenFile
            // 
            this.TSB_OpenFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_OpenFile, "TSB_OpenFile");
            this.TSB_OpenFile.Name = "TSB_OpenFile";
            this.TSB_OpenFile.Click += new System.EventHandler(this.TSB_OpenFile_Click);
            // 
            // TSB_CloseFile
            // 
            this.TSB_CloseFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_CloseFile, "TSB_CloseFile");
            this.TSB_CloseFile.Name = "TSB_CloseFile";
            this.TSB_CloseFile.Click += new System.EventHandler(this.TSB_CloseFile_Click);
            // 
            // TSB_SaveFile
            // 
            this.TSB_SaveFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_SaveFile, "TSB_SaveFile");
            this.TSB_SaveFile.Name = "TSB_SaveFile";
            this.TSB_SaveFile.Click += new System.EventHandler(this.TSB_SaveFile_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            resources.ApplyResources(this.toolStripSeparator6, "toolStripSeparator6");
            // 
            // TSB_Undo
            // 
            this.TSB_Undo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_Undo, "TSB_Undo");
            this.TSB_Undo.Name = "TSB_Undo";
            this.TSB_Undo.Click += new System.EventHandler(this.TSB_Undo_Click);
            // 
            // TSB_Redo
            // 
            this.TSB_Redo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_Redo, "TSB_Redo");
            this.TSB_Redo.Name = "TSB_Redo";
            this.TSB_Redo.Click += new System.EventHandler(this.TSB_Redo_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            resources.ApplyResources(this.toolStripSeparator7, "toolStripSeparator7");
            // 
            // TSB_RunScript
            // 
            this.TSB_RunScript.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.TSB_RunScript, "TSB_RunScript");
            this.TSB_RunScript.Name = "TSB_RunScript";
            this.TSB_RunScript.Click += new System.EventHandler(this.TSB_RunScript_Click);
            // 
            // frmTextEditor
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmTextEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextEditor_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextEditorForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextEditorForm_DragEnter);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem TSMI_File;
		private System.Windows.Forms.ToolStripMenuItem TSMI_OpenFile;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.ToolStripMenuItem TSMI_SaveFile;
		private System.Windows.Forms.TabControl fileTabs;
		private System.Windows.Forms.ToolStripMenuItem TSMI_NewFile;
		private System.Windows.Forms.ToolStripMenuItem TSMI_SaveAs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Exit;
		private System.Windows.Forms.ToolStripMenuItem TSMI_Edit;
		private System.Windows.Forms.ToolStripMenuItem TSMI_EditCut;
		private System.Windows.Forms.ToolStripMenuItem menuEditCopy;
		private System.Windows.Forms.ToolStripMenuItem menuEditPaste;
		private System.Windows.Forms.ToolStripMenuItem menuEditDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem menuEditFind;
		private System.Windows.Forms.ToolStripMenuItem menuEditReplace;
		private System.Windows.Forms.ToolStripMenuItem TSMI_CloseFile;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.ToolStripMenuItem menuFindAgain;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem menuSplitTextArea;
		private System.Windows.Forms.ToolStripMenuItem menuShowSpacesTabs;
		private System.Windows.Forms.ToolStripMenuItem menuShowNewlines;
		private System.Windows.Forms.ToolStripMenuItem menuHighlightCurrentRow;
		private System.Windows.Forms.ToolStripMenuItem menuBracketMatchingStyle;
		private System.Windows.Forms.ToolStripMenuItem menuEnableVirtualSpace;
		private System.Windows.Forms.ToolStripMenuItem menuShowLineNumbers;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem menuFindAgainReverse;
		private System.Windows.Forms.ToolStripMenuItem menuSetTabSize;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem menuSetFont;
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem menuToggleBookmark;
		private System.Windows.Forms.ToolStripMenuItem menuGoToNextBookmark;
		private System.Windows.Forms.ToolStripMenuItem menuGoToPrevBookmark;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton TSB_RunScript;
        private System.Windows.Forms.ToolStripButton TSB_NewFile;
        private System.Windows.Forms.ToolStripButton TSB_OpenFile;
        private System.Windows.Forms.ToolStripButton TSB_SaveFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton TSB_Undo;
        private System.Windows.Forms.ToolStripButton TSB_Redo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox TB_Output;
        private System.Windows.Forms.ToolStripButton TSB_CloseFile;
	}
}

