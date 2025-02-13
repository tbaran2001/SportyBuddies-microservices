﻿using IdentityService.Models;

namespace IdentityService.Data.Seed;

public static class InitialData
{
    public static List<ApplicationUser> Users { get; }

    static InitialData()
    {
        Users =
        [
            new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = "admin@xd.pl",
                Email = "admin@xd.pl",
                SecurityStamp = Guid.NewGuid().ToString()
            },

            new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = "user@xd.pl",
                Email = "user@xd.pl",
                SecurityStamp = Guid.NewGuid().ToString()
            }
        ];
    }
}