using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerSearch : MonoBehaviour
    {
        public bool isPlayerFound = false;
        RaycastHit hit = new RaycastHit();

        private void Update()
        {
            if (!isPlayerFound)
                if (SearchPlayer())
                    isPlayerFound = true;
        }
        bool SearchPlayer()
        {
            bool serachProcess = false;
            foreach (var direction in Exploder.directions)
                if (Physics.Raycast(direction*2+ new Vector3(transform.position.x, 0.4f, transform.position.z), direction, out hit))
                    serachProcess = (hit.transform.gameObject.CompareTag("Player"));
            return serachProcess;
        }
    }
}