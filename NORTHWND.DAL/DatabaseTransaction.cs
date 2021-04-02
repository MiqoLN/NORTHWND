using Microsoft.EntityFrameworkCore.Storage;
using NORTHWND.Core.Abstractions;
using System;
using System.Data;

namespace NORTHWND.DAL
{
    public class DatabaseTransaction : IDatabaseTransaction
    {
        private readonly IDbContextTransaction _transaction;
        private bool disposedValue;
        public DatabaseTransaction(NORTHWNDContext context,IsolationLevel isolation)
        {
            _transaction = context.Database.BeginTransaction();
        }
        public void Commit()
        {
            _transaction.Commit();
        }
        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    _transaction.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void RollBack()
        {
            _transaction.Rollback();
        }
    }
}
