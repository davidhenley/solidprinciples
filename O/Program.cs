using System;
using System.Collections.Generic;

namespace O
{
    class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = { apple, tree, house };

            var bf = new BetterFilter();

            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                System.Console.WriteLine(p.name + " is green");
            }

            foreach (var p in bf.Filter(products, new SizeSpecification(Size.Large)))
            {
                System.Console.WriteLine(p.name + " is large");
            }

            foreach (var p in bf.Filter(products, new AndSpecification<Product>(new SizeSpecification(Size.Large), new ColorSpecification(Color.Blue))))
            {
                System.Console.WriteLine(p.name + " is large and blue");
            }
        }
    }

    public enum Color
    {
        Red, Green, Blue
    }

    public enum Size
    {
        Small, Medium, Large, Huge
    }

    public class Product
    {
        public string name;
        public Color color;
        public Size size;

        public Product(string name, Color color, Size size)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            this.name = name;
            this.color = color;
            this.size = size;
        }
    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T item);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product item)
        {
            return item.color == color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product item)
        {
            return item.size == size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            this.first = first;
            this.second = second;
        }

        public bool IsSatisfied(T item)
        {
            return first.IsSatisfied(item) && second.IsSatisfied(item);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i)) yield return i;
        }
    }
}
