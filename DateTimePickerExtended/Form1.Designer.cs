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
			this.button1 = new System.Windows.Forms.Button();
			this.timePicker2 = new DateTimePickerExtended.TimePicker();
			this.timePicker1 = new DateTimePickerExtended.TimePicker();
			this._timePickerCancelButton1 = new DateTimePickerExtended.TimePickerCancellable();
			this._timePicker = new DateTimePickerExtended.TimePicker();
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
			this.dateTimePicker1.CustomFormat = "HH:mm";
			this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker1.Location = new System.Drawing.Point(12, 46);
			this.dateTimePicker1.Name = "dateTimePicker1";
			this.dateTimePicker1.ShowUpDown = true;
			this.dateTimePicker1.Size = new System.Drawing.Size(219, 20);
			this.dateTimePicker1.TabIndex = 2;
			this.dateTimePicker1.Value = new System.DateTime(2014, 11, 29, 0, 0, 0, 0);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(12, 139);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(112, 23);
			this.button1.TabIndex = 5;
			this.button1.Text = "Set 13:00";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// timePicker2
			// 
			this.timePicker2.CustomFormat = "HH:mm";
			this.timePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.timePicker2.Location = new System.Drawing.Point(12, 238);
			this.timePicker2.Name = "timePicker2";
			this.timePicker2.ShowUpDown = true;
			this.timePicker2.Size = new System.Drawing.Size(200, 20);
			this.timePicker2.TabIndex = 7;
			this.timePicker2.Value = new System.DateTime(2014, 11, 27, 21, 52, 44, 877);
			// 
			// timePicker1
			// 
			this.timePicker1.CustomFormat = "HH:mm";
			this.timePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.timePicker1.Location = new System.Drawing.Point(12, 201);
			this.timePicker1.Name = "timePicker1";
			this.timePicker1.ShowUpDown = true;
			this.timePicker1.Size = new System.Drawing.Size(200, 20);
			this.timePicker1.TabIndex = 6;
			this.timePicker1.Value = new System.DateTime(2014, 11, 27, 21, 51, 58, 931);
			// 
			// _timePickerCancelButton1
			// 
			this._timePickerCancelButton1.AutoSize = true;
			this._timePickerCancelButton1.Location = new System.Drawing.Point(12, 84);
			this._timePickerCancelButton1.Name = "_timePickerCancelButton1";
			this._timePickerCancelButton1.Size = new System.Drawing.Size(207, 21);
			this._timePickerCancelButton1.TabIndex = 4;
			// 
			// _timePicker
			// 
			this._timePicker.Checked = false;
			this._timePicker.CustomFormat = "HH:mm";
			this._timePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this._timePicker.Location = new System.Drawing.Point(12, 12);
			this._timePicker.Name = "_timePicker";
			this._timePicker.ShowUpDown = true;
			this._timePicker.Size = new System.Drawing.Size(138, 20);
			this._timePicker.TabIndex = 3;
			this._timePicker.Value = new System.DateTime(2014, 11, 27, 21, 59, 33, 888);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 277);
			this.Controls.Add(this.timePicker2);
			this.Controls.Add(this.timePicker1);
			this.Controls.Add(this.button1);
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
		private System.Windows.Forms.Button button1;
		private TimePicker timePicker1;
		private TimePicker timePicker2;
	}
}

