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
        Point nextPoint, enemyPosition;
        Point positionOfPlayer;
        Run run;

        private void Start()
        {
            run = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Run>();
            rows = run.countOfRows;
            columns = run.countOfColumns;
            movementSpeed = 0.05f;
            speedOfChangingDirection = 20;
            positionOfPlayer = GetPlayerPosition();
            enemyPosition = GetEnemyPosition();
            //StartCoroutine(RefreshPath());
            //StartCoroutine(MoveByChangePosition());
        }

        private void Update()
        {
            if (GetPlayerPosition() != positionOfPlayer)
            {
                InitializeSmartMovement();
                positionOfPlayer = GetPlayerPosition();
            }
        }

        protected override void SetCoordinates()
        {
            if (path == null)
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
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(RefreshPath());
        }

        IEnumerator MoveByChangePosition()
        {
            if (GetPlayerPosition() != positionOfPlayer)
            {
                InitializeSmartMovement();
                positionOfPlayer = GetPlayerPosition();
            }
            else base.SetCoordinates();
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(MoveByChangePosition());
        }

        void InitializeSmartMovement()
        {
            GetField();
            path = Astar.RunAstar(GetEnemyPosition(), GetPlayerPosition(), field);
            if (path != null)
            {
                TakeNextDestination();
                enemyPosition = GetEnemyPosition();
            }
        }

        void CheckSetAndTakeNext()
        {
            if ((enemyPosition.Y + 1) == nextPoint.Y)
            {
                moveHorizontal = 0;
                moveVertical = 1;
            }

            if ((enemyPosition.Y - 1) == nextPoint.Y)
            {
                moveHorizontal = 0;
                moveVertical = -1;
            }

            if ((enemyPosition.X - 1) == nextPoint.X)
            {
                moveHorizontal = -1;
                moveVertical = 0;
            }

            if ((enemyPosition.X + 1) == nextPoint.X)
            {
                moveHorizontal = 1;
                moveVertical = 0;
            }
            if (nextPoint == enemyPosition)
                TakeNextDestination();
        }

        void TakeNextDestination()
        {
            nextPoint = path.ElementAt(0);
            path.RemoveAt(0);
        }

        void GetField()
        {
            field = new int[columns + 1, rows + 1];
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Wall");
            AddObjectInField(objects);
            objects = GameObject.FindGameObjectsWithTag("BreakableWall");
            AddObjectInField(objects);
            objects = GameObject.FindGameObjectsWithTag("Bomb");
            AddObjectInField(objects);
            objects = GameObject.FindGameObjectsWithTag("Enemy");
            AddObjectInField(objects);
        }

        void AddObjectInField(GameObject[] objects)
        {
            foreach (var obj in objects)
            {
                field[GetPosition(obj.transform.position).X, GetPosition(obj.transform.position).Y] = 1;
            }
        }

        Point GetPosition(Vector3 position)
        {
            return new Point(Convert.ToInt32(position.x) + (columns / 2), Convert.ToInt32(position.z) + (rows / 2));
        }

        Point GetEnemyPosition()
        {
            return GetPosition(transform.position);
        }

        Point GetPlayerPosition()
        {
            return GetPosition(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }
}
