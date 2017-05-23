using UnityEngine;

namespace Assets.Scripts
{
    public class Run : MonoBehaviour
    {
        public int countOfRows;
        public int countOfColumns;
        public int countOfBreakableWalls;
        ObjectCreator creator;

        void Start()
        {
            Camera cam = Camera.main;
            if (countOfColumns >= countOfRows) cam.transform.position = new Vector3(0, countOfColumns/2, -countOfColumns/2);
            else cam.transform.position = new Vector3(0, countOfRows, -countOfRows);
            creator = new ObjectCreator(0.9f,countOfColumns,countOfRows);
            creator.CreateFloor();
            creator.CreateUnbreakableWalls();
            if (countOfBreakableWalls <= (countOfColumns - 4) * (countOfRows - 4))
                creator.CreateBreakableWalls(countOfBreakableWalls);
            creator.CreatePlayer();
            CreateEnemies(4);
        }

        void CreateEnemies(int count)
        {
            for (int i = 0; i < count; i++)
                creator.CreateEnemy();
        }

    }
}
