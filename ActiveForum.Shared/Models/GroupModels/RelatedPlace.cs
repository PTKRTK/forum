namespace ActiveForum.Shared.Models.GroupModels
{
    public class RelatedPlace
    {
        public int Id { get; set; }

        public int PrivateGroupId { get; set; }

        public string PlaceName { get; set; }

        public string PlaceType { get; set; }
    }
}
