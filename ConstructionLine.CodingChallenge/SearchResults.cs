using System;
using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class SearchResults
    {
        public List<Shirt> Shirts { get; set; }


        public List<SizeCount> SizeCounts { get; set; }


        public List<ColorCount> ColorCounts { get; set; }
    }


    public class SizeCount
    {
        public SizeCount()
        {
        }

        public SizeCount(Size size, int count)
        {
            Size = size;
            Count = count;
        }

        public Size Size { get; set; }

        public int Count { get; set; }

        protected bool Equals(SizeCount other)
        {
            return Equals(Size, other.Size) && Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SizeCount) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Size, Count);
        }
    }


    public class ColorCount
    {
        public ColorCount()
        {
        }

        public ColorCount( Color color, int count)
        {
            Color = color;
            Count = count;
        }

        public Color Color { get; set; }

        public int Count { get; set; }

        protected bool Equals(ColorCount other)
        {
            return Equals(Color, other.Color) && Count == other.Count;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ColorCount) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Color, Count);
        }
    }
}