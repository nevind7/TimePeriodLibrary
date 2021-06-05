// -- FILE ------------------------------------------------------------------
// name       : Quarters.cs
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
	public sealed class Quarters : QuarterTimeRange
	{

		// ----------------------------------------------------------------------
		public Quarters( DateTime moment, YearQuarter yearQuarter, int count ) :
			this( moment, yearQuarter, count, new TimeCalendar() )
		{
		} // Quarters

		// ----------------------------------------------------------------------
		public Quarters( DateTime moment, YearQuarter yearQuarter, int count, ITimeCalendar calendar ) :
			this( TimeTool.GetYearOf( calendar.YearBaseMonth, calendar.GetYear( moment ), calendar.GetMonth( moment ) ),
			yearQuarter, count, calendar )
		{
		} // Quarters

		// ----------------------------------------------------------------------
		public Quarters( int year, YearQuarter yearQuarter, int quarterCount ) :
			this( year, yearQuarter, quarterCount, new TimeCalendar() )
		{
		} // Quarters

		// ----------------------------------------------------------------------
		public Quarters( int year, YearQuarter yearQuarter, int quarterCount, ITimeCalendar calendar ) :
			base( year, yearQuarter, quarterCount, calendar )
		{
		} // Quarters

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetQuarters()
		{
			TimePeriodCollection quarters = new TimePeriodCollection();
			for ( int i = 0; i < QuarterCount; i++ )
			{
                TimeTool.AddQuarter( BaseYear, StartQuarter, i, out var year, out var quarter );
				quarters.Add( new Quarter( year, quarter, Calendar ) );
			}
			return quarters;
		} // GetQuarters

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( StartQuarterOfYearName, EndQuarterOfYearName,
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

	} // class Quarters

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
