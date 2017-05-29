using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    class Run : MonoBehaviour
    {
        public int countOfRows;
        public int countOfColumns;
        public int countOfBreakableWalls;
        public int enemiesCount;
        ObjectCreator creator;
        public List<Powerup> powerUps = new List<Powerup>();

        void Start()
        {
            Camera cam = Camera.main;
            float cameraPositionAttitude = 1.5f;
            if (countOfColumns >= countOfRows) cam.transform.position = new Vector3(0, countOfColumns / cameraPositionAttitude, -countOfColumns / cameraPositionAttitude);
            else cam.transform.position = new Vector3(0, countOfRows / cameraPositionAttitude, -countOfRows / cameraPositionAttitude);
            creator = new ObjectCreator(0.9f, countOfColumns, countOfRows);
            creator.CreateFloor();
            creator.CreateUnbreakableWalls();
            creator.CreateBreakableWalls(countOfBreakableWalls);
            creator.CreatePlayer();
            creator.CreateEnemies(enemiesCount);
            powerUps.Add(Powerup.Detonator);
            powerUps.Add(Powerup.ExplosionRadius);
            powerUps.Add(Powerup.MoreBombs);
            powerUps.Add(Powerup.Speed);
            powerUps.Add(Powerup.WalkOnBombs);
            powerUps.Add(Powerup.WalkOnWalls);
        }

    }
}
