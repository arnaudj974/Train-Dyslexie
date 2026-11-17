using UnityEngine;
using UnityEngine.UIElements; // Indispensable pour UI Toolkit
using UnityEngine.SceneManagement; // Indispensable pour changer de scène

public class PauseGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
	
	private Button continuer;
	private Button rejouer;
	private Button quitter;
	private UIDocument uiDocument;
	private VisualElement root;
	
    void Start()
    {
        uiDocument = GetComponent<UIDocument>();
		root = uiDocument.rootVisualElement;
		continuer = root.Q<Button>("Continuer");
		rejouer = root.Q<Button>("Rejouer");
		quitter = root.Q<Button>("Quitter");
		//monBouton.clicked += OnBoutonClique;
		continuer.clicked += Continue;
		rejouer.clicked += Rejoue;
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
			Pause();
		}
		
    }
	
	private void Pause(){
		Time.timeScale = 0f;
		// Pour afficher le bouton
		continuer.style.display = DisplayStyle.Flex;
		rejouer.style.display = DisplayStyle.Flex;
		quitter.style.display = DisplayStyle.Flex;
	}
	private void Continue(){
		Time.timeScale = 1f;
		continuer.style.display = DisplayStyle.None;
		rejouer.style.display = DisplayStyle.None;
		quitter.style.display = DisplayStyle.None;
	}
	
	private void Rejoue(){
		Time.timeScale = 1f;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		//SceneManager.LoadScene("Game");
		Debug.Log("Scène rejoué");
	}
	
}
