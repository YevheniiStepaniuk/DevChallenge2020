using System.Collections.Generic;
using Newtonsoft.Json;

namespace UniqueArticles.Domain.Models
{
    public class ArticleGroupsResponse
    {
        [JsonProperty("duplicate_groups")]
        public IEnumerable<IEnumerable<int>> DuplicateGroups { get; set; }
    }
}