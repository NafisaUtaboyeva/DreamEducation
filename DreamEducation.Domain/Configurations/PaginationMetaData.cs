using System;

namespace DreamEducation.Domain.Configurations
{
    public class PaginationMetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public PaginationMetaData(int totalCount, PaginationParams @params)
        {
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling((double)totalCount / @params.PageSize);
            CurrentPage = @params.PageIndex;
        }
    }
}
