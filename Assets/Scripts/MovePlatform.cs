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
    [SerializeField] private float unitsToRight;
    [SerializeField] private bool startsMovingRight;
    [SerializeField] private bool canMove;

    private Vector3 iniPosition;

    // Start is called before the first frame update
    void Start()
    {
        gameObj = this.GameObject();
        iniPosition = transform.position;
        canMove = true;
        gameObj.AddComponent<Rigidbody2D>().GetComponent<Rigidbody2D>().gravityScale = 0;
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
        if (startsMovingRight) { gameObject.transform.Translate(Mathf.Abs(speed) * Time.deltaTime, 0, 0) ; }
        else { gameObject.transform.Translate(-Mathf.Abs(speed) * Time.deltaTime, 0, 0); }

        if (transform.position.x >= iniPosition.x + unitsToRight || transform.position.x <= iniPosition.x)
        {
            startsMovingRight = !startsMovingRight;
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

