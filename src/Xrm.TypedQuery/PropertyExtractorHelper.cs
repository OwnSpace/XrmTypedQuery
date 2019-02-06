using System;
using System.Linq.Expressions;
using System.Reflection;

using Microsoft.Xrm.Sdk;

namespace Xrm.TypedQuery
{
    internal static class PropertyExtractorHelper
    {
        internal static string ExtractPropertyName<TEntity>(this Expression<Func<TEntity, object>> column) where TEntity : Entity
        {
            switch (column.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return ((PropertyInfo)((MemberExpression)column.Body).Member).Name.ToLowerInvariant();
                case ExpressionType.Convert:
                    var expr = (MemberExpression)((UnaryExpression)column.Body).Operand;
                    return ((PropertyInfo)expr.Member).Name.ToLowerInvariant();
                default:
                    throw new NotSupportedException($"Unsupported property expression, node type: {column.Body.NodeType}");
            }
        }
    }
}
