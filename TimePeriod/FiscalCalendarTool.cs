// -- FILE ------------------------------------------------------------------
// name       : FiscalCalendarTool.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2014.03.08
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2014 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System;

namespace TimePeriod
{

	// ------------------------------------------------------------------------
	public static class FiscalCalendarTool
	{

		#region Month

		// ----------------------------------------------------------------------
		public static int GetDaysInMonth( YearMonth month, YearMonth yearBaseMonth, FiscalQuarterGrouping quarterGrouping )
		{
			int diffMonthCount = month - yearBaseMonth;
			if ( diffMonthCount < 0 )
			{
				diffMonthCount = TimeSpec.MonthsPerYear + diffMonthCount;
			}
			return ( ( diffMonthCount + (int)quarterGrouping + 1 ) % TimeSpec.MonthsPerQuarter ) == 0 ?
				TimeSpec.FiscalDaysPerLongMonth : TimeSpec.FiscalDaysPerShortMonth;
		} // GetDaysInMonth

		// ----------------------------------------------------------------------
		public static int GetYear( int year, YearMonth month, YearMonth yearBaseMonth, YearMonth fiscalYearBaseMonth )
		{
			year = yearBaseMonth >= fiscalYearBaseMonth ? year : year - 1;
			return ( month - yearBaseMonth ) < 0 ? year : year + 1;
		} // GetYear

		// ----------------------------------------------------------------------
		public static DateTime GetStartOfMonth( int year, YearMonth month, YearMonth yearBaseMonth, DayOfWeek yearStartDay,
            FiscalYearAlignment yearAlignment, FiscalQuarterGrouping quarterGrouping )
		{
			int diffMonthCount = month - yearBaseMonth;
			if ( diffMonthCount < 0 )
			{
				year--;
				diffMonthCount = TimeSpec.MonthsPerYear + diffMonthCount;
			}
			DateTime ofYear = GetStartOfYear( year, yearBaseMonth, yearStartDay, yearAlignment );
			int fiveWeekMonthCount = ( diffMonthCount + (int)quarterGrouping ) / TimeSpec.MonthsPerQuarter;
			return ofYear.AddDays(
				( diffMonthCount * TimeSpec.FiscalDaysPerShortMonth ) + ( fiveWeekMonthCount * TimeSpec.DaysPerWeek ) );
		} // GetStartOfMonth

		#endregion

		#region Quarter

		// ----------------------------------------------------------------------
		public static DateTime GetStartOfQuarter( int year, YearQuarter quarter, YearMonth yearBaseMonth, DayOfWeek yearStartDay,
            FiscalYearAlignment yearAlignment )
		{
			DateTime ofYear = GetStartOfYear( year, yearBaseMonth, yearStartDay, yearAlignment );
			DateTime ofQuarter;
			switch ( yearAlignment )
			{
				case FiscalYearAlignment.None:
					ofQuarter = ofYear.AddMonths( ( (int)( quarter ) - 1 ) * TimeSpec.MonthsPerQuarter );
					break;
				case FiscalYearAlignment.LastDay:
				case FiscalYearAlignment.NearestDay:
					ofQuarter = ofYear.AddDays( ( (int)( quarter ) - 1 ) * TimeSpec.FiscalWeeksPerQuarter * TimeSpec.DaysPerWeek );
					break;
				default:
					throw new InvalidOperationException( string.Format( "unknown year alignment {0}", yearAlignment ) );
			}
			return ofQuarter;
		} // GetStartOfQuarter

		#endregion

		#region HalfYear

		// ----------------------------------------------------------------------
		public static DateTime GetStartOfHalfYear( int year, YearHalfYear halfYear, YearMonth yearBaseMonth, DayOfWeek yearStartDay,
            FiscalYearAlignment yearAlignment )
		{
			DateTime ofYear = GetStartOfYear( year, yearBaseMonth, yearStartDay, yearAlignment );
			DateTime ofHalfYear;
			switch ( yearAlignment )
			{
				case FiscalYearAlignment.None:
					ofHalfYear = ofYear.AddMonths( ( (int)halfYear - 1 ) * TimeSpec.MonthsPerHalfYear );
					break;
				case FiscalYearAlignment.LastDay:
				case FiscalYearAlignment.NearestDay:
					ofHalfYear = ofYear.AddDays( ( (int)halfYear - 1 ) * TimeSpec.FiscalWeeksPerHalfYear * TimeSpec.DaysPerWeek );
					break;
				default:
					throw new InvalidOperationException( string.Format( "unknown year alignment {0}", yearAlignment ) );
			}
			return ofHalfYear;
		} // GetStartOfHalfYear

		#endregion

		#region Year

		// ----------------------------------------------------------------------
		public static int GetFiscalYear( int calendarYear, YearMonth fiscalYearBaseMonth, YearMonth yearBaseMonth )
		{
			return yearBaseMonth >= fiscalYearBaseMonth ? calendarYear + 1 : calendarYear;
		} // GetFiscalYear

		// ----------------------------------------------------------------------
		public static DateTime GetStartOfYear( int year, YearMonth yearBaseMonth, DayOfWeek yearStartDay, FiscalYearAlignment yearAlignment )
		{
			DateTime ofYear = new DateTime( year, (int)yearBaseMonth, 1 );

			switch ( yearAlignment )
			{
				case FiscalYearAlignment.None:
					break;
				case FiscalYearAlignment.LastDay:
					while ( ofYear.DayOfWeek != yearStartDay )
					{
						ofYear = ofYear.AddDays( -1 );
					}
					break;
				case FiscalYearAlignment.NearestDay:
					int diffDayCount = Math.Abs( (int)ofYear.DayOfWeek - (int)yearStartDay );
					ofYear = ofYear.AddDays( diffDayCount > 3 ? TimeSpec.DaysPerWeek - diffDayCount : -diffDayCount );
					break;
				default:
					throw new InvalidOperationException( string.Format( "unknown year alignment {0}", yearAlignment ) );
			}

			return ofYear;
		} // GetStartOfYear

		#endregion

	} // class FiscalCalendarTool

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
