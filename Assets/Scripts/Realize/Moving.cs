using UnityEngine;
namespace Assets.Scripts
{
    class Moving : MonoBehaviour
    {
        public int movementSpeed = 10;
        protected float moveHorizontal;
        protected float moveVertical;
        float yRotation;
        protected System.Random random = new System.Random();

        private void FixedUpdate()
        {
            GetCoordinates();
            Move();
            Rotate();
        }

        protected void Move()
        {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            transform.position += movement / movementSpeed;
        }

        protected void Rotate()
        {
            if (moveHorizontal > 0) yRotation = 0;
            if (moveVertical > 0) yRotation = 270;
            if (moveHorizontal < 0) yRotation = 180;
            if (moveVertical < 0) yRotation = 90;
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        protected virtual void GetCoordinates()
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }
    }
}
