using System.Security.Claims;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.DataSeeding;

public static class AppDataInit
{
    public static void SeedAppData(AppDbContext context)
    {
        SeedArtistRoles(context);
        SeedLinkTypes(context);
        SeedMoods(context);
        SeedTags(context);
        
    }


    public static void MigrateDatabase(AppDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DeleteDatabase(AppDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedIdentity(UserManager<Artist> userManager)
    {
        foreach (var userInfo in InitialData.Users)
        {
            var user = userManager.FindByEmailAsync(userInfo.email).Result;
            if (user == null)
            {
                user = new Artist()
                {
                    Id = (userInfo.id ?? Guid.NewGuid()).ToString(),
                    Email = userInfo.email,
                    UserName = userInfo.email,
                    DisplayName = userInfo.displayName,
                    ProfilePicture = null,
                    Bio = null,
                    JoinDate = DateTime.UtcNow,
                };
                var result = userManager.CreateAsync(user, userInfo.password).Result;
                
                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");
                }

                result = userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, user.DisplayName)).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException("GivenName claim adding failed!");
                }

                result = userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, "-")).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException("Surname claim adding failed!");
                }
            }
        }
    }
    
    private static void SeedArtistRoles(AppDbContext context)
    {
        if (context.ArtistRoles.Any()) return;

        foreach (var (name, id) in InitialData.ArtistRoles)
        {
            context.ArtistRoles.Add(new ArtistRole
            {
                Id = id ?? Guid.NewGuid(),
                Name = name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder"
            });
        }

        context.SaveChanges();
    }
    
    private static void SeedLinkTypes(AppDbContext context)
    {
        if (context.LinkTypes.Any()) return;

        foreach (var (name, id) in InitialData.LinkTypes)
        {
            context.LinkTypes.Add(new LinkType
            {
                Id = id ?? Guid.NewGuid(),
                Name = name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder"
            });
        }

        context.SaveChanges();
    }
    
    private static void SeedMoods(AppDbContext context)
    {
        if (context.Moods.Any()) return;

        foreach (var (name, id) in InitialData.Moods)
        {
            context.Moods.Add(new Mood
            {
                Id = id ?? Guid.NewGuid(),
                Name = name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder"
            });
        }

        context.SaveChanges();
    }
    
    private static void SeedTags(AppDbContext context)
    {
        if (context.Tags.Any()) return;

        foreach (var (name, id) in InitialData.Tags)
        {
            context.Tags.Add(new Tag
            {
                Id = id ?? Guid.NewGuid(),
                Name = name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Seeder"
            });
        }

        context.SaveChanges();
    }

}