using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Shouldly;
using Xunit;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SearchEngineShould
    {
        private readonly SearchEngine _searchEngine;
        private readonly List<Shirt> _shirts;
        private readonly Mock<ISearchIndexerFactory> _searchIndexerFactory;
        private readonly Mock<IShirtsIndexer> _searchIndexerMock;
        private readonly Mock<ISearchSummarizer> _searchSummarizer;

        public SearchEngineShould()
        {
            _shirts = new List<Shirt>() { new Shirt(Guid.NewGuid(), "", Size.Small, Color.Blue) };

            _searchIndexerFactory = new Mock<ISearchIndexerFactory>();
            _searchIndexerMock = new Mock<IShirtsIndexer>();
            _searchSummarizer = new Mock<ISearchSummarizer>();

            _searchIndexerFactory.Setup(p => p.Instantiate(_shirts))
                .Returns(_searchIndexerMock.Object);

            _searchEngine = new SearchEngine(_shirts, _searchIndexerFactory.Object, _searchSummarizer.Object);
        }

        [Fact]
        public void OrchestrateTheSearchFlow()
        {
            var searchOptions = new SearchOptions();
            var expectedShirts = new List<Shirt> { new Shirt(Guid.Empty, "", Size.Small, Color.Red) };
            var expectedSearchResults = new SearchResults();
            _searchIndexerMock.Setup(p => p.FilterBy(searchOptions))
                .Returns(expectedShirts);
            _searchSummarizer.Setup(p => p.Summarize(expectedShirts))
                .Returns(expectedSearchResults);

            var searchResults = _searchEngine.Search(searchOptions);

            searchResults.ShouldBe(expectedSearchResults);
        }
    }
}
