// -- FILE ------------------------------------------------------------------
// name       : HalfYearsTest.cs
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

    public sealed class HalfYearsTest : TestUnitBase
    {
        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYears")]
        [Fact]
        public void YearBaseMonthTest()
        {
            DateTime moment = new DateTime(2009, 2, 15);
            int year = TimeTool.GetYearOf(YearMonth.April, moment.Year, moment.Month);
            HalfYears halfyears = new HalfYears(moment, YearHalfYear.First, 3, TimeCalendar.New(YearMonth.April));
            Assert.Equal(YearMonth.April, halfyears.YearBaseMonth);
            Assert.Equal(halfyears.Start, new DateTime(year, (int)YearMonth.April, 1));
        } // YearBaseMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYears")]
        [Fact]
        public void SingleHalfYearsTest()
        {
            const int startYear = 2004;
            const YearHalfYear startHalfYear = YearHalfYear.Second;
            HalfYears halfyears = new HalfYears(startYear, startHalfYear, 1);

            Assert.Equal(YearMonth.January, halfyears.YearBaseMonth);
            Assert.Equal(1, halfyears.HalfYearCount);
            Assert.Equal(halfyears.StartHalfYear, startHalfYear);
            Assert.Equal(halfyears.StartYear, startYear);
            Assert.Equal(halfyears.EndYear, startYear);
            Assert.Equal(YearHalfYear.Second, halfyears.EndHalfYear);
            Assert.Equal(1, halfyears.GetHalfYears().Count);
            Assert.True(halfyears.GetHalfYears()[0].IsSamePeriod(new HalfYear(2004, YearHalfYear.Second)));
        } // SingleHalfYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYears")]
        [Fact]
        public void FirstCalendarHalfYearsTest()
        {
            const int startYear = 2004;
            const YearHalfYear startHalfYear = YearHalfYear.First;
            const int halfyearCount = 3;
            HalfYears halfyears = new HalfYears(startYear, startHalfYear, halfyearCount);

            Assert.Equal(YearMonth.January, halfyears.YearBaseMonth);
            Assert.Equal(halfyears.HalfYearCount, halfyearCount);
            Assert.Equal(halfyears.StartHalfYear, startHalfYear);
            Assert.Equal(halfyears.StartYear, startYear);
            Assert.Equal(2005, halfyears.EndYear);
            Assert.Equal(YearHalfYear.First, halfyears.EndHalfYear);
            Assert.Equal(halfyears.GetHalfYears().Count, halfyearCount);
            Assert.True(halfyears.GetHalfYears()[0].IsSamePeriod(new HalfYear(2004, YearHalfYear.First)));
            Assert.True(halfyears.GetHalfYears()[1].IsSamePeriod(new HalfYear(2004, YearHalfYear.Second)));
            Assert.True(halfyears.GetHalfYears()[2].IsSamePeriod(new HalfYear(2005, YearHalfYear.First)));
        } // FirstCalendarHalfYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYears")]
        [Fact]
        public void SecondCalendarHalfYearsTest()
        {
            const int startYear = 2004;
            const YearHalfYear startHalfYear = YearHalfYear.Second;
            const int halfyearCount = 3;
            HalfYears halfyears = new HalfYears(startYear, startHalfYear, halfyearCount);

            Assert.Equal(YearMonth.January, halfyears.YearBaseMonth);
            Assert.Equal(halfyears.HalfYearCount, halfyearCount);
            Assert.Equal(halfyears.StartHalfYear, startHalfYear);
            Assert.Equal(halfyears.StartYear, startYear);
            Assert.Equal(2005, halfyears.EndYear);
            Assert.Equal(YearHalfYear.Second, halfyears.EndHalfYear);
            Assert.Equal(halfyears.GetHalfYears().Count, halfyearCount);
            Assert.True(halfyears.GetHalfYears()[0].IsSamePeriod(new HalfYear(2004, YearHalfYear.Second)));
            Assert.True(halfyears.GetHalfYears()[1].IsSamePeriod(new HalfYear(2005, YearHalfYear.First)));
            Assert.True(halfyears.GetHalfYears()[2].IsSamePeriod(new HalfYear(2005, YearHalfYear.Second)));
        } // SecondCalendarHalfYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYears")]
        [Fact]
        public void FirstCustomCalendarHalfYearsTest()
        {
            TimeCalendar calendar = TimeCalendar.New(YearMonth.October);
            const int startYear = 2004;
            const YearHalfYear startHalfYear = YearHalfYear.First;
            const int halfyearCount = 3;
            HalfYears halfyears = new HalfYears(startYear, startHalfYear, halfyearCount, calendar);

            Assert.Equal(YearMonth.October, halfyears.YearBaseMonth);
            Assert.Equal(halfyears.HalfYearCount, halfyearCount);
            Assert.Equal(halfyears.StartHalfYear, startHalfYear);
            Assert.Equal(halfyears.StartYear, startYear);
            Assert.Equal(2005, halfyears.EndYear);
            Assert.Equal(YearHalfYear.First, halfyears.EndHalfYear);
            Assert.Equal(halfyears.GetHalfYears().Count, halfyearCount);
            Assert.True(halfyears.GetHalfYears()[0].IsSamePeriod(new HalfYear(2004, YearHalfYear.First, calendar)));
            Assert.True(halfyears.GetHalfYears()[1].IsSamePeriod(new HalfYear(2004, YearHalfYear.Second, calendar)));
            Assert.True(halfyears.GetHalfYears()[2].IsSamePeriod(new HalfYear(2005, YearHalfYear.First, calendar)));
        } // FirstCustomCalendarHalfYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYears")]
        [Fact]
        public void SecondCustomCalendarHalfYearsTest()
        {
            TimeCalendar calendar = TimeCalendar.New(YearMonth.October);
            const int startYear = 2004;
            const YearHalfYear startHalfYear = YearHalfYear.Second;
            const int halfyearCount = 3;
            HalfYears halfyears = new HalfYears(startYear, startHalfYear, halfyearCount, calendar);

            Assert.Equal(YearMonth.October, halfyears.YearBaseMonth);
            Assert.Equal(halfyears.HalfYearCount, halfyearCount);
            Assert.Equal(halfyears.StartHalfYear, startHalfYear);
            Assert.Equal(halfyears.StartYear, startYear);
            Assert.Equal(2005, halfyears.EndYear);
            Assert.Equal(YearHalfYear.Second, halfyears.EndHalfYear);
            Assert.Equal(halfyears.GetHalfYears().Count, halfyearCount);
            Assert.True(halfyears.GetHalfYears()[0].IsSamePeriod(new HalfYear(2004, YearHalfYear.Second, calendar)));
            Assert.True(halfyears.GetHalfYears()[1].IsSamePeriod(new HalfYear(2005, YearHalfYear.First, calendar)));
            Assert.True(halfyears.GetHalfYears()[2].IsSamePeriod(new HalfYear(2005, YearHalfYear.Second, calendar)));
        } // SecondCustomCalendarHalfYearsTest

        // ----------------------------------------------------------------------
        private ITimeCalendar GetFiscalYearCalendar(FiscalYearAlignment yearAlignment)
        {
            return new TimeCalendar(
                new TimeCalendarConfig
                {
                    YearType = YearType.FiscalYear,
                    YearBaseMonth = YearMonth.September,
                    FiscalFirstDayOfYear = DayOfWeek.Sunday,
                    FiscalYearAlignment = yearAlignment,
                    FiscalQuarterGrouping = FiscalQuarterGrouping.FourFourFiveWeeks
                });
        } // GetFiscalYearCalendar

        // ----------------------------------------------------------------------
        // http://en.wikipedia.org/wiki/4-4-5_Calendar
        [Trait("Category", "HalfYears")]
        [Fact]
        public void GetFiscalQuartersTest()
        {
            const int halfyearCount = 4;
            HalfYears halfyears = new HalfYears(2006, YearHalfYear.First, halfyearCount, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            ITimePeriodCollection quarters = halfyears.GetQuarters();

            Assert.NotNull(quarters);
            Assert.Equal(quarters.Count, TimeSpec.QuartersPerHalfYear * halfyearCount);

            Assert.Equal(quarters[0].Start.Date, halfyears.Start);
            Assert.Equal(quarters[(TimeSpec.QuartersPerHalfYear * halfyearCount) - 1].End, halfyears.End);
        } // GetFiscalQuartersTest

        // ----------------------------------------------------------------------
        // http://en.wikipedia.org/wiki/4-4-5_Calendar
        [Trait("Category", "HalfYears")]
        [Fact]
        public void YearGetMonthsTest()
        {
            const int halfyearCount = 4;
            HalfYears halfyears = new HalfYears(2006, YearHalfYear.First, halfyearCount, GetFiscalYearCalendar(FiscalYearAlignment.LastDay));
            ITimePeriodCollection months = halfyears.GetMonths();
            Assert.NotNull(months);
            Assert.Equal(months.Count, TimeSpec.MonthsPerHalfYear * halfyearCount);

            Assert.Equal(months[0].Start, new DateTime(2006, 8, 27));
            for (int i = 0; i < months.Count; i++)
            {
                Month month = (Month)months[i];

                // last month of a leap year (6 weeks)
                // http://en.wikipedia.org/wiki/4-4-5_Calendar
                if ((month.YearMonth == YearMonth.August) && (month.Year == 2008 || month.Year == 2013 || month.Year == 2019))
                {
                    Assert.Equal(month.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days, TimeSpec.FiscalDaysPerLeapMonth);
                }
                else if ((i + 1) % 3 == 0) // first and second month of quarter (4 weeks)
                {
                    Assert.Equal(month.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days, TimeSpec.FiscalDaysPerLongMonth);
                }
                else // third month of quarter (5 weeks)
                {
                    Assert.Equal(month.Duration.Subtract(TimeCalendar.DefaultEndOffset).Days, TimeSpec.FiscalDaysPerShortMonth);
                }
            }
            Assert.Equal(months[(TimeSpec.MonthsPerHalfYear * halfyearCount) - 1].End, halfyears.End);
        } // FiscalYearGetMonthsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYears")]
        [Fact]
        public void FirstHalfYearMonthTest()
        {
            HalfYears halfyears = new HalfYears(2016, YearHalfYear.First, 1);
            Assert.Equal(6, halfyears.GetMonths().Count);
            Assert.Equal(1, halfyears.GetMonths()[0].Start.Month);
            Assert.Equal(6, halfyears.GetMonths()[5].Start.Month);
        } // FirstHalfYearMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYears")]
        [Fact]
        public void SecondHalfYearMonthTest()
        {
            HalfYears halfyears = new HalfYears(2016, YearHalfYear.Second, 1);
            Assert.Equal(6, halfyears.GetMonths().Count);
            Assert.Equal(7, halfyears.GetMonths()[0].Start.Month);
            Assert.Equal(12, halfyears.GetMonths()[5].Start.Month);
        } // SecondHalfYearMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYears")]
        [Fact]
        public void FirstHalfYearQuarterTest()
        {
            HalfYears halfyears = new HalfYears(2016, YearHalfYear.First, 1);
            Assert.Equal(2, halfyears.GetQuarters().Count);
            Assert.Equal(1, halfyears.GetQuarters()[0].Start.Month);
            Assert.Equal(4, halfyears.GetQuarters()[1].Start.Month);
        } // FirstHalfYearQuarterTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYears")]
        [Fact]
        public void SecondHalfYearQuarterTest()
        {
            HalfYears halfyears = new HalfYears(2016, YearHalfYear.Second, 1);
            Assert.Equal(2, halfyears.GetQuarters().Count);
            Assert.Equal(7, halfyears.GetQuarters()[0].Start.Month);
            Assert.Equal(10, halfyears.GetQuarters()[1].Start.Month);
        } // SecondHalfYearQuarterTest
    } // class HalfYearsTest
} // namespace Itenso.TimePeriodTests

// -- EOF -------------------------------------------------------------------