using AutoSync.Core.Authorization;
using AutoSync.Core.Job;
using Microsoft.EntityFrameworkCore;
using System;

namespace AutoSync.EFC
{
    public class AutoSyncDbContext : DbContext
    {
        public AutoSyncDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "superadmin", Password = "superadmin@123", IsActive = true },
                new User { Id = 2, Username = "supervisor", Password = "supervisor@123", IsActive = true },
                new User { Id = 3, Username = "Kishore", Password = "kishore@123", IsActive = true },
                new User { Id = 4, Username = "Satheesh", Password = "satheesh@123", IsActive = true }
            );

            modelBuilder.Entity<Roles>().HasData(
                new Roles {Id = 1, RoleName = "SuperAdmin" },
                new Roles { Id = 2, RoleName = "Admin" },
                new Roles { Id = 3, RoleName = "Supervisor" },
                new Roles { Id = 4, RoleName = "Quality Checker" },
                new Roles { Id = 5, RoleName = "Technician" }
            );

            modelBuilder.Entity<UserRoles>().HasData(
                new UserRoles {Id = 1, UserId = 1, RoleId = 1 },
                new UserRoles { Id = 2, UserId = 2, RoleId = 3 },
                new UserRoles { Id = 3, UserId = 3, RoleId = 5 },
                new UserRoles { Id = 4, UserId = 4, RoleId = 5 }
            );

            modelBuilder.Entity<Setting>().HasData(
                new Setting { Id = 1, UserId = 3, SupervisorId = 2, FolderFilePath = @"D:\Kishore\PrimeMover", AutoSyncTime = "17:30", AutoSyncDays = "Sunday,Monday,Tuesday", AutoDeleteInterval = 15 },
                new Setting { Id = 2, UserId = 4, SupervisorId = 2, FolderFilePath = @"D:\Satheesh\PrimeMover", AutoSyncTime = "17:30", AutoSyncDays = "Sunday,Monday,Tuesday", AutoDeleteInterval = 15 }
            );


            modelBuilder.Entity<Job>();
            modelBuilder.Entity<JobDetail>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
