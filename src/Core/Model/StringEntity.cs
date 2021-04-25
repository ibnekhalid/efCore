using System;

namespace Core.Model
{
    public class StringEntity
    {
        public string Id { get; protected set; } = Guid.NewGuid().ToString();
    }
  
}
