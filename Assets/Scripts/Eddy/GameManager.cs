using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Données du jeu")]
    public List<WordData> listeMots;
    private int indexMotActuel = 0;

    [Header("Références UI")]
    public Image imageCibleUI;
    public Transform zoneConstruction;
    public Transform zonePioche;
    public GameObject prefabCarteSyllabe;
    public TextMeshProUGUI texteFeedback;
    public TextMeshProUGUI texteScore; // NOUVEAU : Référence au compteur

    [Header("Récompenses visuelles")] // NOUVEAU
    public ParticleSystem effetVictoire; // NOUVEAU : Référence aux confettis
    private int etoilesGagnees = 0; // NOUVEAU : Compteur interne

    [Header("Audio")]
    public AudioSource lecteurAudio;
    public AudioClip sonVictoire;
    public AudioClip sonErreurDoux;

    [Header("Couleurs adaptées")]
    public Color couleurSyllabe1 = new Color(0.82f, 0.91f, 0.96f); // Bleu pastel
    public Color couleurSyllabe2 = new Color(0.85f, 0.95f, 0.85f); // Vert pastel

    private List<string> reponseJoueur = new List<string>();
    private List<SyllableCard> cartesPlacees = new List<SyllableCard>();

    void Start()
    {
        ChargerMot(indexMotActuel);
    }

    public void ChargerMot(int index)
    {
        // Nettoyer les zones
        foreach (Transform child in zoneConstruction) Destroy(child.gameObject);
        foreach (Transform child in zonePioche) Destroy(child.gameObject);
        
        reponseJoueur.Clear();
        cartesPlacees.Clear();
        texteFeedback.text = "";

        WordData mot = listeMots[index];
        imageCibleUI.sprite = mot.imageObjet;
        JouerSon(mot.audioMotComplet);

        // Préparer la liste des syllabes à afficher
        List<(string texte, AudioClip audio)> poolSyllabes = new List<(string, AudioClip)>();
        
        for (int i = 0; i < mot.syllabesCorrectes.Length; i++)
        {
            AudioClip clip = (i < mot.audioSyllabes.Length) ? mot.audioSyllabes[i] : null;
            poolSyllabes.Add((mot.syllabesCorrectes[i], clip));
        }

        foreach (string intrus in mot.syllabesIntrus)
        {
            poolSyllabes.Add((intrus, null));
        }

        // Mélanger les syllabes
        for (int i = 0; i < poolSyllabes.Count; i++)
        {
            var temp = poolSyllabes[i];
            int randomIndex = Random.Range(i, poolSyllabes.Count);
            poolSyllabes[i] = poolSyllabes[randomIndex];
            poolSyllabes[randomIndex] = temp;
        }

        // Créer les cartes dans la zone de pioche
        for (int i = 0; i < poolSyllabes.Count; i++)
        {
            GameObject nouvelleCarte = Instantiate(prefabCarteSyllabe, zonePioche);
            SyllableCard scriptCarte = nouvelleCarte.GetComponent<SyllableCard>();
            Color couleur = (i % 2 == 0) ? couleurSyllabe1 : couleurSyllabe2;
            
            scriptCarte.Initialiser(poolSyllabes[i].texte, poolSyllabes[i].audio, couleur, this);
        }
    }

    public void SelectionnerSyllabe(SyllableCard carte, string valeur)
    {
        WordData motActuel = listeMots[indexMotActuel];
        if (reponseJoueur.Count >= motActuel.syllabesCorrectes.Length) return;

        carte.transform.SetParent(zoneConstruction);
        carte.DefinirEtatPlace(true);
        
        reponseJoueur.Add(valeur);
        cartesPlacees.Add(carte);

        if (reponseJoueur.Count == motActuel.syllabesCorrectes.Length)
        {
            VerifierReponse();
        }
    }

    public void RetirerSyllabe(SyllableCard carte)
    {
        int index = cartesPlacees.IndexOf(carte);
        if (index != -1)
        {
            reponseJoueur.RemoveAt(index);
            cartesPlacees.RemoveAt(index);
            carte.transform.SetParent(zonePioche);
            carte.DefinirEtatPlace(false);
            texteFeedback.text = "";
        }
    }

    private void VerifierReponse()
    {
        WordData motActuel = listeMots[indexMotActuel];
        string motForme = string.Join("", reponseJoueur);

        if (motForme == motActuel.motComplet)
        {
            texteFeedback.text = "Bravo ! C'est bien : " + motActuel.motComplet;
            JouerSon(sonVictoire);
			// NOUVEAU : Déclencher les confettis
            if (effetVictoire != null)
            {
                effetVictoire.Play();
            }

            // NOUVEAU : Ajouter une étoile et mettre à jour l'affichage
            etoilesGagnees++;
            if (texteScore != null)
            {
                texteScore.text = "Étoiles : " + etoilesGagnees;
            }
            Invoke(nameof(MotSuivant), 2.5f);
			Invoke(nameof(EndGame), 2.5f);
        }
        else
        {
            texteFeedback.text = "Écoute bien les syllabes et essaie encore.";
            JouerSon(sonErreurDoux);
			Invoke(nameof(MotActuel), 1.5f);
        }
    }
	
    private void MotActuel()
    {
        indexMotActuel = (indexMotActuel) % listeMots.Count;
        ChargerMot(indexMotActuel);
    }
	
    private void MotSuivant()
    {
        indexMotActuel = (indexMotActuel + 1) % listeMots.Count;
        ChargerMot(indexMotActuel);
    }

    public void ReecouterMot()
    {
        JouerSon(listeMots[indexMotActuel].audioMotComplet);
    }

    public void JouerSon(AudioClip clip)
    {
        if (clip != null && lecteurAudio != null)
        {
            lecteurAudio.Stop();
            lecteurAudio.PlayOneShot(clip);
        }
    }
	public void EndGame(){
		indexMotActuel = (indexMotActuel) % listeMots.Count;
		if(indexMotActuel == 0){
			SceneManager.LoadScene("EndGame");
		}
	}
}
