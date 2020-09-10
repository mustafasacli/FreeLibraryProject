using FreeLibrary.Core.Enums;
using System;

namespace FreeLibrary.Core.Interfaces
{
    public interface IConnection : IConnectionOperations, IDisposable
    {
        void Open();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        void DisposeTransaction();

        void Close();

        string ConnectionString { get; set; }

        ConnectionTypes ConnectionType { get; }

    }
}