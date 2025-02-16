using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Дейкстер
{
    /// <summary>
    /// Класс для реализации алгоритма Дейкстры
    /// </summary>
    public class ShortestPathFinder
    {

        /// <summary>
        /// Функция для вывода матрицы
        /// </summary>
        /// <param name="Matrix"> сама матрица </param>
        public static void MatrixPrint(int[,] Matrix)
        {
            Console.WriteLine("Ваша исходная матрица : ");
            Console.WriteLine();
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < Matrix.GetLength(1); j++)
                {
                    Console.Write("{0,3}", Matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Функция для нахождения минимальной дистанции
        /// </summary>
        /// <param name="dist"> содержит расстояние между вершинами </param>
        /// <param name="sptSet"> служит для отмеки посещенных вершин </param>
        /// <param name="MatrixSize"> размер матрицы </param>
        public static int MinDistance(int[] dist, bool[] sptSet, int MatrixSize) // Находит ближайщую непосещенную вершину к тек. узлу
        {
            var min = int.MaxValue;
            var minIndex = -1;

            for (int i = 0; i < MatrixSize; i++)
            {
                if (sptSet[i] == false && dist[i] <= min)
                {
                    min = dist[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }

        /// <summary>
        /// Функция, вызывающая сам метод
        /// </summary>
        /// <param name="matrix"> содержит расстояние между вершинами </param>
        /// <param name="MatrixSize"> размер матрицы </param> 
        public static int[] Dijkstra(int[,] matrix, int MatrixSize)
        {
            var dist = new int[MatrixSize]; // массив дистанций 
            var path = new int[MatrixSize];

            var checkPoint = new bool[MatrixSize];// массив посещенных вершин

            for (int i = 0; i < MatrixSize; i++) // заполняем масив кратчайших путей и посещенных точек
            {
                dist[i] = int.MaxValue;
                checkPoint[i] = false;
            }

            dist[0] = 0; //  устанавливает начальную вершину

            for (int i = 0; i < MatrixSize - 1; i++) // находим кратчайшие пути до всех остальных вершин в графе
            {

                var minDist = MinDistance(dist, checkPoint, MatrixSize);

                checkPoint[minDist] = true;

                for (int j = 0; j < MatrixSize; j++)

                    if (!checkPoint[j] && matrix[minDist, j] != 0 && dist[minDist] != int.MaxValue && dist[minDist] + matrix[minDist, j] < dist[j]) // условие для обновления расстояния при нахождении кротчайшего
                        dist[j] = dist[minDist] + matrix[minDist, j];
            }

            Console.WriteLine("Данные о путях:");
            Console.WriteLine();
            Console.WriteLine($"Наша начальная точка 1");

            for (int i = 1; i < MatrixSize; i++)
            {
                if (path[i] == 0)
                    Console.WriteLine($"Кратчайший путь: из 1 -> {i + 1} | Мин.Расстояние: {dist[i]}");
            }
            return dist;
        }

        static void Main(string[] args)
        {
            //Матрица  используемая в задаче
            int[,] graph = {
            { 0, 7, 9, 0, 0, 14 },
            { 7, 0, 10, 15, 0, 0 },
            { 9, 10, 0, 11, 0, 2},
            { 0, 15, 11, 0, 6, 0},
            { 0, 0, 0, 6, 0, 9},
            {14, 0, 2, 0, 9, 0}
        };
            var matrixSize = 6;

            Dijkstra(graph, matrixSize);

            Console.ReadLine();
        }
    }

}