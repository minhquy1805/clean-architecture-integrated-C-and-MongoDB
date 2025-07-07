using Shared.Helpers;

namespace Application.DTOs.Abstract
{
    public class BasePagingFilterDto 
    {

        public int Start { get; set; } = 0;


        public int NumberOfRows { get; set; } = GridConfig.NumberOfRows;


        public string SortBy { get; set; } = "CreatedAt";


        public string SortDirection { get; set; } = "DESC";


        public int CurrentPage { get; set; } = 1;

    }
}
