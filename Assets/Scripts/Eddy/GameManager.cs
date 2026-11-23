using System.Collections.Generic;
using System.Collections;
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
    public TextMeshProUGUI texteScore;

    [Header("Récompenses visuelles")]
    public ParticleSystem effetVictoire;
    private int etoilesGagnees = 0;

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
        if (listeMots != null && listeMots.Count > 0)
        {
            ChargerMot(indexMotActuel);
        }
    }

    public void ChargerMot(int index)
    {
        // 1. Nettoyer les zones précédentes
        foreach (Transform child in zoneConstruction) Destroy(child.gameObject);
        foreach (Transform child in zonePioche) Destroy(child.gameObject);
        
        reponseJoueur.Clear();
        cartesPlacees.Clear();
        if (texteFeedback != null) texteFeedback.text = "";

        // 2. Charger les informations du mot actuel
        WordData mot = listeMots[index];
        if (imageCibleUI != null) imageCibleUI.sprite = mot.imageObjet;
		imageCibleUI.color = new Color(1f, 1f, 1f, 1f);
        JouerSon(mot.audioMotComplet);

        // 3. Préparer la liste globale des syllabes (justes + intrus)
        List<(string texte, AudioClip audio)> poolSyllabes = new List<(string, AudioClip)>();
        
        // Ajouter les syllabes justes
        for (int i = 0; i < mot.syllabesCorrectes.Length; i++)
        {
            AudioClip clip = (mot.audioSyllabes != null && i < mot.audioSyllabes.Length) 
                ? mot.audioSyllabes[i] 
                : null;
                
            poolSyllabes.Add((mot.syllabesCorrectes[i], clip));
        }

        // Ajouter les syllabes intruses avec leur fichier audio respectif
        if (mot.syllabesIntrus != null)
        {
            for (int i = 0; i < mot.syllabesIntrus.Length; i++)
            {
                AudioClip clipIntrus = (mot.audioSyllabesIntrus != null && i < mot.audioSyllabesIntrus.Length) 
                    ? mot.audioSyllabesIntrus[i] 
                    : null;
                    
                poolSyllabes.Add((mot.syllabesIntrus[i], clipIntrus));
            }
        }

        // 4. Mélanger aléatoirement toutes les syllabes
        for (int i = 0; i < poolSyllabes.Count; i++)
        {
            var temp = poolSyllabes[i];
            int randomIndex = Random.Range(i, poolSyllabes.Count);
            poolSyllabes[i] = poolSyllabes[randomIndex];
            poolSyllabes[randomIndex] = temp;
        }

        // 5. Créer les cartes dans la zone de pioche
        for (int i = 0; i < poolSyllabes.Count; i++)
        {
            GameObject nouvelleCarte = Instantiate(prefabCarteSyllabe, zonePioche);
            SyllableCard scriptCarte = nouvelleCarte.GetComponent<SyllableCard>();
            
            Color couleurChoisie = (i % 2 == 0) ? couleurSyllabe1 : couleurSyllabe2;
            scriptCarte.Initialiser(poolSyllabes[i].texte, poolSyllabes[i].audio, couleurChoisie, this);

            // Personnaliser la couleur au clic (Pressed Color)
            Button btn = nouvelleCarte.GetComponent<Button>();
            if (btn != null)
            {
                ColorBlock blocCouleurs = btn.colors;
                if (couleurChoisie == couleurSyllabe1)
                {
                    blocCouleurs.pressedColor = new Color(0f, 0.5f, 1f);
                }
                else
                {
                    blocCouleurs.pressedColor = new Color(0.2f, 0.7f, 0.3f);
                }
                btn.colors = blocCouleurs;
            }
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
            if (texteFeedback != null) texteFeedback.text = "";
        }
    }

    private void VerifierReponse()
    {
        WordData motActuel = listeMots[indexMotActuel];
        string motForme = string.Join("", reponseJoueur);

        if (motForme == motActuel.motComplet)
        {
            if (texteFeedback != null) texteFeedback.text = "Bravo ! C'est bien : " + motActuel.motComplet;
			//JouerSon(sonVictoire);

            
            if (effetVictoire != null)
            {
                effetVictoire.Play();
            }

            etoilesGagnees++;
            if (texteScore != null)
            {
                texteScore.text = "Indices Trouvés : " + etoilesGagnees;
            }

            Invoke(nameof(MotSuivant), 2.5f);
			Invoke(nameof(ReturnGame), 2.5f);
        }
        else
        {
            if (texteFeedback != null) texteFeedback.text = "Écoute bien les syllabes et essaie encore.";
            //JouerSon(sonErreurDoux);
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
        if (listeMots != null && listeMots.Count > 0)
        {
            JouerSon(listeMots[indexMotActuel].audioMotComplet);
        }
    }

    public void JouerSon(AudioClip clip)
    {
        if (clip != null && lecteurAudio != null)
        {
            lecteurAudio.Stop();
            lecteurAudio.PlayOneShot(clip);
        }
    }
	public void ReturnGame(){
		indexMotActuel = (indexMotActuel) % listeMots.Count;
		if(indexMotActuel == 0){
			//SceneManager.LoadScene("Game");
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
		}
	}
	

}
