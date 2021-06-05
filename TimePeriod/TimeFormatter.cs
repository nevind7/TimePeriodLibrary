// -- FILE ------------------------------------------------------------------
// name       : TimeFormatter.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Text;

namespace TimePeriod
{

	// ------------------------------------------------------------------------
	public class TimeFormatter : ITimeFormatter
	{

		// ----------------------------------------------------------------------
		public TimeFormatter() :
			this( CultureInfo.CurrentCulture )
		{
		} // TimeFormatter

		// ----------------------------------------------------------------------
		public TimeFormatter( CultureInfo culture = null,
			string contextSeparator = "; ", string endSeparator = " - ",
			string durationSeparator = " | ",
			string dateTimeFormat = null,
			string shortDateFormat = null,
			string longTimeFormat = null,
			string shortTimeFormat = null,
			DurationFormatType durationType = DurationFormatType.Compact,
			bool useDurationSeconds = false,
			bool useIsoIntervalNotation = false,
			string durationItemSeparator = " ",
			string durationLastItemSeparator = " ",
			string durationValueSeparator = " ",
			string intervalStartClosed = "[",
			string intervalStartOpen = "(",
			string intervalStartOpenIso = "]",
			string intervalEndClosed = "]",
			string intervalEndOpen = ")",
			string intervalEndOpenIso = "[" )
		{
			if ( culture == null )
			{
				culture = CultureInfo.CurrentCulture;
			}
			this.culture = culture;
			listSeparator = culture.TextInfo.ListSeparator;
			this.contextSeparator = contextSeparator;
			this.endSeparator = endSeparator;
			this.durationSeparator = durationSeparator;
			this.durationItemSeparator = durationItemSeparator;
			this.durationLastItemSeparator = durationLastItemSeparator;
			this.durationValueSeparator = durationValueSeparator;
			this.intervalStartClosed = intervalStartClosed;
			this.intervalStartOpen = intervalStartOpen;
			this.intervalStartOpenIso = intervalStartOpenIso;
			this.intervalEndClosed = intervalEndClosed;
			this.intervalEndOpen = intervalEndOpen;
			this.intervalEndOpenIso = intervalEndOpenIso;
			this.dateTimeFormat = dateTimeFormat;
			this.shortDateFormat = shortDateFormat;
			this.longTimeFormat = longTimeFormat;
			this.shortTimeFormat = shortTimeFormat;
			this.durationType = durationType;
			this.useDurationSeconds = useDurationSeconds;
			this.useIsoIntervalNotation = useIsoIntervalNotation;
		} // TimeFormatter

		// ----------------------------------------------------------------------
		public static TimeFormatter Instance
		{
			get
			{
				if ( _instance == null )
				{
					lock ( Mutex )
					{
						if ( _instance == null )
						{
							_instance = new TimeFormatter();
						}
					}
				}
				return _instance;
			}
			set
			{
				if ( value == null )
				{
					throw new ArgumentNullException( "value" );
				}
				lock ( Mutex )
				{
					_instance = value;
				}
			}
		} // Instance

		// ----------------------------------------------------------------------
		public CultureInfo Culture => culture; // Culture

		// ----------------------------------------------------------------------
		public string ListSeparator => listSeparator; // ListSeparator

		// ----------------------------------------------------------------------
		public string ContextSeparator => contextSeparator; // ContextSeparator

		// ----------------------------------------------------------------------
		public string StartEndSeparator => endSeparator; // StartEndSeparator

		// ----------------------------------------------------------------------
		public string DurationSeparator => durationSeparator; // DurationSeparator

		// ----------------------------------------------------------------------
		public string DurationItemSeparator => durationItemSeparator; // DurationItemSeparator

		// ----------------------------------------------------------------------
		public string DurationLastItemSeparator => durationLastItemSeparator; // DurationLastItemSeparator

		// ----------------------------------------------------------------------
		public string DurationValueSeparator => durationValueSeparator; // DurationValueSeparator

		// ----------------------------------------------------------------------
		public string IntervalStartClosed => intervalStartClosed; // IntervalStartClosed

		// ----------------------------------------------------------------------
		public string IntervalStartOpen => intervalStartOpen; // IntervalStartOpen

		// ----------------------------------------------------------------------
		public string IntervalStartOpenIso => intervalStartOpenIso; // IntervalStartOpenIso

		// ----------------------------------------------------------------------
		public string IntervalEndClosed => intervalEndClosed; // IntervalEndClosed

		// ----------------------------------------------------------------------
		public string IntervalEndOpen => intervalEndOpen; // IntervalEndOpen

		// ----------------------------------------------------------------------
		public string IntervalEndOpenIso => intervalEndOpenIso; // IntervalEndOpenIso

		// ----------------------------------------------------------------------
		public string DateTimeFormat => dateTimeFormat; // DateTimeFormat

		// ----------------------------------------------------------------------
		public string ShortDateFormat => shortDateFormat; // ShortDateFormat

		// ----------------------------------------------------------------------
		public string LongTimeFormat => longTimeFormat; // LongTimeFormat

		// ----------------------------------------------------------------------
		public string ShortTimeFormat => shortTimeFormat; // ShortTimeFormat

		// ----------------------------------------------------------------------
		public DurationFormatType DurationType => durationType; // DurationType

		// ----------------------------------------------------------------------
		public bool UseDurationSeconds => useDurationSeconds; // UseDurationSeconds

		// ----------------------------------------------------------------------
		public bool UseIsoIntervalNotation => useIsoIntervalNotation; // UseIsoIntervalNotation

		#region Collection

		// ----------------------------------------------------------------------
		public virtual string GetCollection( int count )
		{
			return string.Format( "Count = {0}", count );
		} // GetCollection

		// ----------------------------------------------------------------------
		public virtual string GetCollectionPeriod( int count, DateTime start, DateTime end, TimeSpan duration )
		{
			return string.Format( "{0}{1} {2}", GetCollection( count ), ListSeparator, GetPeriod( start, end, duration ) );
		} // GetCollectionPeriod

		#endregion

		#region DateTime

		// ----------------------------------------------------------------------
		public string GetDateTime( DateTime dateTime )
		{
			return !string.IsNullOrEmpty( dateTimeFormat ) ? dateTime.ToString( dateTimeFormat ) : dateTime.ToString( culture );
		} // GetDateTime

		// ----------------------------------------------------------------------
		public string GetShortDate( DateTime dateTime )
		{
			return !string.IsNullOrEmpty( shortDateFormat ) ? dateTime.ToString( shortDateFormat ) : dateTime.ToString( "d" );
		} // GetShortDate

		// ----------------------------------------------------------------------
		public string GetLongTime( DateTime dateTime )
		{
			return !string.IsNullOrEmpty( longTimeFormat ) ? dateTime.ToString( longTimeFormat ) : dateTime.ToString( "T" );
		} // GetLongTime

		// ----------------------------------------------------------------------
		public string GetShortTime( DateTime dateTime )
		{
			return !string.IsNullOrEmpty( shortTimeFormat ) ? dateTime.ToString( shortTimeFormat ) : dateTime.ToString( "t" );
		} // GetShortTime

		#endregion

		#region Duration

		// ----------------------------------------------------------------------
		public string GetPeriod( DateTime start, DateTime end )
		{
			return GetPeriod( start, end, end - start );
		} // GetPeriod

		// ----------------------------------------------------------------------
		public string GetDuration( TimeSpan timeSpan )
		{
			return GetDuration( timeSpan, durationType );
		} // GetDuration

		// ----------------------------------------------------------------------
		public string GetDuration( TimeSpan timeSpan, DurationFormatType durationFormatType )
		{
			switch ( durationFormatType )
			{
				case DurationFormatType.Detailed:
					int days = (int)timeSpan.TotalDays;
					int hours = timeSpan.Hours;
					int minutes = timeSpan.Minutes;
					int seconds = UseDurationSeconds ? timeSpan.Seconds : 0;
					return GetDuration( 0, 0, days, hours, minutes, seconds );
				default:
					return UseDurationSeconds ?
						string.Format( "{0}.{1:00}:{2:00}:{3:00}", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds ) :
						string.Format( "{0}.{1:00}:{2:00}", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes );
			}
		} // GetDuration

		// ----------------------------------------------------------------------
		public virtual string GetDuration( int years, int months, int days, int hours, int minutes, int seconds )
		{
			StringBuilder sb = new StringBuilder();

			// years(s)
			if ( years != 0 )
			{
				sb.Append( years );
				sb.Append( DurationValueSeparator );
				sb.Append( years == 1 ? Strings.TimeSpanYear : Strings.TimeSpanYears );
			}

			// month(s)
			if ( months != 0 )
			{
				if ( sb.Length > 0 )
				{
					sb.Append( days == 0 && hours == 0 && minutes == 0 && seconds == 0 ? DurationLastItemSeparator : DurationItemSeparator );
				}
				sb.Append( months );
				sb.Append( DurationValueSeparator );
				sb.Append( months == 1 ? Strings.TimeSpanMonth : Strings.TimeSpanMonths );
			}

			// day(s)
			if ( days != 0 )
			{
				if ( sb.Length > 0 )
				{
					sb.Append( hours == 0 && minutes == 0 && seconds == 0 ? DurationLastItemSeparator : DurationItemSeparator );
				}
				sb.Append( days );
				sb.Append( DurationValueSeparator );
				sb.Append( days == 1 ? Strings.TimeSpanDay : Strings.TimeSpanDays );
			}

			// hour(s)
			if ( hours != 0 )
			{
				if ( sb.Length > 0 )
				{
					sb.Append( minutes == 0 && seconds == 0 ? DurationLastItemSeparator : DurationItemSeparator );
				}
				sb.Append( hours );
				sb.Append( DurationValueSeparator );
				sb.Append( hours == 1 ? Strings.TimeSpanHour : Strings.TimeSpanHours );
			}

			// minute(s)
			if ( minutes != 0 )
			{
				if ( sb.Length > 0 )
				{
					sb.Append( seconds == 0 ? DurationLastItemSeparator : DurationItemSeparator );
				}
				sb.Append( minutes );
				sb.Append( DurationValueSeparator );
				sb.Append( minutes == 1 ? Strings.TimeSpanMinute : Strings.TimeSpanMinutes );
			}

			// second(s)
			if ( seconds != 0 )
			{
				if ( sb.Length > 0 )
				{
					sb.Append( DurationLastItemSeparator );
				}
				sb.Append( seconds );
				sb.Append( DurationValueSeparator );
				sb.Append( seconds == 1 ? Strings.TimeSpanSecond : Strings.TimeSpanSeconds );
			}

			return sb.ToString();
		} // GetDuration

		#endregion

		#region Period

		// ----------------------------------------------------------------------
		public virtual string GetPeriod( DateTime start, DateTime end, TimeSpan duration )
		{
			if ( end < start )
			{
				throw new ArgumentOutOfRangeException( "end" );
			}

			bool hasTimeOfDay = TimeTool.HasTimeOfDay( start );

			// no duration - schow start date (optionally with the time part)
			if ( duration == TimeSpec.MinPeriodDuration )
			{
				return hasTimeOfDay ? GetDateTime( start ) : GetShortDate( start );
			}

			// within one day: show full start, end time and suration
			if ( TimeCompare.IsSameDay( start, end ) )
			{
				return GetDateTime( start ) + endSeparator + GetLongTime( end ) + durationSeparator + GetDuration( duration );
			}

			// show start date, end date and duration (optionally with the time part)
			bool endHasTimeOfDay = TimeTool.HasTimeOfDay( end );
			bool hasTimeOfDays = hasTimeOfDay || endHasTimeOfDay;
			string part = hasTimeOfDays ? GetDateTime( start ) : GetShortDate( start );
			string endPart = hasTimeOfDays ? GetDateTime( end ) : GetShortDate( end );
			return part + endSeparator + endPart + durationSeparator + GetDuration( duration );
		} // GetPeriod

		// ----------------------------------------------------------------------
		public string GetCalendarPeriod( string start, string end, TimeSpan duration )
		{
			string timePeriod = start.Equals( end ) ? start : start + endSeparator + end;
			return timePeriod + durationSeparator + GetDuration( duration );
		} // GetCalendarPeriod

		// ----------------------------------------------------------------------
		public string GetCalendarPeriod( string context, string start, string end, TimeSpan duration )
		{
			string timePeriod = start.Equals( end ) ? start : start + endSeparator + end;
			return context + contextSeparator + timePeriod + durationSeparator + GetDuration( duration );
		} // GetCalendarPeriod

		// ----------------------------------------------------------------------
		public string GetCalendarPeriod( string context, string endContext, string start, string end, TimeSpan duration )
		{
			string contextPeriod = context.Equals( endContext ) ? context : context + endSeparator + endContext;
			string timePeriod = start.Equals( end ) ? start : start + endSeparator + end;
			return contextPeriod + contextSeparator + timePeriod + durationSeparator + GetDuration( duration );
		} // GetCalendarPeriod

		#endregion

		#region Interval

		// ----------------------------------------------------------------------
		public string GetInterval( DateTime start, DateTime end,
			IntervalEdge edge, IntervalEdge endEdge, TimeSpan duration )
		{
			if ( end < start )
			{
				throw new ArgumentOutOfRangeException( "end" );
			}

			StringBuilder sb = new StringBuilder();

			// interval start
			switch ( edge )
			{
				case IntervalEdge.Closed:
					sb.Append( IntervalStartClosed );
					break;
				case IntervalEdge.Open:
					sb.Append( UseIsoIntervalNotation ? intervalStartOpenIso : intervalStartOpen );
					break;
			}

			bool addDuration = true;
			bool hasTimeOfDay = TimeTool.HasTimeOfDay( start );

			// no duration - schow start date (optionally with the time part)
			if ( duration == TimeSpec.MinPeriodDuration )
			{
				sb.Append( hasTimeOfDay ? GetDateTime( start ) : GetShortDate( start ) );
				addDuration = false;
			}
			// within one day: show full start, end time and suration
			else if ( TimeCompare.IsSameDay( start, end ) )
			{
				sb.Append( GetDateTime( start ) );
				sb.Append( endSeparator );
				sb.Append( GetLongTime( end ) );
			}
			else
			{
				bool endHasTimeOfDay = TimeTool.HasTimeOfDay( start );
				bool hasTimeOfDays = hasTimeOfDay || endHasTimeOfDay;
				if ( hasTimeOfDays )
				{
					sb.Append( GetDateTime( start ) );
					sb.Append( endSeparator );
					sb.Append( GetDateTime( end ) );
				}
				else
				{
					sb.Append( GetShortDate( start ) );
					sb.Append( endSeparator );
					sb.Append( GetShortDate( end ) );
				}
			}

			// interval end
			switch ( endEdge )
			{
				case IntervalEdge.Closed:
					sb.Append( IntervalEndClosed );
					break;
				case IntervalEdge.Open:
					sb.Append( UseIsoIntervalNotation ? IntervalEndOpenIso : IntervalEndOpen );
					break;
			}

			// duration
			if ( addDuration )
			{
				sb.Append( durationSeparator );
				sb.Append( GetDuration( duration ) );
			}

			return sb.ToString();
		} // GetInterval

		#endregion

		// ----------------------------------------------------------------------
		// members
		private readonly CultureInfo culture;
		private readonly string listSeparator;
		private readonly string contextSeparator;
		private readonly string endSeparator;
		private readonly string durationSeparator;
		private readonly string durationItemSeparator;
		private readonly string durationLastItemSeparator;
		private readonly string durationValueSeparator;
		private readonly string intervalStartClosed;
		private readonly string intervalStartOpen;
		private readonly string intervalStartOpenIso;
		private readonly string intervalEndClosed;
		private readonly string intervalEndOpen;
		private readonly string intervalEndOpenIso;
		private readonly string dateTimeFormat;
		private readonly string shortDateFormat;
		private readonly string longTimeFormat;
		private readonly string shortTimeFormat;
		private readonly DurationFormatType durationType;
		private readonly bool useDurationSeconds;
		private readonly bool useIsoIntervalNotation;

		private static readonly object Mutex = new object();
		private static volatile TimeFormatter _instance;

	} // class TimeFormatter

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
