using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Assets.Scripts
{
    class SmartAutoMovement : AutoMovement
    {
        bool[,] field, baseField;
        int rows, columns;
        List<Point> path = new List<Point>();
        Point nextPoint, enemyPosition;
        Point positionOfPlayer;
        Run run;

        public override void LeftStep()
        {
            base.LeftStep();
        }
        public override void RightStep()
        {
            base.RightStep();
        }

        private void Start()
        {
            run = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Run>();
            rows = run.countOfRows;
            columns = run.countOfColumns;
            movementSpeed = 0.05f;
            speedOfChangingDirection = 20;
            positionOfPlayer = GetPlayerPosition();
            enemyPosition = GetEnemyPosition();
            GetBaseField();
        }

        private void Update()
        {
            if (((moveHorizontal > 0) || (moveVertical > 0)) || ((moveHorizontal < 0) || (moveVertical < 0))) animator.SetFloat("Movement", 1.1f);
            else animator.SetFloat("Movement", 0);
            if (!field[GetPlayerPosition().X,GetPlayerPosition().Y])
                if (GetPlayerPosition() != positionOfPlayer)
                {
                    InitializeSmartMovement();
                    positionOfPlayer = GetPlayerPosition();
                }
        }

        protected override void SetCoordinates()
        {
            if (!gameObject.GetComponent<PlayerSearch>().isPlayerFound)
                base.SetCoordinates();
            else
            {
                CheckSetAndTakeNext();
                enemyPosition = GetEnemyPosition();
            }
        }

        void InitializeSmartMovement()
        {
            GetField();
            path = Astar.RunAstar(GetEnemyPosition(), GetPlayerPosition(), field);
            if ((path != null) && (path.Count >= 1))
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
            if ((nextPoint == enemyPosition) && (path.Count >= 1))
                TakeNextDestination();
        }

        void TakeNextDestination()
        {

            nextPoint = path.ElementAt(0);
            path.RemoveAt(0);
        }

        void GetBaseField()
        {
            baseField = new bool[columns + 1, rows + 1];
            field = new bool[columns + 1, rows + 1];
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Wall");
            AddObjectInField(objects, ref baseField);
        }

        void RefreshField()
        {
            for (int i = 0; i < field.GetLength(0); i++)
                for (int j = 0; j < field.GetLength(1); j++)
                    field[i, j] = baseField[i, j];
        }

        void GetField()
        {
            RefreshField();
            GameObject[] objects = GameObject.FindGameObjectsWithTag("BreakableWall");
            AddObjectInField(objects, ref field);
            objects = GameObject.FindGameObjectsWithTag("Bomb");
            AddObjectInField(objects, ref field);
            objects = GameObject.FindGameObjectsWithTag("Enemy");
            AddObjectInField(objects, ref field);
        }

        void AddObjectInField(GameObject[] objects, ref bool[,] field)
        {
            foreach (var obj in objects)
            {
                field[GetPosition(obj.transform.position).X, GetPosition(obj.transform.position).Y] = true;
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
