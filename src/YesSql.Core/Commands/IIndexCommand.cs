﻿using System.Data.Common;
using System.Threading.Tasks;

namespace YesSql.Core.Commands
{
    public interface IIndexCommand
    {
        Task ExecuteAsync(DbConnection connection, DbTransaction transaction);
        int ExecutionOrder { get; }
    }
}