using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] string nomSceneLose;
    [SerializeField] string nomSceneWin;
    [SerializeField] string sceneEddy;
    [SerializeField] Tableau tableau;
    [SerializeField] Ticket ticket;
    [SerializeField] PlayerMovement player;
    void Start()
    {
        if (PlayerPrefs.GetInt("inGame", 0) == 1) Load();
    }

    [ContextMenu("Save")]
    void Save()
    {
        PlayerPrefs.SetInt("inGame", 1);
        tableau.Save();
        player.Save();
        ticket.Save();
    }

    [ContextMenu("Load")]
    void Load()
    {
        tableau.Load();
        player.Load();
        ticket.Load();
    }

    [ContextMenu("Clear")]
    void Clear()
    {
        PlayerPrefs.DeleteAll();
    }

    public void ChangeToSceneEddy()
    {
        Save();
        SceneManager.LoadSceneAsync(sceneEddy);
    }

    public void ChangeToSceneLose()
    {
        SceneManager.LoadSceneAsync(nomSceneLose);
    }
    public void ChangeToSceneWin()
    {
        SceneManager.LoadSceneAsync(nomSceneWin);
    }


}
