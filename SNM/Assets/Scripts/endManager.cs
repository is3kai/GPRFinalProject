using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class endManager : MonoBehaviour
{
    public GameObject deathScreen;
    public void LoadLevel()
    {
        deathScreen.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
