using Core.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Common
{
    public class PagedQuerryFilter<T> where T : IFilter
    {
        public int Page { get; set; }
        public int PageCount { get; set; }
        public T FIlter { get; set; }

        public string? SortField { get; set; }
        public bool SortDescending { get; set; }

        [JsonIgnore]
        public string? Sort => $"{(string.IsNullOrWhiteSpace(SortField) ? "Id" : SortField)} {(SortDescending ? "desc" : "asc")}";

        [JsonIgnore]
        public string? Paging => (Page > 0 && PageCount > 0) ? $"offset {(Page - 1) * PageCount} rows fetch next {PageCount} rows only" : null;
    }
}
