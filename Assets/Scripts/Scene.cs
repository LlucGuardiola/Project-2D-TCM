using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    string[] scenes = new string[] { "1_Aimar", "2_Lluc", "3_JanB", "4_Edgar"};
    void Awake()
    {
        foreach (var item in scenes)
        {
            SceneManager.LoadScene(item, LoadSceneMode.Additive);
        }
    }
}
