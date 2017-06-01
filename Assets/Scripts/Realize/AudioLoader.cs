using UnityEngine;
namespace Assets.Scripts
{
    static class AudioLoader
    {
        public static AudioClip LoadPlayerLeftStep()
        {
            return Resources.Load("Audio/PlayerLeft") as AudioClip;
        }
        public static AudioClip LoadPlayerRightStep()
        {
            return Resources.Load("Audio/PlayerRight") as AudioClip;
        }
        public static AudioClip LoadEnemyLeftStep()
        {
            return Resources.Load("Audio/EnemyLeft") as AudioClip;
        }
        public static AudioClip LoadEnemyRightStep()
        {
            return Resources.Load("Audio/EnemyRight") as AudioClip;
        }
        public static AudioClip LoadPlayerDead()
        {
            return Resources.Load("Audio/PlayerDead") as AudioClip;
        }
        public static AudioClip LoadEnemyDead()
        {
            return Resources.Load("Audio/EnemyDeath") as AudioClip;
        }
        public static AudioClip LoadPlayerWin()
        {
            return Resources.Load("Audio/Win") as AudioClip;
        }
        public static AudioClip LoadEnemyWin()
        {
            return Resources.Load("Audio/EnemyLaugh") as AudioClip;
        }
        public static AudioClip LoadBombSetup()
        {
            return Resources.Load("Audio/SetBomb") as AudioClip;
        }
        public static AudioClip LoadBombExplosion()
        {
            return Resources.Load("Audio/Explosion") as AudioClip;
        }
        public static AudioClip LoadCoin()
        {
            return Resources.Load("Audio/coin") as AudioClip;
        }
        public static AudioClip LoadPunch()
        {
            return Resources.Load("Audio/Punch") as AudioClip;
        }
    }
}
