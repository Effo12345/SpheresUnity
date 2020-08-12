using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Serialization : MonoBehaviour
{
    public bool isMenu;
    [HideInInspector]
    public static int nextBuildIndex = 1;
    int currentBuildIndex;

    private void OnEnable()
    {
        JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("spheres level data"), this);

        //Debug.Log("Values read as, currentBuildIndex = " + currentBuildIndex + " and nextBuildIndex = " + nextBuildIndex);

        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;

        //Debug.Log("After set, currentBuildIndex is " + currentBuildIndex);
    }

    public void NextLevel()
    {
        if (nextBuildIndex <= currentBuildIndex && currentBuildIndex < 6)
        {
            nextBuildIndex ++;

            //Debug.Log("NextLevel triggered, currentBuildIndex = " + currentBuildIndex + " and nextBuildIndex = " + nextBuildIndex);
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetString("spheres level data", JsonUtility.ToJson(this, true));

        //Debug.Log("Values written as, currentBuildIndex = " + currentBuildIndex + " and nextBuildIndex = " + nextBuildIndex);
    }
}
