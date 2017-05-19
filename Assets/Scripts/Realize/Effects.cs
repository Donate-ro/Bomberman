using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class Effects : MonoBehaviour
    {
        public static IEnumerator FadeEffect(GameObject hit)
        {
            yield return new WaitForSeconds(0.75f);
            Color color = hit.GetComponent<Renderer>().material.color;
            while (color.a > 0)
            {
                color.a -= 0.05f;
                hit.GetComponent<Renderer>().material.color = color;
                yield return new WaitForSeconds(0.01f);
            }
            hit.SetActive(false);
        }
    }
}
