using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class Exploder : MonoBehaviour
    {
        List<Vector3> directions = new List<Vector3>()
        {
            Vector3.forward, Vector3.back,Vector3.left,Vector3.right
        };
        List<RaycastHit> hits = new List<RaycastHit>();

        public float strengthOfExplosion = 1;

        public void Explode(GameObject bomb, GameObject explosion, Action<List<RaycastHit>> action = null)
        {
            Destroy(bomb);
            Destroy(Instantiate(explosion, bomb.transform.position, new Quaternion(0, 0, 0, 0)), strengthOfExplosion);
            if (action != null)
            {
                FindCollisions(bomb.transform.position);
                action(hits);
                hits = new List<RaycastHit>();
            }
            
        }

        void FindCollisions(Vector3 startPosition)
        {
            foreach (var direction in directions)
            {
                RaycastHit hit = Physics.RaycastAll(startPosition, direction, strengthOfExplosion).FirstOrDefault();
                if (hit.collider != null)
                    hits.Add(hit);
            }
        }
    }
}
