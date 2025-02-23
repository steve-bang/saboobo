
using Microsoft.AspNetCore.Http.HttpResults;
using SaBooBo.Domain.Shared.ExceptionHandler;

namespace Domain.Shared
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public const int MinPageNumber = 1;
        public const int MaxPageSize = 100;
        public const int DefaultPageNumber = 0;
        public const int DefaultPageSize = 10;


        public PaginationFilter()
        {
            PageNumber = DefaultPageNumber;
            PageSize = DefaultPageSize;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            if (pageNumber < MinPageNumber)
            {
                throw new BadRequestException(
                    "Page_Number_Invalid",
                    "Page number must be greater than or equal to 1",
                    "The page number must be greater than or equal to 1"
                );
            }

            if (pageSize > MaxPageSize)
            {
                throw new BadRequestException(
                    "Page_Size_Invalid",
                    "Page size must be less than or equal to 100",
                    "The page size must be less than or equal to 100"
                );
            }

            PageNumber = pageNumber;
            PageSize = pageSize;

        }
    }
}