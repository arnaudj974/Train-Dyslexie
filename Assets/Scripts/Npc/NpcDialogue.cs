using UnityEngine;

[CreateAssetMenu(fileName = "Diag ", menuName = "JeuDyslexie/NpcDialogue")]
public class NpcDialogue : ScriptableObject
{
    public string npcName;
    public string[] dialoguesLines;
    public float typingSpeed = 0.05f;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
}
