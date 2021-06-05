// -- FILE ------------------------------------------------------------------
// name       : WeekTimeRange.cs
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
	public abstract class WeekTimeRange : CalendarTimeRange
	{

		// ----------------------------------------------------------------------
		protected WeekTimeRange( int year, int week, int weekCount ) :
			this( year, week, weekCount, new TimeCalendar() )
		{
		} // WeekTimeRange

		// ----------------------------------------------------------------------
		protected WeekTimeRange( int year, int week, int weekCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( year, week, weekCount, calendar ), calendar )
		{
			this.year = year;
			this.week = week;
			this.weekCount = weekCount;
		} // WeekTimeRange

		// ----------------------------------------------------------------------
		protected WeekTimeRange( DateTime moment, int weekCount ) :
			this( moment, weekCount, new TimeCalendar() )
		{
		} // WeekTimeRange

		// ----------------------------------------------------------------------
		protected WeekTimeRange( DateTime moment, int weekCount, ITimeCalendar calendar ) :
			base( GetPeriodOf( moment, weekCount, calendar ), calendar )
		{
			TimeTool.GetWeekOfYear( moment, calendar.Culture, calendar.YearWeekType, out year, out week );
			this.weekCount = weekCount;
		} // WeekTimeRange

		// ----------------------------------------------------------------------
		public int Year => year; // Year

		// ----------------------------------------------------------------------
		public int WeekCount => weekCount; // WeekCount

		// ----------------------------------------------------------------------
		public int StartWeek => week; // StartWeek

		// ----------------------------------------------------------------------
		public int EndWeek => week + weekCount - 1; // EndWeek

		// ----------------------------------------------------------------------
		public string StartWeekOfYearName => Calendar.GetWeekOfYearName( Year, StartWeek ); // StartWeekOfYearName

		// ----------------------------------------------------------------------
		public string EndWeekOfYearName => Calendar.GetWeekOfYearName( Year, EndWeek ); // EndWeekOfYearName

		// ----------------------------------------------------------------------
		public DateTime GetStartOfWeek( int weekIndex )
		{
			if ( weekIndex < 0 || weekIndex >= weekCount )
			{
				throw new ArgumentOutOfRangeException( "weekIndex" );
			}

			DateTime date = TimeTool.GetStartOfYearWeek( year, week, Calendar.Culture, Calendar.YearWeekType );
			return date.AddDays( weekIndex * TimeSpec.DaysPerWeek );
		} // GetStartOfWeek

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetDays()
		{
			TimePeriodCollection days = new TimePeriodCollection();
			DateTime date = TimeTool.GetStartOfYearWeek( year, week, Calendar.Culture, Calendar.YearWeekType );
			int dayCount = weekCount * TimeSpec.DaysPerWeek;
			for ( int i = 0; i < dayCount; i++ )
			{
				days.Add( new Day( date.AddDays( i ), Calendar ) );
			}
			return days;
		} // GetDays

		// ----------------------------------------------------------------------
		protected override bool IsEqual( object obj )
		{
			return base.IsEqual( obj ) && HasSameData( obj as WeekTimeRange );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( WeekTimeRange comp )
		{
			return year == comp.year && week == comp.week && weekCount == comp.weekCount;
		} // HasSameData

		// ----------------------------------------------------------------------
		protected override int ComputeHashCode()
		{
			return HashTool.ComputeHashCode( base.ComputeHashCode(), year, week, weekCount );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( DateTime moment, int weekCount, ITimeCalendar calendar )
		{
            TimeTool.GetWeekOfYear( moment, calendar.Culture, calendar.YearWeekType, out var year, out var weekOfYear );
			return GetPeriodOf( year, weekOfYear, weekCount, calendar );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		private static TimeRange GetPeriodOf( int year, int weekOfYear, int weekCount, ITimeCalendar calendar )
		{
			if ( weekCount < 1 )
			{
				throw new ArgumentOutOfRangeException( "weekCount" );
			}

			DateTime start = TimeTool.GetStartOfYearWeek( year, weekOfYear, calendar.Culture, calendar.YearWeekType );
			DateTime end = start.AddDays( weekCount * TimeSpec.DaysPerWeek );
			return new TimeRange( start, end );
		} // GetPeriodOf

		// ----------------------------------------------------------------------
		// members
		private readonly int year;
		private readonly int week;
		private readonly int weekCount;

	} // class WeekTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
