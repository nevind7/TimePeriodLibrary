	// -- FILE ------------------------------------------------------------------
// name       : HalfYearDemo.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

    using TimePeriod;

    namespace TimePeriodDemo
{

	// ------------------------------------------------------------------------
	public class HalfYearDemo : TimePeriodDemo
	{

		// ----------------------------------------------------------------------
		public static void ShowAll( int periodCount, int startYear, YearHalfYear yearHalfYear, TimeCalendarConfig calendarConfig )
		{
			WriteLine( "Input: count={0}, year={1}, halfyear={2}", periodCount, startYear, yearHalfYear );
			WriteLine();

			HalfYearTimeRange halfyearTimeRange;
			if ( periodCount == 1 )
			{
				HalfYear halfyear = new HalfYear( startYear, yearHalfYear, new TimeCalendar( calendarConfig ) );
				halfyearTimeRange = halfyear;

				HalfYear previousHalfYear = halfyear.GetPreviousHalfYear();
				HalfYear nextHalfYear = halfyear.GetNextHalfYear();

				ShowHalfYear( halfyear );
				ShowCompactHalfYear( previousHalfYear, "Previous HalfYear" );
				ShowCompactHalfYear( nextHalfYear, "Next HalfYear" );
				WriteLine();
			}
			else
			{
				HalfYears halfyears = new HalfYears( startYear, yearHalfYear, periodCount, new TimeCalendar( calendarConfig ) );
				halfyearTimeRange = halfyears;

				ShowHalfYears( halfyears );
				WriteLine();
				
				foreach ( HalfYear halfyear in halfyears.GetHalfYears() )
				{
					ShowCompactHalfYear( halfyear );
				}
				WriteLine();
			}

			foreach ( Quarter quarter in halfyearTimeRange.GetQuarters() )
			{
				QuarterDemo.ShowCompactQuarter( quarter );
			}
			WriteLine();
			foreach ( Month month in halfyearTimeRange.GetMonths() )
			{
				MonthDemo.ShowCompactMonth( month );
			}
			WriteLine();
		} // ShowAll

		// ----------------------------------------------------------------------
		public static void ShowCompactHalfYear( HalfYear halfyear, string caption = "HalfYear" )
		{
			WriteLine( "{0}: {1}", caption, halfyear );
		} // ShowCompactHalfYear

		// ----------------------------------------------------------------------
		public static void ShowHalfYear( HalfYear halfyear, string caption = "HalfYear" )
		{
			WriteLine( "{0}: {1}", caption, halfyear );
			WriteIndentLine( "YearBaseMonth: {0}", halfyear.YearBaseMonth );
			WriteIndentLine( "StartMonth: {0}", halfyear.StartMonth );
			WriteIndentLine( "Year: {0}", halfyear.Year );
			WriteIndentLine( "YearHalfYear: {0}", halfyear.YearHalfYear );
			WriteIndentLine( "IsCalendarHalfYear: {0}", halfyear.IsCalendarHalfYear );
			WriteIndentLine( "MultipleCalendarYears: {0}", halfyear.MultipleCalendarYears );
			WriteIndentLine( "HalfYearName: {0}", halfyear.HalfYearName );
			WriteIndentLine( "HalfYearOfYearName: {0}", halfyear.HalfYearOfYearName );
			WriteIndentLine( "FirstDayStart: {0}", Format( halfyear.FirstDayStart ) );
			WriteIndentLine( "LastDayStart: {0}", Format( halfyear.LastDayStart ) );
			WriteLine();
		} // ShowHalfYear

		// ----------------------------------------------------------------------
		public static void ShowCompactHalfYears( HalfYears halfyears, string caption = "HalfYears" )
		{
			WriteLine( "{0}: {1}", caption, halfyears );
		} // ShowCompactHalfYears

		// ----------------------------------------------------------------------
		public static void ShowHalfYears( HalfYears halfyears, string caption = "HalfYears" )
		{
			WriteLine( "{0}: {1}", caption, halfyears );
			WriteIndentLine( "YearBaseMonth: {0}", halfyears.YearBaseMonth );
			WriteIndentLine( "StartYear: {0}", halfyears.StartYear );
			WriteIndentLine( "StartHalfYear: {0}", halfyears.StartHalfYear );
			WriteIndentLine( "StartHalfYearName: {0}", halfyears.StartHalfYearName );
			WriteIndentLine( "StartHalfYearOfYearName: {0}", halfyears.StartHalfYearOfYearName );
			WriteIndentLine( "EndYear: {0}", halfyears.EndYear );
			WriteIndentLine( "EndHalfYear: {0}", halfyears.EndHalfYear );
			WriteIndentLine( "EndHalfYearName: {0}", halfyears.EndHalfYearName );
			WriteIndentLine( "EndHalfYearOfYearName: {0}", halfyears.EndHalfYearOfYearName );
			WriteLine();
		} // ShowHalfYears

	} // class HalfYearDemo

} // namespace Itenso.TimePeriodDemo
// -- EOF -------------------------------------------------------------------
