using System;

namespace Sheriff.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityType, int id)
            : base(Strings.NotFound.Format(entityType, id))
        {
        }
    }
}