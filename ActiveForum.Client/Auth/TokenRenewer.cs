namespace ActiveForum.Client.Auth
{
using System;
using System.Timers;
using ActiveForum.Shared.Interfaces;
   
public class TokenRenewer : IDisposable
{
    private readonly ILoginService loginService;
    private Timer timer;

    public TokenRenewer(ILoginService loginService)
    {
        this.loginService = loginService;
    }

    public void Dispose()
    {
        timer?.Dispose();
    }

    public void Initiate()
    {
        timer = new Timer();
        timer.Interval = 1000 * 60 * 4; // 4 minutes
        timer.Elapsed += Timer_Elapsed;
        timer.Start();
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        Console.WriteLine("timer elapsed");
        loginService.TryRenewToken();
    }
}
}
