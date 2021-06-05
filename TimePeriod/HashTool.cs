// -- FILE ------------------------------------------------------------------
// name       : HashTool.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System.Collections;

namespace TimePeriod
{

	// ------------------------------------------------------------------------
	/// <summary>
	/// Some hash utility methods for use in the implementation of value types
	/// and collections.
	/// </summary>
	public static class HashTool
	{

		// ----------------------------------------------------------------------
		public static int AddHashCode( int hash, object obj )
		{
			int combinedHash = obj != null ? obj.GetHashCode() : FiscalNullValue;
			// if ( hash != 0 ) // perform this check to prevent FxCop warning 'op could overflow'
			// {
			combinedHash += hash * FiscalFactor;
			// }
			return combinedHash;
		} // AddHashCode

		// ----------------------------------------------------------------------
		public static int AddHashCode( int hash, int objHash )
		{
			int combinedHash = objHash;
			// if ( hash != 0 ) // perform this check to prevent FxCop warning 'op could overflow'
			// {
			combinedHash += hash * FiscalFactor;
			// }
			return combinedHash;
		} // AddHashCode

		// ----------------------------------------------------------------------
		public static int ComputeHashCode( object obj )
		{
			return obj != null ? obj.GetHashCode() : FiscalNullValue;
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		public static int ComputeHashCode( params object[] objs )
		{
			int hash = FiscalInitValue;
			if ( objs != null )
			{
				foreach ( object obj in objs )
				{
					hash = hash * FiscalFactor + ( obj != null ? obj.GetHashCode() : FiscalNullValue );
				}
			}
			return hash;
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		public static int ComputeHashCode( IEnumerable enumerable )
		{
			int hash = FiscalInitValue;
			foreach ( object item in enumerable )
			{
				hash = hash * FiscalFactor + ( item != null ? item.GetHashCode() : FiscalNullValue );
			}
			return hash;
		} // ComputeHashCode

		// ----------------------------------------------------------------------
		// members
		private const int FiscalNullValue = 0;
		private const int FiscalInitValue = 1;
		private const int FiscalFactor = 31;

	} // class HashTool

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
