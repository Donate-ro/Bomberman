using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Assets.Scripts
{
    class SmartAutoMovement : AutoMovement
    {
        int[,] field;
        int rows, columns;
        List<Point> path = new List<Point>();
        Point currentDestination, enemyPosition;

        private void Start()
        {
            movementSpeed = 0.07f;
            speedOfChangingDirection = 20;
            StartCoroutine(RefreshPath());
        }

        protected override void SetCoordinates()
        {
            if (path==null)
                base.SetCoordinates();
            else
            {
                CheckSetAndTakeNext();
                enemyPosition = GetEnemyPosition();
            }
        }

        IEnumerator RefreshPath()
        {
            InitializeSmartMovement();
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(RefreshPath());
        }

        void InitializeSmartMovement()
        {
            GetField();
            path = Astar.RunAstar(GetPositionByTag("Enemy"), GetPositionByTag("Player"), field);
            if (path != null)
            {
                TakeNextDestination();
                enemyPosition = GetEnemyPosition();
            }
        }

        void CheckSetAndTakeNext()
        {
            if ((enemyPosition.Y + 1) == currentDestination.Y)
            {
                moveHorizontal = 0;
                moveVertical = 1;
            }

            if ((enemyPosition.Y - 1) == currentDestination.Y)
            {
                moveHorizontal = 0;
                moveVertical = -1;
            }

            if ((enemyPosition.X - 1) == currentDestination.X)
            {
                moveHorizontal = -1;
                moveVertical = 0;
            }

            if ((enemyPosition.X + 1) == currentDestination.X)
            {
                moveHorizontal = 1;
                moveVertical = 0;
            }
            if (currentDestination == enemyPosition)
                TakeNextDestination();
        }

        void TakeNextDestination()
        {
            currentDestination = path.ElementAt(0);
            path.RemoveAt(0);
        }

        void GetField()
        {
            Run run = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Run>();
            rows = run.countOfRows;
            columns = run.countOfColumns;
            field = new int[columns + 1, rows + 1];
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Wall");
            AddObjectInField(objects);
            objects = GameObject.FindGameObjectsWithTag("BreakableWall");
            AddObjectInField(objects);
            objects = GameObject.FindGameObjectsWithTag("Bomb");
            AddObjectInField(objects);
        }

        void AddObjectInField(GameObject[] walls)
        {
            foreach (var wall in walls)
            {
                int x = Convert.ToInt32(wall.transform.position.x) + (columns / 2) + (columns % 2);
                int y = Convert.ToInt32(wall.transform.position.z) + (rows / 2) + (rows % 2);
                field[x, y] = 1;
            }
        }

        Point GetPositionByTag(string tag)
        {
            Vector3 position = GameObject.FindGameObjectsWithTag(tag)[0].transform.position;
            return new Point(Convert.ToInt32(position.x) + (columns / 2) + (columns % 2), Convert.ToInt32(position.z) + (rows / 2) + (rows % 2));
        }

        Point GetEnemyPosition()
        {
            Vector3 position = transform.position;
            return new Point(Convert.ToInt32(position.x) + (columns / 2) + (columns % 2), Convert.ToInt32(position.z) + (rows / 2) + (rows % 2));
        }
    }
}
