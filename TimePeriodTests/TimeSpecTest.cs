// -- FILE ------------------------------------------------------------------
// name       : TimeSpecTest.cs
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
	
	public sealed class TimeSpecTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "TimeSpec")]
        [Fact]
		public void WeeksPerTimeSpecTest()
		{
			// relations
			Assert.Equal(12, TimeSpec.MonthsPerYear);
			Assert.Equal(2, TimeSpec.HalfYearsPerYear);
			Assert.Equal(4, TimeSpec.QuartersPerYear);
			Assert.Equal( TimeSpec.QuartersPerHalfYear, TimeSpec.QuartersPerYear / TimeSpec.HalfYearsPerYear );
			Assert.Equal(53, TimeSpec.MaxWeeksPerYear);
			Assert.Equal( TimeSpec.MonthsPerHalfYear, TimeSpec.MonthsPerYear / TimeSpec.HalfYearsPerYear );
			Assert.Equal( TimeSpec.MonthsPerQuarter, TimeSpec.MonthsPerYear / TimeSpec.QuartersPerYear );
			Assert.Equal(31, TimeSpec.MaxDaysPerMonth);
			Assert.Equal(7, TimeSpec.DaysPerWeek);
			Assert.Equal(24, TimeSpec.HoursPerDay);
			Assert.Equal(60, TimeSpec.MinutesPerHour);
			Assert.Equal(60, TimeSpec.SecondsPerMinute);
			Assert.Equal(1000, TimeSpec.MillisecondsPerSecond);

			// halfyear
			Assert.Equal( TimeSpec.FirstHalfYearMonths.Length, TimeSpec.MonthsPerHalfYear );
			Assert.Equal(YearMonth.January, TimeSpec.FirstHalfYearMonths[ 0 ]);
			Assert.Equal(YearMonth.February, TimeSpec.FirstHalfYearMonths[ 1 ]);
			Assert.Equal(YearMonth.March, TimeSpec.FirstHalfYearMonths[ 2 ]);
			Assert.Equal(YearMonth.April, TimeSpec.FirstHalfYearMonths[ 3 ]);
			Assert.Equal(YearMonth.May, TimeSpec.FirstHalfYearMonths[ 4 ]);
			Assert.Equal(YearMonth.June, TimeSpec.FirstHalfYearMonths[ 5 ]);

			Assert.Equal( TimeSpec.SecondHalfYearMonths.Length, TimeSpec.MonthsPerHalfYear );
			Assert.Equal(YearMonth.July, TimeSpec.SecondHalfYearMonths[ 0 ]);
			Assert.Equal(YearMonth.August, TimeSpec.SecondHalfYearMonths[ 1 ]);
			Assert.Equal(YearMonth.September, TimeSpec.SecondHalfYearMonths[ 2 ]);
			Assert.Equal(YearMonth.October, TimeSpec.SecondHalfYearMonths[ 3 ]);
			Assert.Equal(YearMonth.November, TimeSpec.SecondHalfYearMonths[ 4 ]);
			Assert.Equal(YearMonth.December, TimeSpec.SecondHalfYearMonths[ 5 ]);

			// quarter
			Assert.Equal(1, TimeSpec.FirstQuarterMonthIndex);
			Assert.Equal( TimeSpec.SecondQuarterMonthIndex,  TimeSpec.FirstQuarterMonthIndex +  TimeSpec.MonthsPerQuarter );
			Assert.Equal( TimeSpec.ThirdQuarterMonthIndex,  TimeSpec.SecondQuarterMonthIndex +  TimeSpec.MonthsPerQuarter );
			Assert.Equal( TimeSpec.FourthQuarterMonthIndex,  TimeSpec.ThirdQuarterMonthIndex +  TimeSpec.MonthsPerQuarter );

			Assert.Equal( TimeSpec.FirstQuarterMonths.Length,  TimeSpec.MonthsPerQuarter );
			Assert.Equal(YearMonth.January, TimeSpec.FirstQuarterMonths[ 0 ]);
			Assert.Equal(YearMonth.February, TimeSpec.FirstQuarterMonths[ 1 ]);
			Assert.Equal(YearMonth.March, TimeSpec.FirstQuarterMonths[ 2 ]);

			Assert.Equal( TimeSpec.SecondQuarterMonths.Length,  TimeSpec.MonthsPerQuarter );
			Assert.Equal(YearMonth.April, TimeSpec.SecondQuarterMonths[ 0 ]);
			Assert.Equal(YearMonth.May, TimeSpec.SecondQuarterMonths[ 1 ]);
			Assert.Equal(YearMonth.June, TimeSpec.SecondQuarterMonths[ 2 ]);

			Assert.Equal( TimeSpec.ThirdQuarterMonths.Length,  TimeSpec.MonthsPerQuarter );
			Assert.Equal(YearMonth.July, TimeSpec.ThirdQuarterMonths[ 0 ]);
			Assert.Equal(YearMonth.August, TimeSpec.ThirdQuarterMonths[ 1 ]);
			Assert.Equal(YearMonth.September, TimeSpec.ThirdQuarterMonths[ 2 ]);

			Assert.Equal( TimeSpec.FourthQuarterMonths.Length,  TimeSpec.MonthsPerQuarter );
			Assert.Equal(YearMonth.October, TimeSpec.FourthQuarterMonths[ 0 ]);
			Assert.Equal(YearMonth.November, TimeSpec.FourthQuarterMonths[ 1 ]);
			Assert.Equal(YearMonth.December, TimeSpec.FourthQuarterMonths[ 2 ]);

			// duration
			Assert.Equal( TimeSpec.NoDuration, TimeSpan.Zero );
			Assert.Equal( TimeSpec.MinPositiveDuration, new TimeSpan( 1 ) );
			Assert.Equal( TimeSpec.MinNegativeDuration, new TimeSpan( -1 ) );

			// period
			Assert.Equal( TimeSpec.MinPeriodDate, DateTime.MinValue );
			Assert.Equal( TimeSpec.MaxPeriodDate, DateTime.MaxValue );
			Assert.Equal( TimeSpec.MinPeriodDuration, TimeSpan.Zero );
			Assert.Equal( TimeSpec.MaxPeriodDuration, TimeSpec.MaxPeriodDate - TimeSpec.MinPeriodDate );

		} // WeeksPerTimeSpecTest

	} // class TimeSpecTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
