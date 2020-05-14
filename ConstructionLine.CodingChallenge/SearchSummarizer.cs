using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public interface ISearchSummarizer
    {
        SearchResults Summarize(List<Shirt> shirts);
    }
    public class SearchSummarizer : ISearchSummarizer
    {
        public SearchResults Summarize(List<Shirt> shirts)
        {
            return new SearchResults
            {
                Shirts = shirts,
                ColorCounts = GetColorCounts(shirts),
                SizeCounts = GetSizeCounts(shirts)
            };
        }

        private static List<ColorCount> GetColorCounts(List<Shirt> shirts)
        {
            var allColorCounts = Color.All.Select(p => new ColorCount(p, 0)).ToList();
            foreach (var group in shirts.GroupBy(p => p.Color, p => p))
            {
                var coloCount = allColorCounts.Single(p => p.Color.Equals(group.Key));
                coloCount.Count = group.Count();
            }
            return allColorCounts;

        }

        private static List<SizeCount> GetSizeCounts(List<Shirt> shirts)
        {
            var allSizeCounts = Size.All.Select(p => new SizeCount(p, 0)).ToList();
            foreach (var group in shirts.GroupBy(p => p.Size, p => p))
            {
                var sizeCount = allSizeCounts.Single(p => p.Size.Equals(group.Key));
                sizeCount.Count = group.Count();
            }
            return allSizeCounts;
        }
    }
}