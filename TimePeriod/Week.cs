// -- FILE ------------------------------------------------------------------
// name       : Week.cs
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
	public sealed class Week : WeekTimeRange
	{

		// ----------------------------------------------------------------------
		public Week() :
			this( new TimeCalendar() )
		{
		} // Week

		// ----------------------------------------------------------------------
		public Week( ITimeCalendar calendar ) :
			this( ClockProxy.Clock.Now, calendar )
		{
		} // Week

		// ----------------------------------------------------------------------
		public Week( DateTime moment ) :
			this( moment, new TimeCalendar() )
		{
		} // Week

		// ----------------------------------------------------------------------
		public Week( DateTime moment, ITimeCalendar calendar ) :
			base( moment, 1, calendar )
		{
		} // Week

		// ----------------------------------------------------------------------
		public Week( int year, int weekOfYear ) :
			this( year, weekOfYear, new TimeCalendar() )
		{
		} // Week

		// ----------------------------------------------------------------------
		public Week( int year, int weekOfYear, ITimeCalendar calendar ) :
			base( year, weekOfYear, 1, calendar )
		{
		} // Week

		// ----------------------------------------------------------------------
		public int WeekOfYear => StartWeek; // WeekOfYear

		// ----------------------------------------------------------------------
		public string WeekOfYearName => StartWeekOfYearName; // WeekOfYearName

		// ----------------------------------------------------------------------
		public DateTime FirstDayOfWeek => Start; // FirstDayOfWeek

		// ----------------------------------------------------------------------
		public DateTime LastDayOfWeek => FirstDayOfWeek.AddDays( TimeSpec.DaysPerWeek - 1 ); // LastDayOfWeek

		// ----------------------------------------------------------------------
		public bool MultipleCalendarYears => FirstDayOfWeek.Year != LastDayOfWeek.Year; // IsCalendarHalfYear

		// ----------------------------------------------------------------------
		public Week GetPreviousWeek()
		{
			return AddWeeks( -1 );
		} // GetPreviousWeek

		// ----------------------------------------------------------------------
		public Week GetNextWeek()
		{
			return AddWeeks( 1 );
		} // GetNextWeek

		// ----------------------------------------------------------------------
		public Week AddWeeks( int weeks )
		{
			DateTime date = TimeTool.GetStartOfYearWeek( Year, StartWeek, Calendar.Culture, Calendar.YearWeekType );
			return new Week( date.AddDays( weeks * TimeSpec.DaysPerWeek ), Calendar );
		} // AddWeeks

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( WeekOfYearName, 
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

	} // class Week

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
