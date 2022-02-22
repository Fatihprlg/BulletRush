using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.3f;
    float xDelta, yDelta;
    void Start()
    {
           
    }

    void Update()
    {
        
        if (SwipeManager.isDraging)
        {
            xDelta = SwipeManager.x * Time.deltaTime * moveSpeed;
            yDelta = SwipeManager.y * Time.deltaTime * moveSpeed;
            transform.Translate(xDelta, 0, yDelta);
        }        
    }
}
