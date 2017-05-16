using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class DynamicObjectCreator : AbstractDynamicObjectCreator
    {
        protected ResourseLoader Loader;
        public float timeOfLife = 2;
        public int maxBombCount = 1;
        public int bombCount = 0;

        public DynamicObjectCreator()
        {
            Loader = new ResourseLoader();
        }

        public override void CreateBomb(Vector3 positionOfPlayer, float scale)
        {
            if (maxBombCount >= bombCount)
            {
                GameObject bomb = Loader.LoadBomb();
                bomb.transform.localScale = new Vector3(scale, scale, scale);
                Destroy(Instantiate(bomb, positionOfPlayer, new Quaternion(0, 0, 0, 0)), timeOfLife);
            }
        }

    }
}
