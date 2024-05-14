using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  Map
{
    public class SingleMap
    {
        // 2的5次方
        public int MaxWidth = 32;
        public int MaxHeight = 32;
        public int[,] MapData;

        public SingleMap()
        {
            MapData = new int[MaxHeight, MaxWidth];
            
        }
    }
}
