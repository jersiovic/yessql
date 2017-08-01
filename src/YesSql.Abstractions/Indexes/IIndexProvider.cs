using System;
using System.Collections.Generic;

namespace YesSql.Indexes
{
    public interface IIndexProvider
    {
        void Describe(IDescriptor context);
        Type ForType();
        string CollectionName { get; set; }
        IStore Store { get; set; }
    }
}