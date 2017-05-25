using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class Run : MonoBehaviour
    {
        public int countOfRows;
        public int countOfColumns;
        public int countOfBreakableWalls;
        public int enemiesCount = 2;
        ObjectCreator creator;
        public List<Powerup> powerUps = new List<Powerup>();

        void Start()
        {
            Camera cam = Camera.main;
            if (countOfColumns >= countOfRows) cam.transform.position = new Vector3(0, countOfColumns / 2, -countOfColumns / 2);
            else cam.transform.position = new Vector3(0, countOfRows, -countOfRows);
            creator = new ObjectCreator(0.9f, countOfColumns, countOfRows);
            creator.CreateFloor();
            creator.CreateUnbreakableWalls();
            creator.CreateBreakableWalls(countOfBreakableWalls);
            creator.CreatePlayer();
            CreateEnemies(enemiesCount);
            powerUps.Add(Powerup.Speed);
            powerUps.Add(Powerup.WalkOnWalls);
        }
        void CreateEnemies(int count)
        {
            for (int i = 0; i < count; i++)
                creator.CreateEnemy();
        }
    }
}
