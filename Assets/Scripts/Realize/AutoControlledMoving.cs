using UnityEngine;

namespace Assets.Scripts
{
    class AutoControlledMoving : Moving
    {
        public int speedOfChangingDirection = 20;
        int changeDirection = 50;
        int countBeforeChangingDirection;
        int leftOrDown = -1;
        int rightOrUp = 2;
        bool wallCollision;
        System.Random random = new System.Random();

        protected override void SettingCoordinates()
        {
            if (countBeforeChangingDirection == changeDirection) RandomCoordinates();
            else countBeforeChangingDirection++;
        }


        void RandomCoordinates()
        {
            moveHorizontal = random.Next(leftOrDown, rightOrUp);
            moveVertical = random.Next(leftOrDown, rightOrUp);
            countBeforeChangingDirection = speedOfChangingDirection;
            wallCollision = false;
        }

        void OnCollisionEnter(Collision collision)
        {
            if ((collision.gameObject.tag == "Wall") || (collision.gameObject.tag == "BreakableWall"))
            {
                RandomCoordinates();
                wallCollision = true;
            }
        }

        void OnCollisionStay(Collision collision)
        {
            if ((collision.gameObject.tag == "Enemy") || (wallCollision)) RandomCoordinates();
        }
    }
}