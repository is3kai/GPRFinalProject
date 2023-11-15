using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
   public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
