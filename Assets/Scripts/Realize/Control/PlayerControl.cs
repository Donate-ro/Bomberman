using UnityEngine;
namespace Assets.Scripts
{
    class PlayerControl : Movement
    {
        BombCreator bombCreator;

        private void Start()
        {
            bombCreator = gameObject.GetComponent<BombCreator>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy") gameObject.SetActive(false);
        }

        //private void OnParticleCollision(GameObject other)
        //{
        //    if ((other.CompareTag("Player")) || (other.CompareTag("BreakableWall")) || (other.CompareTag("Enemy"))) other.SetActive(false); //StartCoroutine(Effects.FadeEffect(other));
        //}
    }
}
