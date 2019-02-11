[![Build Status](https://travis-ci.org/OwnSpace/XrmTypedQuery.svg?branch=master)](https://travis-ci.org/OwnSpace/XrmTypedQuery)
# CRM 365 TypedQuery

This package provides ability to use strongly typed ColumnSet and QueryExpression and so construct them without typo:
```csharp
var query =
    new QueryExpression<Account>
        {
            ColumnSet = new ColumnSet<Account>(a => a.AccountNumber, a => a.AccountCategoryCode, a => a.Address1_Longitude)
        };

query.AddOrder(a => a.AccountCategoryCode, OrderType.Ascending);

var linkEntity = query.AddLink<Lead>(a => a.AccountId, l => l.LeadId);
```

### Checkout package on [NuGet](https://www.nuget.org/packages/XrmTypedQuery/)
