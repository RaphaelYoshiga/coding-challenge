using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public interface IShirtsIndexer
    {
        List<Shirt> FilterBy(SearchOptions searchOptions);
    }

    public class ShirtsIndexer : IShirtsIndexer
    {
        private readonly ILookup<Color, Shirt> _colorLookup;
        private readonly ILookup<Size, Shirt> _sizeLookup;

        public ShirtsIndexer(List<Shirt> shirts)
        {
            _colorLookup = shirts.ToLookup(p => p.Color, p => p);
            _sizeLookup = shirts.ToLookup(p => p.Size, p => p);
        }

        public List<Shirt> FilterBy(SearchOptions searchOptions)
        {
            var colorFiltered = GetFilteredByColors(searchOptions);
            var sizeFiltered = GetFilteredBySize(searchOptions);
            
            return sizeFiltered.Intersect(colorFiltered).ToList();
        }

        private IEnumerable<Shirt> GetFilteredBySize(SearchOptions searchOptions)
        {
            return _sizeLookup
                .Where(p => !searchOptions.Sizes.Any() || searchOptions.Sizes.Contains(p.Key))
                .SelectMany(p => p.ToList());
        }

        private IEnumerable<Shirt> GetFilteredByColors(SearchOptions searchOptions)
        {
            return _colorLookup
                .Where(p => !searchOptions.Colors.Any() || searchOptions.Colors.Contains(p.Key))
                .SelectMany(p => p.ToList());

        }
    }
}