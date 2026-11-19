using UnityEngine;
using UnityEngine.UI;

public class Ticket : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] Button btnHide;
    [SerializeField] Button btnShow;

    void Start()
    {
        btnShow.gameObject.SetActive(false);
    }

    public void AfficheTicket(bool show)
    {
        image.gameObject.SetActive(!show);
        btnHide.gameObject.SetActive(!show);
        btnShow.gameObject.SetActive(show);
    }
}
