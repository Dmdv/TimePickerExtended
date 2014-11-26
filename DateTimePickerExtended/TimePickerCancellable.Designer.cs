namespace DateTimePickerExtended
{
	partial class TimePickerCancellable
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
			this._clearButton = new System.Windows.Forms.Button();
			this._timePicker = new DateTimePickerExtended.TimePicker();
			this.SuspendLayout();
			// 
			// _clearButton
			// 
			this._clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._clearButton.Image = global::DateTimePickerExtended.Properties.Resources._1416844396_DeleteRed;
			this._clearButton.Location = new System.Drawing.Point(254, -1);
			this._clearButton.Margin = new System.Windows.Forms.Padding(0);
			this._clearButton.Name = "_clearButton";
			this._clearButton.Size = new System.Drawing.Size(22, 22);
			this._clearButton.TabIndex = 1;
			this._clearButton.UseVisualStyleBackColor = true;
			this._clearButton.Click += new System.EventHandler(this._clearButton_Click);
			// 
			// _timePicker
			// 
			this._timePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this._timePicker.Checked = false;
			this._timePicker.CustomFormat = " ";
			this._timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this._timePicker.Location = new System.Drawing.Point(3, 0);
			this._timePicker.Margin = new System.Windows.Forms.Padding(0);
			this._timePicker.Name = "_timePicker";
			this._timePicker.ShowUpDown = true;
			this._timePicker.Size = new System.Drawing.Size(251, 20);
			this._timePicker.TabIndex = 2;
			this._timePicker.Value = null;
			// 
			// TimePickerCancellable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this._timePicker);
			this.Controls.Add(this._clearButton);
			this.Name = "TimePickerCancellable";
			this.Size = new System.Drawing.Size(277, 22);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button _clearButton;
		private TimePicker _timePicker;
	}
}
