namespace DateTimePickerExtended
{
	partial class Form1
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
			this.bnClear = new System.Windows.Forms.Button();
			this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
			this._timePicker = new DateTimePickerExtended.TimePicker();
			this._timePickerCancelButton1 = new DateTimePickerExtended.TimePickerCancellable();
			this.SuspendLayout();
			// 
			// bnClear
			// 
			this.bnClear.Image = global::DateTimePickerExtended.Properties.Resources._1416844396_DeleteRed;
			this.bnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.bnClear.Location = new System.Drawing.Point(156, 11);
			this.bnClear.Name = "bnClear";
			this.bnClear.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
			this.bnClear.Size = new System.Drawing.Size(75, 23);
			this.bnClear.TabIndex = 1;
			this.bnClear.Text = "Clear";
			this.bnClear.UseVisualStyleBackColor = true;
			this.bnClear.Click += new System.EventHandler(this.bnClear_Click);
			// 
			// dateTimePicker1
			// 
			this.dateTimePicker1.Checked = false;
			this.dateTimePicker1.Location = new System.Drawing.Point(12, 46);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.ShowUpDown = true;
			this.dateTimePicker1.Size = new System.Drawing.Size(219, 20);
			this.dateTimePicker1.TabIndex = 2;
			// 
			// _timePicker
			// 
			this._timePicker.Checked = false;
			this._timePicker.CustomFormat = " ";
			this._timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this._timePicker.Location = new System.Drawing.Point(12, 12);
			this._timePicker.Name = "_timePicker";
			this._timePicker.ShowUpDown = true;
			this._timePicker.Size = new System.Drawing.Size(138, 20);
			this._timePicker.TabIndex = 3;
			this._timePicker.Value = null;
			// 
			// _timePickerCancelButton1
			// 
			this._timePickerCancelButton1.AutoSize = true;
			this._timePickerCancelButton1.Location = new System.Drawing.Point(12, 85);
			this._timePickerCancelButton1.Name = "_timePickerCancelButton1";
			this._timePickerCancelButton1.Size = new System.Drawing.Size(218, 21);
			this._timePickerCancelButton1.TabIndex = 4;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 149);
			this.Controls.Add(this._timePickerCancelButton1);
			this.Controls.Add(this._timePicker);
			this.Controls.Add(this.dateTimePicker1);
			this.Controls.Add(this.bnClear);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button bnClear;
		private System.Windows.Forms.DateTimePicker dateTimePicker1;
		private TimePicker _timePicker;
		private TimePickerCancellable _timePickerCancelButton1;
	}
}

