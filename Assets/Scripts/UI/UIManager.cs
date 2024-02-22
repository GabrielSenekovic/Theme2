using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text lives;
    public Text coins;

    public Vector3 checkPos;

    public GameObject player;

    public PortalHub portalHub;

    int lives_counter;
    int coins_counter;

    static UIManager instance;
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
    public void ChangeLives(int value)
    {
        lives_counter += value;
        lives.text = lives_counter.ToString();
    }
    public void ChangeCoins(int value)
    {
        coins_counter += value;
        if(coins_counter >= 100)
        {
            coins_counter = coins_counter - 100;
            ChangeLives(1);
        }
        coins.text = coins_counter.ToString();
    }
    public void Reset()
    {
        portalHub.Reset();
    }
}
