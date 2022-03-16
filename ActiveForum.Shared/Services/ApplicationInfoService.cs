namespace ActiveForum.Shared.Services
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using ActiveForum.Shared.Interfaces;

    public class ApplicationInfoService<T> : IApplicationInfoService
    {
        private static readonly string ApplicationId;
        private static readonly Version ApplicationVersion;
        private static readonly string ApplicationGitHash;
        private static readonly bool HasDebugRuntime;

        static ApplicationInfoService()
        {
            ApplicationInfoService<T>.ApplicationId = typeof(T).Namespace;

            ApplicationInfoService<T>.ApplicationVersion =
                ApplicationInfoService<T>.GetGitVersion("version.txt", out ApplicationInfoService<T>.ApplicationGitHash)
                ?? ApplicationInfoService<T>.GetAssemblyVersion();

            ApplicationInfoService<T>.HasDebugRuntime = typeof(T).Assembly
                .GetCustomAttributes(false)
                .OfType<DebuggableAttribute>()
                .Where(x => x.IsJITTrackingEnabled)
                .Any();
        }

        public static string Id => ApplicationInfoService<T>.ApplicationId;

        public static Version Version => ApplicationInfoService<T>.ApplicationVersion;

        public static string GitHash => ApplicationInfoService<T>.ApplicationGitHash;

        public static bool HasGitVersion => ApplicationInfoService<T>.ApplicationGitHash != null;

        public static bool Debug => ApplicationInfoService<T>.HasDebugRuntime;
                
        public string GetId()
        {
            return ApplicationInfoService<T>.Id;
        }

        public Version GetVersion()
        {
            return ApplicationInfoService<T>.Version;
        }

        public bool IsGitVersion()
        {
            return ApplicationInfoService<T>.HasGitVersion;
        }

        public string GetGitHash()
        {
            return ApplicationInfoService<T>.ApplicationGitHash;
        }

        public bool IsDebug()
        {
            return ApplicationInfoService<T>.Debug;
        }

        private static Version GetGitVersion(string resourceName, out string gitHash)
        {
            Version version = null;
            string gitHashString = null;
            gitHash = gitHashString;

            Assembly assembly = Assembly.GetAssembly(typeof(ApplicationInfoService<T>));
            string versionResource = assembly.GetManifestResourceNames()
                .Where(x => x.ToLower().Contains(resourceName))
                .FirstOrDefault();

            if (versionResource == null)
            {
                return null;
            }

            try
            {
                string gitVersion = null;
                using (Stream stream = assembly.GetManifestResourceStream(versionResource))
                using (StreamReader reader = new StreamReader(stream))
                {
                    gitVersion = reader.ReadToEnd()
                        ?.Trim()
                        .ToLower();

                    gitVersion = ApplicationInfoService<T>.RemovePrefix(gitVersion, "v");
                    if (string.IsNullOrWhiteSpace(gitVersion))
                    {
                        return null;
                    }
                }

                StringSplitOptions stringSplitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;       
                string[] tokens = gitVersion.Split(".", stringSplitOptions);
                if (tokens.Any() == false)
                {
                    return null;
                }

                string major = tokens.Skip(0).Take(1).FirstOrDefault() ?? "0";
                string minor = tokens.Skip(1).Take(1).FirstOrDefault() ?? "0";
                string other = tokens.Skip(2).Take(1).FirstOrDefault() ?? "0";
                string build = other?.Split("-", stringSplitOptions).Skip(0).Take(1).FirstOrDefault() ?? "0";
                string revision = other?.Split("-", stringSplitOptions).Skip(1).Take(1).FirstOrDefault() ?? "0";
                string versionString = $"{major}.{minor}.{build}.{revision}";

                gitHashString = other?.Split("-", stringSplitOptions).Skip(2).Take(1).FirstOrDefault() ?? string.Empty;
                version = new Version(versionString);
            }
            catch
            {
                return null;
            }

            gitHash = gitHashString;
            return version;
        }

        private static Version GetAssemblyVersion()
        {
            var version = typeof(T).Assembly.GetName().Version;

            return version;
        }

        private static string RemovePrefix(string inst, string prefix, bool isCaseSensitive = true)
        {
            string text = inst;
            if (isCaseSensitive == false)
            {
                text = text.ToLower();
                prefix = prefix.ToLower();
            }

            if (text.StartsWith(prefix))
            {
                return inst.Substring(prefix.Length, inst.Length - prefix.Length);
            }

            return inst;
        }
    }
}
