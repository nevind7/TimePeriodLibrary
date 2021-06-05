// -- FILE ------------------------------------------------------------------
// name       : Months.cs
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
	public sealed class Months : MonthTimeRange
	{

		// ----------------------------------------------------------------------
		public Months( DateTime moment, YearMonth month, int count ) :
			this( moment, month, count, new TimeCalendar() )
		{
		} // Months

		// ----------------------------------------------------------------------
		public Months( DateTime moment, YearMonth month, int count, ITimeCalendar calendar ) :
			this( calendar.GetYear( moment ), month, count, calendar )
		{
		} // Months

		// ----------------------------------------------------------------------
		public Months( int year, YearMonth month, int monthCounth ) :
			this( year, month, monthCounth, new TimeCalendar() )
		{
		} // Months

		// ----------------------------------------------------------------------
		public Months( int year, YearMonth month, int monthCount, ITimeCalendar calendar ) :
			base( year, month, monthCount, calendar )
		{
		} // Months

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetMonths()
		{
			TimePeriodCollection months = new TimePeriodCollection();
			for ( int i = 0; i < MonthCount; i++ )
			{
                TimeTool.AddMonth( StartYear, StartMonth, i, out var year, out var month );
				months.Add( new Month( year, month, Calendar ) );
			}
			return months;
		} // GetMonths

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( StartMonthOfYearName, EndMonthOfYearName, 
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

	} // class Months

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
