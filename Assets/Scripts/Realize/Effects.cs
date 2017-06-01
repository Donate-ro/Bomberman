using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class Effects : MonoBehaviour
    {
        public static IEnumerator FadeEffect(GameObject obj)
        {
            yield return new WaitForSeconds(0.75f);
            Color color = obj.GetComponent<Renderer>().material.color;
            while (color.a > 0)
            {
                color.a -= 0.05f;
                obj.GetComponent<Renderer>().material.color = color;
                yield return new WaitForSeconds(0.01f);
            }
            if (obj.transform.parent != null) obj = obj.transform.parent.gameObject;
            obj.SetActive(false);
        }

        public static void PlayerDeath(GameObject obj)
        {
            obj.GetComponent<Animator>().SetTrigger("Death");
            obj.transform.GetChild(1).gameObject.GetComponent<AudioSource>().PlayOneShot(AudioLoader.LoadPlayerDead());
            Destroy(obj.transform.GetChild(1).GetComponent<Collider>());
            obj.transform.position = new Vector3(obj.transform.position.x, -0.65f, obj.transform.position.z);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Run>().isPlayerDead = true;
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (!enemy.transform.parent)
                {
                    DestroyContol(enemy);
                    enemy.GetComponent<Animator>().SetTrigger("PlayerDead");
                    enemy.transform.GetChild(1).gameObject.GetComponent<AudioSource>().PlayOneShot(AudioLoader.LoadEnemyWin());
                }
            }
            DestroyContol(obj);
        }

        static void DestroyContol(GameObject obj)
        {
            Destroy(obj.GetComponent<AutoMovement>());
            Destroy(obj.GetComponent<SmartAutoMovement>());
            Destroy(obj.GetComponent<PlayerSearch>());
            Destroy(obj.GetComponent<Movement>());
            Destroy(obj.GetComponent<PowerUp>());
            Destroy(obj.GetComponent<BombCreator>());
        }
    }
}
