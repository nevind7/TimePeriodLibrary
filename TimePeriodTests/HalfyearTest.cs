// -- FILE ------------------------------------------------------------------
// name       : HalfYearTest.cs
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
	
	public sealed class HalfYearTest : TestUnitBase
	{

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void InitValuesTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			DateTime firstHalfYear = new DateTime( now.Year, 1, 1 );
			DateTime secondHalfYear = new DateTime( now.Year, 7, 1 );
			HalfYear halfyear = new HalfYear( now.Year, YearHalfYear.First, TimeCalendar.NewEmptyOffset() );

			Assert.Equal( halfyear.Start.Year, firstHalfYear.Year );
			Assert.Equal( halfyear.Start.Month, firstHalfYear.Month );
			Assert.Equal( halfyear.Start.Day, firstHalfYear.Day );
			Assert.Equal(0, halfyear.Start.Hour);
			Assert.Equal(0, halfyear.Start.Minute);
			Assert.Equal(0, halfyear.Start.Second);
			Assert.Equal(0, halfyear.Start.Millisecond);

			Assert.Equal( halfyear.End.Year, secondHalfYear.Year );
			Assert.Equal( halfyear.End.Month, secondHalfYear.Month );
			Assert.Equal( halfyear.End.Day, secondHalfYear.Day );
			Assert.Equal(0, halfyear.End.Hour);
			Assert.Equal(0, halfyear.End.Minute);
			Assert.Equal(0, halfyear.End.Second);
			Assert.Equal(0, halfyear.End.Millisecond);
		} // InitValuesTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void DefaultCalendarTest()
		{
			DateTime yearStart = new DateTime( ClockProxy.Clock.Now.Year, 1, 1 );
			foreach ( YearHalfYear yearHalfYear in Enum.GetValues( typeof( YearHalfYear ) ) )
			{
				int offset = (int)yearHalfYear - 1;
				HalfYear halfyear = new HalfYear( yearStart.AddMonths( TimeSpec.MonthsPerHalfYear * offset ) );
				Assert.Equal(YearMonth.January, halfyear.YearBaseMonth);
				Assert.Equal( halfyear.BaseYear, yearStart.Year );
				Assert.Equal( halfyear.Start, yearStart.AddMonths( TimeSpec.MonthsPerHalfYear * offset ).Add( halfyear.Calendar.StartOffset ) );
				Assert.Equal( halfyear.End, yearStart.AddMonths( TimeSpec.MonthsPerHalfYear * ( offset + 1 ) ).Add( halfyear.Calendar.EndOffset ) );
			}
		} // DefaultCalendarTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void MomentTest()
		{
			DateTime now = ClockProxy.Clock.Now;
			TimeCalendar timeCalendar = TimeCalendar.New( YearMonth.April );

			Assert.Equal( new HalfYear().YearHalfYear, now.Month <= 6 ? YearHalfYear.First : YearHalfYear.Second );

			Assert.Equal(YearHalfYear.First, new HalfYear( new DateTime( now.Year, 1, 1 ) ).YearHalfYear);
			Assert.Equal(YearHalfYear.First, new HalfYear( new DateTime( now.Year, 6, 30 ) ).YearHalfYear);
			Assert.Equal(YearHalfYear.Second, new HalfYear( new DateTime( now.Year, 7, 1 ) ).YearHalfYear);
			Assert.Equal(YearHalfYear.Second, new HalfYear( new DateTime( now.Year, 12, 31 ) ).YearHalfYear);

			Assert.Equal(YearHalfYear.First, new HalfYear( new DateTime( now.Year, 4, 1 ), timeCalendar ).YearHalfYear);
			Assert.Equal(YearHalfYear.First, new HalfYear( new DateTime( now.Year, 9, 30 ), timeCalendar ).YearHalfYear);
			Assert.Equal(YearHalfYear.Second, new HalfYear( new DateTime( now.Year, 10, 1 ), timeCalendar ).YearHalfYear);
			Assert.Equal(YearHalfYear.Second, new HalfYear( new DateTime( now.Year, 3, 31 ), timeCalendar ).YearHalfYear);
		} // MomentTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void YearBaseMonthTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			HalfYear halfyear = new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.April ) );
			Assert.Equal(YearMonth.April, halfyear.YearBaseMonth);
			Assert.Equal(YearMonth.January, new HalfYear( currentYear, YearHalfYear.Second ).YearBaseMonth);
		} // YearBaseMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void YearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Assert.Equal( new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.April ) ).BaseYear, currentYear );
			Assert.Equal(2006, new HalfYear( 2006, YearHalfYear.First ).BaseYear);
		} // YearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void YearHalfYearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			Assert.Equal(YearHalfYear.First, new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.April ) ).YearHalfYear);
			Assert.Equal(YearHalfYear.Second, new HalfYear( currentYear, YearHalfYear.Second, TimeCalendar.New( YearMonth.April ) ).YearHalfYear);
		} // YearHalfYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void StartMonthTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;

			Assert.Equal(YearMonth.January, new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.January ) ).StartMonth);
			Assert.Equal(YearMonth.February, new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.February ) ).StartMonth);
			Assert.Equal(YearMonth.March, new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.March ) ).StartMonth);
			Assert.Equal(YearMonth.April, new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.April ) ).StartMonth);
			Assert.Equal(YearMonth.May, new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.May ) ).StartMonth);
			Assert.Equal(YearMonth.June, new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.June ) ).StartMonth);
			Assert.Equal(YearMonth.July, new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.July ) ).StartMonth);
			Assert.Equal(YearMonth.August, new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.August ) ).StartMonth);
			Assert.Equal(YearMonth.September, new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.September ) ).StartMonth);
			Assert.Equal(YearMonth.October, new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.October ) ).StartMonth);
			Assert.Equal(YearMonth.November, new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.November ) ).StartMonth);
			Assert.Equal(YearMonth.December, new HalfYear( currentYear, YearHalfYear.First, TimeCalendar.New( YearMonth.December ) ).StartMonth);

			Assert.Equal(YearMonth.July, new HalfYear( currentYear, YearHalfYear.Second, TimeCalendar.New( YearMonth.January ) ).StartMonth);
			Assert.Equal(YearMonth.August, new HalfYear( currentYear, YearHalfYear.Second, TimeCalendar.New( YearMonth.February ) ).StartMonth);
			Assert.Equal(YearMonth.September, new HalfYear( currentYear, YearHalfYear.Second, TimeCalendar.New( YearMonth.March ) ).StartMonth);
			Assert.Equal(YearMonth.October, new HalfYear( currentYear, YearHalfYear.Second, TimeCalendar.New( YearMonth.April ) ).StartMonth);
			Assert.Equal(YearMonth.November, new HalfYear( currentYear, YearHalfYear.Second, TimeCalendar.New( YearMonth.May ) ).StartMonth);
			Assert.Equal(YearMonth.December, new HalfYear( currentYear, YearHalfYear.Second, TimeCalendar.New( YearMonth.June ) ).StartMonth);
			Assert.Equal(YearMonth.January, new HalfYear( currentYear, YearHalfYear.Second, TimeCalendar.New( YearMonth.July ) ).StartMonth);
			Assert.Equal(YearMonth.February, new HalfYear( currentYear, YearHalfYear.Second, TimeCalendar.New( YearMonth.August ) ).StartMonth);
			Assert.Equal(YearMonth.March, new HalfYear( currentYear, YearHalfYear.Second, TimeCalendar.New( YearMonth.September ) ).StartMonth);
			Assert.Equal(YearMonth.April, new HalfYear( currentYear, YearHalfYear.Second, TimeCalendar.New( YearMonth.October ) ).StartMonth);
			Assert.Equal(YearMonth.May, new HalfYear( currentYear, YearHalfYear.Second, TimeCalendar.New( YearMonth.November ) ).StartMonth);
			Assert.Equal(YearMonth.June, new HalfYear( currentYear, YearHalfYear.Second, TimeCalendar.New( YearMonth.December ) ).StartMonth);
		} // StartMonthTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void IsCalendarHalfYearTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			foreach ( YearHalfYear yearHalfYear in Enum.GetValues( typeof( YearHalfYear ) ) )
			{
				Assert.True( new HalfYear( now.Year, yearHalfYear, TimeCalendar.New( YearMonth.January ) ).IsCalendarHalfYear );
				Assert.False( new HalfYear( now.Year, yearHalfYear, TimeCalendar.New( YearMonth.February ) ).IsCalendarHalfYear );
				Assert.False( new HalfYear( now.Year, yearHalfYear, TimeCalendar.New( YearMonth.March ) ).IsCalendarHalfYear );
				Assert.False( new HalfYear( now.Year, yearHalfYear, TimeCalendar.New( YearMonth.April ) ).IsCalendarHalfYear );
				Assert.False( new HalfYear( now.Year, yearHalfYear, TimeCalendar.New( YearMonth.May ) ).IsCalendarHalfYear );
				Assert.False( new HalfYear( now.Year, yearHalfYear, TimeCalendar.New( YearMonth.June ) ).IsCalendarHalfYear );
				Assert.True( new HalfYear( now.Year, yearHalfYear, TimeCalendar.New( YearMonth.July ) ).IsCalendarHalfYear );
				Assert.False( new HalfYear( now.Year, yearHalfYear, TimeCalendar.New( YearMonth.August ) ).IsCalendarHalfYear );
				Assert.False( new HalfYear( now.Year, yearHalfYear, TimeCalendar.New( YearMonth.September ) ).IsCalendarHalfYear );
				Assert.False( new HalfYear( now.Year, yearHalfYear, TimeCalendar.New( YearMonth.October ) ).IsCalendarHalfYear );
				Assert.False( new HalfYear( now.Year, yearHalfYear, TimeCalendar.New( YearMonth.November ) ).IsCalendarHalfYear );
				Assert.False( new HalfYear( now.Year, yearHalfYear, TimeCalendar.New( YearMonth.December ) ).IsCalendarHalfYear );
			}
		} // IsCalendarHalfYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void MultipleCalendarYearsTest()
		{
			DateTime now = ClockProxy.Clock.Now;

			Assert.False( new HalfYear( now.Year, YearHalfYear.First, TimeCalendar.New( YearMonth.January ) ).MultipleCalendarYears );
			Assert.False( new HalfYear( now.Year, YearHalfYear.First, TimeCalendar.New( YearMonth.February ) ).MultipleCalendarYears );
			Assert.False( new HalfYear( now.Year, YearHalfYear.First, TimeCalendar.New( YearMonth.March ) ).MultipleCalendarYears );
			Assert.False( new HalfYear( now.Year, YearHalfYear.First, TimeCalendar.New( YearMonth.April ) ).MultipleCalendarYears );
			Assert.False( new HalfYear( now.Year, YearHalfYear.First, TimeCalendar.New( YearMonth.May ) ).MultipleCalendarYears );
			Assert.False( new HalfYear( now.Year, YearHalfYear.First, TimeCalendar.New( YearMonth.June ) ).MultipleCalendarYears );
			Assert.False( new HalfYear( now.Year, YearHalfYear.First, TimeCalendar.New( YearMonth.July ) ).MultipleCalendarYears );
			Assert.True( new HalfYear( now.Year, YearHalfYear.First, TimeCalendar.New( YearMonth.August ) ).MultipleCalendarYears );
			Assert.True( new HalfYear( now.Year, YearHalfYear.First, TimeCalendar.New( YearMonth.September ) ).MultipleCalendarYears );
			Assert.True( new HalfYear( now.Year, YearHalfYear.First, TimeCalendar.New( YearMonth.October ) ).MultipleCalendarYears );
			Assert.True( new HalfYear( now.Year, YearHalfYear.First, TimeCalendar.New( YearMonth.November ) ).MultipleCalendarYears );
			Assert.True( new HalfYear( now.Year, YearHalfYear.First, TimeCalendar.New( YearMonth.December ) ).MultipleCalendarYears );

			Assert.False( new HalfYear( now.Year, YearHalfYear.Second, TimeCalendar.New( YearMonth.January ) ).MultipleCalendarYears );
			Assert.True( new HalfYear( now.Year, YearHalfYear.Second, TimeCalendar.New( YearMonth.February ) ).MultipleCalendarYears );
			Assert.True( new HalfYear( now.Year, YearHalfYear.Second, TimeCalendar.New( YearMonth.March ) ).MultipleCalendarYears );
			Assert.True( new HalfYear( now.Year, YearHalfYear.Second, TimeCalendar.New( YearMonth.April ) ).MultipleCalendarYears );
			Assert.True( new HalfYear( now.Year, YearHalfYear.Second, TimeCalendar.New( YearMonth.May ) ).MultipleCalendarYears );
			Assert.True( new HalfYear( now.Year, YearHalfYear.Second, TimeCalendar.New( YearMonth.June ) ).MultipleCalendarYears );
			Assert.False( new HalfYear( now.Year, YearHalfYear.Second, TimeCalendar.New( YearMonth.July ) ).MultipleCalendarYears );
			Assert.False( new HalfYear( now.Year, YearHalfYear.Second, TimeCalendar.New( YearMonth.August ) ).MultipleCalendarYears );
			Assert.False( new HalfYear( now.Year, YearHalfYear.Second, TimeCalendar.New( YearMonth.September ) ).MultipleCalendarYears );
			Assert.False( new HalfYear( now.Year, YearHalfYear.Second, TimeCalendar.New( YearMonth.October ) ).MultipleCalendarYears );
			Assert.False( new HalfYear( now.Year, YearHalfYear.Second, TimeCalendar.New( YearMonth.November ) ).MultipleCalendarYears );
			Assert.False( new HalfYear( now.Year, YearHalfYear.Second, TimeCalendar.New( YearMonth.December ) ).MultipleCalendarYears );
		} // MultipleCalendarYearsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void CalendarHalfYearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			TimeCalendar calendar = TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero );

			HalfYear h1 = new HalfYear( currentYear, YearHalfYear.First, calendar );
			Assert.True( h1.IsReadOnly );
			Assert.True( h1.IsCalendarHalfYear );
			Assert.Equal( h1.YearBaseMonth, TimeSpec.CalendarYearStartMonth );
			Assert.Equal(YearHalfYear.First, h1.YearHalfYear);
			Assert.Equal( h1.BaseYear, currentYear );
			Assert.Equal( h1.Start, new DateTime( currentYear, 1, 1 ) );
			Assert.Equal( h1.End, new DateTime( currentYear, 7, 1 ) );

			HalfYear h2 = new HalfYear( currentYear, YearHalfYear.Second, calendar );
			Assert.True( h2.IsReadOnly );
			Assert.True( h2.IsCalendarHalfYear );
			Assert.Equal( h2.YearBaseMonth, TimeSpec.CalendarYearStartMonth );
			Assert.Equal(YearHalfYear.Second, h2.YearHalfYear);
			Assert.Equal( h2.BaseYear, currentYear );
			Assert.Equal( h2.Start, new DateTime( currentYear, 7, 1 ) );
			Assert.Equal( h2.End, new DateTime( currentYear + 1, 1, 1 ) );
		} // CalendarHalfYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void DefaultHalfYearTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			const YearMonth yearStartMonth = YearMonth.April;
			TimeCalendar calendar = TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero, yearStartMonth );

			HalfYear h1 = new HalfYear( currentYear, YearHalfYear.First, calendar );
			Assert.True( h1.IsReadOnly );
			Assert.False( h1.IsCalendarHalfYear );
			Assert.Equal( h1.YearBaseMonth, yearStartMonth );
			Assert.Equal(YearHalfYear.First, h1.YearHalfYear);
			Assert.Equal( h1.BaseYear, currentYear );
			Assert.Equal( h1.Start, new DateTime( currentYear, 4, 1 ) );
			Assert.Equal( h1.End, new DateTime( currentYear, 10, 1 ) );

			HalfYear h2 = new HalfYear( currentYear, YearHalfYear.Second, calendar );
			Assert.True( h2.IsReadOnly );
			Assert.False( h2.IsCalendarHalfYear );
			Assert.Equal( h2.YearBaseMonth, yearStartMonth );
			Assert.Equal(YearHalfYear.Second, h2.YearHalfYear);
			Assert.Equal( h2.BaseYear, currentYear );
			Assert.Equal( h2.Start, new DateTime( currentYear, 10, 1 ) );
			Assert.Equal( h2.End, new DateTime( currentYear + 1, 4, 1 ) );
		} // DefaultHalfYearTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void GetQuartersTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			TimeCalendar timeCalendar = TimeCalendar.New( YearMonth.October );
			HalfYear h1 = new HalfYear( currentYear, YearHalfYear.First, timeCalendar );

			ITimePeriodCollection h1Quarters = h1.GetQuarters();
			Assert.NotNull(h1Quarters);

			int h1Index = 0;
			foreach ( Quarter h1Quarter in h1Quarters )
			{
				Assert.Equal( h1Quarter.BaseYear, h1.BaseYear );
				Assert.Equal( h1Quarter.YearQuarter, h1Index == 0 ? YearQuarter.First : YearQuarter.Second );
				Assert.Equal( h1Quarter.Start, h1.Start.AddMonths( h1Index * TimeSpec.MonthsPerQuarter ) );
				Assert.Equal( h1Quarter.End, h1Quarter.Calendar.MapEnd( h1Quarter.Start.AddMonths( TimeSpec.MonthsPerQuarter ) ) );
				h1Index++;
			}
			Assert.Equal( h1Index, TimeSpec.QuartersPerHalfYear );

			HalfYear h2 = new HalfYear( currentYear, YearHalfYear.Second, timeCalendar );

			ITimePeriodCollection h2Quarters = h2.GetQuarters();
			Assert.NotNull(h2Quarters);

			int h2Index = 0;
			foreach ( Quarter h2Quarter in h2Quarters )
			{
				Assert.Equal( h2Quarter.BaseYear, h2.BaseYear );
				Assert.Equal( h2Quarter.YearQuarter, h2Index == 0 ? YearQuarter.Third : YearQuarter.Fourth );
				Assert.Equal( h2Quarter.Start, h2.Start.AddMonths( h2Index * TimeSpec.MonthsPerQuarter ) );
				Assert.Equal( h2Quarter.End, h2Quarter.Calendar.MapEnd( h2Quarter.Start.AddMonths( TimeSpec.MonthsPerQuarter ) ) );
				h2Index++;
			}
			Assert.Equal( h2Index, TimeSpec.QuartersPerHalfYear );
		} // GetQuartersTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void GetMonthsTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			TimeCalendar timeCalendar = TimeCalendar.New( YearMonth.October );
			HalfYear halfyear = new HalfYear( currentYear, YearHalfYear.First, timeCalendar );

			ITimePeriodCollection months = halfyear.GetMonths();
			Assert.NotNull(months);

			int index = 0;
			foreach ( Month month in months )
			{
				Assert.Equal( month.Start, halfyear.Start.AddMonths( index ) );
				Assert.Equal( month.End, month.Calendar.MapEnd( month.Start.AddMonths( 1 ) ) );
				index++;
			}
			Assert.Equal( index, TimeSpec.MonthsPerHalfYear );
		} // GetMonthsTest

        // ----------------------------------------------------------------------
        [Trait("Category", "HalfYear")]
        [Fact]
		public void AddHalfYearsTest()
		{
			int currentYear = ClockProxy.Clock.Now.Year;
			const YearMonth yearStartMonth = YearMonth.April;
			TimeCalendar calendar = TimeCalendar.New( TimeSpan.Zero, TimeSpan.Zero, yearStartMonth );

			DateTime calendarStartDate = new DateTime( currentYear, 4, 1 );
			HalfYear calendarHalfYear = new HalfYear( currentYear, YearHalfYear.First, calendar );

			Assert.Equal( calendarHalfYear.AddHalfYears( 0 ), calendarHalfYear );

			HalfYear prevH1 = calendarHalfYear.AddHalfYears( -1 );
			Assert.Equal(YearHalfYear.Second, prevH1.YearHalfYear);
			Assert.Equal( prevH1.BaseYear, currentYear - 1 );
			Assert.Equal( prevH1.Start, calendarStartDate.AddMonths( -6 ) );
			Assert.Equal( prevH1.End, calendarStartDate );

			HalfYear prevH2 = calendarHalfYear.AddHalfYears( -2 );
			Assert.Equal(YearHalfYear.First, prevH2.YearHalfYear);
			Assert.Equal( prevH2.BaseYear, currentYear - 1 );
			Assert.Equal( prevH2.Start, calendarStartDate.AddMonths( -12 ) );
			Assert.Equal( prevH2.End, calendarStartDate.AddMonths( -6 ) );

			HalfYear prevH3 = calendarHalfYear.AddHalfYears( -3 );
			Assert.Equal(YearHalfYear.Second, prevH3.YearHalfYear);
			Assert.Equal( prevH3.BaseYear, currentYear - 2 );
			Assert.Equal( prevH3.Start, calendarStartDate.AddMonths( -18 ) );
			Assert.Equal( prevH3.End, calendarStartDate.AddMonths( -12 ) );

			HalfYear futureH1 = calendarHalfYear.AddHalfYears( 1 );
			Assert.Equal(YearHalfYear.Second, futureH1.YearHalfYear);
			Assert.Equal( futureH1.BaseYear, currentYear );
			Assert.Equal( futureH1.Start, calendarStartDate.AddMonths( 6 ) );
			Assert.Equal( futureH1.End, calendarStartDate.AddMonths( 12 ) );

			HalfYear futureH2 = calendarHalfYear.AddHalfYears( 2 );
			Assert.Equal(YearHalfYear.First, futureH2.YearHalfYear);
			Assert.Equal( futureH2.BaseYear, currentYear + 1 );
			Assert.Equal( futureH2.Start, calendarStartDate.AddMonths( 12 ) );
			Assert.Equal( futureH2.End, calendarStartDate.AddMonths( 18 ) );

			HalfYear futureH3 = calendarHalfYear.AddHalfYears( 3 );
			Assert.Equal(YearHalfYear.Second, futureH3.YearHalfYear);
			Assert.Equal( futureH3.BaseYear, currentYear + 1 );
			Assert.Equal( futureH3.Start, calendarStartDate.AddMonths( 18 ) );
			Assert.Equal( futureH3.End, calendarStartDate.AddMonths( 24 ) );
		} // AddHalfYearsTest

		// ----------------------------------------------------------------------
		private ITimeCalendar GetFiscalYearCalendar( FiscalYearAlignment yearAlignment )
		{
			return new TimeCalendar(
				new TimeCalendarConfig
				{
					YearType = YearType.FiscalYear,
					YearBaseMonth = YearMonth.September,
					FiscalFirstDayOfYear = DayOfWeek.Sunday,
					FiscalYearAlignment = yearAlignment,
					FiscalQuarterGrouping = FiscalQuarterGrouping.FourFourFiveWeeks
				} );
		} // GetFiscalYearCalendar

        // ----------------------------------------------------------------------
        // http://en.wikipedia.org/wiki/4-4-5_Calendar
        [Trait("Category", "HalfYear")]
        [Fact]
		public void GetFiscalQuartersTest()
		{
			HalfYear halfyear = new HalfYear( 2006, YearHalfYear.First, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection quarters = halfyear.GetQuarters();

			Assert.NotNull(quarters);
			Assert.Equal( quarters.Count, TimeSpec.QuartersPerHalfYear );

			Assert.Equal( quarters[ 0 ].Start.Date, halfyear.Start );
			Assert.Equal( quarters[ TimeSpec.QuartersPerHalfYear - 1 ].End, halfyear.End );
		} // GetFiscalQuartersTest

        // ----------------------------------------------------------------------
        // http://en.wikipedia.org/wiki/4-4-5_Calendar
        [Trait("Category", "HalfYear")]
        [Fact]
		public void YearGetMonthsTest()
		{
			HalfYear halfyear = new HalfYear( 2006,YearHalfYear.First, GetFiscalYearCalendar( FiscalYearAlignment.LastDay ) );
			ITimePeriodCollection months = halfyear.GetMonths();
			Assert.NotNull(months);
			Assert.Equal( months.Count, TimeSpec.MonthsPerHalfYear );

			Assert.Equal( months[ 0 ].Start, new DateTime( 2006, 8, 27 ) );
			for ( int i = 0; i < months.Count; i++ )
			{
				Assert.Equal( months[ i ].Duration.Subtract( TimeCalendar.DefaultEndOffset ).Days,
					( i + 1 ) % 3 == 0 ? TimeSpec.FiscalDaysPerLongMonth : TimeSpec.FiscalDaysPerShortMonth );
			}
			Assert.Equal( months[ TimeSpec.MonthsPerHalfYear - 1 ].End, halfyear.End );
		} // FiscalYearGetMonthsTest

	} // class HalfYearTest

} // namespace Itenso.TimePeriodTests
// -- EOF -------------------------------------------------------------------
