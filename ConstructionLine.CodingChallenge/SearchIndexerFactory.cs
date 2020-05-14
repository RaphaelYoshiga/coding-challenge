using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public interface ISearchIndexerFactory
    {
        IShirtsIndexer Instantiate(List<Shirt> shirts);
    }

    public class SearchIndexerFactory : ISearchIndexerFactory
    {
        public IShirtsIndexer Instantiate(List<Shirt> shirts)
        {
            return new ShirtsIndexer(shirts);
        }
    }
}