// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.DigitalWorkplace.DigitalTwins.QueryBuilder.Dynamic
{
    using System.Collections.Generic;
    using Microsoft.DigitalWorkplace.DigitalTwins.QueryBuilder.Common.Clauses;
    using Microsoft.DigitalWorkplace.DigitalTwins.QueryBuilder.Common.Statements;

    /// <summary>
    /// Base query for all select queries. This query does not support Joins.
    /// </summary>
    public abstract class SelectQueryBase<TQuery, TWhereStatement> : JoinQuery<TQuery, TWhereStatement>
        where TQuery : SelectQueryBase<TQuery, TWhereStatement>
        where TWhereStatement : WhereBaseStatement<TWhereStatement>
    {
        internal SelectQueryBase(string rootTwinAlias, IList<string> allowedAliases, SelectClause selectClause, FromClause fromClause, IList<JoinClause> joinClauses, WhereClause whereClause) : base(rootTwinAlias, allowedAliases, selectClause, fromClause, joinClauses, whereClause)
        {
        }

        /// <summary>
        /// Select Top(N) records.
        /// </summary>
        /// <param name="numberOfRecords">Postive number.</param>
        /// <returns>The ADT Query with TOP clause.</returns>
        public TQuery Top(ushort numberOfRecords)
        {
            selectClause.NumberOfRecords = numberOfRecords;
            return (TQuery)this;
        }
    }

    /// <summary>
    /// Base query for all select queries. This query does not support Joins.
    /// </summary>
    public abstract class SelectNonJoinQueryBase<TQuery, TWhereStatement> : FilteredQuery<TQuery, TWhereStatement>
        where TQuery : SelectNonJoinQueryBase<TQuery, TWhereStatement>
        where TWhereStatement : WhereBaseStatement<TWhereStatement>
    {
        internal SelectNonJoinQueryBase(string rootTwinAlias, IList<string> allowedAliases, SelectClause selectClause, FromClause fromClause, IList<JoinClause> joinClauses, WhereClause whereClause) : base(rootTwinAlias, allowedAliases, selectClause, fromClause, joinClauses, whereClause)
        {
        }

        /// <summary>
        /// Select Top(N) records.
        /// </summary>
        /// <param name="numberOfRecords">Postive number.</param>
        /// <returns>The ADT Query with TOP clause.</returns>
        public TQuery Top(ushort numberOfRecords)
        {
            selectClause.NumberOfRecords = numberOfRecords;
            return (TQuery)this;
        }
    }

    /// <summary>
    /// The query that has the default select that was generated by the FROM clause.
    /// </summary>
    public class DefaultSelectQuery<TWhereStatement> : SelectQueryBase<DefaultSelectQuery<TWhereStatement>, TWhereStatement>
    where TWhereStatement : WhereBaseStatement<TWhereStatement>
    {
        internal DefaultSelectQuery(string rootTwinAlias, IList<string> allowedAliases, SelectClause selectClause, FromClause fromClause, IList<JoinClause> joinClauses, WhereClause whereClauses) : base(rootTwinAlias, allowedAliases, selectClause, fromClause, joinClauses, whereClauses)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="aliases"></param>
        /// <returns></returns>
        public Query<TWhereStatement> Select(params string[] aliases)
        {
            ClearSelects();
            foreach (var name in aliases)
            {
                ValidateAndAddSelect(name);
            }

            return new Query<TWhereStatement>(RootTwinAlias, allowedAliases, selectClause, fromClause, joinClauses, whereClause);
        }
    }

    /// <summary>
    /// The query that has the default select that was generated by the FROM RELATIONSHIPS clause. This query does not support Joins.
    /// </summary>
    public class DefaultSelectNonJoinQuery<TWhereStatement> : SelectNonJoinQueryBase<DefaultSelectNonJoinQuery<TWhereStatement>, TWhereStatement>
    where TWhereStatement : WhereBaseStatement<TWhereStatement>
    {
        internal DefaultSelectNonJoinQuery(string rootTwinAlias, IList<string> allowedAliases, SelectClause selectClause, FromClause fromClause, IList<JoinClause> joinClauses, WhereClause whereClauses) : base(rootTwinAlias, allowedAliases, selectClause, fromClause, joinClauses, whereClauses)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="aliases"></param>
        /// <returns></returns>
        public NonJoinQuery<TWhereStatement> Select(params string[] aliases)
        {
            ClearSelects();
            foreach (var name in aliases)
            {
                ValidateAndAddSelect(name);
            }

            return new NonJoinQuery<TWhereStatement>(RootTwinAlias, allowedAliases, selectClause, fromClause, joinClauses, whereClause);
        }
    }

    /// <summary>
    /// The query that has one select clause.
    /// </summary>
    public class Query<TWhereStatement> : SelectQueryBase<Query<TWhereStatement>, TWhereStatement>
        where TWhereStatement : WhereBaseStatement<TWhereStatement>
    {
        internal Query(string rootTwinAlias, IList<string> allowedAliases, SelectClause selectClause, FromClause fromClause, IList<JoinClause> joinClauses, WhereClause whereClauses) : base(rootTwinAlias, allowedAliases, selectClause, fromClause, joinClauses, whereClauses)
        {
        }
    }

    /// <summary>
    /// The query that has one select clause. This query does not support Joins.
    /// </summary>
    public class NonJoinQuery<TWhereStatement> : SelectNonJoinQueryBase<NonJoinQuery<TWhereStatement>, TWhereStatement>
    where TWhereStatement : WhereBaseStatement<TWhereStatement>
    {
        internal NonJoinQuery(string rootTwinAlias, IList<string> allowedAliases, SelectClause selectClause, FromClause fromClause, IList<JoinClause> joinClauses, WhereClause whereClauses) : base(rootTwinAlias, allowedAliases, selectClause, fromClause, joinClauses, whereClauses)
        {
        }
    }
}