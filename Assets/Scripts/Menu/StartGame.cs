using UnityEngine;
using UnityEngine.UIElements; // Indispensable pour UI Toolkit
using UnityEngine.SceneManagement; // Indispensable pour changer de scène

public class StartGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
	
	private Button monBouton;
	private UIDocument uiDocument;
	private VisualElement root; 
	
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
		root = uiDocument.rootVisualElement;
		monBouton = root.Q<Button>("Jouer");
		monBouton.clicked += OnBoutonClique;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	    private void OnBoutonClique()
    {
        Debug.Log("Clic détecté sur : " + monBouton.text + ". Chargement de la scène...");
        SceneManager.LoadScene("Game");
    }
}

/*
using UnityEngine;
using UnityEngine.UIElements; // Indispensable pour UI Toolkit
using UnityEngine.SceneManagement; // Indispensable pour changer de scène

public class MenuController : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("Le nom exact de la scène à charger")]
    public string nomDeLaScene = "Jeu";

    [Tooltip("Le nom (Name) du bouton défini dans UI Builder")]
    public string nomDuBouton = "BtnJouer";

    private Button monBouton;

    private void OnEnable()
    {
        // 1. Récupérer le composant UIDocument attaché à cet objet
        UIDocument uiDocument = GetComponent<UIDocument>();

        if (uiDocument == null)
        {
            Debug.LogError("Aucun composant UIDocument trouvé sur cet objet !");
            return;
        }

        // 2. Accéder au rootVisualElement (la racine de l'interface)
        VisualElement root = uiDocument.rootVisualElement;

        // 3. Trouver le bouton par son nom (Query)
        monBouton = root.Q<Button>(nomDuBouton);

        if (monBouton != null)
        {
            // Afficher le texte actuel du bouton dans la console
            Debug.Log("Texte du bouton trouvé : " + monBouton.text);

            // (Optionnel) Modifier le texte du bouton par code :
            // monBouton.text = "Lancer la partie";

            // 4. Abonner une fonction à l'événement du clic
            monBouton.clicked += OnBoutonClique;
        }
        else
        {
            Debug.LogError($"Le bouton nommé '{nomDuBouton}' est introuvable dans l'UXML !");
        }
    }

    private void OnDisable()
    {
        // Toujours désabonner l'événement pour éviter les fuites de mémoire
        if (monBouton != null)
        {
            monBouton.clicked -= OnBoutonClique;
        }
    }

    // Fonction exécutée lors du clic
    private void OnBoutonClique()
    {
        Debug.Log("Clic détecté sur : " + monBouton.text + ". Chargement de la scène...");
        SceneManager.LoadScene("Game");
    }
}

*/