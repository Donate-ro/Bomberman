using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractDynamicObjectCreator : MonoBehaviour
    {
        abstract public void CreateBomb(Vector3 positionOfPlayer, float scale);
    }
}