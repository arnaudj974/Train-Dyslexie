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

    public void Save()
    {
        PlayerPrefs.SetInt("Ticket", image.gameObject.activeSelf ? 1 : 0);
    }

    public void Load()
    {
        AfficheTicket(PlayerPrefs.GetInt("Ticket", 1) == 0 ? true : false);
    }
}
