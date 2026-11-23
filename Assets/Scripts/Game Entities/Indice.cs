using UnityEngine;

public class Indice : MonoBehaviour
{
    AudioSource audioS;

    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!audioS.isPlaying) audioS.Play();
        }
    }
}
