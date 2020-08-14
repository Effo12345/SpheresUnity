using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Serialization : MonoBehaviour
{
    public bool isMenu;
    [HideInInspector]
    public static int nextBuildIndex = 0;
    int currentBuildIndex;

    private void OnEnable()
    {
       //JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString("spheres level data"), this);

        //Debug.Log("Values read as, currentBuildIndex = " + currentBuildIndex + " and nextBuildIndex = " + nextBuildIndex);

        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;

        nextBuildIndex = PlayerPrefs.GetInt("nextBuildIndex");

        //Debug.Log("nextBuildIndex = " + nextBuildIndex + " from PlayerPrefs");

        //Debug.Log("After set, currentBuildIndex is " + currentBuildIndex);

        if (isMenu && nextBuildIndex == 0)
        {
            nextBuildIndex = 1;
        }
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
        //PlayerPrefs.SetString("spheres level data", JsonUtility.ToJson(this, true));

        PlayerPrefs.SetInt("nextBuildIndex", nextBuildIndex);
        PlayerPrefs.Save();

        //Debug.Log("Values written as, currentBuildIndex = " + currentBuildIndex + " and nextBuildIndex = " + nextBuildIndex);
    }
}
