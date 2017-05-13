using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class DynamicObjectCreator : AbstractDynamicObjectCreator
    {
        ResourseLoader Loader;
        public float timeOfLife = 3;

        public DynamicObjectCreator()
        {
            Loader = new ResourseLoader();
        }

        public override void CreateBomb(Vector3 positionOfPlayer, float scale)
        {
            GameObject bomb = Loader.LoadBomb();
            bomb.transform.localScale = new Vector3(scale, scale, scale);
            Destroy(Instantiate(bomb, positionOfPlayer, new Quaternion(0, 0, 0, 0)), timeOfLife);
        }
    }
}
