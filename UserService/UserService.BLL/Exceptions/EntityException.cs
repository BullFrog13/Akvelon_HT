using System;

namespace UserService.BLL.Exceptions
{
    public class EntityException : Exception
    {
        public string Entity { get; }

        public EntityException(string message, string entity) : base(message)
        {
            Entity = entity;
        }
    }
}