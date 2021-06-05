// -- FILE ------------------------------------------------------------------
// name       : HalfyearTimeRange.cs
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
	public abstract class HalfYearTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected HalfYearTimeRange( int startYear, YearHalfYear startHalfYear, int halfYearCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( calendar, startYear, startHalfYear, halfYearCount ), calendar )
		{
			this.startYear = startYear;
			this.startHalfYear = startHalfYear;
			this.halfYearCount = halfYearCount;
			TimeTool.AddHalfYear( startYear, startHalfYear, halfYearCount - 1, out endYear, out endHalfYear );
		} // HalfyearTimeRange

		// ----------------------------------------------------------------------
		public override int BaseYear => startYear; // BaseYear

		// ----------------------------------------------------------------------
		public int StartYear => Calendar.GetYear( startYear, (int)Calendar.YearBaseMonth ); // StartYear

		// ----------------------------------------------------------------------
		public int EndYear => Calendar.GetYear( endYear, (int)Calendar.YearBaseMonth ); // EndYear

		// ----------------------------------------------------------------------
		public YearHalfYear StartHalfYear => startHalfYear; // StartHalfYear

		// ----------------------------------------------------------------------
		public YearHalfYear EndHalfYear => endHalfYear; // EndHalfYear

		// ----------------------------------------------------------------------
		public int HalfYearCount => halfYearCount; // HalfYearCount

		// ----------------------------------------------------------------------
		public string StartHalfYearName => Calendar.GetHalfYearName( StartHalfYear ); // StartHalfyearName

		// ----------------------------------------------------------------------
		public string StartHalfYearOfYearName => Calendar.GetHalfYearOfYearName( StartYear, StartHalfYear ); // StartHalfyearOfYearName

		// ----------------------------------------------------------------------
		public string EndHalfYearName => Calendar.GetHalfYearName( EndHalfYear ); // EndHalfyearName

		// ----------------------------------------------------------------------
		public string EndHalfYearOfYearName => Calendar.GetHalfYearOfYearName( EndYear, EndHalfYear ); // EndHalfyearOfYearName

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetQuarters()
		{
			TimePeriodCollection quarters = new TimePeriodCollection();
			YearQuarter startQuarter = StartHalfYear == YearHalfYear.First ? YearQuarter.First : YearQuarter.Third;
			for ( int i = 0; i < halfYearCount; i++ )
			{
				for ( int quarter = 0; quarter < TimeSpec.QuartersPerHalfYear; quarter++ )
				{
					int year;
					YearQuarter yearQuarter;
					TimeTool.AddQuarter( startYear, startQuarter, ( i * TimeSpec.QuartersPerHalfYear ) + quarter, out year, out yearQuarter );
					quarters.Add( new Quarter( year, yearQuarter, Calendar ) );
				}
			}
			return quarters;
		} // GetQuarters

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetMonths()
		{
			TimePeriodCollection months = new TimePeriodCollection();
            YearMonth startMonth = YearBaseMonth;
            if ( StartHalfYear == YearHalfYear.Second )
            {
                int year;
                TimeTool.AddMonth( startYear, startMonth, TimeSpec.MonthsPerHalfYear, out year, out startMonth );
            }
			for ( int i = 0; i < halfYearCount; i++ )
			{
				for ( int month = 0; month < TimeSpec.MonthsPerHalfYear; month++ )
				{
					int year;
					YearMonth yearMonth;
					TimeTool.AddMonth( startYear, startMonth, ( i * TimeSpec.MonthsPerHalfYear ) + month, out year, out yearMonth );
					months.Add( new Month( year, yearMonth, Calendar ) );
				}
			}
			return months;
		} // GetMonths

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as HalfYearTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( HalfYearTimeRange comp )
		{
			return
				startYear == comp.startYear &&
                startHalfYear == comp.startHalfYear &&
				halfYearCount == comp.halfYearCount &&
				endYear == comp.endYear &&
				endHalfYear == comp.endHalfYear;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), startYear, startHalfYear, halfYearCount, endYear, endHalfYear );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static DateTime GetStartOfHalfYear( ITimeCalendar calendar, int year, YearHalfYear halfyear )
		{
			DateTime startOfHalfyear;

			switch ( calendar.YearType )
			{
				case YearType.FiscalYear:
					startOfHalfyear = FiscalCalendarTool.GetStartOfHalfYear( year, halfyear,
						calendar.YearBaseMonth, calendar.FiscalFirstDayOfYear, calendar.FiscalYearAlignment );
					break;
				default:
					DateTime yearStart = new DateTime( year, (int)calendar.YearBaseMonth, 1 );
					startOfHalfyear = yearStart.AddMonths( ( (int)halfyear - 1 ) * TimeSpec.MonthsPerHalfYear );
					break;
			}
			return startOfHalfyear;
		} // GetStartOfHalfYear

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( ITimeCalendar calendar, int startYear, YearHalfYear startHalfyear, int halfyearCount )
		{
			if ( halfyearCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "halfyearCount" );
			}

			DateTime start = GetStartOfHalfYear( calendar, startYear, startHalfyear );
			int endYear;
			YearHalfYear endHalfyear;
			TimeTool.AddHalfYear( startYear, startHalfyear, halfyearCount, out endYear, out endHalfyear );
			DateTime end = GetStartOfHalfYear( calendar, endYear, endHalfyear );

			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int startYear;
		private readonly YearHalfYear startHalfYear;
		private readonly int halfYearCount;
		private readonly int endYear; // cache
		private readonly YearHalfYear endHalfYear; // cache

	} // class HalfyearTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------