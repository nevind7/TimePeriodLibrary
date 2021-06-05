// -- FILE ------------------------------------------------------------------
// name       : ClockProxy.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

namespace TimePeriod
{

	// ------------------------------------------------------------------------
	public static class ClockProxy
	{

		// ----------------------------------------------------------------------
		public static IClock Clock
		{
			get
			{
                if (_clock != null) return _clock;

                lock ( Mutex )
                {
                    _clock ??= new SystemClock();
                }
                return _clock;
			}
			set
			{
				lock ( Mutex )
				{
					_clock = value;
				}
			}
		} // Clock

		// ----------------------------------------------------------------------
		// members
		private static readonly object Mutex = new object();
		private static volatile IClock _clock;

	} // class ClockProxy

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
