namespace ActiveForum.Shared.Interfaces.UserConnectionsInterfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using ActiveForum.Shared.Models.GroupModels;
    
    public interface IRelatedPlacesRepository
    {
        Task<int> AddPlace(RelatedPlace relatedPlace);

        Task<List<RelatedPlace>> GetRelatedPlaces(int groupId);

        Task DeletePlace(int placeId);     
    }
}
