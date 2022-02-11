using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamblingBlock : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject contents;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(contents != null)
            {
                GameObject obj = Instantiate<GameObject>(contents);
                obj.transform.position = transform.position;
            }
            Destroy(gameObject);
        }
    }

}
