
namespace Auditor.WebApi.Models
{
    public abstract class PagingData
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
    }
}
