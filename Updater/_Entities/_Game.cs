//using System;
//using Insight.Database;
//using Updater.Entities;

//namespace Updater.Statistics
//{
//	[BindChildren(BindChildrenFor.All)]
//	public class Game
//	{
//		[RecordId]
//		public string Id { get; set; }
//		public DateTime DateStart { get; set; }
//		public Country Country { get; set; }
//		public League League { get; set; }
//		public Season Season { get; set; }
//		public Stage Stage { get; set; }
//		public Team TeamHome { get; set; }
//		public Team TeamAway { get; set; }

//		public Period P1 { get; set; }
//		public Period P2 { get; set; }
//		public Period P3 { get; set; }
//		public Period P4 { get; set; }
//		public Period OT { get; set; }
//		public Period Total { get; set; }

//		public override string ToString()
//		{
//			return $"{DateStart.ToShortDateString()} {DateStart.ToShortTimeString()} ({P1.Home}:{P1.Away}, {P2.Home}:{P2.Away}, {P3.Home}:{P3.Away}, {P4.Home}:{P4.Away}{(OT.Both > 0 ? ", " + OT.Home + ":" + OT.Away : "")}) ({Total.Home}:{Total.Away}) ({(Total.BothIsOdd ? "нечет" : "чет")})";
//		}
//	}
//}