using System;
using System.Collections.Generic;
using System.Text;
using IPS.Interfaces;

namespace IPS.DAL.BASE
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }


}
