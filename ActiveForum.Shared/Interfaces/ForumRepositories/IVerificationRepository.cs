namespace ActiveForum.Shared.Interfaces.ForumRepositories
{
    using System.Threading.Tasks;
    using ActiveForum.Shared.DTOs;

    public interface IVerificationRepository
    {
        Task Verify(VerifiedDTO verifiedDTO);
    }
}
