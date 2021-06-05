// -- FILE ------------------------------------------------------------------
// name       : HourRange.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

namespace TimePeriod
{

	// ------------------------------------------------------------------------
	public class HourRange
	{

		// ----------------------------------------------------------------------
		public HourRange( int hour ) :
			this( hour, hour )
		{
		} // HourRange

		// ----------------------------------------------------------------------
		public HourRange( int hour, int endHour ) :
			this( new Time( hour ), new Time( endHour ) )
		{
		} // HourRange

		// ----------------------------------------------------------------------
		public HourRange( Time start, Time end )
		{
			if ( start.Ticks <= end.Ticks )
			{
				this.start = start;
				this.end = end;
			}
			else
			{
				this.end = start;
				this.start = end;
			}
		} // HourRange

		// ----------------------------------------------------------------------
		public Time Start => start; // Start

		// ----------------------------------------------------------------------
		public Time End => end; // End

		// ----------------------------------------------------------------------
		public bool IsMoment => start.Equals( end ); // IsMoment

		// ----------------------------------------------------------------------
		public override string ToString()
		{
			return start + " - " + end;
		} // ToString

		// ----------------------------------------------------------------------
		// members
		private readonly Time start;
		private readonly Time end;

	} // class HourRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
