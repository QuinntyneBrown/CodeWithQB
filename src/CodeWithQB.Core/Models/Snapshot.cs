using System;

namespace CodeWithQB.Core.Models
{
    public class Snapshot
    {
        public Guid SnapshotId { get; set; }
        public DateTime AsOfDateTime { get; set; }
        public string Data { get; set; }
        public int Sequence { get; set; }
    }
}




