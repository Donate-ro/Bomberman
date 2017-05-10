﻿using System;
using UnityEngine;
namespace Assets.Scripts
{
    public class ObjectCreator : AbstractObjectCreator
    {

        public float ScaleOfCube { get; private set; }

        ResourseLoader Loader;
        System.Random random;
        int rowCount;
        int columnCount;
        int dymamicObjects;

        int[,] coordinatesOfBreakableWalls;

        public ObjectCreator(float cubeScale, int RowCount, int ColumnCount)
        {
            ScaleOfCube = cubeScale;
            Loader = new ResourseLoader();
            rowCount = RowCount;
            columnCount = ColumnCount;
            coordinatesOfBreakableWalls = new int[(rowCount - 1) * (columnCount - 1), 2];
            random = new System.Random();
            dymamicObjects = 0;
        }


        public override void CreateFloor()
        {
            GameObject floor = Loader.LoadFloor();
            floor.transform.localScale = new Vector3((rowCount + 2 * ScaleOfCube / 2) / 10f, 1, (columnCount + 2 * ScaleOfCube / 2) / 10f);
            Instantiate(floor, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }

        public override void CreateUnbreakableWalls()
        {
            GameObject unbreakableWall = Loader.LoadUnbreakableWall();
            unbreakableWall.transform.localScale = new Vector3(ScaleOfCube, ScaleOfCube, ScaleOfCube);
            for (int row = 0; row <= rowCount; row++)
            {
                for (int column = 0; column <= columnCount; column++)
                {
                    if (CanCreateUnbreakableWall(row, column))
                    {
                        Instantiate(unbreakableWall, new Vector3(row - rowCount / 2f, ScaleOfCube / 2, column - columnCount / 2f), new Quaternion(0, 0, 0, 0));
                    }
                }
            }

        }
        public override void CreateBreakableWalls(int countOfBreakableWalls)
        {
            GameObject breakableWall = Loader.LoadBreakableWall();
            breakableWall.transform.localScale = new Vector3(ScaleOfCube, ScaleOfCube, ScaleOfCube);
            while (countOfBreakableWalls != 0)
            {
                if (GenerateAndCheck(breakableWall, countOfBreakableWalls - 1, ScaleOfCube / 2))
                {
                    countOfBreakableWalls--;
                }
            }
        }

        public override void CreateEnemy()
        {
            GameObject enemy = Loader.LoadEnemy();
            //enemy.AddComponent<Assets.Scripts.Moving>();
            CreateDynamicObjects(enemy);
        }

        public override void CreatePlayer()
        {
            GameObject player = Loader.LoadPlayer();
            //player.AddComponent<Assets.Scripts.Moving>();
            CreateDynamicObjects(player);

        }

        void CreateDynamicObjects(GameObject obj)
        {
            bool check = false;
            while (!check)
            {
                check = GenerateAndCheck(obj, coordinatesOfBreakableWalls.Length / 2 - dymamicObjects - 1, ScaleOfCube + 0.2f);
            }
            dymamicObjects++;
        }

        bool GenerateAndCheck(GameObject obj, int numOfObject, float scale)
        {
            int row = random.Next(1, rowCount);
            int column = random.Next(1, columnCount);
            if ((!CanCreateUnbreakableWall(row, column)) && (!BreakableWall(row, column)))
            {
                Instantiate(obj, new Vector3(row - rowCount / 2f, scale, column - columnCount / 2f), new Quaternion(0, 0, 0, 0));
                coordinatesOfBreakableWalls[numOfObject, 0] = row;
                coordinatesOfBreakableWalls[numOfObject, 1] = column;
                return true;
            }
            return false;
        }

        bool CanCreateUnbreakableWall(int row, int column)
        {
            return ((row == 0) || (column == 0) || (row == rowCount) || (column == columnCount) || ((column % 2 == 0) && (row % 2 == 0)));
        }

        bool BreakableWall(int checkRow, int checkColumn)
        {
            for (int i = 0; i < coordinatesOfBreakableWalls.Length / 2; i++)
                if (checkRow == coordinatesOfBreakableWalls[i, 0]) if (checkColumn == coordinatesOfBreakableWalls[i, 1]) return true;
            return false;
        }


    }
}
