namespace WinForms_BackgroundService;

partial class ExtensionForm
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
		lastBattlePropGrid = new System.Windows.Forms.PropertyGrid();
		errorText = new System.Windows.Forms.TextBox();
		backgroundWorkerButton = new System.Windows.Forms.Button();
		SuspendLayout();
		// 
		// lastBattlePropGrid
		// 
		lastBattlePropGrid.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		lastBattlePropGrid.Enabled = false;
		lastBattlePropGrid.Location = new System.Drawing.Point(431, 0);
		lastBattlePropGrid.Name = "lastBattlePropGrid";
		lastBattlePropGrid.Size = new System.Drawing.Size(433, 517);
		lastBattlePropGrid.TabIndex = 0;
		// 
		// errorText
		// 
		errorText.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
		errorText.Location = new System.Drawing.Point(12, 12);
		errorText.Multiline = true;
		errorText.Name = "errorText";
		errorText.ReadOnly = true;
		errorText.Size = new System.Drawing.Size(413, 193);
		errorText.TabIndex = 1;
		// 
		// backgroundWorkerButton
		// 
		backgroundWorkerButton.Location = new System.Drawing.Point(29, 237);
		backgroundWorkerButton.Name = "backgroundWorkerButton";
		backgroundWorkerButton.Size = new System.Drawing.Size(193, 23);
		backgroundWorkerButton.TabIndex = 2;
		backgroundWorkerButton.Text = "Trigger Background Worker";
		backgroundWorkerButton.UseVisualStyleBackColor = true;
		backgroundWorkerButton.Click += backgroundWorkerButton_Click;
		// 
		// ExtensionForm
		// 
		AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
		AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		ClientSize = new System.Drawing.Size(864, 517);
		Controls.Add(backgroundWorkerButton);
		Controls.Add(errorText);
		Controls.Add(lastBattlePropGrid);
		Name = "ExtensionForm";
		Text = "ExtensionForm";
		ResumeLayout(false);
		PerformLayout();
	}

	#endregion

	private System.Windows.Forms.PropertyGrid lastBattlePropGrid;
	private System.Windows.Forms.TextBox errorText;
	private System.Windows.Forms.Button backgroundWorkerButton;
}