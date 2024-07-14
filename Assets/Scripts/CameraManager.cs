using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject mapCamera;
    public GameObject equipmentViewCamera;

    public bool map;

    CanvasController canvasController;

    #region Singleton
    public static CameraManager instance;

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

    void Start()
    {
        canvasController = FindObjectOfType<CanvasController>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (canvasController.Inventory.activeInHierarchy) {
            mapCamera.SetActive(map);
            equipmentViewCamera.SetActive(true);
        }
        else {
            mapCamera.SetActive(false);
            equipmentViewCamera.SetActive(false);
        }
    }

}
