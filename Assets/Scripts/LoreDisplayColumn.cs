using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class LoreDisplayColumn : MonoBehaviour
{
    public enum LoreColumnDisplayType { LOREBOOK, ENTRY, TEXT }
    public LoreColumnDisplayType loreDisplayType;

    public LoreDisplayColumn childColumn;

    public GameObject lorebookDisplaySlotPrefab;
    public GameObject scrollerContent;

    public TextMeshProUGUI numberUnlockedDisplay;



    void Start()
    {
        if (loreDisplayType == LoreColumnDisplayType.LOREBOOK) { InitializeLorebookDisplaySlots(); }
    }

    private void OnEnable()
    {
        ResetScrolling();
    }


    void ToggleValueChanged(Toggle change)
    {
        if(change.isOn)
        {
            UpdateChildColumn(change.gameObject.GetComponent<LoreDisplaySlot>());
        }
    }

    void ResetScrolling()
    {
        RectTransform rt = scrollerContent.GetComponent<RectTransform>();
        rt.position = new Vector3(rt.transform.position.x, 0, rt.transform.position.z);
    }

    


    public void UpdateChildColumn(LoreDisplaySlot lds)
    {
        if (childColumn == null) return;

        if (childColumn.loreDisplayType == LoreColumnDisplayType.ENTRY) { childColumn.InitializeEntryDisplaySlots(lds.lorebook); }
        else if (childColumn.loreDisplayType == LoreColumnDisplayType.TEXT) {
            childColumn.DisplayText(lds.entry);
        }
    }


    private void InitializeLorebookDisplaySlots()
    {
        DestroyAllChildren(scrollerContent);

        ToggleGroup tg = this.GetComponent<ToggleGroup>();
        int count = 0;

        foreach (Lorebook lorebook in LorebookManager.instance.lorebooks)
        {
            GameObject newPrefab = GameObject.Instantiate(lorebookDisplaySlotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newPrefab.transform.SetParent(scrollerContent.transform);
            newPrefab.transform.position = scrollerContent.transform.position + new Vector3(300, -60 + -110 * count, 0);
            LoreDisplaySlot newLorebookSlot = newPrefab.GetComponent<LoreDisplaySlot>();
            newLorebookSlot.loreDisplayType = LoreDisplaySlot.LoreDisplayType.LOREBOOK;
            newLorebookSlot.lorebook = lorebook;
            newLorebookSlot.entry = null;

            Toggle tog = newPrefab.GetComponent<Toggle>();
            tog.group = tg;
            tog.onValueChanged.AddListener(delegate {
                ToggleValueChanged(tog);
            });

            if (count == 0) UpdateChildColumn(newLorebookSlot);

            count++;
        }

        RectTransform rt = scrollerContent.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0, 5 + count * 110);
        rt.position = new Vector3(rt.transform.position.x, 0, rt.transform.position.z);
    }

    private void InitializeEntryDisplaySlots(Lorebook lorebook)
    {
        DestroyAllChildren(scrollerContent);

        ToggleGroup tg = this.GetComponent<ToggleGroup>();
        int count = 0;
        int acquiredCount = 0;


        foreach (Entry entry in lorebook.entries)
        {
            GameObject newPrefab = GameObject.Instantiate(lorebookDisplaySlotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            newPrefab.transform.SetParent(scrollerContent.transform);
            newPrefab.transform.position = scrollerContent.transform.position + new Vector3(300, -60 + -110 * count, 0);
            LoreDisplaySlot newLorebookSlot = newPrefab.GetComponent<LoreDisplaySlot>();
            newLorebookSlot.loreDisplayType = LoreDisplaySlot.LoreDisplayType.ENTRY;
            newLorebookSlot.entry = entry;
            newLorebookSlot.lorebook = null;
            if (newLorebookSlot.entry.acquired) acquiredCount++;

            Toggle tog = newPrefab.GetComponent<Toggle>();
            tog.group = tg;
            tog.onValueChanged.AddListener(delegate {
                ToggleValueChanged(tog);
            });

            if (count == 0) tog.isOn = true;

            count++;
        }

        RectTransform rt = scrollerContent.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(0, 5 + count * 110);
        rt.position = new Vector3(rt.transform.position.x, 0, rt.transform.position.z);

        numberUnlockedDisplay.text = acquiredCount + "/" + count;
    }

    private void DisplayText(Entry entry)
    {
        TextMeshProUGUI tm = scrollerContent.GetComponent<TextMeshProUGUI>();
        if (entry.acquired) { tm.text = entry.text; }
        else { tm.text = "This entry remains undiscovered."; }
        tm.ForceMeshUpdate();

        RectTransform rt = scrollerContent.GetComponent<RectTransform>();      
        rt.sizeDelta = new Vector2(0, tm.textBounds.size.y);
        rt.position = new Vector3(rt.transform.position.x, 0, rt.transform.position.z);

        if (entry.unread && entry.acquired) entry.unread = false;
    }

    private void DestroyAllChildren(GameObject g)
    {
        for(int i = 0; i < g.transform.childCount; i++)
        {
            Destroy(g.transform.GetChild(i).gameObject);
        }

    }
}
