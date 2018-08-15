//namespace Updater.Statistics
//{
//	public class Period
//	{
//		public int Home { get; set; }

//		public int Away { get; set; }

//		public int Both => Home + Away;

//		public bool HomeIsOdd => Home % 2 == 1;
//		public bool AwayIsOdd => Away % 2 == 1;
//		public bool BothIsOdd => Both % 2 == 1;

//		public override string ToString()
//		{
//			return $"{Home}:{Away} ({(BothIsOdd ? "нечет" : "чет")})";
//		}
//	}
//}