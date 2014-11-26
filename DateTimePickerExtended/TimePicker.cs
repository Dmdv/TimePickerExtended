using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace DateTimePickerExtended
{
	/// <summary>
	/// Timepicker supports for Null Values and supports clear with Del or Backspace buttons.
	/// </summary>
	public class TimePicker : DateTimePicker
	{
		private const string Null = @" ";

		public TimePicker()
		{
			Format = DateTimePickerFormat.Custom;
			ShowUpDown = true;
			Clear();
		}

		/// <summary>
		/// Modified CustomFormat Property stores the assigned CustomFormat when null, 
		/// otherwise functions as normal
		/// </summary>
		public new string CustomFormat
		{
			get { return _customFormat; }
			set
			{
				_customFormat = value;
				SetCustomFormat();
			}
		}

		/// <summary>
		/// Modified Format Property stores the assigned Format and the propagates the change to base.CustomFormat
		/// </summary>
		[Browsable(true)]
		[DefaultValue(DateTimePickerFormat.Long), TypeConverter(typeof (Enum))]
		public new DateTimePickerFormat Format
		{
			get { return _format; }
			set
			{
				_format = value;
				base.Format = value;
				SetCustomFormat();
			}
		}

		public new object Value
		{
			get { return _isNull ? (object) null : base.Value; }
			set
			{
				if (value == null || value == DBNull.Value)
				{
					if (!_isNull)
					{
						_isNull = true;
						OnValueChanged(EventArgs.Empty);
					}
				}
				else
				{
					var dateTime = (DateTime) value;

					if (_isNull && DateTime.Equals(base.Value, dateTime))
					{
						_isNull = false;
						OnValueChanged(EventArgs.Empty);
					}
					else
					{
						_isNull = false;
						base.Value = dateTime;
					}
				}

				SetCustomFormat();
			}
		}

		public void Clear()
		{
			CustomFormat = Null;
			Text = string.Empty;
			Value = null;
		}

		public void SetTime12Format()
		{
			_isNull = false;
			CustomFormat = @"hh:mm";
		}

		public void SetTime24Format()
		{
			_isNull = false;
			CustomFormat = @"HH:mm";
		}

		/// <summary>
		/// When Null and a Number is pressed this method takes the UDTP out of Null Mode
		/// and resends the pressed key for timign reasons
		/// </summary>
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (_isNull && Char.IsDigit(e.KeyChar))
			{
				Value = base.Value;
				e.Handled = true;
				SendKeys.Send("{RIGHT}");
				SendKeys.Send(e.KeyChar.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				base.OnKeyPress(e);
			}
		}

		///<summary>
		///Sets UDTP Value to null when Delete or Backspace is pressed
		///</summary>
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
			{
				Clear();
			}
			base.OnKeyUp(e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			SetTime24Format();
		}

		/// <summary>
		/// Used to change the UDTP.Value on Closeup(Without this code Closeup only changes the base.Value)
		/// </summary>
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x204e || m.Msg == 0x4e)
			{
				var nmhdr = (NMHDR) m.GetLParam(typeof (NMHDR));

				if (nmhdr.Code == -722)
				{
					var nmupdown = (NMUPDOWN) m.GetLParam(typeof (NMUPDOWN));

					if (nmupdown.Delta != 0)
					{
						SetTime24Format();
						Value = base.Value;
					}
				}

				if (nmhdr.Code == -746)
				{
					SetTime24Format();
					Value = base.Value;
				}
			}
			base.WndProc(ref m);
		}

		private void SetCustomFormat()
		{
			base.CustomFormat = null;

			if (_isNull)
			{
				base.CustomFormat = Null;
				_customFormat = Null;
			}
			else
			{
				//The Following is used to get a string representation ot the current UDTP Format
				//And then set the CustomFormat to match the intended format
				var cultureInfo = Thread.CurrentThread.CurrentCulture;
				var dTFormatInfo = cultureInfo.DateTimeFormat;
				switch (_format)
				{
					case DateTimePickerFormat.Long:
						base.CustomFormat = dTFormatInfo.LongDatePattern;
						break;
					case DateTimePickerFormat.Short:
						base.CustomFormat = dTFormatInfo.ShortDatePattern;
						break;
					case DateTimePickerFormat.Time:
						base.CustomFormat = dTFormatInfo.ShortTimePattern;
						break;
					case DateTimePickerFormat.Custom:
						base.CustomFormat = _customFormat;
						break;
				}
			}
		}

		// ReSharper disable UnusedMember.Local
		// ReSharper disable MemberCanBePrivate.Local
		// ReSharper disable InconsistentNaming

		private string _customFormat;
		private DateTimePickerFormat _format;
		private bool _isNull;

		[StructLayout(LayoutKind.Sequential)]
		private struct NMHDR
		{
			public readonly IntPtr HwndFrom;
			public readonly IntPtr IdFrom;
			public readonly int Code;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct NMUPDOWN
		{
			public readonly NMHDR Hdr;
			public readonly int Pos;
			public readonly int Delta;
		}
	}

	// Part of this code can be used to add child control to textbox, how to add this to calendar?

	//protected override void OnCreateControl()
	//{
	//	base.OnCreateControl();

	//	var btn = new Button
	//	{
	//		Size = new Size(25, ClientSize.Height + 2),
	//	};

	//	btn.Location = new Point(ClientSize.Width - btn.Width, -1);
	//	btn.Cursor = Cursors.Default;
	//	btn.Image = Resources._1416844396_DeleteRed;
	//	Controls.Add(btn);

	//	// Send EM_SETMARGINS to prevent text from disappearing underneath the button
	//	SendMessage(Handle, 0xd3, (IntPtr)2, (IntPtr)(btn.Width << 16));
	//}

	//[DllImport("user32.dll")]
	//private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
}