using System;
using System.Collections.Generic;
using Shouldly;
using Xunit;

namespace ConstructionLine.CodingChallenge.Tests
{
    public class ShirtsIndexerShould
    {
        private readonly ShirtsIndexer _shirtsIndex;
        private readonly List<Shirt> _shirts;

        private readonly Shirt _smallRedShirt = new Shirt(Guid.Empty, "Small Red Shirt", Size.Small, Color.Red);
        private readonly Shirt _smallBlueShirt = new Shirt(Guid.Empty, "Small Blue Shirt", Size.Small, Color.Blue);
        private readonly Shirt _mediumRedShirt = new Shirt(Guid.Empty, "Medium Red Shirt", Size.Medium, Color.Red);
        private readonly Shirt _mediumBlueShirt = new Shirt(Guid.Empty, "Medium Blue Shirt", Size.Medium, Color.Blue);

        public ShirtsIndexerShould()
        {
            _shirts = new List<Shirt>
            {
                _smallRedShirt,
                _smallBlueShirt,
                _mediumRedShirt,
                _mediumBlueShirt
            };
            _shirtsIndex = new ShirtsIndexer(_shirts);
        }

        [Fact]
        public void GiveAllShirtsIfAllFiltersSelected()
        {
            var filteredShirts = _shirtsIndex.FilterBy(new SearchOptions()
            {
                Colors = Color.All,
                Sizes = Size.All
            });

            UnorderedAssert(filteredShirts, _shirts);
        }

        [Fact]
        public void GiveAllShirtsIfNoFiltersSelected()
        {
            var filteredShirts = _shirtsIndex.FilterBy(new SearchOptions());

            UnorderedAssert(filteredShirts, _shirts);
        }


        private void UnorderedAssert(List<Shirt> filteredShirts, List<Shirt> expectedShirts)
        {
            filteredShirts.Count.ShouldBe(expectedShirts.Count);

            foreach (var expectedShirt in expectedShirts)
            {
                filteredShirts.ShouldContain(expectedShirt);
            }
        }

        [Fact]
        public void FilterShirtsByColor()
        {
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Red }
            };

            var filteredShirts = _shirtsIndex.FilterBy(searchOptions);

            var expected = new List<Shirt> { _smallRedShirt, _mediumRedShirt };
            UnorderedAssert(filteredShirts, expected);
        }

        [Fact]
        public void FilterShirtsByBlue()
        {
            var searchOptions = new SearchOptions
            {
                Colors = new List<Color> { Color.Blue }
            };

            var filteredShirts = _shirtsIndex.FilterBy(searchOptions);

            var expected = new List<Shirt> { _smallBlueShirt, _mediumBlueShirt };
            UnorderedAssert(filteredShirts, expected);
        }

        [Fact]
        public void FilterShirtsBySmall()
        {
            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size>
                {
                    Size.Small
                }
            };

            var filteredShirts = _shirtsIndex.FilterBy(searchOptions);

            var expected = new List<Shirt> { _smallBlueShirt, _smallRedShirt };
            UnorderedAssert(filteredShirts, expected);
        }

        [Fact]
        public void FilterShirtsBySmallBlue()
        {
            var searchOptions = new SearchOptions
            {
                Sizes = new List<Size>
                {
                    Size.Small
                },
                Colors = new List<Color>
                {
                    Color.Blue
                }
            };

            var filteredShirts = _shirtsIndex.FilterBy(searchOptions);

            var expected = new List<Shirt> { _smallBlueShirt };
            UnorderedAssert(filteredShirts, expected);
        }
    }
}
