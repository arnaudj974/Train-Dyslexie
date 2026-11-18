using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SyllableCard : MonoBehaviour
{
    public TextMeshProUGUI texteSyllabe;
    public Button boutonCarte;
    public Button boutonAudio;
    
    private string syllabeValeur;
    private AudioClip clipAudio;
    private GameManager gameManager;
    private bool estPlacee = false;

    public void Initialiser(string texte, AudioClip audio, Color couleurFond, GameManager manager)
    {
        syllabeValeur = texte;
        clipAudio = audio;
        gameManager = manager;
        
        texteSyllabe.text = texte;
        GetComponent<Image>().color = couleurFond;
        
        boutonCarte.onClick.RemoveAllListeners();
        boutonCarte.onClick.AddListener(OnCarteCliquee);

        if (boutonAudio != null)
        {
            boutonAudio.onClick.RemoveAllListeners();
            boutonAudio.onClick.AddListener(EcouterSyllabeSeule);
            // Le bouton reste toujours actif et visible sur toutes les cartes
            boutonAudio.gameObject.SetActive(true);

        }
    }

    public void EcouterSyllabeSeule()
    {
        if (clipAudio != null)
        {
            gameManager.JouerSon(clipAudio);
        }
    }

    private void OnCarteCliquee()
    {
        if (clipAudio != null)
        {
            gameManager.JouerSon(clipAudio);
        }

        if (!estPlacee)
        {
            gameManager.SelectionnerSyllabe(this, syllabeValeur);
        }
        else
        {
            gameManager.RetirerSyllabe(this);
        }
    }

    public void DefinirEtatPlace(bool etat)
    {
        estPlacee = etat;
    }
}
