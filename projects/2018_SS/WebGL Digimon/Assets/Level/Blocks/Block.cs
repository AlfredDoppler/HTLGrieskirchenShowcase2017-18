using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Level
{
    public class Block
    {
        public Level level { get; private set; }

        public float X { get; set; }
        public float Y { get; set; }

        public static readonly int WIDTH_SCALE = 8;
        public static readonly int HEIGHT_SCALE = WIDTH_SCALE;

        public static Block init(int x, int y, Level level, GameObject instance)
        {
            Block block = new Block { X = x, Y = y };
            block.level = level;

            instance.transform.localScale = new Vector3(WIDTH_SCALE, HEIGHT_SCALE, 0);
            instance.transform.position = new Vector3(x * 2.65f, y * 2.65f);

            return block;
        }

    }
}
