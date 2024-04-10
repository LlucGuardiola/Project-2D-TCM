using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private Vector3 iniPosition;
    private bool movingRight;
    private float unitsToRight;
    private bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        movingRight = true;
        iniPosition = transform.position;
        unitsToRight = 8f;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }
    private void Move()
    {
        if (movingRight) { gameObject.transform.Translate( 1 * Time.deltaTime, 0,0 ); }
        else { gameObject.transform.Translate(-1 * Time.deltaTime, 0, 0); }

        if (transform.position.x >= iniPosition.x + unitsToRight || transform.position.x <= iniPosition.x)
        {
            movingRight = !movingRight;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        canMove = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        canMove = true;
    }
}

