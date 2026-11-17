using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tableau : MonoBehaviour
{
    [SerializeField] Image conteneur;
    void Start()
    {
        ChangeCouleurLigne();
        ChangeNumVoie();
    }


    void ChangeCouleurLigne()
    {
        int i = 0;
        while (i < conteneur.transform.childCount)
        {
            Image imageLigne = conteneur.transform.GetChild(i).GetComponent<Image>();
            if (imageLigne != null)
            {

                imageLigne.color = Color.white;
            }
            i += 2;
        }
    }

    void ChangeNumVoie()
    {
        char c= 'A';
        foreach (Transform ligne in conteneur.transform)
        {
            TextMeshProUGUI voie = ligne.GetChild(2).GetChild(0).GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
            if (voie != null)
            {
                voie.text = c.ToString();
                c++;
            }
        }
    }

}
