using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Exploder: DynamicObjectCreator
    {
        public void Explode(Vector3 positionOfPlayer)
        {
            GameObject explosion = Loader.LoadExplosion();
            Instantiate(explosion, positionOfPlayer, new Quaternion(0, 0, 0, 0));
        }
    }
}
