using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Level.Blocks
{
    public class Disguise : MonoBehaviour
    {
        public Sprite camouflage; // the image displayed while disguised: will be exchanged after player contact
        public Sprite identity; // the real identity of this block

        public void HideIdentity()
        {
            GetComponent<SpriteRenderer>().sprite = camouflage;
        }

        public void ExposeIdentity()
        {
            GetComponent<SpriteRenderer>().sprite = identity;
        }

    }
}
