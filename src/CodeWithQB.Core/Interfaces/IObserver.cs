using reactive.pipes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeWithQB.Core.Interfaces
{
    public interface IObserver<T>: IConsume<T>
    {
    }
}
