using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LorebookManager : MonoBehaviour
{
    public List<Lorebook> lorebooks = new List<Lorebook>();

    #region Singleton
    public static LorebookManager instance;

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

    public void AcquireEntry(Entry entry)
    {
        if (entry.acquired) return;

        entry.acquired = true;
        entry.unread = true;
        
    }

}
