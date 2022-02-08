using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    Text timer;
    public Text lives;
    Text coins;
    int lives_counter;

    static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Start()
    {
        if(!instance)
        {
            DontDestroyOnLoad(this);
            instance = this;
            instance.lives_counter = 3;
            instance.lives.text = instance.lives_counter.ToString();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static void ChangeLives(int value)
    {
        instance.lives_counter += value;
        instance.lives.text = instance.lives_counter.ToString();
    }

    public static void LoadScene()
    {
        instance.StartCoroutine(LoadSceneAsync());
    }

    static IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Hi");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
