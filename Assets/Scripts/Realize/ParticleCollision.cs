using UnityEngine;

namespace Assets.Scripts
{
    public class ParticleCollision : MonoBehaviour
    {
        private void OnParticleCollision(GameObject other)
        {
            if ((other.CompareTag("Player")) || (other.CompareTag("Enemy")))
                if (other.transform.GetChild(1).GetComponent<Collider>().CompareTag("Player") || other.transform.GetChild(1).GetComponent<Collider>().CompareTag("Enemy"))
                    GameObject.FindGameObjectWithTag("Player").GetComponent<BombCreator>().DestroyObject(other);
        }
    }
}