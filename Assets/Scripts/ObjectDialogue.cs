using UnityEngine;

[CreateAssetMenu (fileName ="NewNPCDialogue", menuName = "NPC Dialogue")]
public class ObjectDialogue : ScriptableObject

{
    public string npcName;
    public Sprite npcPotrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines;
    public float autoProgressDelay = 1.5f;
    public float typingSpeed = 0.05f;
    public AudioClip VoiceSound;
    public float voicePitch = 1f;

}
