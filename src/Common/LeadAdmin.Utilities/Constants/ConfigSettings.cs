namespace LeadAdmin.Utilities.Constants
{
    public sealed class AppSettings
    {
        public DataSection Data { get; set; }
        public ConfigSettingsSection ConfigSettings { get; set; }
        public StorageSettingsSection AzureBlobSettings { get; set; }
        public SmtpSection DefaultSmtpSettings { get; set; }
    }
    
    public class SmtpSection
    {
        public string FromAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string Smtp { get; set; }
    }

    public class DataSection
    {
        public string ERPConnectionString { get; set; }
        public int CommandTimeout { get; set; }
    }

    public class ConfigSettingsSection
    {
        public bool IsAzureBlob { get; set; }
        public string GoogleAuthSecretKey { get; set; }
        public bool IsDevelopment { get; set; }
        public string Auth_ValidIssuer { get; set; }
        public string Auth_ValidAudience { get; set; }
        public string Auth_IssuerSigningKey { get; set; }
        public string BootstrapUrl { get; set; }
        public string FontAwesome { get; set; }
        public int TokenExpiryInMinutes { get; set; } = 180;
        public short DefaultTimezoneOffset { get; set; } = 330;
    }

    public class StorageSettingsSection
    {
        public string LogsConnectionString { get; set; }
    }

    public class ApplicationInsightsSection
    {
        public string InstrumentationKey { get; set; }
    }

    public sealed class ConfigSettings
    {
        public static AppSettings Instance { get; set; }
    }

}
