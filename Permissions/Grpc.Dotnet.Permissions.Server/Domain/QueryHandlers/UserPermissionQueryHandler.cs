﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Dotnet.Permissions.Server.Domain.Queries;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Grpc.Dotnet.Permissions.Server.Domain.QueryHandlers
{
    public class UserPermissionQueryHandler : IRequestHandler<UserPermissionsQuery, IEnumerable<string>>
    {
        private readonly Guid adminId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        private readonly ILogger<UserPermissionQueryHandler> logger;

        public UserPermissionQueryHandler(ILogger<UserPermissionQueryHandler> logger)
        {
            this.logger = logger;
        }

        public Task<IEnumerable<string>> Handle(UserPermissionsQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<string> permissions = Enumerable.Empty<string>();

            // TODO: query database here
            if (request.UserId == adminId)
            {
                permissions = new string[]
                {
                    "READ",
                    "CREATE",
                    "DELETE",
                    "UPDATE"
                };
            }
            else
            {
                permissions = new string[]
                {
                    "READ",
                    "CREATE"
                };
            }

            logger.LogInformation($"User permissions: {string.Join(",", permissions)}");

            return Task.FromResult(permissions);
        }
    }
}
