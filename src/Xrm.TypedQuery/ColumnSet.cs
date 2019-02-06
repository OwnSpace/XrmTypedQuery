using System;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Xrm.TypedQuery
{
    /// <summary>
    /// Provides ability to construct regular <see cref="ColumnSet"/> using strongly typed properties as columns instead strings and avoid typo and mismatches
    /// </summary>
    /// <typeparam name="TEntity">Early-bound proxy-class entity type</typeparam>
    public class ColumnSet<TEntity> where TEntity : Entity
    {
        private static readonly ColumnSet<TEntity> Instance = new ColumnSet<TEntity>();

        private ColumnSet columnSet = new ColumnSet();

        public ColumnSet(params Expression<Func<TEntity, object>>[] columns)
        {
            AddColumns(columns);
        }

        public static implicit operator ColumnSet(ColumnSet<TEntity> cs)
        {
            return cs.columnSet;
        }

        public static implicit operator ColumnSet<TEntity>(ColumnSet cs)
        {
            Instance.columnSet = cs;
            return Instance;
        }

        public DataCollection<string> Columns => columnSet.Columns;

        private void AddColumns(params Expression<Func<TEntity, object>>[] columns)
        {
            foreach (var column in columns.Select(c => c.ExtractPropertyName()))
            {
                AddColumn(column);
            }
        }

        private void AddColumn(string column)
        {
            Columns.Add(column);
        }
    }
}
