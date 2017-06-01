using UnityEngine;

namespace Assets.Scripts
{
    class AutoMovement : Movement
    {
        public int speedOfChangingDirection = 20;
        int changeDirection = 50;
        int countBeforeChangingDirection;
        int leftOrDown = -1;
        int rightOrUp = 2;
        bool wallCollision;
        System.Random random = new System.Random();

        protected override void SetCoordinates()
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

        public override void LeftStep()
        {
            audioSource.PlayOneShot(AudioLoader.LoadEnemyLeftStep());
        }
        public override void RightStep()
        {
            audioSource.PlayOneShot(AudioLoader.LoadEnemyRightStep());
        }
    }
}