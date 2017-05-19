using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    class Exploder : MonoBehaviour
    {
        public float strengthOfExplosion = 1;
        List<Vector3> directions = new List<Vector3>()
        {
            Vector3.forward, Vector3.back,Vector3.left,Vector3.right
        };

        public void Explode(GameObject bomb, GameObject explosion, Action<List<RaycastHit>> action = null)
        {
            if (bomb.GetComponent<Collider>().isTrigger == true) GameObject.FindGameObjectsWithTag("Player")[0].SetActive(false);
            Destroy(bomb);
            Destroy(Instantiate(explosion, bomb.transform.position, new Quaternion(0, 0, 0, 0)), strengthOfExplosion);
            if (action != null)
                action(FindCollisions(bomb.transform.position));
        }

        List<RaycastHit> FindCollisions(Vector3 startPosition)
        {
            List<RaycastHit> hits = new List<RaycastHit>();
            foreach (var direction in directions)
                 CheckHits(Physics.RaycastAll(startPosition, direction, strengthOfExplosion), hits);
            return hits;
        }

        void CheckHits(RaycastHit[] allHits, List<RaycastHit> hits)
        {
            if (allHits.FirstOrDefault().collider != null)
                foreach (var hit in allHits)
                    hits.Add(hit);
        }
    }
}
