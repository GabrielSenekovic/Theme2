using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Code made by Gabriel Senekovic
public class Background : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> backgrounds;
    public void Update()
    {
        float width = backgrounds[0].sprite.rect.width;
        for(int i = 0; i < backgrounds.Count; i++)
        {
            if(!backgrounds[i].isVisible)
            {
                Vector2 directionToCamera = Camera.main.transform.position - backgrounds[i].transform.position;
                int horizontalDir = (int)Mathf.Sign(directionToCamera.x);
                backgrounds[i].transform.position = 
                    new Vector2(backgrounds[i].transform.position.x + width /16 * 3 * horizontalDir, Camera.main.transform.position.y);
            }
            else
            {
                backgrounds[i].transform.position =
                    new Vector2(backgrounds[i].transform.position.x, Camera.main.transform.position.y);
            }
        }
    }
}
