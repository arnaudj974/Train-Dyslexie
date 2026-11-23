using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Train : MonoBehaviour
{
    [SerializeField] Image container;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Image screenDefaite;
    [SerializeField] char voie;
    [SerializeField] bool isWin;
    GameObject ticket;
    GameObject tableau;
    TransitionManager transition;

    void Start()
    {
        text.text= "Es-tu sûr de prendre le train voie " + voie + " ?";
        container.gameObject.SetActive(false);
        screenDefaite.gameObject.SetActive(false);
        tableau=FindFirstObjectByType<Tableau>().gameObject;
        ticket= FindFirstObjectByType<Ticket>().gameObject;
        transition = FindFirstObjectByType<TransitionManager>();

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            container.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            container.gameObject.SetActive(false);
        }
    }

    public void HideContainer()
    {
        container.gameObject.SetActive(false);
    }

    public void TakeTrain()
    {
        if (isWin)
        {
            transition.ChangeToSceneWin();
        }
        else
        {
            StartCoroutine(Defaite());
        }
    }

    IEnumerator Defaite()
    {
        tableau.SetActive(false);
        ticket.SetActive(false);
        HideContainer();
        screenDefaite.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        transition.ChangeToSceneLose();
    }
}
