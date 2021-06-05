// -- FILE ------------------------------------------------------------------
// name       : HalfYear.cs
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
	public sealed class HalfYear : HalfYearTimeRange
	{

		// ----------------------------------------------------------------------
		public HalfYear() :
			this( new TimeCalendar() )
		{
		} // HalfYear

		// ----------------------------------------------------------------------
		public HalfYear( DateTime moment ) :
			this( moment, new TimeCalendar() )
		{
		} // HalfYear

		// ----------------------------------------------------------------------
		public HalfYear( ITimeCalendar calendar ) :
			this( ClockProxy.Clock.Now, calendar )
		{
		} // HalfYear

		// ----------------------------------------------------------------------
		public HalfYear( DateTime moment, ITimeCalendar calendar ) :
			this( TimeTool.GetYearOf( calendar.YearBaseMonth, calendar.GetYear( moment ), calendar.GetMonth( moment ) ),
				TimeTool.GetHalfYearOfMonth( calendar.YearBaseMonth, (YearMonth)calendar.GetMonth( moment ) ), calendar )
		{
		} // HalfYear

		// ----------------------------------------------------------------------
		public HalfYear( int year, YearHalfYear yearHalfYear ) :
			this( year, yearHalfYear, new TimeCalendar() )
		{
		} // HalfYear

		// ----------------------------------------------------------------------
		public HalfYear( int year, YearHalfYear yearHalfYear, ITimeCalendar calendar ) :
			base( year, yearHalfYear, 1, calendar )
		{
		} // HalfYear

		// ----------------------------------------------------------------------
		public int Year => StartYear; // Year

		// ----------------------------------------------------------------------
		public YearMonth StartMonth
		{
			get
			{
                int monthCount = ( (int)StartHalfYear - 1 ) * TimeSpec.MonthsPerHalfYear;
				TimeTool.AddMonth( BaseYear, Calendar.YearBaseMonth, monthCount, out var year, out var month );
				return month;
			}
		} // StartMonth

		// ----------------------------------------------------------------------
		public YearHalfYear YearHalfYear => StartHalfYear; // YearHalfYear

		// ----------------------------------------------------------------------
		public string HalfYearName => StartHalfYearName; // HalfYearName

		// ----------------------------------------------------------------------
		public string HalfYearOfYearName => StartHalfYearOfYearName; // HalfYearOfYearName

		// ----------------------------------------------------------------------
		public bool IsCalendarHalfYear => ( (int)YearBaseMonth - 1 ) % TimeSpec.MonthsPerHalfYear == 0; // IsCalendarHalfYear

		// ----------------------------------------------------------------------
		public bool MultipleCalendarYears
		{
			get
			{
				if ( IsCalendarHalfYear )
				{
					return false;
				}

                int monthCount = ( (int)StartHalfYear - 1 ) * TimeSpec.MonthsPerHalfYear;
				TimeTool.AddMonth( BaseYear, YearBaseMonth, monthCount, out var year, out var month );

                monthCount += TimeSpec.MonthsPerHalfYear;
				TimeTool.AddMonth( BaseYear, YearBaseMonth, monthCount, out var endYear, out month );
				return year != endYear;
			}
		} // MultipleCalendarYears

		// ----------------------------------------------------------------------
		public HalfYear GetPreviousHalfYear()
		{
			return AddHalfYears( -1 );
		} // GetPreviousHalfYear

		// ----------------------------------------------------------------------
		public HalfYear GetNextHalfYear()
		{
			return AddHalfYears( 1 );
		} // GetNextHalfYear

		// ----------------------------------------------------------------------
		public HalfYear AddHalfYears( int count )
		{
            TimeTool.AddHalfYear( BaseYear, StartHalfYear, count, out var year, out var halfYear );
			return new HalfYear( year, halfYear, Calendar );
		} // AddHalfYears

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( HalfYearOfYearName,
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

	} // class HalfYear

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
