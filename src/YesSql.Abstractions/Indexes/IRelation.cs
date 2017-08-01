using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesSql.Indexes
{
    public interface IRelation<T>
        where T : class
    {
        IEnumerable<TIndex> GetRelationIndexes<TIndex>(T sourceRootEntity, IStore store)
            where TIndex : class, new();
        IEnumerable<object> GetSourceEntities(T sourceRootEntity);
        void InitializeNestedEntitiesIds(T entity, string collectionName, IStore store, ISession session);
    }
}
