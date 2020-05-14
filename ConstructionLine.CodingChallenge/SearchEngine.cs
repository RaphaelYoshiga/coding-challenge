using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly IShirtsIndexer _shirtsIndexer;
        private readonly ISearchSummarizer _searchSummarizer;

        public SearchEngine(List<Shirt> shirts) : this(shirts, new SearchIndexerFactory(), new SearchSummarizer())
        {
            // Kept for backwards compatibility
        }

        public SearchEngine(List<Shirt> shirts, ISearchIndexerFactory searchIndexerFactory, ISearchSummarizer searchSummarizer)
        {
            _searchSummarizer = searchSummarizer;
            _shirtsIndexer = searchIndexerFactory.Instantiate(shirts);
        }

        public SearchResults Search(SearchOptions options)
        {
            var shirts = _shirtsIndexer.FilterBy(options);
            return _searchSummarizer.Summarize(shirts);
        }
    }


}