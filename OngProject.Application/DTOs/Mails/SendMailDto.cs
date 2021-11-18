namespace OngProject.Application.DTOs.Mails
{
    public class SendMailDto
    {
        public string Name { get; set; }
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}