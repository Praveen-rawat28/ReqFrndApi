namespace ReqFrndApi.Models
{
    public class ResponseModel
    {
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object Data { get; set; }
        public DateTime DateTime { get; set; }


    }
}
