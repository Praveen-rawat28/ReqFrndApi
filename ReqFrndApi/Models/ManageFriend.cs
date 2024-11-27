namespace ReqFrndApi.Models
{
    public class ManageFriend
    {
        public int SenderId { get; set; }
        public int RecevierId { get; set; }
        public DateTime DateTime { get; set; }
        public string Action { get; set; }

    }
}
