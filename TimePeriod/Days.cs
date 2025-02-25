// -- FILE ------------------------------------------------------------------
// name       : Days.cs
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
	public sealed class Days : DayTimeRange
	{

		// ----------------------------------------------------------------------
		public Days( DateTime moment, int count ) :
			this( moment, count, new TimeCalendar() )
		{
		} // Days

		// ----------------------------------------------------------------------
		public Days( DateTime moment, int count, ITimeCalendar calendar ) :
			this( calendar.GetYear( moment ), calendar.GetMonth( moment ), calendar.GetDayOfMonth( moment ), count, calendar )
		{
		} // Days

		// ----------------------------------------------------------------------
		public Days( int year, int month, int day, int dayCount ) :
			this( year, month, day, dayCount, new TimeCalendar() )
		{
		} // Days

		// ----------------------------------------------------------------------
		public Days( int year, int month, int day, int dayCount, ITimeCalendar calendar ) :
			base( year, month, day, dayCount, calendar )
		{
		} // Days

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetDays()
		{
			TimePeriodCollection days = new TimePeriodCollection();
			DateTime day = new DateTime( StartYear, StartMonth, StartDay );
			for ( int i = 0; i < DayCount; i++ )
			{
				days.Add( new Day( day.AddDays( i ), Calendar ) );
			}
			return days;
		} // GetDays

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( StartDayName, EndDayName, 
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

	} // class Days

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
