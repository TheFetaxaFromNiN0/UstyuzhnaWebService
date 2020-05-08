namespace Ust.Api.Models.Request
{
    public class CreateAdsRequest
    {
        public string Title { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public string ContactName { get; set; }

        public string ContactPhone { get; set; }

        public string Email { get; set; }
    }
}
