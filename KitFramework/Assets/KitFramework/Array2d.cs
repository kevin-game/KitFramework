using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System;
using UnityEngine;

namespace KitFramework
{
    public class Array2d<T>
    {
        private T[,] _array;

        public int Width { get; }
        public int Height { get; }

        public Array2d(int width, int height)
        {
            Width = width;
            Height = height;
            _array = new T[width, height];
        }

        public bool IsIndexValid(int index, bool isInWidth)
        {
            if (isInWidth)
                return index >= 0 && index < Width;
            else
                return index >= 0 && index < Height;
        }

        public bool IsIndexValidInWidth(int index) => IsIndexValid(index, true);

        public bool IsIndexValidInHeight(int index) => IsIndexValid(index, false);

        public bool TryGet(int x, int y, out T? obj)
        {
            if (IsIndexValidInWidth(x) && IsIndexValidInHeight(y))
            {
                obj = _array[x, y];
                return true;
            }

            obj = default(T);
            return false;
        }

        public T? this[int x, int y]
        {
            set
            {
                _array[x, y] = value;
            }
            get
            {
                return _array[x, y];
            }
        }

        public void ForEach(Func<int, int, T?> func)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    _array[x, y] = func(x, y);
                }
            }
        }

        public void ForEach(Action<int, int, T?> action)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    action(x, y, _array[x, y]);
                }
            }
        }
    }
}