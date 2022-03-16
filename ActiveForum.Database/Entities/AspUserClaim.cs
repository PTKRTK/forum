namespace ActiveForum.Database.Entities
{
    public class AspUserClaim
    {
        public int Id { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public string UserId { get; set; }
    }
}
