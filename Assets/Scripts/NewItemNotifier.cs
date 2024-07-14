using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewItemNotifier : MonoBehaviour
{
    #region Singleton
    public static NewItemNotifier instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    public float newItemDisplayTime;


    public GameObject itemDisplaySlotPrefab;
    public GameObject loreDisplaySlotPrefab;

    

    public void NotifyOfNewItem(Item newItem) {
        GameObject newPrefab = GameObject.Instantiate(itemDisplaySlotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newPrefab.transform.SetParent(this.transform);
        newPrefab.transform.position = this.transform.position;
        ItemDisplaySlot displaySlot = newPrefab.GetComponent<ItemDisplaySlot>();
        displaySlot.item = newItem;
        newPrefab.GetComponent<Button>().interactable = false;

        StartCoroutine(WaitThenDestroy(newPrefab));
    }

    public void NotifyOfNewLoreEntry(Entry newEntry)
    {
        GameObject newPrefab = GameObject.Instantiate(loreDisplaySlotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newPrefab.transform.SetParent(this.transform);
        newPrefab.transform.position = this.transform.position;

        StartCoroutine(WaitThenDestroy(newPrefab));
    }

    IEnumerator WaitThenDestroy(GameObject g)
    {
        yield return new WaitForSeconds(newItemDisplayTime);
        Destroy(g);
    }
}
