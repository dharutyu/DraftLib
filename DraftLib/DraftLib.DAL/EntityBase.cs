using System;

namespace DraftLib.DAL
{
    public abstract class EntityBase
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}