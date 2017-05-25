﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    class Exploder : MonoBehaviour
    {
        public float strengthOfExplosion = 1;
        public static List<Vector3> directions = new List<Vector3>()
        {
            Vector3.forward, Vector3.back,Vector3.left,Vector3.right
        };

        public void Explode(GameObject bomb, GameObject explosion, Action<List<RaycastHit>> action = null)
        {
            bomb.SetActive(false);
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

        public void Explode(List<GameObject> bombs, GameObject explosion, Action<List<RaycastHit>> action = null)
        {
            foreach (GameObject bomb in bombs)
                Explode(bomb, explosion, action);
        }

        List<RaycastHit> FindCollisions(Vector3 startPosition)
        {
            List<RaycastHit> hits = new List<RaycastHit>();
            RaycastHit hit = new RaycastHit();
            foreach (var direction in directions)
                if (Physics.Raycast(GetStartPosition(direction, startPosition), direction, out hit, strengthOfExplosion + 1))
                    if (!hit.transform.gameObject.CompareTag("Wall")) hits.Add(hit);
            return hits;
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