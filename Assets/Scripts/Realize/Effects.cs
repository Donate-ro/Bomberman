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
            if (obj.CompareTag("Player"))
            {
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                    enemy.GetComponent<Animator>().SetTrigger("PlayerDead");
            }
        }
    }
}
