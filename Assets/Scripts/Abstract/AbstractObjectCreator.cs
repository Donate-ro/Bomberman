using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public abstract class AbstractObjectCreator : MonoBehaviour
    {

        public abstract void CreateFloor(GameObject floor, int rowCount, int columnCount);
        public abstract void CreateUnbreakableWalls(GameObject unbreakableWall, int rowCount, int columnCount);
        public abstract void CreateBreakableWalls(GameObject breakableWall, int rowCount, int columnCount, int countOfBreakableWalls);

    }
}