// -- FILE ------------------------------------------------------------------
// name       : DateTimeSet.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;

namespace TimePeriod
{

	// ------------------------------------------------------------------------
	public class DateTimeSet : IDateTimeSet
	{

		// ----------------------------------------------------------------------
		public DateTimeSet()
		{
		} // DateTimeSet

		// ----------------------------------------------------------------------
		public DateTimeSet( IEnumerable<DateTime> moments )
		{
			AddAll( moments );
		} // DateTimeSet

		// ----------------------------------------------------------------------
		public DateTime this[ int index ] => moments[ index ];  // this[]

		// ----------------------------------------------------------------------
		public DateTime? Min => !IsEmpty ? moments[ 0 ] : (DateTime?)null; // Min

		// ----------------------------------------------------------------------
		public DateTime? Max => !IsEmpty ? moments[ Count - 1 ] : (DateTime?)null; // Max

		// ----------------------------------------------------------------------
		public TimeSpan? Duration
		{
			get
			{
				DateTime? min = Min;
				DateTime? max = Max;
				return min.HasValue && max.HasValue ? max.Value - min.Value : (TimeSpan?)null;
			}
		} // Duration

		// ----------------------------------------------------------------------
		public bool IsEmpty => Count == 0; // IsEmpty

		// ----------------------------------------------------------------------
		public bool IsMoment
		{
			get
			{
				TimeSpan? duration = Duration;
				return duration.HasValue && duration.Value == TimeSpan.Zero;
			}
		} // IsMoment

		// ----------------------------------------------------------------------
		public bool IsAnytime
		{
			get
			{
				DateTime? min = Min;
				DateTime? max = Max;
				return
					min.HasValue && min.Value == DateTime.MinValue &&
					max.HasValue && max.Value == DateTime.MaxValue;
			}
		} // IsAnytime

		// ----------------------------------------------------------------------
		public int Count => moments.Count; // Count

		// ----------------------------------------------------------------------
		bool ICollection<DateTime>.IsReadOnly => false; // ICollection<DateTime>.IsReadOnly

		// ----------------------------------------------------------------------
		public int IndexOf( DateTime moment )
		{
			return moments.IndexOf( moment );
		} // IndexOf

		// ----------------------------------------------------------------------
		public DateTime? FindPrevious( DateTime moment )
		{
			if ( IsEmpty )
			{
				return null;
			}

			for ( int i = Count - 1; i >= 0; i-- )
			{
				if ( moments[ i ] < moment )
				{
					return moments[ i ];
				}
			}

			return null;
		} // FindPrevious

		// ----------------------------------------------------------------------
		public DateTime? FindNext( DateTime moment )
		{
			if ( IsEmpty )
			{
				return null;
			}

			for ( int i = 0; i < Count; i++ )
			{
				if ( moments[ i ] > moment )
				{
					return moments[ i ];
				}
			}

			return null;
		} // FindNext

		// ----------------------------------------------------------------------
		public IEnumerator<DateTime> GetEnumerator()
		{
			return moments.GetEnumerator();
		} // GetEnumerator

		// ----------------------------------------------------------------------
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		} // IEnumerable.GetEnumerator

		// ----------------------------------------------------------------------
		public bool Add( DateTime moment )
		{
			if ( Contains( moment ) )
			{
				return false;
			}

			for ( int i = 0; i < Count; i++ )
			{
				if ( moments[ i ] <= moment )
				{
					continue;
				}
				moments.Insert( i, moment );
				return true;
			}

			moments.Add( moment );
			return true;
		} // Add

		// ----------------------------------------------------------------------
		void ICollection<DateTime>.Add( DateTime moment )
		{
			Add( moment );
		} // Add

		// ----------------------------------------------------------------------
		public void AddAll( IEnumerable<DateTime> items )
		{
			if ( items == null )
			{
				throw new ArgumentNullException( "items" );
			}

			foreach ( DateTime item in items )
			{
				Add( item );
			}
		} // AddAll

		// ----------------------------------------------------------------------
		public IList<TimeSpan> GetDurations( int index, int count )
		{
			if ( index >= Count - 1 )
			{
				throw new ArgumentOutOfRangeException( "index" );
			}
			if ( count <= 0 )
			{
				throw new ArgumentOutOfRangeException( "count" );
			}

			int endIndex = index + count - 1;
			if ( endIndex >= Count - 1 )
			{
				endIndex = Count - 2;
			}

			List<TimeSpan> durations = new List<TimeSpan>();
			if ( endIndex >= index )
			{
				for ( int i = index; i <= endIndex; i++ )
				{
					durations.Add( this[ i + 1 ] - this[ i ] );
				}
			}
			return durations;
		} // GetDurations

		// ----------------------------------------------------------------------
		public void Clear()
		{
			moments.Clear();
		} // Clear

		// ----------------------------------------------------------------------
		public bool Contains( DateTime moment )
		{
			return moments.Contains( moment );
		} // Contains

		// ----------------------------------------------------------------------
		public void CopyTo( DateTime[] array, int arrayIndex )
		{
			moments.CopyTo( array, arrayIndex );
		} // CopyTo

		// ----------------------------------------------------------------------
		public bool Remove( DateTime moment )
		{
			return moments.Remove( moment );
		} // Remove

		// ----------------------------------------------------------------------
		public override bool Equals( object obj )
		{
			return moments.Equals( obj );
		} // Equals

		// ----------------------------------------------------------------------
		public override int GetHashCode()
		{
			return moments.GetHashCode();
		} // GetHashCode

		// ----------------------------------------------------------------------
		public override string ToString()
		{
			DateTime? min = Min;
			DateTime? max = Max;
			TimeSpan? duration = Duration;
			if ( !min.HasValue || !max.HasValue || !duration.HasValue )
			{
				return TimeFormatter.Instance.GetCollection( Count );
			}

			return TimeFormatter.Instance.GetCollectionPeriod( Count, min.Value, max.Value, duration.Value );
		} // ToString

		// ----------------------------------------------------------------------
		// members
		private readonly List<DateTime> moments = new List<DateTime>();

	} // class DateTimeSet

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
