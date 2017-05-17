using UnityEngine;
namespace Assets.Scripts
{
    class Moving : MonoBehaviour
    {
        public float movementSpeed = 0.1f;
        protected float moveHorizontal;
        protected float moveVertical;
        BombCreator bombCreator = new BombCreator();

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
            transform.rotation = Quaternion.Euler(0, CheckRotation(moveHorizontal,moveVertical), 0);
        }

        protected virtual void TypeOfMoving()
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.Space)) CreateBomb();

        }

        void CreateBomb()
        {
            StartCoroutine(bombCreator.CreateBomb(new Vector3(transform.position.x, transform.position.y / 2, transform.position.z)));
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Bomb")) other.isTrigger = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag=="Enemy") gameObject.SetActive(false);
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
