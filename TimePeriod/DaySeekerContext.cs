// -- FILE ------------------------------------------------------------------
// name       : DaySeekerContext.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.03.21
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System;

namespace TimePeriod
{

	// ------------------------------------------------------------------------
	public class DaySeekerContext : ICalendarVisitorContext
	{

		// ----------------------------------------------------------------------
		public DaySeekerContext( Day day, int dayCount )
		{
			if ( day == null )
			{
				throw new ArgumentNullException( "day" );
			}
			StartDay = day;
			DayCount = Math.Abs( dayCount );
			RemaingDays = DayCount;
		} // DaySeekerContext

		// ----------------------------------------------------------------------
		public int DayCount { get; private set; }

		// ----------------------------------------------------------------------
		public int RemaingDays { get; private set; }

		// ----------------------------------------------------------------------
		public Day StartDay { get; private set; }

		// ----------------------------------------------------------------------
		public Day FoundDay { get; private set; }

		// ----------------------------------------------------------------------
		public bool IsFinished => RemaingDays == 0; // IsFinished

		// ----------------------------------------------------------------------
		public void ProcessDay( Day day )
		{
			if ( IsFinished )
			{
				return;
			}

			RemaingDays -= 1;
			if ( IsFinished )
			{
				FoundDay = day;
			}
		} // ProcessDay

	} // class DaySeekerContext

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
