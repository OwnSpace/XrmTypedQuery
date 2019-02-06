using Microsoft.Xrm.Sdk.Query;
using NUnit.Framework;

namespace Xrm.TypedQuery.Tests
{
    [TestFixture]
    public class TypedQueryTests
    {
        [Test]
        public void ColumnsAddedProperly()
        {
            var query =
                new QueryExpression(Account.EntityLogicalName)
                    {
                        ColumnSet = new ColumnSet<Account>(a => a.AccountNumber, a => a.AccountCategoryCode, a => a.Address1_Longitude, a => a.CreatedBy)
                    };

            Assert.That(query.ColumnSet.Columns, Has.Count.EqualTo(4));
            Assert.That(query.ColumnSet.Columns, Does.Contain("accountnumber"));
            Assert.That(query.ColumnSet.Columns, Does.Contain("accountcategorycode"));
            Assert.That(query.ColumnSet.Columns, Does.Contain("address1_longitude"));
            Assert.That(query.ColumnSet.Columns, Does.Contain("createdby"));
        }

        [Test]
        public void QueryExpressionTest()
        {
            var query =
                new QueryExpression<Account>
                    {
                        ColumnSet = new ColumnSet<Account>(a => a.AccountNumber, a => a.AccountCategoryCode, a => a.Address1_Longitude, a => a.CreatedBy)
                    };

            Assert.That(query.EntityName, Is.EqualTo(Account.EntityLogicalName));
            Assert.That(query.ColumnSet.Columns, Has.Count.EqualTo(4));
            Assert.That(query.ColumnSet.Columns, Does.Contain("accountnumber"));
            Assert.That(query.ColumnSet.Columns, Does.Contain("accountcategorycode"));
            Assert.That(query.ColumnSet.Columns, Does.Contain("address1_longitude"));
            Assert.That(query.ColumnSet.Columns, Does.Contain("createdby"));
        }

        [Test]
        public void QueryExpressionAddLink()
        {
            var query =
                new QueryExpression<Account>
                    {
                        ColumnSet = new ColumnSet<Account>(a => a.AccountNumber)
                    };

            Assert.That(query.EntityName, Is.EqualTo(Account.EntityLogicalName));
            Assert.That(query.ColumnSet.Columns, Has.Count.EqualTo(1));
            Assert.That(query.ColumnSet.Columns, Does.Contain("accountnumber"));

            var linkEntity = query.AddLink<Lead>(a => a.AccountCategoryCode, l => l.LeadId);

            Assert.That(linkEntity.LinkFromEntityName, Is.EqualTo(Account.EntityLogicalName));
            Assert.That(linkEntity.LinkToEntityName, Is.EqualTo(Lead.EntityLogicalName));
            Assert.That(linkEntity.LinkFromAttributeName, Is.EqualTo("accountcategorycode"));
            Assert.That(linkEntity.LinkToAttributeName, Is.EqualTo("leadid"));
        }

        [Test]
        public void QueryExpressionAddOrder()
        {
            var query = new QueryExpression<Account>();

            query.AddOrder(a => a.AccountCategoryCode, OrderType.Ascending);

            Assert.That(query.Orders, Has.Count.EqualTo(1));
            Assert.That(query.Orders[0].AttributeName, Is.EqualTo("accountcategorycode"));
            Assert.That(query.Orders[0].OrderType, Is.EqualTo(OrderType.Ascending));
        }
    }
}
