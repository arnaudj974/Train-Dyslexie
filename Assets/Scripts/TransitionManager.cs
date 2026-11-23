using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] Tableau tableau;
    [SerializeField] PlayerInputs player;

    void Save()
    {
        tableau.Save();
        //player.Save();
    }

    void Load()
    {
        tableau.Load();
        //player.Load();
    }
}
