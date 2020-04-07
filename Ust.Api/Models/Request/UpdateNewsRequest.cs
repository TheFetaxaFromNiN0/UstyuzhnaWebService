namespace Ust.Api.Models.Request
{
    public class UpdateNewsRequest
    {
        public string Title { get; set; }

        public string Text { get; set; }

        public int NewsType { get; set; }
    }
}
