using LinqToTwitter;

namespace ReqFrndApi.Models
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public string Action { get; set; }
        public DateTime  DateTime { get; set; }
        
        //public int MutualFriendId { get; set; } // ID of the mutual friend (if applicable)

         
       

    }
}
