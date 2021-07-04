using System;

namespace Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public override string Message => "Record not found";
    }
}
