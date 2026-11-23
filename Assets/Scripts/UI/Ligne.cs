using TMPro;
using UnityEngine;

public class Ligne : MonoBehaviour
{
    [SerializeField] TMP_Dropdown[] dropdowns;

    public void Save(char c)
    {
        for (int i = 0; i < dropdowns.Length; i++)
        {
            PlayerPrefs.SetInt((c+i).ToString(), dropdowns[i].value);
        }
    }

    public void Load(char c)
    {
        for (int i = 0; i < dropdowns.Length; i++)
        {
            dropdowns[i].value = PlayerPrefs.GetInt((c + i).ToString(), 0);
        }
    }
}
