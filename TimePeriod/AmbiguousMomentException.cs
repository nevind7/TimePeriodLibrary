// -- FILE ------------------------------------------------------------------
// name       : AmbiguousMomentException.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.11.03
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace TimePeriod
{
    // ------------------------------------------------------------------------
    [Serializable]
    public class AmbiguousMomentException : Exception
    {
        // ----------------------------------------------------------------------
        public AmbiguousMomentException(DateTime moment)
        {
            this.moment = moment;
        } // AmbiguousMomentException

        // ----------------------------------------------------------------------
        public AmbiguousMomentException(DateTime moment, string message) :
            base(message)
        {
            this.moment = moment;
        } // AmbiguousMomentException

        // ----------------------------------------------------------------------
        public AmbiguousMomentException(DateTime moment, Exception cause) :
            base(cause.Message, cause)
        {
            this.moment = moment;
        } // AmbiguousMomentException

        // ----------------------------------------------------------------------
        public AmbiguousMomentException(DateTime moment, string message, Exception cause) :
            base(message, cause)
        {
            this.moment = moment;
        } // AmbiguousMomentException

        // ----------------------------------------------------------------------

        private AmbiguousMomentException(SerializationInfo info, StreamingContext context) :
            base(info, context)
        {
            moment = (DateTime)info.GetValue("moment", typeof(DateTime));
        } // AmbiguousMomentException

        // ----------------------------------------------------------------------
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("faultInfo", moment);
            base.GetObjectData(info, context);
        } // GetObjectData

        // ----------------------------------------------------------------------
        public DateTime Moment => moment; // Moment

        // ----------------------------------------------------------------------
        // members
        private readonly DateTime moment;
    } // class AmbiguousMomentException
} // namespace Itenso.TimePeriod

// -- EOF -------------------------------------------------------------------