using System.Collections;
using UnityEngine;

public class AnnoncesSNCF : MonoBehaviour
{
    [SerializeField] AudioSource audioJingle;
    [SerializeField] AudioSource audioMessage;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(15);
        while (true)
        {
            audioJingle.Play();
            yield return new WaitForSeconds(audioJingle.clip.length);
            audioMessage.Play();
            yield return new WaitForSeconds(30);
        }
    }
}
