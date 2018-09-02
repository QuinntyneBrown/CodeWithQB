using System;
using System.Collections.Generic;
using System.Text;

namespace CodeWithQB.Core.Models
{
    public class Snapshot
    {
        public Guid SnapshotId { get; set; }
        public DateTime AsOfDateTime { get; set; }
        public string Data { get; set; }
    }
}
