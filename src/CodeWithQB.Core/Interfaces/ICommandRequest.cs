using System.Collections.Generic;

namespace CodeWithQB.Core.Interfaces
{
    public interface ICommandRequest<TResponse> 
    {
        string Key { get; }        
        IEnumerable<string> SideEffects { get; }
    }
}
