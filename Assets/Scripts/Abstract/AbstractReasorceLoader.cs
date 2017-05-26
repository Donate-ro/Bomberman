using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractReasorceLoader : MonoBehaviour
    {
        public abstract GameObject LoadFloor();
        public abstract GameObject LoadUnbreakableWall();
        public abstract GameObject LoadBreakableWall();
        public abstract GameObject LoadPlayer();
        public abstract GameObject LoadEnemy();
        public abstract GameObject LoadBomb();
        public abstract GameObject LoadExplosionEffect();
    }
}
