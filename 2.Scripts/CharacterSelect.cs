using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public int i = 0;
    public UserData userData;
    public Image characterImage0;
    public Image characterImage1;
    public Image characterImage2;
    public Image characterImage3;
    public Image characterImage4;
    public Image characterImage5;
    public Image characterImage6;
    public Image characterImage7;
    public Image characterImage8;
    public Image characterImage9;
    
    public void CharacterSelectRight()
    {
        if (i >= 0 && i < 10)
            i++;
        if (i == 10)
            i = 0;
        userData.charIndex = i;
        CharacterImageChange(i);
    }

    public void CharacterSelectLeft()
    {
        if (i >= 0 && i < 10)
            i--;
        if (i == -1)
            i = 9;
        userData.charIndex = i;
        CharacterImageChange(i);
    }

    public void CharacterImageChange(int i)
    {
        if (i == 0)
        {
            characterImage9.gameObject.SetActive(false);
            characterImage0.gameObject.SetActive(true);
            characterImage1.gameObject.SetActive(false);
        }
        if (i == 1)
        {
            characterImage0.gameObject.SetActive(false);
            characterImage1.gameObject.SetActive(true);
            characterImage2.gameObject.SetActive(false);
        }
        if (i == 2)
        {
            characterImage1.gameObject.SetActive(false);
            characterImage2.gameObject.SetActive(true);
            characterImage3.gameObject.SetActive(false);
        }
        if (i == 3)
        {
            characterImage2.gameObject.SetActive(false);
            characterImage3.gameObject.SetActive(true);
            characterImage4.gameObject.SetActive(false);
        }
        if (i == 4)
        {
            characterImage3.gameObject.SetActive(false);
            characterImage4.gameObject.SetActive(true);
            characterImage5.gameObject.SetActive(false);
        }
        if (i == 5)
        {
            characterImage4.gameObject.SetActive(false);
            characterImage5.gameObject.SetActive(true);
            characterImage6.gameObject.SetActive(false);
        }
        if (i == 6)
        {
            characterImage5.gameObject.SetActive(false);
            characterImage6.gameObject.SetActive(true);
            characterImage7.gameObject.SetActive(false);
        }
        if (i == 7)
        {
            characterImage6.gameObject.SetActive(false);
            characterImage7.gameObject.SetActive(true);
            characterImage8.gameObject.SetActive(false);
        }
        if (i == 8)
        {
            characterImage7.gameObject.SetActive(false);
            characterImage8.gameObject.SetActive(true);
            characterImage9.gameObject.SetActive(false);
        }
        if (i == 9)
        {
            characterImage8.gameObject.SetActive(false);
            characterImage9.gameObject.SetActive(true);
            characterImage0.gameObject.SetActive(false);
        }
    }
}
