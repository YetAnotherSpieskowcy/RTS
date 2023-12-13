using System.Collections.Generic;
using DialogueEditor;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private NPCConversation conversation;
    private ConversationManager mgr;
    private AIAnimation anim;
    private Quest quest;
    private List<string> flags = new();

    public void StartConversation()
    {
        mgr.StartConversation(conversation);
        if (quest != null)
        {
            mgr.SetBool("QuestActive", quest.IsStarted());
            mgr.SetBool("QuestCompleted", quest.IsCompleted());
        }
        flags.ForEach(it => mgr.SetBool(it, true));
    }
    public void SetPersistantFlag(string flag, bool value)
    {
        if (!value)
        {
            flags.Remove(flag);
        }
        else
        {
            flags.Add(flag);
        }
    }
    public void SetPersistantFlag(string flag)
    {
        flags.Add(flag);
    }
    public List<string> GetFlags()
    {
        return flags;
    }
    void Awake()
    {
        mgr = GameObject.Find("ConversationManager").GetComponent<ConversationManager>();
        conversation = GetComponent<NPCConversation>();
        quest = GetComponent<Quest>();
    }
    void Update()
    {
        if (anim != null)
        {
            anim.Move(new Vector3(0, 0, 0));
        }
    }
}
