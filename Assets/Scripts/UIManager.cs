using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class UIManager : MonoBehaviour
{
    Text timer;
    public Text lives;
    public Text coins;
    int lives_counter;
    int coins_counter;
    public Tilemap tileMap;
    public Tilemap contentMap;
    public Tilemap smallTileMap;
    public AudioClip death;

    static UIManager instance;

    public Vector3 checkPos;

    public GameObject player;

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(!instance)
        {
            DontDestroyOnLoad(this);
            instance = this;
            instance.lives_counter = 3;
            instance.coins_counter = 0;
            instance.lives.text = instance.lives_counter.ToString();
            instance.coins.text = instance.coins_counter.ToString();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() 
    {
        checkPos = new Vector3(0f,0f,100f);
    }

    public static void ChangeLives(int value)
    {
        instance.lives_counter += value;
        instance.lives.text = instance.lives_counter.ToString();
    }
    public static void ChangeCoins(int value)
    {
        instance.coins_counter += value;
        if(instance.coins_counter >= 100)
        {
            instance.coins_counter = instance.coins_counter - 100;
            ChangeLives(1);
        }
        instance.coins.text = instance.coins_counter.ToString();
    }

    public static void LoadScene()
    {
        AudioManager.PlaySound(instance.death);
        instance.StartCoroutine(LoadSceneAsync());
    }

    static IEnumerator LoadSceneAsync()
    {
        yield return new WaitForSeconds(1.0f);
        Debug.Log("Hi");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
