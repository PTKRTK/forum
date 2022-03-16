namespace ActiveForum.Shared.Interfaces
{
    using System;

    public interface IApplicationInfoService
    {
        string GetId();

        Version GetVersion();
        
        bool IsGitVersion();

        string GetGitHash();

        bool IsDebug();
    }
}
