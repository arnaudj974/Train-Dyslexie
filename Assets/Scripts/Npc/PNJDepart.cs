using UnityEngine;

public class PNJDepart : MonoBehaviour
{
    AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!audioS.isPlaying) audioS.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (audioS.isPlaying) audioS.Stop();
        }
    }
}
