using UnityEngine;
namespace Assets.Scripts
{
    public class ObjectCreator : AbstractObjectCreator
    {

        public float ScaleOfCube { get; private set; }

        public ObjectCreator(float cubeScale)
        {
            ScaleOfCube = cubeScale;
        }


        public override void CreateFloor(GameObject floor, int rowCount, int columnCount)
        {
            floor.transform.localScale = new Vector3((rowCount + 2 * ScaleOfCube / 2) / 10f, 1, (columnCount + 2 * ScaleOfCube / 2) / 10f);
            Instantiate(floor, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        }
        public override void CreateUnbreakableWalls(GameObject unbreakableWall, int rowCount, int columnCount)
        {
            unbreakableWall.transform.localScale = new Vector3(ScaleOfCube, ScaleOfCube, ScaleOfCube);
            for (int row = 0; row <= rowCount; row++)
            {
                for (int column = 0; column <= columnCount; column++)
                {
                    if (CanCreateUnbreakableWall(row, column, rowCount, columnCount))
                    {
                        Instantiate(unbreakableWall, new Vector3(row - rowCount / 2f, ScaleOfCube / 2, column - columnCount / 2f), new Quaternion(0, 0, 0, 0));
                    }
                }
            }

        }
        public override void CreateBreakableWalls(GameObject breakableWall, int rowCount, int columnCount, int countOfBreakableWalls)
        {
            breakableWall.transform.localScale = new Vector3(ScaleOfCube, ScaleOfCube, ScaleOfCube);
            int row, column;
            int[] randomRows = new int[countOfBreakableWalls];
            int[] randomColumns = new int[countOfBreakableWalls];
            System.Random random = new System.Random();
            while (countOfBreakableWalls != 0)
            {
                row = random.Next(1, rowCount - 1);
                column = random.Next(1, columnCount - 1);
                if (!CanCreateUnbreakableWall(row, column, rowCount, columnCount))
                    if (!BreakableWall(row, column, randomRows, randomColumns))
                    {
                        Instantiate(breakableWall, new Vector3(row - rowCount / 2f, ScaleOfCube / 2, column - columnCount / 2f), new Quaternion(0, 0, 0, 0));
                        randomRows[countOfBreakableWalls - 1] = row;
                        randomColumns[countOfBreakableWalls - 1] = column;
                        countOfBreakableWalls--;
                    }
            }
        }

        bool CanCreateUnbreakableWall(int row, int column, int rowCount, int columnCount)
        {
            if ((row == 0) || (column == 0) || (row == rowCount) || (column == columnCount) || ((column % 2 == 0) && (row % 2 == 0))) return true;
            else return false;
        }

        bool BreakableWall(int checkRow, int checkColumn, int[] rows, int[] columns)
        {
            for (int i = 0; i < rows.Length; i++)
                if (checkRow == rows[i]) if (checkColumn == columns[i]) return true;
            return false;
        }
    }
}
