using UnityEngine;

namespace Assets.Scripts
{
    public class Run : MonoBehaviour
    {
        public int countOfRows;
        public int countOfColumns;
        public int countOfBreakableWalls;

        void Start()
        {
            Camera cam = Camera.main;
            if (countOfColumns >= countOfRows) cam.transform.position = new Vector3(0, countOfColumns/2, -countOfColumns/2);
            else cam.transform.position = new Vector3(0, countOfRows, -countOfRows);
            ObjectCreator creator = new ObjectCreator(0.9f,countOfColumns,countOfRows);
            creator.CreateFloor();
            creator.CreateUnbreakableWalls();
            if (countOfBreakableWalls <= (countOfColumns - 4) * (countOfRows - 4))
                creator.CreateBreakableWalls(countOfBreakableWalls);
            creator.CreatePlayer();
            creator.CreateEnemy();

        }

    }
}
