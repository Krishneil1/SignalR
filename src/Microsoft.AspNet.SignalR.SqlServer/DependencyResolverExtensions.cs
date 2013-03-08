﻿// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.md in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNet.SignalR.SqlServer;

namespace Microsoft.AspNet.SignalR
{
    public static class DependencyResolverExtensions
    {
        /// <summary>
        /// Use SqlServer as the backplane for SignalR.
        /// </summary>
        /// <param name="resolver">The dependency resolver.</param>
        /// <param name="connectionString">The SQL Server connection string.</param>
        /// <returns>The dependency resolver.</returns>
        public static IDependencyResolver UseSqlServer(this IDependencyResolver resolver, string connectionString)
        {
            return UseSqlServer(resolver, connectionString, 1);
        }

        /// <summary>
        /// Use SqlServer as the backplane for SignalR.
        /// </summary>
        /// <param name="resolver">The dependency resolver.</param>
        /// <param name="connectionString">The SQL Server connection string.</param>
        /// <param name="tableCount">The number of tables to use as "message tables".</param>
        /// <returns>The dependency resolver.</returns>
        public static IDependencyResolver UseSqlServer(this IDependencyResolver resolver, string connectionString, int tableCount)
        {
            return UseSqlServer(resolver, connectionString, 1, SqlMessageBus.DefaultQueueSize);
        }

        /// <summary>
        /// Use SqlServer as the backplane for SignalR.
        /// </summary>
        /// <param name="resolver">The dependency resolver.</param>
        /// <param name="connectionString">The SQL Server connection string.</param>
        /// <param name="tableCount">The number of tables to use as "message tables".</param>
        /// <param name="queueSize">The max number of outgoing messages to queue in case SQL server goes offline.</param>
        /// <returns>The dependency resolver</returns>
        public static IDependencyResolver UseSqlServer(this IDependencyResolver resolver, string connectionString, int tableCount, int queueSize)
        {
            var bus = new Lazy<SqlMessageBus>(() => new SqlMessageBus(connectionString, tableCount, queueSize, resolver));
            resolver.Register(typeof(IMessageBus), () => bus.Value);

            return resolver;
        }
    }
}
