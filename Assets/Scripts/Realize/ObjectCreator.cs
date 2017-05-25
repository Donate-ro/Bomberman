using System.Linq;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts
{
    public class ObjectCreator : AbstractObjectCreator
    {

        public float ScaleOfCube { get; private set; }

        ResourseLoader loader;
        System.Random random;
        int rowCount;
        int columnCount;
        int dymamicObjects;
        float scaleOfField = 10f;
        int[,] coordinatesOfBreakableWalls;

        public ObjectCreator()
        {
            loader = new ResourseLoader();
        }

        public ObjectCreator(float cubeScale, int countOfRows, int countOfColumns)
        {
            ScaleOfCube = cubeScale;
            loader = new ResourseLoader();
            rowCount = countOfRows;
            columnCount = countOfColumns;
            coordinatesOfBreakableWalls = new int[(rowCount - 1) * (columnCount - 1), 2];
            random = new System.Random();
            dymamicObjects = 0;
        }


        public override void CreateFloor()
        {
            GameObject floor = loader.LoadFloor();
            floor.transform.localScale = new Vector3((rowCount + ScaleOfCube) / scaleOfField, 1, (columnCount + ScaleOfCube) / scaleOfField);
            Instantiate(floor, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }

        public override void CreateUnbreakableWalls()
        {
            GameObject unbreakableWall = loader.LoadUnbreakableWall();
            unbreakableWall.transform.localScale = new Vector3(ScaleOfCube, ScaleOfCube, ScaleOfCube);
            CheckAndAddBoxCollider(unbreakableWall);
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
            GameObject breakableWall = loader.LoadBreakableWall();
            CheckAndAddBoxCollider(breakableWall);
            breakableWall.transform.localScale = new Vector3(ScaleOfCube, ScaleOfCube, ScaleOfCube);
            while (countOfBreakableWalls != 0)
            {
                if (GeneratePositionOfDynamicObject(breakableWall, countOfBreakableWalls - 1, ScaleOfCube / 2))
                {
                    countOfBreakableWalls--;
                }
            }
        }

        public override void CreateEnemy()
        {
            GameObject enemy = loader.LoadEnemy();
            CreateDynamicObjects(enemy);
        }

        public override void CreatePlayer()
        {
            GameObject player = loader.LoadPlayer();
            CreateDynamicObjects(player);

        }

        public GameObject CreateTextScore()
        {
            Instantiate(loader.LoadEventSystem());
            return Instantiate(loader.LoadCanvas());

        }

        void CheckAndAddBoxCollider(GameObject obj)
        {
            if (obj.GetComponent<BoxCollider>() == null)
                obj.AddComponent<BoxCollider>();
            obj.GetComponent<BoxCollider>().size = new Vector3(ScaleOfCube, ScaleOfCube, ScaleOfCube);
        }

        void CreateDynamicObjects(GameObject obj)
        {
            obj.GetComponent<Rigidbody>().drag = 1;
            bool check = false;
            while (!check)
            {
                check = GeneratePositionOfDynamicObject(obj, coordinatesOfBreakableWalls.Length / 2 - dymamicObjects - 1, 1);
            }
            dymamicObjects++;
        }

        bool GeneratePositionOfDynamicObject(GameObject obj, int numOfObject, float scale)
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
