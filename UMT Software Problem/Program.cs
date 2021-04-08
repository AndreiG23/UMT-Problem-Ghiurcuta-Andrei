using System;
using System.Collections.Generic;
using System.Linq;

namespace UMT_Software_Problem
{
    //Coordinate este obiectul ce stocheaza coordonate x si y cu care sa pot lucra
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    class Program
    {   //citim fiecare coordonata (x,y) pe un rand nou cu spatiu intre x si y pana cand inputul este null
        static List<Coordinate> readCoordinates()
        {
            List<Coordinate> list = new List<Coordinate>();
            Console.WriteLine("Read coordinates:");
            int x, y;
            string inputString = Console.ReadLine();

            while (!string.IsNullOrEmpty(inputString))
            {
                var split = inputString.Split(' ');
                x = int.Parse(split[0]);
                y = int.Parse(split[1]);
                list.Add(new Coordinate(x, y));
                inputString = Console.ReadLine();
            }
            return list;
        }
        /*
            Daca doua coordonate X diferite sunt imperecheate cu aceleasi 2 coordonate Y inseamna ca avem un dreptunghi
               1: [2,3,4] 
               2: [2,3,5] 
               (1 x 2):[2,3] =>(1,2),(1,3),(2,2),(2,3) este dreptunghi
            Astfel ma folosesc de "lista de adiacenta" sub forma unui dictionar ca sa aflu intersectiile Y pe care le au 2 coordonate X
        */
        static int numberOfRectangles(Dictionary<int, List<int>> dict)
        {
            int rectangles = 0;
            var keys = dict.Keys.ToList();
            //Trecem prin fiecare element din dictionar
            for (int i = 0; i < dict.Count - 1; i++)
                for (int j = i + 1; j < dict.Count; j++)
                {
                    //iar pentru fiecare pereche de chei din dictionar luam listele si calculam intersectia listelor(adica puncte Y comune)
                    List<int> a = dict[keys[i]];
                    List<int> b = dict[keys[j]];
                    IEnumerable<int> aXb = a.Intersect(b);
                    //acolo unde avem mai multe de 2 puncte comune inseamna ca avem un dreptunghi
                    if (aXb.Count() >= 2)
                    {
                        rectangles++;
                    }

                }
            return rectangles;
        }

        static void Main(string[] args)
        {
            List<Coordinate> list = readCoordinates();
            /*
                Parcurgem lista de coordonate in timp ce cream un dictionar(hash map) cu coordonate grupate dupa X
                Deci fiecare element din dictionar va avea o cheie X ce corespunde la o lista de coordonate Y(doar Y) asemenea unei liste de adiacenta
            */
            Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
            foreach (Coordinate c in list)
            {
                if (!dict.ContainsKey(c.X))
                {   // cream lista cu coordonate Y sortate(pentru a fi mai usor de comparat listele intre ele atunci cand facem intersectia)
                    List<int> l = new List<int>();
                    foreach (Coordinate z in list.Where(l => l.X == c.X).OrderBy(c => c.Y))
                    {
                        l.Add(z.Y);
                    }
                    dict.Add(c.X, l);
                }
            }

            Console.WriteLine(numberOfRectangles(dict));
        }
    }
}
