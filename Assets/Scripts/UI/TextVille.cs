using TMPro;
using UnityEngine;

public class TextVille : MonoBehaviour
{
    TextMeshProUGUI text;
    [SerializeField] float delaiMax = 0.2f;

    float delai = 0f;
    void Start()
    {
        text=GetComponent<TextMeshProUGUI>();
        delaiMax += Random.Range(-0.1f, 0.1f);
    }

    void Update()
    {
        if(delai>= delaiMax)
        {
            text.text = ChangeTxt();
            delai = 0f;
        }
        else
        {
            delai += Time.deltaTime;
        }
    }

    string ChangeTxt()
    {
        int taille = Random.Range(7, 17);
        char[] c = new char[taille];
        for(int i=0; i < taille; i++)
        {
            c[i] = i != 0 ? System.Char.ToLower((char)Random.Range(65, 91)) : (char)Random.Range(65, 91);
        }
        return new string(c);
    }
}
