using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using YesSql.Attributes;
using YesSql.Collections;
using YesSql.Indexes;
using YesSql.Tests.Models;

namespace YesSql.Tests.Indexes
{
    public class OrderRelationIndex : RelationIndex {}

    public class OrderRelationIndexProvider : RelationIndexProvider<Order, OrderRelationIndex>
    {
        static readonly IRelation<Order>[] Relations = new IRelation<Order>[] {
            new Relation<Order,Product>(o => o.OrderLines, nameof(OrderLine.Product))
        };

        public override IEnumerable<IRelation<Order>> GetRelations()
        {
            return Relations;
        }
    }
}