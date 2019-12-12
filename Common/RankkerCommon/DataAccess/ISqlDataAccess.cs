using System;
using System.Collections.Generic;

namespace RankkerCommon.DataAccess
{
    public interface ISqlDataAccess : IDisposable
    {
        void StartTransaction(string connectionString);
        List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters);
        void SaveDataInTransaction<T>(string storedProcedure, T parameters);
        void CommitTransaction();
        void RollbackTransaction();
    }
}