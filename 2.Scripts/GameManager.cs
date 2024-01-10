using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject chatUI, talkUI, miniMap, inventoryUI, questUI,quizUI;
    public bool isMapOpen = false;
    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
        miniMap.SetActive(false);
        chatUI.SetActive(true);
        talkUI.SetActive(false);
        inventoryUI.SetActive(false);
        questUI.SetActive(false);
        quizUI.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeInHierarchy);

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            miniMap.SetActive(!miniMap.activeInHierarchy);
           
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            chatUI.SetActive(!chatUI.activeInHierarchy);
            
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            questUI.SetActive(!questUI.activeInHierarchy);
            
        }
    }
}
