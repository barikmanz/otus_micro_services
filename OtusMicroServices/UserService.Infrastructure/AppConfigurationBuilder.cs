using Microsoft.Extensions.Configuration;

namespace UserService.Infrastructure
{
    public static class AppConfigurationBuilder
    {
        public static IConfiguration Build()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{GetEnvironmentVariable()}.json", optional: true)
                .AddJsonFile($"appsettings.secrets.{GetEnvironmentVariable()}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }
        
        public static IConfiguration BuildWithoutEnv()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();
        }

        private static string GetEnvironmentVariable()
        {
            const string webEnv = "ASPNETCORE_ENVIRONMENT";
            const string bsEnv = "BACKGROUNDSERVICES_ENVIRONMENT";

            var env = Environment.GetEnvironmentVariable(webEnv)
                ?? Environment.GetEnvironmentVariable(bsEnv, EnvironmentVariableTarget.Machine);

            if (string.IsNullOrWhiteSpace(env))
                throw new ApplicationException($"EnvironmentVariables: {webEnv}, {bsEnv} undefined.");

            return env;
        }
        
        public static TChild GetChildOptions<TOptions, TChild>() 
            where TOptions : class
            where TChild : class
        {
            var section = GetSection<TOptions>();
            return section.GetSection(typeof(TChild).Name).Get<TChild>();
        } 

        public static IConfigurationSection GetSection<TOptions>() where TOptions: class
        {
            var builder = Build();
            return builder.GetSection(typeof(TOptions).Name);
        }
    }
}
