using System.Collections;
using UnityEngine;

public class NPC_Interaction : MonoBehaviour
{
   [SerializeField] private GameObject dialouge;

    public void ActivateDialogue()
    {
        dialouge.SetActive(true);
    }

    public bool DialogueActive()
    {
        return dialouge.activeInHierarchy;
    }
}
