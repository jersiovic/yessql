using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YesSql.Extensions;
using System.Reflection;
using YesSql.Collections;

namespace YesSql.Indexes
{
    public class Relation<T1, T2> : IRelation<T1>
        where T1 : class
        where T2 : class
    {
        public Relation(string sourcePropertyName)
        {
            InitializeRelation(e => e, sourcePropertyName, null, null);
        }

        public Relation(Expression<Func<T1, object>> sourceEntityExpr, string sourcePropertyName)
        {
            InitializeRelation(sourceEntityExpr, sourcePropertyName, null, null);
        }

        public Relation(Expression<Func<T1, object>> sourceEntityExpr, string sourcePropertyName, Func<T1, object> targetRootEntityFunc, Func<T2, object> targetEntityFunc)
        {
            InitializeRelation(sourceEntityExpr, sourcePropertyName, targetRootEntityFunc, targetEntityFunc);
        }

        private string _sourcePath;
        private string _sourcePropertyName;

        private Func<T1, object> _sourceEntityFunc;
        private Func<T1, object> _targetRootEntityFunc;
        private Func<T2, object> _targetEntityFunc;

        private void InitializeRelation(Expression<Func<T1, object>> sourceEntityExpr, string sourcePropertyName, Func<T1, object> targetRootEntityFunc, Func<T2, object> targetEntityFunc)
        {
            _sourcePath = sourceEntityExpr.Body.GetPath();
            _sourceEntityFunc = sourceEntityExpr.Compile();
            _sourcePropertyName = sourcePropertyName;
            _targetRootEntityFunc = targetRootEntityFunc;
            _targetEntityFunc = targetEntityFunc;
        }

        public IEnumerable<TIndex> GetRelationIndexes<TIndex>(T1 sourceRootEntity, IStore store)
            where TIndex : class, new()
        {
            var indexes = new List<TIndex>();
            foreach (var sourceEntity in GetSourceEntities(sourceRootEntity))
            {
                var targetEntity = sourceEntity.GetType().GetRuntimeProperties()
                    .Where(p=>p.Name== _sourcePropertyName).First().GetValue(sourceEntity);
                if (targetEntity == null)
                    continue;
                var index = new TIndex() as RelationIndex;
                index.EntityId = store.GetIdAccessor(sourceEntity.GetType(), "Id").Get(sourceEntity);
                index.EntityPath = _sourcePath + "." + _sourcePropertyName;
                index.EntityPropertyName = _sourcePropertyName;
                index.TargetEntityId = store.GetIdAccessor(targetEntity.GetType(), "Id").Get(targetEntity);
                if (_targetRootEntityFunc != null)
                {
                    var targetRootEntity = _targetRootEntityFunc.Invoke(sourceRootEntity);
                    index.TargetDocumentId = store.GetIdAccessor(typeof(T1), "Id").Get(targetRootEntity);
                }
                indexes.Add(index as TIndex);
            }
            return indexes;
        }
        public IEnumerable<object> GetSourceEntities(T1 sourceRootEntity)
        {
            var sourceEntities = new List<object>();

            var srcEntity = _sourceEntityFunc.Invoke(sourceRootEntity);
            //if navigation property is null it means it was not initialized.
            if (srcEntity == null)
                return sourceEntities;

            if (srcEntity as IEnumerable == null)
                sourceEntities.Add(srcEntity);
            else
            {
                foreach (var sourceEntity in srcEntity as IEnumerable)
                {
                    if (srcEntity!=null)
                        sourceEntities.Add(sourceEntity);
                }
            }
            return sourceEntities;
        }

        public void InitializeNestedEntitiesIds(T1 rootEntity, string collectionName, IStore store, ISession session)
        {
            foreach (var entity in GetSourceEntities(rootEntity))
            {
                if (entity == rootEntity)
                    continue;
                var accessor = store.GetIdAccessor(entity.GetType(), "Id");
                if (accessor == null)
                {
                    throw new Exception();
                }

                // it's a new entity
                if (accessor.Get(entity) == 0)
                {
                    var collection = collectionName;
                    var id = store.GetNextId(session, collection);
                    accessor.Set(entity, id);
                }
            }
        }
    }

}
