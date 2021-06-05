// -- FILE ------------------------------------------------------------------
// name       : TimeCalendar.cs
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
	public class TimeCalendar : ITimeCalendar
	{

	// ----------------------------------------------------------------------
		public static readonly TimeSpan DefaultStartOffset = TimeSpec.NoDuration;
		public static readonly TimeSpan DefaultEndOffset = TimeSpec.MinNegativeDuration;

		// ----------------------------------------------------------------------
		public TimeCalendar() :
			this( new TimeCalendarConfig() )
		{
		} // TimeCalendar

		// ----------------------------------------------------------------------
		public TimeCalendar( TimeCalendarConfig config )
		{
			if ( config.StartOffset < TimeSpan.Zero )
			{
				throw new ArgumentOutOfRangeException( "config" );
			}
			if ( config.EndOffset > TimeSpan.Zero )
			{
				throw new ArgumentOutOfRangeException( "config" );
			}

            culture = config.Culture ?? CultureInfo.CurrentCulture;
            yearType = config.YearType ?? YearType.SystemYear;
			startOffset = config.StartOffset ?? DefaultStartOffset;
			endOffset = config.EndOffset ?? DefaultEndOffset;
			yearBaseMonth = config.YearBaseMonth ?? TimeSpec.CalendarYearStartMonth;
			fiscalYearBaseMonth = config.FiscalYearBaseMonth ?? TimeSpec.FiscalYearBaseMonth;
			fiscalFirstDayOfYear = config.FiscalFirstDayOfYear ?? DayOfWeek.Sunday;
			fiscalYearAlignment = config.FiscalYearAlignment ?? FiscalYearAlignment.None;
			fiscalQuarterGrouping = config.FiscalQuarterGrouping ?? FiscalQuarterGrouping.FourFourFiveWeeks;
			yearWeekType = config.YearWeekType ?? YearWeekType.Calendar;
			dayNameType = config.DayNameType ?? CalendarNameType.Full;
			monthNameType = config.MonthNameType ?? CalendarNameType.Full;
		} // TimeCalendar

		// ----------------------------------------------------------------------
		public CultureInfo Culture => culture; // Culture

		// ----------------------------------------------------------------------
		public YearType YearType => yearType; // YearType

		// ----------------------------------------------------------------------
		public TimeSpan StartOffset => startOffset; // StartOffset

		// ----------------------------------------------------------------------
		public TimeSpan EndOffset => endOffset; // EndOffset

		// ----------------------------------------------------------------------
		public YearMonth YearBaseMonth => yearBaseMonth; // YearBaseMonth

		// ----------------------------------------------------------------------
		public YearMonth FiscalYearBaseMonth => fiscalYearBaseMonth; // FiscalYearBaseMonth

		// ----------------------------------------------------------------------
		public DayOfWeek FiscalFirstDayOfYear => fiscalFirstDayOfYear; // FiscalFirstDayOfYear

		// ----------------------------------------------------------------------
		public FiscalYearAlignment FiscalYearAlignment => fiscalYearAlignment; // FiscalYearAlignment

		// ----------------------------------------------------------------------
		public FiscalQuarterGrouping FiscalQuarterGrouping => fiscalQuarterGrouping; // FiscalQuarterGrouping

		// ----------------------------------------------------------------------
		public virtual DayOfWeek FirstDayOfWeek => culture.DateTimeFormat.FirstDayOfWeek; // FirstDayOfWeek

		// ----------------------------------------------------------------------
		public YearWeekType YearWeekType => yearWeekType; // YearWeekType

		// ----------------------------------------------------------------------
		public static TimeCalendar New( CultureInfo culture )
		{
			return new TimeCalendar( new TimeCalendarConfig
			{
				Culture = culture
			} );
		} // New

		// ----------------------------------------------------------------------
		public static TimeCalendar New( YearMonth yearBaseMonth )
		{
			return new TimeCalendar( new TimeCalendarConfig
			{
				YearBaseMonth = yearBaseMonth
			} );
		} // New

		// ----------------------------------------------------------------------
		public static TimeCalendar New( TimeSpan startOffset, TimeSpan endOffset )
		{
			return new TimeCalendar( new TimeCalendarConfig
			{
				StartOffset = startOffset,
				EndOffset = endOffset
			} );
		} // New

		// ----------------------------------------------------------------------
		public static TimeCalendar New( TimeSpan startOffset, TimeSpan endOffset, YearMonth yearBaseMonth )
		{
			return new TimeCalendar( new TimeCalendarConfig
			{
				StartOffset = startOffset,
				EndOffset = endOffset,
				YearBaseMonth = yearBaseMonth,
			} );
		} // New

		// ----------------------------------------------------------------------
		public static TimeCalendar New( CultureInfo culture, TimeSpan startOffset, TimeSpan endOffset )
		{
			return new TimeCalendar( new TimeCalendarConfig
			{
				Culture = culture,
				StartOffset = startOffset,
				EndOffset = endOffset
			} );
		} // New

		// ----------------------------------------------------------------------
		public static TimeCalendar New( CultureInfo culture, YearMonth yearBaseMonth, YearWeekType yearWeekType )
		{
			return new TimeCalendar( new TimeCalendarConfig
			{
				Culture = culture,
				YearBaseMonth = yearBaseMonth,
				YearWeekType = yearWeekType
			} );
		} // New

		// ----------------------------------------------------------------------
		public static TimeCalendar NewEmptyOffset()
		{
			return new TimeCalendar( new TimeCalendarConfig
			{
				StartOffset = TimeSpan.Zero,
				EndOffset = TimeSpan.Zero
			} );
		} // NewEmptyOffset

		// ----------------------------------------------------------------------
		public virtual DateTime MapStart( DateTime moment )
		{
			return moment.Add( startOffset );
		} // MapStart

		// ----------------------------------------------------------------------
		public virtual DateTime MapEnd( DateTime moment )
		{
			return moment.Add( endOffset );
		} // MapEnd

		// ----------------------------------------------------------------------
		public virtual DateTime UnmapStart( DateTime moment )
		{
			return moment.Subtract( startOffset );
		} // UnmapStart

		// ----------------------------------------------------------------------
		public virtual DateTime UnmapEnd( DateTime moment )
		{
			return moment.Subtract( endOffset );
		} // UnmapEnd

		// ----------------------------------------------------------------------
		public virtual int GetYear( DateTime time )
		{
			return culture.Calendar.GetYear( time );
		} // GetYear

		// ----------------------------------------------------------------------
		public virtual int GetMonth( DateTime time )
		{
			return culture.Calendar.GetMonth( time );
		} // GetMonth

		// ----------------------------------------------------------------------
		public virtual int GetHour( DateTime time )
		{
			return culture.Calendar.GetHour( time );
		} // GetHour

		// ----------------------------------------------------------------------
		public virtual int GetMinute( DateTime time )
		{
			return culture.Calendar.GetMinute( time );
		} // GetMinute

		// ----------------------------------------------------------------------
		public virtual int GetDayOfMonth( DateTime time )
		{
			return culture.Calendar.GetDayOfMonth( time );
		} // GetDayOfMonth

		// ----------------------------------------------------------------------
		public virtual DayOfWeek GetDayOfWeek( DateTime time )
		{
			return culture.Calendar.GetDayOfWeek( time );
		} // GetDayOfWeek

		// ----------------------------------------------------------------------
		public virtual int GetDaysInMonth( int year, int month )
		{
			return culture.Calendar.GetDaysInMonth( year, month );
		} // GetDaysInMonth

		// ----------------------------------------------------------------------
		public int GetYear( int year, int month )
		{
			if ( YearType == YearType.FiscalYear )
			{
				year = FiscalCalendarTool.GetYear( year, (YearMonth)month, YearBaseMonth, FiscalYearBaseMonth );
			}
			return year;
		} // GetYear

		// ----------------------------------------------------------------------
		public virtual string GetYearName( int year )
		{
			switch ( YearType )
			{
				case YearType.CalendarYear:
					return Strings.CalendarYearName( year );
				case YearType.FiscalYear:
					return Strings.FiscalYearName( year );
				case YearType.SchoolYear:
					return Strings.SchoolYearName( year );
				default:
					return Strings.SystemYearName( year );
			}
		} // GetYearName

		// ----------------------------------------------------------------------
		public virtual string GetHalfYearName( YearHalfYear yearHalfyear )
		{
			switch ( YearType )
			{
				case YearType.CalendarYear:
					return Strings.CalendarHalfYearName( yearHalfyear );
				case YearType.FiscalYear:
					return Strings.FiscalHalfYearName( yearHalfyear );
				case YearType.SchoolYear:
					return Strings.SchoolHalfYearName( yearHalfyear );
				default:
					return Strings.SystemHalfYearName( yearHalfyear );
			}
		} // GetHalfyearName

		// ----------------------------------------------------------------------
		public virtual string GetHalfYearOfYearName( int year, YearHalfYear yearHalfyear )
		{
			switch ( YearType )
			{
				case YearType.CalendarYear:
					return Strings.CalendarHalfYearOfYearName( yearHalfyear, year );
				case YearType.FiscalYear:
					return Strings.FiscalHalfYearOfYearName( yearHalfyear, year );
				case YearType.SchoolYear:
					return Strings.SchoolHalfYearOfYearName( yearHalfyear, year );
				default:
					return Strings.SystemHalfYearOfYearName( yearHalfyear, year );
			}
		} // GetHalfyearOfYearName

		// ----------------------------------------------------------------------
		public virtual string GetQuarterName( YearQuarter yearQuarter )
		{
			switch ( YearType )
			{
				case YearType.CalendarYear:
					return Strings.CalendarQuarterName( yearQuarter );
				case YearType.FiscalYear:
					return Strings.FiscalQuarterName( yearQuarter );
				case YearType.SchoolYear:
					return Strings.SchoolQuarterName( yearQuarter );
				default:
					return Strings.SystemQuarterName( yearQuarter );
			}
		} // GetQuarterName

		// ----------------------------------------------------------------------
		public virtual string GetQuarterOfYearName( int year, YearQuarter yearQuarter )
		{
			switch ( YearType )
			{
				case YearType.CalendarYear:
					return Strings.CalendarQuarterOfYearName( yearQuarter, year );
				case YearType.FiscalYear:
					return Strings.FiscalQuarterOfYearName( yearQuarter, year );
				case YearType.SchoolYear:
					return Strings.SchoolQuarterOfYearName( yearQuarter, year );
				default:
					return Strings.SystemQuarterOfYearName( yearQuarter, year );
			}
		} // GetQuarterOfYearName

		// ----------------------------------------------------------------------
		public virtual string GetMonthName( int month )
		{
			switch ( monthNameType )
			{
				case CalendarNameType.Abbreviated:
					return culture.DateTimeFormat.GetAbbreviatedMonthName( month );
				default:
					return culture.DateTimeFormat.GetMonthName( month );
			}
		} // GetMonthName

		// ----------------------------------------------------------------------
		public virtual string GetMonthOfYearName( int year, int month )
		{
			return Strings.MonthOfYearName( GetMonthName( month ), GetYearName( year ) );
		} // GetMonthOfYearName

		// ----------------------------------------------------------------------
		public virtual string GetWeekOfYearName( int year, int weekOfYear )
		{
			return Strings.WeekOfYearName( weekOfYear, GetYearName( year ) );
		} // GetWeekOfYearName

		// ----------------------------------------------------------------------
		public virtual string GetDayName( DayOfWeek dayOfWeek )
		{
			switch ( dayNameType )
			{
				case CalendarNameType.Abbreviated:
					return culture.DateTimeFormat.GetAbbreviatedDayName( dayOfWeek );
				default:
					return culture.DateTimeFormat.GetDayName( dayOfWeek );
			}
		} // GetDayName

		// ----------------------------------------------------------------------
		public virtual int GetWeekOfYear( DateTime time )
		{
			int year;
			int weekOfYear;
			TimeTool.GetWeekOfYear( time, culture, yearWeekType, out year, out weekOfYear );
			return weekOfYear;
		} // GetWeekOfYear

		// ----------------------------------------------------------------------
		public virtual DateTime GetStartOfYearWeek( int year, int weekOfYear )
		{
			return TimeTool.GetStartOfYearWeek( year, weekOfYear, culture, yearWeekType );
		} // GetStartOfYearWeek

		// ----------------------------------------------------------------------
		public sealed override bool Equals( object obj )
		{
			if ( obj == this )
			{
				return true;
			}
			if ( obj == null || GetType() != obj.GetType() )
			{
				return false;
			}
			return IsEqual( obj );
		} // Equals

		// ----------------------------------------------------------------------
		protected virtual bool IsEqual( object obj )
		{
			return HasSameData( obj as TimeCalendar );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( TimeCalendar comp )
		{
			return culture.Equals( comp.culture ) &&
				startOffset == comp.startOffset &&
				endOffset == comp.endOffset &&
				yearBaseMonth == comp.yearBaseMonth &&
				fiscalYearBaseMonth == comp.fiscalYearBaseMonth &&
				yearWeekType == comp.yearWeekType &&
				dayNameType == comp.dayNameType &&
				monthNameType == comp.monthNameType;
		} // HasSameData

		// ----------------------------------------------------------------------
		public sealed override int GetHashCode()
		{
			return HashTool.AddHashCode( GetType().GetHashCode(), ComputeHashCode() );
		} // GetHashCode

		// ----------------------------------------------------------------------
		protected virtual int ComputeHashCode()
		{
			return HashTool.ComputeHashCode(
				culture,
				startOffset,
				endOffset,
				yearBaseMonth,
				fiscalYearBaseMonth,
				fiscalFirstDayOfYear,
				fiscalYearAlignment,
				fiscalQuarterGrouping,
				yearWeekType,
				dayNameType,
				monthNameType );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		// members
		private readonly CultureInfo culture;
		private readonly YearType yearType;
		private readonly TimeSpan startOffset;
		private readonly TimeSpan endOffset;
		private readonly YearMonth yearBaseMonth;
		private readonly YearMonth fiscalYearBaseMonth;
		private readonly DayOfWeek fiscalFirstDayOfYear;
		private readonly FiscalYearAlignment fiscalYearAlignment;
		private readonly FiscalQuarterGrouping fiscalQuarterGrouping;
		private readonly YearWeekType yearWeekType;
		private readonly CalendarNameType dayNameType;
		private readonly CalendarNameType monthNameType;

	} // class TimeCalendar

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
