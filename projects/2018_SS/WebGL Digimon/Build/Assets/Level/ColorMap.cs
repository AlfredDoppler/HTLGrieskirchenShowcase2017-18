using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Level
{
    public class ColorMap
    {
        public static readonly Color DULL_BLOCK = new Color(0, 0, 0);
        public static readonly Color SPEED_BLOCK = new Color(0, 1, 0);
        public static readonly Color CRUMBLY_BLOCK = new Color(1, 0, 0);
        public static readonly Color LEAP_BOOST_BLOCK = new Color(0, 0, 1);
        public static readonly Color AUTO_LEAP_BOOST_BLOCK = new Color(0, 1, 1);
        public static readonly Color CHECKPOINT = new Color(1, 1, 0);
        public static readonly Color FINISH = new Color(1, 0, 1);
        public static readonly Color AIR_BLOCK = new Color(1, 1, 1);

        public static readonly Color DISGUISED_AUTO_LEAP_BOOST_BLOCK = new Color(0.6f, 0f, 0.6f); // 150, 0, 150
        public static readonly Color DISGUISED_CRUMBLY_BLOCK = new Color(0.6f, 0.6f, 0); // 150, 150, 0

        public static readonly Color DISGUISED_DULL_BLOCK = new Color(0.6f, 0.6f, 0.6f);//  150, 150, 150
        public static readonly Color GHOST_BLOCK = new Color(0.9f, 0.9f, 0.9f); // 230, 230, 230

        public static readonly Color THEME_FOREST = new Color(0.9f, 0.9f, 0f); // 230, 230, 0
        public static readonly Color THEME_DESERT = new Color(0.9f, 0f, 0.9f); // 230, 0, 230

        public static readonly Color ENEMY_SUKAMON = new Color(0.9f, 0.6f, 0.6f);  // 230, 150, 150
        public static readonly Color ENEMY_GIGANTIC_SUKAMON = new Color(1f, 0.6f, 0.6f);  // 255, 150, 150
        public static readonly Color ENEMY_SUKAMON_STATIC = new Color(0.9f, 0.6f, 0f);  // 230, 150, 0
	    public static readonly Color ENEMY_KINGETEMON = new Color(0.8f, 0.8f, 0.6f);  // 200, 200, 150

        public static readonly Color WOODEN_SIGN = new Color(0.5f, 0.2f, 0.1f); //139, 69, 19

        public static readonly Color DIGIVICE = new Color(0, 0.9f, 0.9f); // 0, 230, 230

        public static bool MatchesColor(Color colorToMatch, Color color) 
        {
            return MatchesColor(color.r, color.g, color.b, colorToMatch);
        }

        private static bool MatchesColor(float r, float g, float b, Color color)
        {
            return Math.Round(r, 1) == Math.Round(color.r, 1) && Math.Round(g, 1) == Math.Round(color.g, 1) && Math.Round(b, 1) == Math.Round(color.b, 1);
        }
    }
}
