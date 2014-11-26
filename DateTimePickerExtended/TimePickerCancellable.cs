using System;
using System.Windows.Forms;

namespace DateTimePickerExtended
{
	public partial class TimePickerCancellable : UserControl
	{
		public TimePickerCancellable()
		{
			InitializeComponent();
		}

		private void _clearButton_Click(object sender, EventArgs e)
		{
			_timePicker.Clear();
		}
	}
}