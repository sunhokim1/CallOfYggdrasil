using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManger : MonoBehaviour
{
    public GameObject LoginPanel, SelectPanel;

    private void Awake()
    {
        LoginPanel.SetActive(true);
        SelectPanel.SetActive(false);
    }

    public void Btn_Caracter()
    {
        LoginPanel.SetActive(false );
        SelectPanel.SetActive(true );
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
