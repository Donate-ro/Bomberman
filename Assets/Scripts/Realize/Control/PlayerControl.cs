using UnityEngine;
namespace Assets.Scripts
{
    class PlayerControl : Movement
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy") GetComponent<BombCreator>().DestroyObject(gameObject);
        }
    }

}
