using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class inventoryScript : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject inventorySwordUI;
    public GameObject inventoryStaffUI;
    public GameObject inventoryHelmetUI;
    public GameObject inventoryChestplateUI;
    public GameObject inventoryLeggingsUI;
    public GameObject inventoryBootsUI;
    public GameObject normalUI;
    public GameObject player;

    private bool inInventory = false;
    void Start()
    {
        inventoryUI.SetActive(false);
        inventorySwordUI.SetActive(false);
        inventoryStaffUI.SetActive(false);
        inventoryHelmetUI.SetActive(false);
        inventoryChestplateUI.SetActive(false);
        inventoryLeggingsUI.SetActive(false);
        inventoryBootsUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inInventory)
                ResumeGame();
            else
                OpenInventory();
        }
    }

    void OpenInventory()
    {
        Time.timeScale = 0f;
        inventoryUI.SetActive(true);
        normalUI.SetActive(false);
        CheckItemsGained();
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        inventoryUI.SetActive(false);
        inventorySwordUI.SetActive(false);
        inventoryStaffUI.SetActive(false);
        inventoryHelmetUI.SetActive(false);
        inventoryChestplateUI.SetActive(false);
        inventoryBootsUI.SetActive(false);
        inventoryLeggingsUI.SetActive(false);
        normalUI.SetActive(false);
    }

    void CheckItemsGained()
    {

    }
}
