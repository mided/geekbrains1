using System;

namespace Domain.Exceptions
{
    public class DuplicateRecordException : Exception
    {
        public override string Message => "Record already exists";
    }
}
