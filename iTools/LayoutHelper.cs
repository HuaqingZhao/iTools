using System;
using System.Drawing;
using System.Windows.Forms;

namespace iTools
{
	public class LayoutHelper
	{
		private static readonly Color[] Colors = GetColors;

		private static Color[] GetColors
		{
			get
			{
				const int init = 560000000;
				var cs = new Color[5];
				for (var i = 0; i < 5; i++)
				{
					cs[i] = Color.FromArgb(init + i * 1000000);
				}
				return cs;
			}
		}

		public static Color GetRamdomColor
		{
			get
			{
				var rd = new Random(DateTime.Now.Millisecond);
				var index = rd.Next(0, 4);
				return Colors[index];
			}
		}

		public static void CalcSpace(Control control, int width, int height)
		{
			int column = 0;
			int row = 0;
			int maxColumn = control.Width / width;

			for (var i = 0; i < control.Controls.Count; i++)
			{
				var ct = control.Controls[i];
				ct.Size = new Size(width, height);

				if (column == maxColumn)
				{
					column = 0;
					row++;
				}

				var x = width * column;
				column++;
				var y = width * row;

				ct.Location = new Point(x, y);
			}
		}
	}
}
