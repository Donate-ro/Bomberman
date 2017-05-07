using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class Run : MonoBehaviour
    {
        public int CountOfRows;
        public int CountOfColumns;
        public int countOfBreakableWalls;

        void Start()
        {
            Camera cam = Camera.main;
            if (CountOfColumns >= CountOfRows) cam.transform.position = new Vector3(0, CountOfColumns, -CountOfColumns);
            else cam.transform.position = new Vector3(0, CountOfRows, -CountOfRows);
            ObjectCreator creator = new ObjectCreator(1f);
            ResourseLoader loader = new ResourseLoader();
            creator.CreateFloor(loader.LoadFloor(), CountOfColumns, CountOfRows);
            creator.CreateUnbreakableWalls(loader.LoadUnbreakableWall(), CountOfColumns, CountOfRows);
            if (countOfBreakableWalls <= (CountOfColumns - 1) * (CountOfRows - 1))
                creator.CreateBreakableWalls(loader.LoadBreakableWall(), CountOfColumns, CountOfRows, countOfBreakableWalls);
        }

    }
}
