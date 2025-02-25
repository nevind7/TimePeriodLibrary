// -- FILE ------------------------------------------------------------------
// name       : CalendarTimeRangeTest.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System;
using TimePeriod;
using Xunit;

namespace TimePeriodTests.Core
{

	// ------------------------------------------------------------------------
	
	public sealed class CalendarTimeRangeTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarTimeRange")]
        [Fact]
		public void CalendarTest()
		{
			TimeCalendar calendar = new TimeCalendar();
			CalendarTimeRange calendarTimeRange = new CalendarTimeRange( TimeRange.Anytime, calendar );
			Assert.Equal( calendarTimeRange.Calendar, calendar );
		} // CalendarTest

        // ----------------------------------------------------------------------
        [Trait("Category", "CalendarTimeRange")]
        [Fact]
		public void MomentTest()
		{
			DateTime testDate = new DateTime( 2000, 10, 1 );
            Assert.NotNull(Assert.Throws<NotSupportedException>( () =>
                new CalendarTimeRange(testDate, testDate)));
        } // MomentTest

    } // class CalendarTimeRangeTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
