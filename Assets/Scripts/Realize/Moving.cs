using UnityEngine;
namespace Assets.Scripts
{
    class Moving : AbstractMoving
    {
        public float movementSpeed = 0.1f;
        BombCreator bombCreator;

        private void Start()
        {
            bombCreator = gameObject.GetComponent<BombCreator>();
        }

        private void FixedUpdate()
        {
            SettingCoordinates();
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
            transform.rotation = Quaternion.Euler(0, CheckRotation(moveHorizontal,moveVertical), 0);
        }

        protected override void SettingCoordinates()
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Bomb")) other.isTrigger = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag=="Enemy") gameObject.SetActive(false);
        }

        //private void OnParticleCollision(GameObject other)
        //{
        //    if ((other.CompareTag("Player")) || (other.CompareTag("BreakableWall")) || (other.CompareTag("Enemy"))) other.SetActive(false); //StartCoroutine(Effects.FadeEffect(other));
        //}

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
