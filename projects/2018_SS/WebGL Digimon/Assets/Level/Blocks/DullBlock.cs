using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Level.Blocks
{
    public class DullBlock : MonoBehaviour
    {
        public Block block { get; set; }
        public bool isDisguised;
        public bool doAnimation;

        private float animationCeaseTime = -1;
        private readonly float ANIMATION_TIME = 2f;

        private float animationTimer = 0;
        private readonly float animationTime = 5f;

        private Color defaultColor;
        private readonly Color animColor = new Color(0.8f, 0.8f, 0.8f);
        private bool lastColor; // true animColor false defaultColor
        public Sprite animSprite;
        public Sprite defaultSprite;

        private SpriteRenderer spriteRenderer;

        public void Start()
        {
            isDisguised = this.GetComponent<Disguise>() != null;

            spriteRenderer = this.GetComponent<SpriteRenderer>();
            defaultColor = spriteRenderer.color;

            //Debug.Log("isDisguised: " + (this.GetComponent<Disguise>() != null));
            if (!isDisguised)
            {
                defaultSprite = spriteRenderer.sprite;
            }
        }

        public void Update()
        {
            if(doAnimation)
            {
                if(animationCeaseTime == -1)
                {
                    animationCeaseTime = Time.time + ANIMATION_TIME;
                }

                //Debug.Log(Time.time + " : " + animationCeaseTime + " : " + (Time.time - animationCeaseTime));

                if(animationCeaseTime - Time.time <= 0)
                {
                    animationCeaseTime = -1;
                    doAnimation = false;
                    animationTimer = 0;
                    spriteRenderer.sprite = null;
                    //Debug.Log("cease animation");
                }


                if(animationTimer >= animationTime)
                {
                    if(lastColor)
                    {
                        spriteRenderer.color = animColor;
                        spriteRenderer.sprite = animSprite;
                        //Debug.Log("adopted anim color");
                    } else
                    {
                        spriteRenderer.color = defaultColor;
                        spriteRenderer.sprite = defaultSprite;
                        //Debug.Log(defaultSprite.name);
                        //Debug.Log("adopted default color");
                     }

                    lastColor = !lastColor;
                    animationTimer = 0;
                }

                animationTimer++;
            } else
            {
                spriteRenderer.sprite = defaultSprite;
            }
        }

    }
}
