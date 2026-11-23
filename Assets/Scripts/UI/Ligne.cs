using TMPro;
using UnityEngine;

public class Ligne : MonoBehaviour
{
    [SerializeField] TMP_Dropdown[] dropdowns;

    public void Save(char c)
    {
        for (int i = 0; i < dropdowns.Length; i++)
        {
            PlayerPrefs.SetInt(c.ToString() + i.ToString(), dropdowns[i].value);
        }
    }

    public void Load(char c)
    {
        for (int i = 0; i < dropdowns.Length; i++)
        {
            dropdowns[i].value = PlayerPrefs.GetInt(c.ToString() + i.ToString(), 0);
        }
    }
}
