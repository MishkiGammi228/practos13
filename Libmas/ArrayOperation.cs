using System;
using System.Data;
using System.IO;

namespace Libmas
{
    public class ArrayOperation
    {
        public static void SaveArray(int[,] array, string path)
        {
            using (StreamWriter save = new StreamWriter(path))
            {
                save.WriteLine(array.GetLength(0));
                save.WriteLine(array.GetLength(1));
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        save.WriteLine(array[i, j]);
                    }
                }
            }
        }

        public static void OpenArray(out int[,] array, string path)
        {
            using (StreamReader open = new StreamReader(path))
            {
                array = new int[Convert.ToInt32(open.ReadLine()), Convert.ToInt32(open.ReadLine())];
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        array[i, j] = Convert.ToInt32(open.ReadLine());
                    }
                }
            }
        }

        public static void ClearArray(int[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = 0;
                }
            }
        }

        public static void FillArray(int[,] array, int fillValue)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = fillValue;
                }
            }
        }

        public static void FillArrayRandom(int[,] array, int maxValue)
        {
            Random randomNumber = new Random();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = randomNumber.Next(maxValue);
                }
            }
        }
    }

    public static class VisualArray
    {
        //Метод для одномерного массива
        public static DataTable ToDataTable<T>(this T[] arr)
        {
            var res = new DataTable();
            for (int i = 0; i < arr.Length; i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }
            var row = res.NewRow();
            for (int i = 0; i < arr.Length; i++)
            {
                row[i] = arr[i];
            }
            res.Rows.Add(row);
            return res;
        }
        //Метод для двухмерного массива
        public static DataTable ToDataTable<T>(this T[,] matrix)
        {
            var res = new DataTable();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = res.NewRow();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }

                res.Rows.Add(row);
            }
            return res;
        }
    }
}