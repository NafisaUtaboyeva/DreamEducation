namespace DreamEducation.Domain.Configurations
{
    public class PaginationParams
    {
        public const int maxSize = 50;
        public int pageSize;
        public int PageSize { get => pageSize; set => PageSize = value > maxSize ? maxSize : value; }
        public int PageIndex { get; set; }
    }
}
