public class SoundButtonController : UnityEngine.MonoBehaviour
{
	void Start () {
       SoundManager.instance.soundInit(gameObject);
    }
}