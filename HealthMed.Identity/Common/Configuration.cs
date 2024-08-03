namespace HealthMed.Identity.Common;

public static class Configuration
{
    public static string ConnectionString { get; set; } = string.Empty;
    public static string FrontendUrl { get; set; } = string.Empty;
    public static string BackendUrl { get; set; } = string.Empty;
    public const string CorsPolicyName = "wasm";
}
