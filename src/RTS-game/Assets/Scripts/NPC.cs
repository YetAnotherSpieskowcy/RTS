using DialogueEditor;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private NPCConversation conversation;
    private ConversationManager mgr;
    private AIAnimation anim;
    public void StartConversation()
    {
        mgr.StartConversation(conversation);
    }
    void Awake()
    {
        mgr = GameObject.Find("ConversationManager").GetComponent<ConversationManager>();
        conversation = GetComponent<NPCConversation>();
    }
    void Update()
    {
        if (anim != null)
        {
            anim.Move(new Vector3(0, 0, 0));
        }
    }
}
