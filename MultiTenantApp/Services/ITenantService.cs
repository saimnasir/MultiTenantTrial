﻿namespace MultiTenantApp.Services
{
    public interface ITenantService
    {
        string? Tenant { get; }
        void SetTenant(string? tenant);
    }
}
