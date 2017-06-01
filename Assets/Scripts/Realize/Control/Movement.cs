using UnityEngine;
namespace Assets.Scripts
{
    abstract class Movement : AbstractMovement
    {
        public float movementSpeed = 0.07f;
        Animator animator;
        protected AudioSource audioSource;
        private void FixedUpdate()
        {
            SetCoordinates();
            Move();
            Rotate();
        }

        private void Update()
        {
            if (((moveHorizontal > 0) || (moveVertical > 0)) || ((moveHorizontal < 0) || (moveVertical < 0))) animator.SetFloat("Movement", 1.1f);
            else animator.SetFloat("Movement", 0);
        }

        private void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            audioSource = gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
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
                if (horizontal > 0) return 135;
                if (horizontal < 0) return 225;
                if (horizontal == 0) return 180;
            }
            if (vertical > 0)
            {
                if (horizontal > 0) return 45;
                if (horizontal < 0) return 315;
                if (horizontal == 0) return 0;
            }
            if (vertical == 0)
            {
                if (horizontal > 0) return 90;
                if (horizontal < 0) return 270;
            }
            return 0;
        }
        public abstract void LeftStep();
        public abstract void RightStep();
    }
}
