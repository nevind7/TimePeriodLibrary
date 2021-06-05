// -- FILE ------------------------------------------------------------------
// name       : QuarterTimeRange.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System;

namespace TimePeriod
{

	// ------------------------------------------------------------------------
	public abstract class QuarterTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected QuarterTimeRange( int year, YearQuarter quarter, int quarterCount ) :
			this( year, quarter, quarterCount, new TimeCalendar() )
		{
		} // QuarterTimeRange

		// ----------------------------------------------------------------------
		protected QuarterTimeRange( int year, YearQuarter quarter, int quarterCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( calendar, year, quarter, quarterCount ), calendar )
		{
			this.year = year;
			this.quarter = quarter;
			this.quarterCount = quarterCount;
			TimeTool.AddQuarter( year, quarter, quarterCount - 1, out endYear, out endQuarter );
		} // QuarterTimeRange

		// ----------------------------------------------------------------------
		public override int BaseYear => year; // BaseYear

		// ----------------------------------------------------------------------
		public int StartYear => Calendar.GetYear( year, (int)Calendar.YearBaseMonth ); // StartYear

		// ----------------------------------------------------------------------
		public int EndYear => Calendar.GetYear( endYear, (int)Calendar.YearBaseMonth ); // EndYear

		// ----------------------------------------------------------------------
		public YearQuarter StartQuarter => quarter; // StartQuarter

		// ----------------------------------------------------------------------
		public YearQuarter EndQuarter => endQuarter; // EndQuarter

		// ----------------------------------------------------------------------
		public int QuarterCount => quarterCount; // QuarterCount

		// ----------------------------------------------------------------------
		public string StartQuarterName => Calendar.GetQuarterName( StartQuarter ); // StartQuarterName

		// ----------------------------------------------------------------------
		public string StartQuarterOfYearName => Calendar.GetQuarterOfYearName( StartYear, StartQuarter ); // StartQuarterOfYearName

		// ----------------------------------------------------------------------
		public string EndQuarterName => Calendar.GetQuarterName( EndQuarter ); // EndQuarterName

		// ----------------------------------------------------------------------
		public string EndQuarterOfYearName => Calendar.GetQuarterOfYearName( EndYear, EndQuarter ); // EndQuarterOfYearName

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetMonths()
		{
			TimePeriodCollection months = new TimePeriodCollection();
			for ( int i = 0; i < quarterCount; i++ )
			{
				for ( int month = 0; month < TimeSpec.MonthsPerQuarter; month++ )
				{
                    TimeTool.AddMonth( this.year, YearBaseMonth, ( i * TimeSpec.MonthsPerQuarter ) + month, out var year, out var yearMonth );
					months.Add( new Month( year, yearMonth, Calendar ) );
				}
			}
			return months;
		} // GetMonths

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as QuarterTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( QuarterTimeRange comp )
		{
			return
				year == comp.year &&
				quarter == comp.quarter &&
				quarterCount == comp.quarterCount &&
				endYear == comp.endYear &&
				endQuarter == comp.endQuarter;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), year, quarter, quarterCount, endYear, endQuarter );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static DateTime GetStartOfQuarter( ITimeCalendar calendar, int year, YearQuarter quarter )
		{
			DateTime ofQuarter;

			switch ( calendar.YearType )
			{
				case YearType.FiscalYear:
					ofQuarter = FiscalCalendarTool.GetStartOfQuarter( year, quarter,
						calendar.YearBaseMonth, calendar.FiscalFirstDayOfYear, calendar.FiscalYearAlignment );
					break;
				default:
					DateTime yearStart = new DateTime( year, (int)calendar.YearBaseMonth, 1 );
					ofQuarter = yearStart.AddMonths( ( (int)quarter - 1 ) * TimeSpec.MonthsPerQuarter );
					break;
			}

			return ofQuarter;
		} // GetStartOfQuarter

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( ITimeCalendar calendar, int year, YearQuarter quarter, int quarterCount )
		{
			if ( quarterCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "quarterCount" );
			}

			DateTime start = GetStartOfQuarter( calendar, year, quarter );
            TimeTool.AddQuarter( year, quarter, quarterCount, out var endYear, out var endQuarter );
			DateTime end = GetStartOfQuarter( calendar, endYear, endQuarter );

			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int year;
		private readonly YearQuarter quarter;
		private readonly int quarterCount;
		private readonly int endYear; // cache
		private readonly YearQuarter endQuarter; // cache

	} // class QuarterTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
