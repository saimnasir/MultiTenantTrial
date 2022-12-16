using MultiTenantApp.Enums;
using Patika.Framework.Shared.Extensions;

namespace MultiTenantApp.Consts
{
    public static class Tenants
    {
        public const string TK1 = nameof(TK1);
        public const string TK2 = nameof(TK2);
        public static IReadOnlyCollection<string> All = new[] { TK1, TK2 };
        public static string? Find(string? value)
        {
            return All.FirstOrDefault(t => t.Equals(value?.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }
    //public static class Tenants
    //{
    //    public static IList<TenantEnums> All = EnumHelper<TenantEnums>.ToList();
    //    public static string? Find(string? value)
    //    {
    //        var parsed = EnumHelper < TenantEnums >.Parse(value?? "");
    //        return All.FirstOrDefault(t => t == parsed).ToString();
    //    }
    //}
}
