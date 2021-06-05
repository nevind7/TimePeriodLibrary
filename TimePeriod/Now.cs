// -- FILE ------------------------------------------------------------------
// name       : Now.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System;
using System.Globalization;

namespace TimePeriod
{

	// ------------------------------------------------------------------------
	public static class Now
	{

		#region Year

		// ----------------------------------------------------------------------
		public static DateTime CalendarYear => Year( TimeSpec.CalendarYearStartMonth ); // CalendarYear

		// ----------------------------------------------------------------------
		public static DateTime Year( YearMonth yearStartMonth )
		{
			DateTime now = ClockProxy.Clock.Now;
			int month = (int)yearStartMonth;
			int monthOffset = now.Month - month;
			int year = monthOffset < 0 ? now.Year - 1 : now.Year;
			return new DateTime( year, month, 1 );
		} // Year

		#endregion

		#region HalfYear

		// ----------------------------------------------------------------------
		public static DateTime CalendarHalfYear => HalfYear( TimeSpec.CalendarYearStartMonth ); // CalendarHalfYear

		// ----------------------------------------------------------------------
		public static DateTime HalfYear( YearMonth yearStartMonth )
		{
			DateTime now = ClockProxy.Clock.Now;
			int year = now.Year;
			if ( now.Month - (int)yearStartMonth < 0 )
			{
				year--;
			}
			YearHalfYear halfYear = TimeTool.GetHalfYearOfMonth( yearStartMonth, (YearMonth)now.Month );
			int months = ( (int)halfYear - 1 ) * TimeSpec.MonthsPerHalfYear;
			return new DateTime( year, (int)yearStartMonth, 1 ).AddMonths( months );
		} // HalfYear

		#endregion

		#region Quarter

		// ----------------------------------------------------------------------
		public static DateTime CalendarQuarter => Quarter( TimeSpec.CalendarYearStartMonth ); // CalendarQuarter

		// ----------------------------------------------------------------------
		public static DateTime Quarter( YearMonth yearStartMonth )
		{
			DateTime now = ClockProxy.Clock.Now;
			int year = now.Year;
			if ( now.Month - (int)yearStartMonth < 0 )
			{
				year--;
			}
			YearQuarter quarter = TimeTool.GetQuarterOfMonth( yearStartMonth, (YearMonth)now.Month );
			int months = ( (int)quarter - 1 ) * TimeSpec.MonthsPerQuarter;
			return new DateTime( year, (int)yearStartMonth, 1 ).AddMonths( months );
		} // Quarter

		#endregion

		#region Month

		// ----------------------------------------------------------------------
		public static DateTime Month => TimeTrim.Day( ClockProxy.Clock.Now ); // Month

		// ----------------------------------------------------------------------
		public static YearMonth YearMonth => (YearMonth)ClockProxy.Clock.Now.Month; // YearMonth

		#endregion

		#region Week

		// ----------------------------------------------------------------------
		public static DateTime Week()
		{
			return DateTimeFormatInfo.CurrentInfo == null ? DateTime.Now : Week( DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek );
		} // Week

		// ----------------------------------------------------------------------
		public static DateTime Week( DayOfWeek firstDayOfWeek )
		{
			DateTime currentDay = ClockProxy.Clock.Now;
			while ( currentDay.DayOfWeek != firstDayOfWeek )
			{
				currentDay = currentDay.AddDays( -1 );
			}
			return new DateTime( currentDay.Year, currentDay.Month, currentDay.Day );
		} // Week

		#endregion

		#region Day

		// ----------------------------------------------------------------------
		public static DateTime Today => ClockProxy.Clock.Now.Date; // Today

		#endregion

		#region Hour

		// ----------------------------------------------------------------------
		public static DateTime Hour => TimeTrim.Minute( ClockProxy.Clock.Now ); // Hour

		#endregion

		#region Minute

		// ----------------------------------------------------------------------
		public static DateTime Minute => TimeTrim.Second( ClockProxy.Clock.Now ); // Minute

		#endregion

		#region Second

		// ----------------------------------------------------------------------
		public static DateTime Second => TimeTrim.Millisecond( ClockProxy.Clock.Now ); // Second

		#endregion

	} // class Now

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
