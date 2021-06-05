// -- FILE ------------------------------------------------------------------
// name       : HalfYears.cs
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
	public sealed class HalfYears : HalfYearTimeRange
	{

		// ----------------------------------------------------------------------
		public HalfYears( DateTime moment, YearHalfYear halfYear, int count ) :
			this( moment, halfYear, count, new TimeCalendar() )
		{
		} // HalfYears

		// ----------------------------------------------------------------------
		public HalfYears( DateTime moment, YearHalfYear halfYear, int count, ITimeCalendar calendar ) :
			this( TimeTool.GetYearOf( calendar.YearBaseMonth, calendar.GetYear( moment ), calendar.GetMonth( moment ) ),
			halfYear, count, calendar )
		{
		} // HalfYears

		// ----------------------------------------------------------------------
		public HalfYears( int year, YearHalfYear halfYear, int halfYearCount ) :
			this( year, halfYear, halfYearCount, new TimeCalendar() )
		{
		} // HalfYears

		// ----------------------------------------------------------------------
		public HalfYears( int year, YearHalfYear halfYear, int halfYearCount, ITimeCalendar calendar ) :
			base( year, halfYear, halfYearCount, calendar )
		{
		} // HalfYears

		// ----------------------------------------------------------------------
		public ITimePeriodCollection GetHalfYears()
		{
			TimePeriodCollection halfYears = new TimePeriodCollection();
			for ( int i = 0; i < HalfYearCount; i++ )
			{
                TimeTool.AddHalfYear( BaseYear, StartHalfYear, i, out var year, out var halfYear );
				halfYears.Add( new HalfYear( year, halfYear, Calendar ) );
			}
			return halfYears;
		} // GetHalfYears

		// ----------------------------------------------------------------------
		protected override string Format( ITimeFormatter formatter )
		{
			return formatter.GetCalendarPeriod( StartHalfYearOfYearName, EndHalfYearOfYearName,
				formatter.GetShortDate( Start ), formatter.GetShortDate( End ), Duration );
		} // Format

	} // class HalfYears

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
