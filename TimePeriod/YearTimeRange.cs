// -- FILE ------------------------------------------------------------------
// name       : YearTimeRange.cs
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
	public abstract class YearTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected YearTimeRange( int year, int yearCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( calendar, year, yearCount ), calendar )
		{
			this.year = year;
			this.yearCount = yearCount;
			endYear = End.Year;
		} // YearTimeRange

		// ----------------------------------------------------------------------
		public int YearCount => yearCount; // YearCount

		// ----------------------------------------------------------------------
		public override int BaseYear => year; // BaseYear

		// ----------------------------------------------------------------------
		public int StartYear => Calendar.GetYear( year, (int)YearBaseMonth ); // StartYear

		// ----------------------------------------------------------------------
		public int EndYear => Calendar.GetYear( endYear, (int)YearBaseMonth ); // EndYear

		// ----------------------------------------------------------------------
		public string StartYearName => Calendar.GetYearName( StartYear ); // StartYearName

		// ----------------------------------------------------------------------
		public string EndYearName => Calendar.GetYearName( StartYear + YearCount - 1 ); // EndYearName

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetHalfYears()
		{
			TimePeriodCollection halfYears = new TimePeriodCollection();
			for ( int i = 0; i < yearCount; i++ )
			{
				for ( int halfYear = 0; halfYear < TimeSpec.HalfYearsPerYear; halfYear++ )
				{
                    TimeTool.AddHalfYear( this.year, YearHalfYear.First, ( i * TimeSpec.HalfYearsPerYear ) + halfYear, out var year, out var yearHalfYear );
					halfYears.Add( new HalfYear( year, yearHalfYear, Calendar ) );
				}
			}
			return halfYears;
		} // GetHalfYears

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetQuarters()
		{
			TimePeriodCollection quarters = new TimePeriodCollection();
			for ( int i = 0; i < yearCount; i++ )
			{
				for ( int quarter = 0; quarter < TimeSpec.QuartersPerYear; quarter++ )
				{
                    TimeTool.AddQuarter( this.year, YearQuarter.First, ( i * TimeSpec.QuartersPerYear ) + quarter, out var year, out var yearQuarter );
					quarters.Add( new Quarter( year, yearQuarter, Calendar ) );
				}
			}
			return quarters;
		} // GetQuarters

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetMonths()
		{
			TimePeriodCollection months = new TimePeriodCollection();
			for ( int i = 0; i < yearCount; i++ )
			{
				for ( int month = 0; month < TimeSpec.MonthsPerYear; month++ )
				{
                    TimeTool.AddMonth( this.year, YearBaseMonth, ( i * TimeSpec.MonthsPerYear ) + month, out var year, out var yearMonth );
					months.Add( new Month( year, yearMonth, Calendar ) );
				}
			}
			return months;
		} // GetMonths

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as YearTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( YearTimeRange comp )
		{
			return
				year == comp.year &&
				endYear == comp.endYear &&
				yearCount == comp.yearCount;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), year, year, yearCount );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static DateTime GetStartOfYear( ITimeCalendar calendar, int year )
		{
			DateTime startOfYear;

			switch ( calendar.YearType )
			{
				case YearType.FiscalYear:
                    startOfYear = FiscalCalendarTool.GetStartOfYear( year, calendar.YearBaseMonth,
						calendar.FiscalFirstDayOfYear, calendar.FiscalYearAlignment );
					break;
				default:
                    startOfYear = new DateTime( year, (int)calendar.YearBaseMonth, 1 );
					break;
			}
			return startOfYear;
		} // GetStartOfYear

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( ITimeCalendar calendar, int year, int yearCount )
		{
			if ( yearCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "yearCount" );
			}

			DateTime start = GetStartOfYear( calendar, year );
			DateTime end = GetStartOfYear( calendar, year + yearCount );
			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int year;
		private readonly int yearCount;
		private readonly int endYear; // cache

	} // class YearTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
