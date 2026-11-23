using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] string sceneEddy;
    [SerializeField] Tableau tableau;
    //[SerializeField] Player player;
    void Start()
    {
        if (PlayerPrefs.GetInt("inGame") == 1) Load();
    }
    void Save()
    {
        PlayerPrefs.SetInt("inGame", 1);
        tableau.Save();
        //player.Save();
    }

    void Load()
    {
        tableau.Load();
        //player.Load();
    }

    public void ChangeToSceneEddy()
    {
        Save();
        SceneManager.LoadSceneAsync(sceneEddy);
    }
}
