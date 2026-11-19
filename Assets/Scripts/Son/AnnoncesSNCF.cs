using System.Collections;
using UnityEngine;

public class AnnoncesSNCF : MonoBehaviour
{
    [SerializeField] AudioSource audioJingle;
    [SerializeField] AudioSource audioMessage;
    void Start()
    {
        StartCoroutine(PlayMessage());
    }

    IEnumerator PlayMessage()
    {
        yield return new WaitForSeconds(5);
        while (true)
        {
            int i = 0;
            while (i < 2)
            {
                audioJingle.Play();
                yield return new WaitForSeconds(audioJingle.clip.length);
                audioMessage.Play();
                yield return new WaitForSeconds(20);
                i++;
            }
            yield return new WaitForSeconds(60);
        }
    }
}
