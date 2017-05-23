using UnityEngine;
namespace Assets.Scripts
{
    class Movement : AbstractMovement
    {
        public float movementSpeed = 0.1f;

        private void FixedUpdate()
        {
            SetCoordinates();
            Move();
            Rotate();
        }

        protected override void Move()
        {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            transform.position += movement * movementSpeed;
        }

        protected override void Rotate()
        {
            transform.rotation = Quaternion.Euler(0, CheckRotation(moveHorizontal, moveVertical), 0);
        }

        protected override void SetCoordinates()
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }

        float CheckRotation(float horizontal, float vertical)
        {
            if (vertical < 0)
            {
                if (horizontal > 0) return 45;
                if (horizontal < 0) return 135;
                if (horizontal == 0) return 90;
            }
            if (vertical > 0)
            {
                if (horizontal > 0) return 315;
                if (horizontal < 0) return 225;
                if (horizontal == 0) return 270;
            }
            if (vertical == 0)
            {
                if (horizontal > 0) return 0;
                if (horizontal < 0) return 180;
            }
            return 0;
        }
    }
}
