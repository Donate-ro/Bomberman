using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractMovement : MonoBehaviour
    {
        protected float moveHorizontal;
        protected float moveVertical;

        protected abstract void Move();
        protected abstract void Rotate();
        protected abstract void SetCoordinates();
    }
}