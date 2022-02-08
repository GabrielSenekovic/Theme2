using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    public SpriteRenderer number;
    public Sprite[] numbers = new Sprite[4];
    int counter;
    int counter_limit = 60;
    int index = 0;
    public bool count;

    private void FixedUpdate()
    {
        if(count && index < numbers.Length)
        {
            counter++;
            if(counter >= counter_limit)
            {
                counter = 0;
                index++;
                number.sprite = numbers[index];
            }
        }
    }
}
