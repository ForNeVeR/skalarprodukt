using System;

namespace skalarprodukt.Benchmark.CSharp
{
    public class CSharpOptimizedMatrix2D
    {
        public static CSharpOptimizedMatrix2D<T> Initialize<T>(int x, int y, Func<int, int, T> initializer)
        {
            var matrix = new CSharpOptimizedMatrix2D<T>(x, y);
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

    public class CSharpOptimizedMatrix2D<T>
    {
        private readonly int _rowLength;
        private readonly T[] _array;

        public CSharpOptimizedMatrix2D(int x, int y)
        {
            _rowLength = x;
            _array = new T[x * y];
        }

        public T this[int x, int y]
        {
            get { return _array[y * _rowLength + x]; }
            set { _array[y * _rowLength + x] = value; }
        }

        public int X => _rowLength;
        public int Y => _array.Length / _rowLength;

        public CSharpOptimizedMatrix2D<T> Map(Func<T, T> func)
        {
            int x = X, y = Y;
            var copy = new CSharpOptimizedMatrix2D<T>(x, y);
            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    copy[i, j] = func(this[i, j]);
                }
            }

            return copy;
        }

        public CSharpOptimizedMatrix2D<T> Map(Func<int, int, T, T> func)
        {
            int x = X, y = Y;
            var copy = new CSharpOptimizedMatrix2D<T>(x, y);
            for (int i = 0; i < x; ++i)
            {
                for (int j = 0; j < y; ++j)
                {
                    copy[i, j] = func(i, j, this[i, j]);
                }
            }

            return copy;
        }
    }
}
