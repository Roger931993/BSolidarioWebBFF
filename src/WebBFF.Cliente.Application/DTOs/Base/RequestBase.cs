using WebBFF.Cliente.Application.Interfaces.Base;

namespace WebBFF.Cliente.Application.DTOs.Base
{
    public class RequestBase<T> : IRequestBase
    {
        public T? Request { get; set; }
        public Guid? IdTraking { get; set; }
        public InfoSessionDto? InfoSession { get; set; }
        public PaginationQuery? Pagination { get; set; }
    }


    public class PaginationQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
