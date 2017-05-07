using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public abstract class AbstractReasorceLoader : MonoBehaviour
    {
        public abstract GameObject LoadFloor();
        public abstract GameObject LoadUnbreakableWall();
        public abstract GameObject LoadBreakableWall();
    }
}
