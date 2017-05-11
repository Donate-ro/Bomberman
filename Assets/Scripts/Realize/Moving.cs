using UnityEngine;
namespace Assets.Scripts
{
    namespace Assets.Scripts
    {
        class Moving : MonoBehaviour
        {
            float movementSpeed = 0.1f;
            Rigidbody rb;
            float yRotation;
            void Start()
            {
                rb = GetComponent<Rigidbody>();
                rb.detectCollisions = true;
            }
            private void FixedUpdate()
            {
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");
                Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
                transform.position += movement / 10;
                if (moveHorizontal>0) yRotation = 0;
                if (moveVertical>0) yRotation = 270;
                if (moveHorizontal < 0) yRotation = 180;
                if (moveVertical < 0) yRotation = 90;
                transform.rotation = Quaternion.Euler(0, yRotation, 0);
                
                //if (!Input.GetKeyDown(KeyCode.UpArrow))
                //    startPosition.z--;
                //if (!Input.GetKeyDown(KeyCode.DownArrow))
                //    startPosition.z++;
                //if (!Input.GetKeyDown(KeyCode.LeftArrow))
                //    startPosition.x++;
                //if (!Input.GetKeyDown(KeyCode.RightArrow))
                //    startPosition.x--;
                //if (Input.GetKeyDown(KeyCode.Space))
                //    cc.Move(startPosition);
                //rb.MovePosition(movement);
                //rb.AddForce(movement);
                //transform.Translate(startPosition);
                //rb.AddForce(movement * movementSpeed*1000);
            }


        }
    }
}
