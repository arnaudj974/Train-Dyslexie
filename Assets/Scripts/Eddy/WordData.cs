using UnityEngine;

[CreateAssetMenu(fileName = "NouveauMot", menuName = "JeuDyslexie/Mot")]
public class WordData : ScriptableObject
{
    public string motComplet;
    public Sprite imageObjet;
    public AudioClip audioMotComplet;
    
    [Header("Découpage en syllabes")]
    public string[] syllabesCorrectes;
    public AudioClip[] audioSyllabes;
    
    [Header("Syllabes pièges (optionnel)")]
    public string[] syllabesIntrus;
}
