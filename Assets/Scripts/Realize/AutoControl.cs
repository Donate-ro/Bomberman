using UnityEngine;

namespace Assets.Scripts
{
    class AutoControl : Moving
    {
        public int speedOfChangingDirection = 30;
        int changeDirection = 50;
        int countBeforeChangingDirection;
        int leftOrDown = -1;
        int rightOrUp = 2;
        bool wallCollision;
        System.Random random = new System.Random();

        protected override void GetCoordinates()
        {
            if (countBeforeChangingDirection == changeDirection) RandomCoordinates();
            else countBeforeChangingDirection++;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Wall")
            {
                RandomCoordinates();
                wallCollision = true;
            }
        }

        void OnCollisionStay(Collision collision)
        {
            if ((collision.gameObject.tag == "Enemy") || (wallCollision)) RandomCoordinates();
        }

        void RandomCoordinates()
        {
            moveHorizontal = random.Next(leftOrDown, rightOrUp);
            moveVertical = random.Next(leftOrDown, rightOrUp);
            countBeforeChangingDirection = speedOfChangingDirection;
            wallCollision = false;
        }
    }
}