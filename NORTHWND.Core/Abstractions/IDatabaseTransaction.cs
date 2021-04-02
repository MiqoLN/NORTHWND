using System;
using System.Collections.Generic;
using System.Text;

namespace NORTHWND.Core.Abstractions
{
    public interface IDatabaseTransaction:IDisposable
    {
        public void Commit();
        public void RollBack();
    }
}
