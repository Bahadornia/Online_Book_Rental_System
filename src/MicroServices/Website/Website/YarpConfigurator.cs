// Copyright (c) Duende Software. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Duende.Bff;
using Duende.Bff.Yarp;
using Microsoft.AspNetCore.Mvc.Filters;
using Yarp.ReverseProxy.Configuration;

public static class YarpConfigurator
{
    public static void Configure(this IReverseProxyBuilder builder)
    {
        builder.LoadFromMemory(
        [
            new RouteConfig()
            {
                RouteId = "notifications",
                ClusterId = "cluster1",

                Match = new RouteMatch
                {
                    Path = "/hub/OrderNotification/{**catch-all}"
                },
            }
            .WithAntiforgeryCheck()
            .WithAccessToken(TokenType.User),
                ],
                [
            new ClusterConfig
            {
                ClusterId = "cluster1",

                Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                {
                    { "/hub/OrderNotification", new DestinationConfig() { Address = "https://localhost:7101" } },
                }
            }
        ]);
    }
}

