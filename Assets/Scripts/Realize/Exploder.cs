using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    class Exploder : MonoBehaviour
    {
        public float strengthOfExplosion = 5;
        List<Vector3> directions = new List<Vector3>()
        {
            Vector3.forward, Vector3.back,Vector3.left,Vector3.right
        };

        public void Explode(GameObject bomb, GameObject explosion, Action<List<RaycastHit>> action = null)
        {
            Destroy(bomb);
            var particleSystems = explosion.GetComponentsInChildren<ParticleSystem>();
            foreach (var particleSystem in particleSystems)
            {
                particleSystem.startSpeed = strengthOfExplosion;
            }
            Destroy(Instantiate(explosion, bomb.transform.position, new Quaternion(0, 0, 0, 0)), 1);
            if (action != null)
                action(FindCollisions(bomb.transform.position));
        }

        List<RaycastHit> FindCollisions(Vector3 startPosition)
        {
            List<RaycastHit> hits = new List<RaycastHit>();
            foreach (var direction in directions)
                CheckHits(Physics.RaycastAll(GetStartPosition(direction, startPosition), direction, strengthOfExplosion + 1), hits);
            return hits;
        }

        void CheckHits(RaycastHit[] allHits, List<RaycastHit> hits)
        {
            if (allHits.FirstOrDefault().collider != null)
                foreach (var hit in allHits)
                {
                    if (!hit.collider.CompareTag("Wall"))
                    {
                        hits.Add(hit);
                        break;
                    }
                    else break;
                }
        }

        Vector3 GetStartPosition(Vector3 direction, Vector3 startPosition)
        {
            if (direction == Vector3.left) return Vector3.right + startPosition;
            if (direction == Vector3.right) return Vector3.left + startPosition;
            if (direction == Vector3.forward) return Vector3.back + startPosition;
            if (direction == Vector3.back) return Vector3.forward + startPosition;
            return new Vector3();
        }
    }
}
