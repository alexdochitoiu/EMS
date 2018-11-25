namespace WebAPI.Infrastructure.Email.Models
{
    public class EmailDetails
    {
        public string FromName { get; set; }
        public string FromEmail { get; set; }
        public string ToName { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public bool IsHtml { get; set; }
        public string HtmlTitle { get; set; }
        public string HtmlContent1 { get; set; }
        public string HtmlContent2 { get; set; }
        public string HtmlButtonText { get; set; }
        public string HtmlButtonUrl { get; set; }
        public string HtmlFacebookLink { get; set; }
        public string HtmlTwitterLink { get; set; }
        public string HtmlYoutubeLink { get; set; }
    }
}
