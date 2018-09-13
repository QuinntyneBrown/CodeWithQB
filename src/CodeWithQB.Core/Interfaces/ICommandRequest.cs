using System.Collections.Generic;

namespace CodeWithQB.Core.Interfaces
{
    public interface ICommandRequest<TResponse> 
    {
        string Key { get; set; }
        string Partition { get; set; }
        IEnumerable<string> SideEffects { get; set; }
    }
}
