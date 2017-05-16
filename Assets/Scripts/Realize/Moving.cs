using UnityEngine;
namespace Assets.Scripts
{
    class Moving : MonoBehaviour
    {
        public float movementSpeed = 0.1f;
        public float moveHorizontal;
        public float moveVertical;
        float cooldown;
        float yRotation;
        DynamicObjectCreator dynamicObjects = new DynamicObjectCreator();

        private void FixedUpdate()
        {
            TypeOfMoving();
            Move();
            Rotate();
        }

        protected void Move()
        {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            transform.position += movement * movementSpeed;
        }

        protected void Rotate()
        {
            if (moveHorizontal > 0) yRotation = 0;
            if (moveVertical > 0) yRotation = 270;
            if (moveHorizontal < 0) yRotation = 180;
            if (moveVertical < 0) yRotation = 90;
            transform.rotation = Quaternion.Euler(0, yRotation, 0);
        }

        protected virtual void TypeOfMoving()
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(KeyCode.Space)) CreateBomb();

        }

        void CreateBomb()
        {
            float sizeOfBomb = transform.localScale.x - (transform.localScale.x / 10);
            dynamicObjects.CreateBomb(new Vector3(transform.position.x, sizeOfBomb / 2, transform.position.z), sizeOfBomb);
            new Exploder().Explode(new Vector3(transform.position.x, sizeOfBomb / 2, transform.position.z));
        }

        private void OnTriggerExit(Collider collider)
        {
            if (collider.CompareTag("Bomb")) collider.isTrigger = false;
        }
    }
}
