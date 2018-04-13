using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Level
{
    public class Level
    {
        
        public int LevelWidth { get; private set; }
        public int LevelHeight { get; private set; }

        private static GameObject[,] blocks;

        public Level(int levelWidth, int levelHeight)
        {
            blocks = new GameObject[levelWidth, levelHeight];
            LevelWidth = levelWidth;
            LevelHeight = levelHeight;
        }

        public void setBlockAt(int x, int y, GameObject block)
        {
            blocks[x, y] = block;
        }

        public GameObject getBlockAt(int x, int y)
        {
            return blocks[x, y];
        }

    }
}
