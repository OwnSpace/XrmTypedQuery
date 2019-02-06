using System;
using System.Linq.Expressions;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Xrm.TypedQuery
{
    /// <summary>
    /// Provides ability to construct regular <see cref="QueryExpression"/> using strongly typed <see cref="ColumnSet&lt;TEntity&gt;"/>
    /// </summary>
    /// <typeparam name="TEntity">Early-bound proxy-class entity type</typeparam>
    public class QueryExpression<TEntity> where TEntity : Entity, new()
    {
        private static readonly Entity Entity = new TEntity();

        private readonly QueryExpression queryExpression = new QueryExpression();

        public QueryExpression()
        {
            queryExpression.EntityName = Entity.LogicalName;
            queryExpression.Criteria = new FilterExpression();
            queryExpression.PageInfo = new PagingInfo();
        }

        public static implicit operator QueryExpression(QueryExpression<TEntity> qe)
        {
            return qe.queryExpression;
        }

        public bool Distinct
        {
            get => queryExpression.Distinct;
            set => queryExpression.Distinct = value;
        }

        public bool NoLock
        {
            get => queryExpression.NoLock;
            set => queryExpression.NoLock = value;
        }

        public PagingInfo PageInfo
        {
            get => queryExpression.PageInfo;
            set => queryExpression.PageInfo = value;
        }

        public DataCollection<LinkEntity> LinkEntities => queryExpression.LinkEntities;

        public FilterExpression Criteria
        {
            get => queryExpression.Criteria;
            set => queryExpression.Criteria = value;
        }

        public DataCollection<OrderExpression> Orders => queryExpression.Orders;

        public string EntityName => queryExpression.EntityName;

        public ColumnSet<TEntity> ColumnSet
        {
            get => queryExpression.ColumnSet;
            set => queryExpression.ColumnSet = value;
        }

        public int? TopCount
        {
            get => queryExpression.TopCount;
            set => queryExpression.TopCount = value;
        }

        public void AddOrder(Expression<Func<TEntity, object>> attributeName, OrderType orderType)
        {
            Orders.Add(new OrderExpression(attributeName.ExtractPropertyName(), orderType));
        }

        public LinkEntity AddLink<TLinkToEntity>(
            Expression<Func<TEntity, object>> linkFromAttributeName,
            Expression<Func<TLinkToEntity, object>> linkToAttributeName,
            JoinOperator joinOperator = JoinOperator.Inner) where TLinkToEntity : Entity, new()
        {
            var linkEntity = new LinkEntity(EntityName, new TLinkToEntity().LogicalName, linkFromAttributeName.ExtractPropertyName(), linkToAttributeName.ExtractPropertyName(), joinOperator);
            LinkEntities.Add(linkEntity);
            return linkEntity;
        }
    }
}
