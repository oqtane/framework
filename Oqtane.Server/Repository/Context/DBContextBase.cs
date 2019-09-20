﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Oqtane.Models;
using System;
using System.Linq;

namespace Oqtane.Repository
{
    public class DBContextBase :  IdentityUserContext<IdentityUser> 
    {
        private Tenant tenant;
        private IHttpContextAccessor accessor;

        public DBContextBase(ITenantResolver TenantResolver, IHttpContextAccessor accessor)
        {
            tenant = TenantResolver.GetTenant();
            this.accessor = accessor;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(tenant.DBConnectionString
                    .Replace("|DataDirectory|", AppDomain.CurrentDomain.GetData("DataDirectory").ToString())
            );
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (tenant.DBSchema != "")
            {
                modelBuilder.HasDefaultSchema(tenant.DBSchema);
            }
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            string username = "";
            if (accessor.HttpContext.User.Identity.Name != null)
            {
                username = accessor.HttpContext.User.Identity.Name;
            }
            DateTime date = DateTime.Now;

            var created = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added);

            foreach(var item in created)
            {
                if (item.Entity is IAuditable entity)
                {
                    item.CurrentValues[nameof(IAuditable.CreatedBy)] = username;
                    item.CurrentValues[nameof(IAuditable.CreatedOn)] = date;
                }
            }

            var modified = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified || x.State == EntityState.Added);

            foreach (var item in modified)
            {
                if (item.Entity is IAuditable entity)
                {
                    item.CurrentValues[nameof(IAuditable.ModifiedBy)] = username;
                    item.CurrentValues[nameof(IAuditable.ModifiedOn)] = date;
                }

                if (item.Entity is ISoftDeletable softDeleted && item.State != EntityState.Added)
                {
                    if ((bool)item.CurrentValues[nameof(ISoftDeletable.IsSoftDeleted)]
                        && !item.GetDatabaseValues().GetValue<bool>(nameof(ISoftDeletable.IsSoftDeleted)))
                    {
                        item.CurrentValues[nameof(ISoftDeletable.DeletedBy)] = username;
                        item.CurrentValues[nameof(ISoftDeletable.DeletedOn)] = date;
                    }
                    else if (!(bool)item.CurrentValues[nameof(ISoftDeletable.IsSoftDeleted)]
                        && item.GetDatabaseValues().GetValue<bool>(nameof(ISoftDeletable.IsSoftDeleted)))
                    {
                        item.CurrentValues[nameof(ISoftDeletable.DeletedBy)] = null;
                        item.CurrentValues[nameof(ISoftDeletable.DeletedOn)] = null;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
