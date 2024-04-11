using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

public class MovePlatform : MonoBehaviour
{
    private GameObject gameObj; 
    [SerializeField] private float speed;
    [SerializeField] private float distanceToMove;
    [SerializeField] private bool startsMovingRight;
    [SerializeField] private bool canMove;

    private bool movingRight;
    private bool collided;
    private Vector3 iniPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameObj = this.GameObject(); 
        iniPosition = transform.position;
        gameObj.AddComponent<Rigidbody2D>().GetComponent<Rigidbody2D>().gravityScale = 0;
        movingRight = startsMovingRight;
        gameObj.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
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

        if (startsMovingRight && (transform.position.x >= iniPosition.x + distanceToMove || transform.position.x <= iniPosition.x))
        {
            movingRight = !movingRight;
        }
        else if (!startsMovingRight && (transform.position.x <= iniPosition.x - distanceToMove || transform.position.x >= iniPosition.x))
        {
            movingRight = !movingRight;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) { collided = true; }
        canMove = false;
    } 
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collided) { canMove = false; }
        else { canMove = true; }
    }
}

