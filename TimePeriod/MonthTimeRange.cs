// -- FILE ------------------------------------------------------------------
// name       : MonthTimeRange.cs
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
	public abstract class MonthTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected MonthTimeRange( int year, YearMonth month, int monthCounth ) :
			this( year, month, monthCounth, new TimeCalendar() )
		{
		} // MonthTimeRange

		// ----------------------------------------------------------------------
		protected MonthTimeRange( int year, YearMonth month, int monthCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( calendar, year, month, monthCount ), calendar )
		{
			this.year = year;
			this.month = month;
			this.monthCount = monthCount;
			TimeTool.AddMonth( year, month, monthCount - 1, out endYear, out endMonth );
		} // MonthTimeRange

		// ----------------------------------------------------------------------
		public int StartYear => Calendar.GetYear( year, (int)month ); // StartYear

		// ----------------------------------------------------------------------
		public int EndYear => Calendar.GetYear( endYear, (int)endMonth ); // EndYear

		// ----------------------------------------------------------------------
		public YearMonth StartMonth => month; // StartMonth

		// ----------------------------------------------------------------------
		public YearMonth EndMonth => endMonth; // EndMonth

		// ----------------------------------------------------------------------
		public int MonthCount => monthCount; // MonthCount

		// ----------------------------------------------------------------------
		public string StartMonthName => Calendar.GetMonthName( (int)StartMonth ); // StartMonthName

		// ----------------------------------------------------------------------
		public string StartMonthOfYearName => Calendar.GetMonthOfYearName( StartYear, (int)StartMonth ); // StartMonthOfYearName

		// ----------------------------------------------------------------------
		public string EndMonthName => Calendar.GetMonthName( (int)EndMonth ); // EndMonthName

		// ----------------------------------------------------------------------
		public string EndMonthOfYearName => Calendar.GetMonthOfYearName( EndYear, (int)EndMonth ); // EndMonthOfYearName

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetDays()
		{
			TimePeriodCollection days = new TimePeriodCollection();
			DateTime date = GetStartOfMonth( Calendar, year, month );
			for ( int month = 0; month < monthCount; month++ )
			{
				DateTime monthStart = date.AddMonths( month );
				int daysOfMonth = TimeTool.GetDaysInMonth( monthStart.Year, monthStart.Month );
				for ( int day = 0; day < daysOfMonth; day++ )
				{
					days.Add( new Day( monthStart.AddDays( day ), Calendar ) );
				}
			}
			return days;
		} // GetDays

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as MonthTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( MonthTimeRange comp )
		{
			return
				year == comp.year &&
				month == comp.month &&
				monthCount == comp.monthCount &&
				endYear == comp.endYear &&
				endMonth == comp.endMonth;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), year, month, monthCount, endYear, endMonth );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static DateTime GetStartOfMonth( ITimeCalendar calendar, int year, YearMonth month )
		{
			DateTime ofMonth;
			if ( calendar.YearType == YearType.FiscalYear )
			{
				ofMonth = FiscalCalendarTool.GetStartOfMonth(
					year, month, calendar.YearBaseMonth, calendar.FiscalFirstDayOfYear, calendar.FiscalYearAlignment, calendar.FiscalQuarterGrouping );
			}
			else
			{
				ofMonth = new DateTime( year, (int)month, 1 );
			}
			return ofMonth;
		} // GetStartOfMonth

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( ITimeCalendar calendar, int year, YearMonth month, int monthCount )
		{
			if ( monthCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "monthCount" );
			}

			DateTime start = GetStartOfMonth( calendar, year, month );
			DateTime end;
			if ( calendar.YearType == YearType.FiscalYear )
			{
                TimeTool.AddMonth( year, month, monthCount, out var endYear, out var endMonth );
				end = GetStartOfMonth( calendar, endYear, endMonth );
			}
			else
			{
				end = start.AddMonths( monthCount );
			}
			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int year;
		private readonly YearMonth month;
		private readonly int monthCount;
		private readonly int endYear; // cache
		private readonly YearMonth endMonth; // cache

	} // class MonthTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
