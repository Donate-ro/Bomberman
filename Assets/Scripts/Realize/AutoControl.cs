
namespace Assets.Scripts
{
    class AutoControl : Moving
    {
        public int speed;
        public int freeze = 10;
        private void Start()
        {
            speed = movementSpeed;
        }

        private void FixedUpdate()
        {
            GetCoordinates();
            Move();
            Rotate();
        }
        protected override void GetCoordinates()
        {
            if (freeze == movementSpeed)
            {
                moveHorizontal = random.Next(-speed, speed) / (speed - 1);
                moveVertical = random.Next(-speed, speed) / (speed - 1);
                freeze = 0;
            }
            else freeze++;
        }
    }
}
