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
		}
	}
}