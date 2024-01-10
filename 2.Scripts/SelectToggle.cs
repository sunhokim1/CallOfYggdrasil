using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectToggle : MonoBehaviour
{
    public int CharacterIndex;
    public UserData userData;
    //public AudioSource selectSFX;

    public void ToggleSelected()
    {

        userData.charIndex = CharacterIndex;
        //selectSFX.Play();


    }
}
