using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shouldly;
using Xunit;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class SearchSummarizerShould
    {
        private readonly Shirt _smallRedShirt = new Shirt(Guid.Empty, "Small Red Shirt", Size.Small, Color.Red);
        private readonly Shirt _mediumBlueShirt = new Shirt(Guid.Empty, "Medium Blue Shirt", Size.Medium, Color.Blue);

        private readonly SearchSummarizer _summarizer;

        public SearchSummarizerShould()
        {
            _summarizer = new SearchSummarizer();
        }

        [Fact]
        public void ForwardShirtsToResults()
        {
            var shirts = new List<Shirt>
            {
                _smallRedShirt,
                _smallRedShirt,
                _smallRedShirt
            };

            var searchResults = _summarizer.Summarize(shirts);

            searchResults.Shirts.ShouldBe(shirts);
        }

        [Fact]
        public void SummarizeSearchResultsByColor()
        {
            var shirts = new List<Shirt>
            {
                _smallRedShirt,
                _smallRedShirt,
                _smallRedShirt,
                _mediumBlueShirt
            };

            var searchResults = _summarizer.Summarize(shirts);

            var expectedColorCounts = new List<ColorCount>()
            {
                new ColorCount(Color.Red, 3),
                new ColorCount(Color.Blue, 1),
                new ColorCount(Color.Black, 0),
                new ColorCount(Color.White, 0),
                new ColorCount(Color.Yellow, 0),
            };
            AssertColorCounts(expectedColorCounts, searchResults.ColorCounts);
        }
        private static void AssertColorCounts(List<ColorCount> expectedColorCounts, List<ColorCount> actualCounts)
        {
            actualCounts.Count.ShouldBe(expectedColorCounts.Count);
            foreach (var expected in expectedColorCounts)
            {
                var actual = actualCounts.Single(p => p.Color.Equals(expected.Color));
                actual.Count.ShouldBe(expected.Count);
            }
        }

        [Fact]
        public void SummarizeSearchResultsBySize()
        {
            var shirts = new List<Shirt>
            {
                _mediumBlueShirt,
                _smallRedShirt,
            };

            var searchResults = _summarizer.Summarize(shirts);

            var expectedSizeCounts = new List<SizeCount>
            {
                new SizeCount(Size.Small, 1),
                new SizeCount(Size.Medium, 1),
                new SizeCount(Size.Large, 0)
            };
            AssertSizeCounts(expectedSizeCounts, searchResults.SizeCounts);
        }

        private static void AssertSizeCounts(List<SizeCount> expectedSizeCounts, List<SizeCount> actualCounts)
        {
            actualCounts.Count.ShouldBe(expectedSizeCounts.Count);
            foreach (var expected in expectedSizeCounts)
            {
                var actual = actualCounts.Single(p => p.Size.Equals(expected.Size));
                actual.Count.ShouldBe(expected.Count);
            }
        }
    }
}
