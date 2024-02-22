using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update(){}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject colGO = collision.gameObject;

        bool hasPS = colGO.TryGetComponent(out Player PM);

        Rigidbody2D rb = colGO.GetComponent<Rigidbody2D>();

        if (colGO.CompareTag("Player"))
        {
            if (hasPS)
            {
                if (!PM.dead)
                {
                    rb.velocity = new Vector2(0, 0);
                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    //screenwipe
                    //SceneManager.LoadScene("overworld");
                }
            }
        }
    }

}
