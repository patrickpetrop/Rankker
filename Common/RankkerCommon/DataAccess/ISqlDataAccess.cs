using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RankkerCommon.DataAccess
{
    public interface ISqlDataAccess : IDisposable
    {
        void StartTransaction(string connectionString);
        List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters);
        Task SaveDataInTransactionAsync<T>(string storedProcedure, T parameters);
        void CommitTransaction();
        void RollbackTransaction();
    }
}