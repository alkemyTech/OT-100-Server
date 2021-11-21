namespace OngProject.Application.DTOs.News
{
    public class NewsQueryDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}