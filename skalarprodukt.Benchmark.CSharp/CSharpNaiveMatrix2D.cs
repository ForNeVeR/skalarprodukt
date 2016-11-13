using System;

namespace skalarprodukt.Benchmark.CSharp
{
    public class CSharpNaiveMatrix2D
    {
        public static CSharpNaiveMatrix2D<T> Initialize<T>(int x, int y, Func<int, int, T> initializer)
        {
            var matrix = new CSharpNaiveMatrix2D<T>(x, y);
            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    matrix[i, j] = initializer(i, j);
                }
            }

            return matrix;
        }
    }

    public class CSharpNaiveMatrix2D<T>
    {
        private readonly T[,] _array;

        private CSharpNaiveMatrix2D(T[,] data)
        {
            _array = data;
        }

        public CSharpNaiveMatrix2D(int x, int y) : this(new T[x, y])
        {
        }

        public T this[int x, int y]
        {
            get { return _array[x, y]; }
            set { _array[x, y] = value; }
        }

        public int X => _array.GetLength(0);
        public int Y => _array.GetLength(1);

        public CSharpNaiveMatrix2D<T> Map(Func<T, T> func)
        {
            int x = X, y = Y;
            var clone = new T[x, y];
            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    clone[i, j] = func(_array[i, j]);
                }
            }

            return new CSharpNaiveMatrix2D<T>(clone);
        }

        public CSharpNaiveMatrix2D<T> Map(Func<int, int, T, T> func)
        {
            int x = X, y = Y;
            var clone = new T[x, y];
            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    clone[i, j] = func(i, j, _array[i, j]);
                }
            }

            return new CSharpNaiveMatrix2D<T>(clone);
        }
    }
}
