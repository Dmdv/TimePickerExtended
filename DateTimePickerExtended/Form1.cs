using System;
using System.Windows.Forms;

namespace DateTimePickerExtended
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void bnClear_Click(object sender, EventArgs e)
		{
			_timePicker.Clear();
			timePicker1.Clear();
			timePicker2.Clear();

		}

		private void button1_Click(object sender, EventArgs e)
		{
			var dateTime = new DateTime(2014, 1, 1, 13, 30, 0);
			_timePicker.Value = dateTime;
			dateTimePicker1.Value = dateTime;
			timePicker1.Value = dateTime;
			timePicker2.Value = dateTime;
		}
	}
}