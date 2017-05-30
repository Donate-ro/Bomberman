using UnityEngine;
namespace Assets.Scripts
{
    class PlayerControl : Movement
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Animator>().SetTrigger("Attack");
                GetComponent<BombCreator>().DestroyObject(gameObject);
            }
        }
    }

}
