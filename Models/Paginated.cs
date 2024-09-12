using metafar_challenge.DTOs;

namespace metafar_challenge.Models
{
    public class PaginatedResult<T>
    {
        public List<T> Data { get; set; }
        public int TotalCount { get; set; }
    }

    public class PaginatedOperations
    {
        public List<HistoricDto> Operations { get; set; }
        public int Page { get; set; }
    }
}
