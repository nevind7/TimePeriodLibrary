// -- FILE ------------------------------------------------------------------
// name       : TimeInterval.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.05.06
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System;

namespace TimePeriod
{

	// ------------------------------------------------------------------------
	public class TimeInterval : ITimeInterval
	{

		// ----------------------------------------------------------------------
		public static readonly TimeInterval Anytime = new TimeInterval(
			TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate, IntervalEdge.Closed, IntervalEdge.Closed, false, true );

		// ----------------------------------------------------------------------
		public TimeInterval() :
			this( TimeSpec.MinPeriodDate, TimeSpec.MaxPeriodDate )
		{
		} // TimeInterval

		// ----------------------------------------------------------------------
		public TimeInterval( DateTime moment,
			IntervalEdge edge = IntervalEdge.Closed, IntervalEdge endEdge = IntervalEdge.Closed,
			bool isIntervalEnabled = true, bool isReadOnly = false ) :
			this( moment, moment, edge, endEdge, isIntervalEnabled, isReadOnly )
		{
		} // TimeInterval

		// ----------------------------------------------------------------------
		public TimeInterval( DateTime interval, DateTime endInterval,
			IntervalEdge edge = IntervalEdge.Closed, IntervalEdge endEdge = IntervalEdge.Closed, 
			bool isIntervalEnabled = true, bool isReadOnly = false )
		{
			if ( interval <= endInterval )
			{
				this.interval = interval;
				this.endInterval = endInterval;
			}
			else
			{
				this.endInterval = interval;
				this.interval = endInterval;
			}

			this.edge = edge;
			this.endEdge = endEdge;

			this.isIntervalEnabled = isIntervalEnabled;
			this.isReadOnly = isReadOnly;
		} // TimeInterval

		// ----------------------------------------------------------------------
		public TimeInterval( ITimePeriod copy )
		{
			if ( copy == null )
			{
				throw new ArgumentNullException( "copy" );
			}
			ITimeInterval timeInterval = copy as ITimeInterval;
			if ( timeInterval != null )
			{
				interval = timeInterval.StartInterval;
				endInterval = timeInterval.EndInterval;
				edge = timeInterval.StartEdge;
				endEdge = timeInterval.EndEdge;
				isIntervalEnabled = timeInterval.IsIntervalEnabled;
			}
			else
			{
				interval = copy.Start;
				endInterval = copy.End;
			}

			isReadOnly = copy.IsReadOnly;
		} // TimeInterval

		// ----------------------------------------------------------------------
		protected TimeInterval( ITimePeriod copy, bool isReadOnly )
		{
			if ( copy == null )
			{
				throw new ArgumentNullException( "copy" );
			}
			ITimeInterval timeInterval = copy as ITimeInterval;
			if ( timeInterval != null )
			{
				interval = timeInterval.StartInterval;
				endInterval = timeInterval.EndInterval;
				edge = timeInterval.StartEdge;
				endEdge = timeInterval.EndEdge;
				isIntervalEnabled = timeInterval.IsIntervalEnabled;
			}
			else
			{
				interval = copy.Start;
				endInterval = copy.End;
			}
			this.isReadOnly = isReadOnly;
		} // TimeInterval

		// ----------------------------------------------------------------------
		public bool IsReadOnly => isReadOnly; // IsReadOnly

		// ----------------------------------------------------------------------
		public bool IsAnytime => !HasStart && !HasEnd; // IsAnytime

		// ----------------------------------------------------------------------
		public bool IsMoment => interval.Equals( endInterval ); // IsMoment

		// ----------------------------------------------------------------------
		public bool IsStartOpen => edge == IntervalEdge.Open; // IsOpen

		// ----------------------------------------------------------------------
		public bool IsEndOpen => endEdge == IntervalEdge.Open; // IsStartOpen

		// ----------------------------------------------------------------------
		public bool IsOpen => IsStartOpen && IsEndOpen; // IsOpen

		// ----------------------------------------------------------------------
		public bool IsStartClosed => edge == IntervalEdge.Closed; // IsStartClosed

	// ----------------------------------------------------------------------
		public bool IsEndClosed => endEdge == IntervalEdge.Closed; // IsEndClosed

		// ----------------------------------------------------------------------
		public bool IsClosed => IsStartClosed && IsEndClosed; // IsClosed

		// ----------------------------------------------------------------------
		public bool IsEmpty => IsMoment && !IsClosed; // IsMoment

		// ----------------------------------------------------------------------
		public bool IsDegenerate => IsMoment && IsClosed; // IsDegenerate

		// ----------------------------------------------------------------------
		public bool IsIntervalEnabled
		{
			get => isIntervalEnabled;
            set
			{
				CheckModification();
				isIntervalEnabled = value;
			}
		} // IsIntervalEnabled

		// ----------------------------------------------------------------------
		public bool HasStart => !( interval == TimeSpec.MinPeriodDate && edge == IntervalEdge.Closed ); // HasStart

		// ----------------------------------------------------------------------
		public DateTime StartInterval
		{
			get => interval;
            set
			{
				CheckModification();
				if ( value > endInterval )
				{
					throw new ArgumentOutOfRangeException( "value" );
				}
				interval = value;
			}
		} // StartInterval

		// ----------------------------------------------------------------------
		public DateTime Start
		{
			get
			{
				if ( isIntervalEnabled && edge == IntervalEdge.Open )
				{
					return interval.AddTicks( 1 );
				}
				return interval;
			}
		} // Start

		// ----------------------------------------------------------------------
		public IntervalEdge StartEdge
		{
			get => edge;
            set
			{
				CheckModification();
				edge = value;
			}
		} // StartEdge

		// ----------------------------------------------------------------------
		public bool HasEnd => !( endInterval == TimeSpec.MaxPeriodDate && endEdge == IntervalEdge.Closed ); // HasEnd

		// ----------------------------------------------------------------------
		public DateTime EndInterval
		{
			get => endInterval;
            set
			{
				CheckModification();
				if ( value < interval )
				{
					throw new ArgumentOutOfRangeException( "value" );
				}
				endInterval = value;
			}
		} // EndInterval

		// ----------------------------------------------------------------------
		public DateTime End
		{
			get
			{
				if ( isIntervalEnabled && endEdge == IntervalEdge.Open )
				{
					return endInterval.AddTicks( -1 );
				}
				return endInterval;
			}
		} // End

		// ----------------------------------------------------------------------
		public IntervalEdge EndEdge
		{
			get => endEdge;
            set
			{
				CheckModification();
				endEdge = value;
			}
		} // EndEdge

		// ----------------------------------------------------------------------
		public TimeSpan Duration => endInterval.Subtract( interval ); // Duration

		// ----------------------------------------------------------------------
		public string DurationDescription => TimeFormatter.Instance.GetDuration( Duration, DurationFormatType.Detailed ); // DurationDescription

		// ----------------------------------------------------------------------
		public virtual TimeSpan GetDuration( IDurationProvider provider )
		{
			if ( provider == null )
			{
				throw new ArgumentNullException( "provider" );
			}
			return provider.GetDuration( Start, End );
		} // GetDuration

		// ----------------------------------------------------------------------
		public virtual void Setup( DateTime newStartInterval, DateTime newEndInterval )
		{
			CheckModification();
			if ( newStartInterval <= newEndInterval )
			{
				interval = newStartInterval;
				endInterval = newEndInterval;
			}
			else
			{
				endInterval = newStartInterval;
				interval = newEndInterval;
			}
		} // Setup

		// ----------------------------------------------------------------------
		public virtual bool IsSamePeriod( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}
			return Start == test.Start && End == test.End;
		} // IsSamePeriod

		// ----------------------------------------------------------------------
		public virtual bool HasInside( DateTime test )
		{
			return TimePeriodCalc.HasInside( this, test );
		} // HasInside

		// ----------------------------------------------------------------------
		public virtual bool HasInside( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}
			return TimePeriodCalc.HasInside( this, test );
		} // HasInside

		// ----------------------------------------------------------------------
		public ITimeInterval Copy()
		{
			return Copy( TimeSpan.Zero );
		} // Copy

		// ----------------------------------------------------------------------
		public virtual ITimeInterval Copy( TimeSpan offset )
		{
			return new TimeInterval( 
				interval.Add( offset ), 
				endInterval.Add( offset ),
				edge, 
				endEdge,
				IsIntervalEnabled,
				IsReadOnly );
		} // Copy

		// ----------------------------------------------------------------------
		public virtual void Move( TimeSpan offset )
		{
			CheckModification();
			if ( offset == TimeSpan.Zero )
			{
				return;
			}
			interval = interval.Add( offset );
			endInterval = endInterval.Add( offset );
		} // Move

		// ----------------------------------------------------------------------
		public virtual void ExpandStartTo( DateTime moment )
		{
			CheckModification();
			if ( interval > moment )
			{
				interval = moment;
			}
		} // ExpandStartTo

		// ----------------------------------------------------------------------
		public virtual void ExpandEndTo( DateTime moment )
		{
			CheckModification();
			if ( endInterval < moment )
			{
				endInterval = moment;
			}
		} // ExpandEndTo

		// ----------------------------------------------------------------------
		public void ExpandTo( DateTime moment )
		{
			ExpandStartTo( moment );
			ExpandEndTo( moment );
		} // ExpandTo

		// ----------------------------------------------------------------------
		public void ExpandTo( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			ITimeInterval timeInterval = period as ITimeInterval;
			if ( timeInterval != null )
			{
				ExpandStartTo( timeInterval.StartInterval );
				ExpandEndTo( timeInterval.EndInterval );
			}
			else
			{
				ExpandStartTo( period.Start );
				ExpandEndTo( period.End );
			}
		} // ExpandTo

		// ----------------------------------------------------------------------
		public virtual void ShrinkStartTo( DateTime moment )
		{
			CheckModification();
			if ( HasInside( moment ) && interval < moment )
			{
				interval = moment;
			}
		} // ShrinkStartTo

		// ----------------------------------------------------------------------
		public virtual void ShrinkEndTo( DateTime moment )
		{
			CheckModification();
			if ( HasInside( moment ) && endInterval > moment )
			{
				endInterval = moment;
			}
		} // ShrinkEndTo

		// ----------------------------------------------------------------------
		public void ShrinkTo( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			ITimeInterval timeInterval = period as ITimeInterval;
			if ( timeInterval != null )
			{
				ShrinkStartTo( timeInterval.StartInterval );
				ShrinkEndTo( timeInterval.EndInterval );
			}
			else
			{
				ShrinkStartTo( period.Start );
				ShrinkEndTo( period.End );
			}
			ShrinkStartTo( period.Start );
		} // ShrinkTo

		// ----------------------------------------------------------------------
		public virtual bool IntersectsWith( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}
			return TimePeriodCalc.IntersectsWith( this, test );
		} // IntersectsWith

		// ----------------------------------------------------------------------
		public virtual ITimeInterval GetIntersection( ITimePeriod period )
		{
			if ( period == null )
			{
				throw new ArgumentNullException( "period" );
			}
			if ( !IntersectsWith( period ) )
			{
				return null;
			}
			DateTime start = Start;
			DateTime end = End;
			DateTime periodStart = period.Start;
			DateTime periodEnd = period.End;
			return new TimeInterval(
				periodStart > start ? periodStart : start, 
				periodEnd < end ? periodEnd : end, 
				IntervalEdge.Closed,
				IntervalEdge.Closed,
				IsIntervalEnabled,
				IsReadOnly );
		} // GetIntersection

		// ----------------------------------------------------------------------
		public virtual bool OverlapsWith( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}
			return TimePeriodCalc.OverlapsWith( this, test );
		} // OverlapsWith

		// ----------------------------------------------------------------------
		public virtual PeriodRelation GetRelation( ITimePeriod test )
		{
			if ( test == null )
			{
				throw new ArgumentNullException( "test" );
			}
			return TimePeriodCalc.GetRelation( this, test );
		} // GetRelation

		// ----------------------------------------------------------------------
		public virtual int CompareTo( ITimePeriod other, ITimePeriodComparer comparer )
		{
			if ( other == null )
			{
				throw new ArgumentNullException( "other" );
			}
			if ( comparer == null )
			{
				throw new ArgumentNullException( "comparer" );
			}
			return comparer.Compare( this, other );
		} // CompareTo

		// ----------------------------------------------------------------------
		public virtual void Reset()
		{
			CheckModification();
			isIntervalEnabled = true;
			interval = TimeSpec.MinPeriodDate;
			endInterval = TimeSpec.MaxPeriodDate;
			edge = IntervalEdge.Closed;
			endEdge = IntervalEdge.Closed;
		} // Reset

		// ----------------------------------------------------------------------
		public string GetDescription( ITimeFormatter formatter = null )
		{
			return Format( formatter ?? TimeFormatter.Instance );
		} // GetDescription

		// ----------------------------------------------------------------------
		protected virtual string Format( ITimeFormatter formatter )
		{
			return formatter.GetInterval( interval, endInterval, edge, endEdge, Duration );
		} // Format

		// ----------------------------------------------------------------------
		public override string ToString()
		{
			return GetDescription();
		} // ToString

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
			return HasSameData( obj as TimeInterval );
		} // IsEqual

		// ----------------------------------------------------------------------
		private bool HasSameData( TimeInterval comp )
		{
			return
				isReadOnly == comp.isReadOnly &&
				isIntervalEnabled == comp.isIntervalEnabled &&
				interval == comp.interval &&
				endInterval == comp.endInterval &&
				edge == comp.edge &&
				endEdge == comp.endEdge;
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
				isReadOnly,
				isIntervalEnabled,
				interval, 
				endInterval, 
				edge, 
				endEdge );
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		protected void CheckModification()
		{
			if ( IsReadOnly )
			{
				throw new NotSupportedException( "TimeInterval is read-only" );
			}
		} // CheckModification

		// ----------------------------------------------------------------------
		// members
		private readonly bool isReadOnly;
		private bool isIntervalEnabled = true;
		private DateTime interval;
		private DateTime endInterval;
		private IntervalEdge edge;
		private IntervalEdge endEdge;

	} // class TimeInterval

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
