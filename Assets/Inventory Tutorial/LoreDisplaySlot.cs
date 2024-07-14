using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoreDisplaySlot : MonoBehaviour
{
    public enum LoreDisplayType { LOREBOOK, ENTRY }
    public LoreDisplayType loreDisplayType;


    public Lorebook lorebook;
    public Entry entry;


    public TextMeshProUGUI textMesh;
    public GameObject unreadEntriesIndicator;
    public GameObject selectedIndicator;

    // Start is called before the first frame update
    void Awake()
    {
        if(loreDisplayType == LoreDisplayType.LOREBOOK) { this.entry = null;}
        else if (loreDisplayType == LoreDisplayType.ENTRY) { this.lorebook = null; }

       
    }

    // Update is called once per frame
    void Update()
    {

        if (loreDisplayType == LoreDisplayType.LOREBOOK) { UpdateLorebookDisplay(); }
        else if (loreDisplayType == LoreDisplayType.ENTRY) { UpdateEntryDisplay(); }

        selectedIndicator.SetActive(this.GetComponent<Toggle>().isOn);
    }


    //Lorebook type display
    private void UpdateLorebookDisplay()
    {
        if (DetermineAcquiredEntries() == true)
        {
            textMesh.text = lorebook.name;
            if (DetermineUnreadEntries() == true)
            {
                unreadEntriesIndicator.SetActive(true);
            }
            else
            {
                unreadEntriesIndicator.SetActive(false);
            }
        }
        else
        {
            textMesh.text = "???";
            unreadEntriesIndicator.SetActive(false);
        }

    }

    private bool DetermineUnreadEntries()
    {
        foreach(Entry entry in lorebook.entries) {
            if (entry.unread && entry.acquired) return true;
        }
        return false;
    }

    private bool DetermineAcquiredEntries()
    {
        foreach (Entry entry in lorebook.entries)
        {
            if (entry.acquired == true) return true;
        }
        return false;
    }


    //Entry type display
    private void UpdateEntryDisplay()
    {
        if (entry.acquired == true)
        {
            textMesh.text = entry.displayedTitle;
            if (entry.unread == true)
            {
                unreadEntriesIndicator.SetActive(true);
            }
            else
            {
                unreadEntriesIndicator.SetActive(false);
            }
        }
        else
        {
            textMesh.text = "???";
            unreadEntriesIndicator.SetActive(false);
        }
    }
}
