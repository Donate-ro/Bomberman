using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractMoving : MonoBehaviour
    {
        protected float moveHorizontal;
        protected float moveVertical;

        protected abstract void Move();
        protected abstract void Rotate();
        protected abstract void Control();
    }
}