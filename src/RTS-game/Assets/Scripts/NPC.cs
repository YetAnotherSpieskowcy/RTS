using DialogueEditor;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private NPCConversation conversation;
    private ConversationManager mgr;
    public void StartConversation()
    {
        mgr.StartConversation(conversation);
    }
    void Awake()
    {
        mgr = GameObject.Find("ConversationManager").GetComponent<ConversationManager>();
        conversation = GetComponent<NPCConversation>();
    }
}
