using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class ResourseLoader : AbstractReasorceLoader
    {

        public override GameObject LoadFloor()
        {
            return Resources.Load("Floor") as GameObject;
        }
        public override GameObject LoadUnbreakableWall()
        {
            return Resources.Load("Unbreakable Wall") as GameObject;
        }
        public override GameObject LoadBreakableWall()
        {
            return Resources.Load("Breakable Wall") as GameObject;
        }
    }
}
