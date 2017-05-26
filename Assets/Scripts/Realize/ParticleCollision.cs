using UnityEngine;

namespace Assets.Scripts
{
    public class ParticleCollision : MonoBehaviour
    {
        private void OnParticleCollision(GameObject other)
        {
            if (other.GetComponent<Collider>().CompareTag("Player") || other.GetComponent<Collider>().CompareTag("Enemy"))
                GameObject.FindGameObjectWithTag("Player").GetComponent<BombCreator>().DestroyObject(other);
        }
    }
}