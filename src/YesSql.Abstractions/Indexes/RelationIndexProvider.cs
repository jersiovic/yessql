using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YesSql.Indexes
{
    public abstract class RelationIndexProvider<T, TIndex> : IndexProvider<T>
        where T : class
        where TIndex : class, IIndex, new()
    {
        public abstract IEnumerable<IRelation<T>> GetRelations();

        public override void Describe(DescribeContext<T> context)
        {
            // for each entity relation a relation index is created
            context.For<TIndex>()
                .MapRelation(
                    entity =>
                    {
                        var result = new List<TIndex>();
                        foreach (var relation in GetRelations())
                        {
                            result.AddRange(relation.GetRelationIndexes<TIndex>(entity, Store));
                        }
                        return result;
                    })
                .InitializeNestedEntitiesIds(
                    (entity, session) =>
                    {
                        foreach (var relation in GetRelations())
                        {
                            relation.InitializeNestedEntitiesIds(entity, CollectionName, Store, session);
                        }
                    }
                );
        }
    }
}