namespace MnemonicAssignmentCalculator
{
  partial class MainForm
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
			this.btnAction = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtInput = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtOutputPath = new System.Windows.Forms.TextBox();
			this.btnBrowse = new System.Windows.Forms.Button();
			this.lblChecked = new System.Windows.Forms.Label();
			this.lblPermutaionsValue = new System.Windows.Forms.Label();
			this.picProcessing = new System.Windows.Forms.PictureBox();
			this.label4 = new System.Windows.Forms.Label();
			this.lblDistinctChars = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.txtCharsToIgnore = new System.Windows.Forms.TextBox();
			this.lblIncrementMessage = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.picProcessing)).BeginInit();
			this.SuspendLayout();
			// 
			// btnAction
			// 
			this.btnAction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAction.Location = new System.Drawing.Point(288, 527);
			this.btnAction.Name = "btnAction";
			this.btnAction.Size = new System.Drawing.Size(75, 23);
			this.btnAction.TabIndex = 4;
			this.btnAction.Text = "Do Work";
			this.btnAction.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(107, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Words To Assign:";
			// 
			// txtInput
			// 
			this.txtInput.Location = new System.Drawing.Point(15, 25);
			this.txtInput.Multiline = true;
			this.txtInput.Name = "txtInput";
			this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtInput.Size = new System.Drawing.Size(345, 291);
			this.txtInput.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(125, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(122, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "(Enter one word per line)";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(12, 373);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(73, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "Output File:";
			// 
			// txtOutputPath
			// 
			this.txtOutputPath.Location = new System.Drawing.Point(15, 389);
			this.txtOutputPath.Name = "txtOutputPath";
			this.txtOutputPath.Size = new System.Drawing.Size(307, 20);
			this.txtOutputPath.TabIndex = 2;
			// 
			// btnBrowse
			// 
			this.btnBrowse.Location = new System.Drawing.Point(328, 387);
			this.btnBrowse.Name = "btnBrowse";
			this.btnBrowse.Size = new System.Drawing.Size(32, 23);
			this.btnBrowse.TabIndex = 3;
			this.btnBrowse.Text = "...";
			this.btnBrowse.UseVisualStyleBackColor = true;
			this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
			// 
			// lblChecked
			// 
			this.lblChecked.AutoSize = true;
			this.lblChecked.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblChecked.Location = new System.Drawing.Point(12, 484);
			this.lblChecked.Name = "lblChecked";
			this.lblChecked.Size = new System.Drawing.Size(138, 13);
			this.lblChecked.TabIndex = 8;
			this.lblChecked.Text = "Permutations Checked:";
			// 
			// lblPermutaionsValue
			// 
			this.lblPermutaionsValue.AutoSize = true;
			this.lblPermutaionsValue.Location = new System.Drawing.Point(12, 497);
			this.lblPermutaionsValue.Name = "lblPermutaionsValue";
			this.lblPermutaionsValue.Size = new System.Drawing.Size(62, 13);
			this.lblPermutaionsValue.TabIndex = 8;
			this.lblPermutaionsValue.Text = "[[Checked]]";
			// 
			// picProcessing
			// 
			this.picProcessing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.picProcessing.Location = new System.Drawing.Point(246, 521);
			this.picProcessing.Name = "picProcessing";
			this.picProcessing.Size = new System.Drawing.Size(36, 29);
			this.picProcessing.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.picProcessing.TabIndex = 9;
			this.picProcessing.TabStop = false;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(12, 433);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(119, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Distinct Characters:";
			// 
			// lblDistinctChars
			// 
			this.lblDistinctChars.AutoEllipsis = true;
			this.lblDistinctChars.Location = new System.Drawing.Point(12, 446);
			this.lblDistinctChars.Name = "lblDistinctChars";
			this.lblDistinctChars.Size = new System.Drawing.Size(348, 38);
			this.lblDistinctChars.TabIndex = 10;
			this.lblDistinctChars.Text = "[[Letters]]";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.Location = new System.Drawing.Point(91, 374);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(206, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "(Will be overwritten automatically if it exists)";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.Location = new System.Drawing.Point(12, 323);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(126, 13);
			this.label6.TabIndex = 3;
			this.label6.Text = "Characters to ignore:";
			// 
			// txtCharsToIgnore
			// 
			this.txtCharsToIgnore.Location = new System.Drawing.Point(15, 339);
			this.txtCharsToIgnore.Name = "txtCharsToIgnore";
			this.txtCharsToIgnore.Size = new System.Drawing.Size(345, 20);
			this.txtCharsToIgnore.TabIndex = 1;
			// 
			// lblIncrementMessage
			// 
			this.lblIncrementMessage.AutoSize = true;
			this.lblIncrementMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblIncrementMessage.Location = new System.Drawing.Point(154, 485);
			this.lblIncrementMessage.Name = "lblIncrementMessage";
			this.lblIncrementMessage.Size = new System.Drawing.Size(111, 13);
			this.lblIncrementMessage.TabIndex = 11;
			this.lblIncrementMessage.Text = "[[Increment Message]]";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(375, 562);
			this.Controls.Add(this.lblIncrementMessage);
			this.Controls.Add(this.lblDistinctChars);
			this.Controls.Add(this.picProcessing);
			this.Controls.Add(this.lblPermutaionsValue);
			this.Controls.Add(this.lblChecked);
			this.Controls.Add(this.btnBrowse);
			this.Controls.Add(this.txtCharsToIgnore);
			this.Controls.Add(this.txtOutputPath);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtInput);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnAction);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.Text = "Mnemonic Assignment";
			((System.ComponentModel.ISupportInitialize)(this.picProcessing)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnAction;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtInput;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtOutputPath;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.Label lblChecked;
		private System.Windows.Forms.Label lblPermutaionsValue;
		private System.Windows.Forms.PictureBox picProcessing;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblDistinctChars;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtCharsToIgnore;
		private System.Windows.Forms.Label lblIncrementMessage;
  }
}

