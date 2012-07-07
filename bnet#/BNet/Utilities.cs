namespace BNet
{
	public struct Point
	{
		public ushort X { get; set; }
		public ushort Y { get; set; }

		public static Point Empty
		{
			get { return default(Point); }
		}

		public override bool Equals(object obj)
		{
			if(obj is Point)
			{
				var p = (Point)obj;
				return p.X == X && p.Y == Y;
			}
			return false;
		}

		public override int GetHashCode() { return base.GetHashCode(); }
	}

	public struct Size
	{
		public ushort Width { get; set; }
		public ushort Height { get; set; }

		public static Size Empty
		{
			get { return default(Size); }
		}

		public override bool Equals(object obj)
		{
			if(obj is Size)
			{
				var p = (Size)obj;
				return p.Width == Width && p.Height == Height;
			}
			return false;
		}

		public override int GetHashCode() { return base.GetHashCode(); }
	}
}
