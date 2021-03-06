// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace OnlineCourse.IdentityServer
{
    public static class Config
    {
        /*
         * Catalog servise istek atmak için token içinde audience kısmında resource_catalog ve  photo stock servisine istek atmak için ise 
         * resource_photo_stock_catalog olması gerekiyor diyoruz.
         */
        public static IEnumerable<ApiResource> ApiResources =>
             new ApiResource[]
             {
                 /*
                  *Audience 
                  */
                 new ApiResource("resource_catalog"){Scopes={ "coursecatalog_fullpermission" }},
                 new ApiResource("resource_photo_stock_catalog"){Scopes={ "photo_stock_fullpermission" }},
                 new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
             };

        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {

                   };
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("coursecatalog_fullpermission","Catalog API için full erişim"),
                new ApiScope("photo_stock_fullpermission","Photo Stock API için full erişim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
            };
        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId="WebMvcClient",ClientSecrets={ new Secret ("secret".Sha256()) },
                    ClientName="Asp.Net Core Mvc" ,
                    AllowedGrantTypes=GrantTypes.ClientCredentials,AllowedScopes={ "coursecatalog_fullpermission", "photo_stock_fullpermission",IdentityServerConstants.LocalApi.ScopeName }
                }
            };
    }
}