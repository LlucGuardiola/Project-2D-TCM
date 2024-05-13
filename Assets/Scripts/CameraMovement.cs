using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject Player;
    public Vector2 followOffset;
    private Vector2 threshold;
    private Vector2 distanceVector;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        threshold = CalculateThreshold();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 follow = Player.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if (Mathf.Abs(xDifference) >= threshold.x)
        {
            newPosition.x = follow.x;
        }
        if (Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }

        distanceVector = Player.transform.position - transform.position; 

        distance = distanceVector.magnitude * .2f;
        float moveSpeed = Player.GetComponent<PlayerMovement>().Speed;

        transform.position = Vector3.MoveTowards(transform.position, 
                       newPosition, moveSpeed * Time.deltaTime * distance);
        
    }
    private Vector3 CalculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 t = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        t.x -= followOffset.x;
        t.y -= followOffset.y;
        return t;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}