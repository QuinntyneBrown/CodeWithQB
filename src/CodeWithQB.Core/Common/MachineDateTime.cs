using CodeWithQB.Core.Interfaces;
using System;

namespace CodeWithQB.Core.Common
{
    public class MachineDateTime : IDateTime
    {
        public DateTime UtcNow { get { return System.DateTime.Now; } }
    }
}
