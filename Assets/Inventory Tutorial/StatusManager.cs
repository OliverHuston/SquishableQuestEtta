using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    public delegate void OnStatusChangedCallback();
    public OnStatusChangedCallback onStatusChangedCallback;

    public CharacterStatus playerStatus;

    #region Singleton
    public static StatusManager instance;

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

    public void UpdateCharacterStatus(Equipment newItem, Equipment oldItem)
    {
        if (oldItem != null)
        {
            playerStatus.spd -= oldItem.spdModifier;
            playerStatus.atk -= oldItem.atkModifier;
            playerStatus.def -= oldItem.defModifier;
            playerStatus.rec -= oldItem.recModifier;
            playerStatus.pty -= oldItem.ptyModifier;
        }

        playerStatus.spd = playerStatus.base_spd + newItem.spdModifier;
        playerStatus.atk = playerStatus.base_atk + newItem.atkModifier;
        playerStatus.def = playerStatus.base_def + newItem.defModifier;
        playerStatus.rec = playerStatus.base_rec + newItem.recModifier;
        playerStatus.pty = playerStatus.base_pty + newItem.ptyModifier;

        onStatusChangedCallback.Invoke();
    }
}