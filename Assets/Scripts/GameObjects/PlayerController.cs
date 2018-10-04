using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip collectableAudio;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            SoundManager.instance.PlaySingle(collectableAudio);
            GameManager.instance.checkEndGameCollectable(other.transform.position);
        }
    }
}
