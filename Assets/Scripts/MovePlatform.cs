using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float unitsToRight;

    private Vector3 iniPosition;
    private bool movingRight;
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        movingRight = true;
        iniPosition = transform.position;
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
        if (movingRight) { gameObject.transform.Translate(Mathf.Abs(speed) * Time.deltaTime, 0, 0) ; }
        else { gameObject.transform.Translate(-Mathf.Abs(speed) * Time.deltaTime, 0, 0); }

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

