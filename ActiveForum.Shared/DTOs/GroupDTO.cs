namespace ActiveForum.Shared.DTOs
{
    using System.Collections.Generic;
    using ActiveForum.Shared.Models.GroupModels;

    public class GroupDTO
    {
       public PrivateGroup PrivateGroup { get; set; }
       
       public string OwnerName { get; set; }
    }
}
