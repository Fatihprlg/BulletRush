using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 0.05f;
    float xDelta, yDelta;

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
