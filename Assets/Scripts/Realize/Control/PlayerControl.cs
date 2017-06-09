using UnityEngine;
namespace Assets.Scripts
{
    class PlayerControl : Movement
    {
        private void Awake()
        {
            animator = gameObject.GetComponent<Animator>();
            audioSource = gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Animator>().SetTrigger("Attack");
                collision.gameObject.transform.GetChild(1).gameObject.GetComponent<AudioSource>().PlayOneShot(AudioLoader.LoadPunch());
                GetComponent<BombCreator>().DestroyObject(gameObject);
            }
        }
        public override void LeftStep()
        {
            audioSource.PlayOneShot(AudioLoader.LoadPlayerLeftStep());
        }
        public override void RightStep()
        {
            audioSource.PlayOneShot(AudioLoader.LoadPlayerRightStep());
        }
        public void BattleCry()
        {
            audioSource.PlayOneShot(AudioLoader.LoadPlayerWin());
        }
    }
}
