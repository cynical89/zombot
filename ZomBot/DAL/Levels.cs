using System;
using System.Collections.Generic;
using System.Text;

namespace ZomBot.DAL
{
    class Levels
    {
        public static Dictionary<int, int> LevelScale = new Dictionary<int, int>()
        {
            {2, 250},
            {3, 500},
            {4, 1000},
            {5, 2000},
            {6, 4000},
            {7, 8000},
            {8, 16000},
            {9, 17000},
            {10, 18000},
            {11, 360000}
        };
    }
}
