using UnityEngine;

namespace Assets.Scripts
{
    public abstract class AbstractReasorceLoader
    {
        public abstract GameObject LoadFloor();
        public abstract GameObject LoadUnbreakableWall();
        public abstract GameObject LoadBreakableWall();
        public abstract GameObject LoadPlayer();
        public abstract GameObject LoadEnemy();
        public abstract GameObject LoadBomb();
    }
}
