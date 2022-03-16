namespace ActiveForum.Shared.Interfaces.ForumRepositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ActiveForum.Shared.Models.ForumModels;

    public interface ISportsMenagmentRepository
    {
        Task CreateSport(Sport sport);

        Task DeleteSport(int id);

        Task EditSport(Sport sport);
    }
}
