using System;

namespace NORTHWND.Core.Abstractions
{
    public interface IDatabaseTransaction:IDisposable
    {
        public void Commit();
        public void RollBack();
    }
}
