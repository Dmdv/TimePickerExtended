using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace DateTimePickerExtended
{
	/// <summary>
	/// The following code is written from scratch, but was inspired by
	/// the NullableDateTimePicker by Claudio Grazioli, http://www.grazioli.ch
	///
	/// This Extended DateTimePicker adds useful functionality to the DateTimePicker class espicially for Database Apllications
	/// 1. I added the abilty to set the DTP to null with the following features
	///     a. NullText Property that can be used to display Text of your choise when DTP is null
	///     b. Ability to type dates or times into the DTP when set to null
	///     c. BackSpace and Delete keystrokes set the DTP to null
	/// 2. I added a ReadOnly Mode in which the UDTP hides itself and decorates a ReadOnly TextBox in its place
	///     a. By Decorating a TextBox we get all black easily readable text.
	///     b. The ReadOnly TextBox has automatic Clipboard access built in which is useful.
	///     c. I added a TabStopWhenReadOnly Property that allows the ReadOnly Mode to mimic the UDTP Tabstop property or not;
	///        If TabStopWhenReadOnly is true then the ReadOnly Mode's TabStop will be whatever the UDTP Tabstop Property is.
	///        If TabStopWhenReadOnly is false then the the ReadOnly Mode's TabStop is false.
	/// </summary>
	public partial class UltraDateTimePickerWithTextBox : DateTimePicker, ISupportInitialize
	{
		//Format and CustomForamt are shadowed since base.Format is always Custom
		//and base.CustomFormat is used in setFormat to show the intended _Format
		//You have to keep base.Format set to Custom to avoid superfluous ValueChanged
		//events from occuring.

		/// <summary>
		/// Basic Constructer + ReadOnly _textBox initialization
		/// </summary>
		public UltraDateTimePickerWithTextBox()
		{
			InitializeComponent();

			InitTextBox();
			base.Format = DateTimePickerFormat.Custom;
			_format = DateTimePickerFormat.Long;
			if (DesignMode)
			{
				SetFormat();
			}
		}

		/// <summary>
		/// Modified CustomFormat Property stores the assigned CustomFormat when null, otherwise functions as normal
		/// </summary>
		public new string CustomFormat
		{
			get { return _customFormat; }
			set
			{
				_customFormat = value;
				SetFormat();
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
				SetFormat();
			}
		}

		/// <summary>
		/// NullText property is used to access/change the Text shown when the UDTP is null
		/// </summary>
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Text shown when DateTime is 'null'")]
		[DefaultValue("")]
		public string NullText
		{
			get { return _nullText; }
			set { _nullText = value; }
		}

		/// <summary>
		/// Sets the ReadOnly Property and then call the appropriate Display Function
		/// If in Design Mode then return(If we dont do this then the Control could Disappear )
		/// </summary>
		[Browsable(true)]
		[Category("Behavior")]
		[Description("Displays Control as ReadOnly(Black on Gray) if 'true'")]
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get { return _readOnly; }
			set
			{
				_readOnly = value;
				SetVisibility();
			}
		}

		/// <summary>
		/// Modified the TabStop Property to support the added TabStopWhenReadOnly property
		/// </summary>
		public new bool TabStop
		{
			get { return base.TabStop; }
			set
			{
				base.TabStop = value;
				_textBox.TabStop = (_tabStopWhenReadOnly && base.TabStop);
			}
		}

		/// <summary>
		/// This useful property allows you to say wheher or not you want
		/// the TexBox to Mimic the DTP's TabStop value when in ReadOnly Mode.
		/// I personally found this useful on Data entry forms. The default is false,
		/// which means when in ReadOnly Mode you cannot tab into it so Tab will skip
		/// ReadOnly Pickers.
		/// </summary>
		[Category("Behavior")]
		[DefaultValue(false)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public bool TabstopWhenReadOnly
		{
			get { return _tabStopWhenReadOnly; }
			set
			{
				_tabStopWhenReadOnly = value;
				_textBox.TabStop = (_tabStopWhenReadOnly && TabStop); //TextBox is a Tabstop only if mimicing and DTP is a TabStop
			}
		}

		/// <summary>
		/// Modified Value Propety now of type Object and uses MinDate to mark null values
		/// </summary>
		public new object Value
		{
			get
			{
				//if (this.MinDate == base.Value) //Check to see if set to MinDate(null), return null or base.Value accordingly
				if (_isNull)
				{
					return null;
				}
				return base.Value;
			}
			set
			{
				if (value == null || value == DBNull.Value) //Check for null assignment
				{
					if (!_isNull) //If not already null set to null and fire event
					{
						_isNull = true;
						OnValueChanged(EventArgs.Empty);
					}
				}
				else //Value is not null
				{
					if (_isNull && base.Value == (DateTime) value)
						//if null and value matches base.value take out of null and fire event
						//(null->value needs a value changed even though base.Value did not change)
					{
						_isNull = false;
						OnValueChanged(EventArgs.Empty);
					}
					else //change to the new value(changed event fires from base class
					{
						_isNull = false;
						base.Value = (DateTime) value;
					}
				}
				SetFormat(); //refresh format
				_textBox.Text = Text;
			}
		}

		/// <summary>
		/// Sets the Visible Property and then call the appropriate Display Function
		/// If in Design Mode then return(If we dont do this then the Control could Disappear )
		/// </summary>
		public new bool Visible
		{
			get { return _visible; }
			set
			{
				_visible = value;
				SetVisibility();
			}
		}

		public void BeginInit()
		{
			// TODO: Add UserControl1.BeginInit implementation   
			_initializing = true;
		}

		public void EndInit()
		{
			base.Value = DateTime.Today; //Default the value to Today(makes me happy, but not necessary)
			_initializing = false;
			if (DesignMode)
			{
				return;
			}
			if (Parent.GetType() == typeof (TableLayoutPanel))
			{
				var cP = ((TableLayoutPanel) Parent).GetPositionFromControl(this);
				((TableLayoutPanel) Parent).Controls.Add(_textBox, cP.Column, cP.Row);
				((TableLayoutPanel) Parent).SetColumnSpan(_textBox, ((TableLayoutPanel) Parent).GetColumnSpan(this));
				_textBox.Anchor = Anchor;
			}
				//I added special logic here to handle positioning the _TextBox when the UDTP is in a FlowLayoutPanel
			else if (Parent.GetType() == typeof (FlowLayoutPanel))
			{
				Parent.Controls.Add(_textBox);
				Parent.Controls.SetChildIndex(_textBox, Parent.Controls.IndexOf(this));
				_textBox.Anchor = Anchor;
			}
			else //not a TableLayoutPanel or FlowLayoutPanel so just assign the parent
			{
				_textBox.Parent = Parent;
				_textBox.Anchor = Anchor;
			}

			//I use the following block of code to walk up the parent-child
			//chain and find the first member that has a Load event that I can attach to
			//I set the visiblilty during this event so that Databinding will work correctly
			//otherwise the UDTP will fail to bind properly if its visibility is false during the
			//Load event.(Strange but true, has to do with hidden controls not binding for performance reasons)
			Control parent = this;
			var foundLoadingParent = false;

			do
			{
				parent = parent.Parent;
				if (parent.GetType().IsSubclassOf(typeof (UserControl)))
				{
					((UserControl) parent).Load += UltraDateTimePicker_Load;
					foundLoadingParent = true;
				}
				else if (parent.GetType().IsSubclassOf(typeof (Form)))
				{
					((Form) parent).Load += UltraDateTimePicker_Load;
					foundLoadingParent = true;
				}
			} while (!foundLoadingParent);
		}

		public new void Hide()
		{
			Visible = false;
		}

		public new void Show()
		{
			Visible = true;
		}

		/// <summary>
		/// Propagates Dock to the _TextBox
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDockChanged(EventArgs e)
		{
			base.OnDockChanged(e);
			_textBox.Dock = Dock;
		}

		/// Propagates Font to the _TextBox
		/// <param name="e"></param>
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			_textBox.Font = Font;
		}

		/// <summary>
		/// When Null and a Number is pressed this method takes the UDTP out of Null Mode
		/// and resends the pressed key for timign reasons
		/// </summary>
		/// <param name="e"></param>
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
		///<param name="e"></param>
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
			{
				Value = null;
			}
			base.OnKeyUp(e);
		}

		/// <summary>
		/// Propagates Location to the _TextBox
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLocationChanged(EventArgs e)
		{
			base.OnLocationChanged(e);
			_textBox.Location = Location;
		}

		/// <summary>
		/// Refreshes the Visibility of the Control if the Parent Changes(So the _TextBox get moved and Redrawn)
		/// If in Design Mode then return(If we dont do this then the Control could Disappear )
		/// </summary>
		/// <param name="e"></param>
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			if (DesignMode || _initializing)
			{
				return;
			}
			UpdateReadOnlyTextBoxParent(); //update the _TextBox parent
			SetVisibility(); //Reset Visibilty for new parent
		}

		/// <summary>
		/// Propagates Size to the _TextBox
		/// </summary>
		/// <param name="e"></param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			_textBox.Size = Size;
		}

		/// <summary>
		/// Propagates RightToLeft to the _TextBox
		/// </summary>
		/// <param name="e"></param>
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			_textBox.RightToLeft = RightToLeft;
		}

		/// <summary>
		/// Propagates Size to the _TextBox
		/// </summary>
		/// <param name="e"></param>
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			_textBox.Size = Size;
		}

		/// <summary>
		/// Propagates TabIndex to the _TextBox
		/// </summary>
		/// <param name="e"></param>
		protected override void OnTabIndexChanged(EventArgs e)
		{
			base.OnTabIndexChanged(e);
			_textBox.TabIndex = TabIndex;
		}

		/// <summary>
		/// Propagates TabStop to the _TextBox if TabStopWhenReadOnly == true
		/// </summary>
		/// <param name="e"></param>
		protected override void OnTabStopChanged(EventArgs e)
		{
			base.OnTabStopChanged(e);
			_textBox.TabStop = _tabStopWhenReadOnly && TabStop;
		}

		/// <summary>
		/// Used to change the UDTP.Value on Closeup(Without this code Closeup only changes the base.Value)
		/// </summary>
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 0x4e)
			{
				var nm = (NMHDR) m.GetLParam(typeof (NMHDR));
				if (nm.Code == -746 || nm.Code == -722)
				{
					Value = base.Value; //propagate change form base to UTDP
				}
			}
			base.WndProc(ref m);
		}

		////// <summary>

		/// <summary>
		/// Added to initialize the _textBox to the default values to match the DTP
		/// </summary>
		private void InitTextBox()
		{
			if (DesignMode)
			{
				return;
			}

			_textBox = new TextBox
			{
				ReadOnly = true,
				Location = Location,
				Size = Size,
				Dock = Dock,
				Anchor = Anchor,
				RightToLeft = RightToLeft,
				Font = Font,
				TabStop = TabStop,
				TabIndex = TabIndex,
				Visible = false,
				Parent = Parent
			};
		}

		private void SetFormat()
		{
			base.CustomFormat = null; //Resets the CustomFormat(bookkeeping)
			if (_isNull) //If null apply NullText to the UDTP
			{
				base.CustomFormat = String.Concat("'", NullText, "'");
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

		private void SetVisibility()
		{
			if (DesignMode || _initializing) //Dont actually change the visibility if in Design Mode
			{
				return;
			}
			if (_visible)
			{
				if (_readOnly)
				{
					ShowTextBox(); //If Visible and Readonly Show TextBox
				}
				else
				{
					ShowDtp(); //If Visible and NOT ReadOnly Show DateTimePicker
				}
			}
			else
			{
				ShowNone(); //If Not Visible Show Neither
			}
		}

		private void ShowDtp()
		{
			_textBox.Visible = false;
			base.Visible = true;
		}

		private void ShowNone()
		{
			_textBox.Visible = false;
			base.Visible = false;
		}

		private void ShowTextBox()
		{
			base.Visible = false;
			_textBox.Visible = true;
			_textBox.TabStop = _tabStopWhenReadOnly && TabStop;
		}

		private void UltraDateTimePicker_Load(object sender, EventArgs e)
		{
			SetVisibility();
		}

		private void UpdateReadOnlyTextBoxParent()
		{
			if (Parent == null) //If UTDP.Parent == null, set _textBox.Parent == null and return
			{
				_textBox.Parent = null;
				return;
			}
			if (_textBox.Parent != Parent) //If the Parents DO NOT already match
			{
				//I Added Special logic here to handle positioning the _TextBox when the UDTP is in a TableLayoutPanel
				if (Parent.GetType() == typeof (TableLayoutPanel))
				{
					var cP = ((TableLayoutPanel) Parent).GetPositionFromControl(this);
					((TableLayoutPanel) Parent).Controls.Add(_textBox, cP.Column, cP.Row);
					((TableLayoutPanel) Parent).SetColumnSpan(_textBox, ((TableLayoutPanel) Parent).GetColumnSpan(this));
					_textBox.Anchor = Anchor;
				}
					//I added special logic here to handle positioning the _TextBox when the UDTP is in a FlowLayoutPanel
				else if (Parent.GetType() == typeof (FlowLayoutPanel))
				{
					Parent.Controls.Add(_textBox);
					Parent.Controls.SetChildIndex(_textBox, Parent.Controls.IndexOf(this));
					_textBox.Anchor = Anchor;
				}
				else //not a TableLayoutPanel or FlowLayoutPanel so just assign the parent
				{
					_textBox.Parent = Parent;
					_textBox.Anchor = Anchor;
				}
			}
		}

		private string _customFormat; //Variable to store 'CustomFormat'
		private DateTimePickerFormat _format; // Variable to store 'Format'
		private bool _initializing = true;
		private bool _isNull;
		private string _nullText = string.Empty; //Variable to store null Display Text

		private bool _readOnly; //Flag to denote the UDTP is in ReadOnly Mode
		private bool _tabStopWhenReadOnly; //Variable to store whether or not the UDTP is a TabStop when in ReadOnly Mode
		private TextBox _textBox; //TextBox Decorated when in ReadOnly Mode
		private bool _visible = true; //Overridden to show the proper Display for Readonly Mode

		// ReSharper disable  InconsistentNaming
		// ReSharper disable MemberCanBePrivate.Local
		[StructLayout(LayoutKind.Sequential)]
		private struct NMHDR
		{
			public readonly IntPtr HwndFrom;
			public readonly int IdFrom;
			public readonly int Code;
		}
	}
}