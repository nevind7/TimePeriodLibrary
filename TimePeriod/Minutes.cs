// -- FILE ------------------------------------------------------------------
// name       : Minutes.cs
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
	public sealed class Minutes : MinuteTimeRange
	{

		// ----------------------------------------------------------------------
		public Minutes( DateTime moment, int count ) :
			this( moment, count, new TimeCalendar() )
		{
		} // Minutes

		// ----------------------------------------------------------------------
		public Minutes( DateTime moment, int count, ITimeCalendar calendar ) :
			this( calendar.GetYear( moment ), calendar.GetMonth( moment ), calendar.GetDayOfMonth( moment ),
			calendar.GetHour( moment ), calendar.GetMinute( moment ), count, calendar )
		{
		} // Minutes

		// ----------------------------------------------------------------------
		public Minutes( int year, int month, int day, int hour, int minute, int minuteCount ) :
			this( year, month, day, hour, minute, minuteCount, new TimeCalendar() )
		{
		} // Minutes

		// ----------------------------------------------------------------------
		public Minutes( int year, int month, int day, int hour, int minute, int minuteCount, ITimeCalendar calendar ) :
			base( year, month, day, hour, minute, minuteCount, calendar )
		{
		} // Minutes

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetMinutes()
		{
			TimePeriodCollection minutes = new TimePeriodCollection();
			DateTime minute = new DateTime( StartYear, StartMonth, StartDay, StartHour, StartMinute, 0 );
			for ( int i = 0; i < MinuteCount; i++ )
			{
				minutes.Add( new Minute( minute.AddMinutes( i ), Calendar ) );
			}
			return minutes;
		} // GetMinutes

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( formatter.GetShortDate( Start ), formatter.GetShortDate( End ),
				formatter.GetShortTime( Start ), formatter.GetShortTime( End ), Duration );
		} // Format

	} // class Minutes

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
