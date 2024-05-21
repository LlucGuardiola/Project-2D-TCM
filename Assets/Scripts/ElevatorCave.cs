using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorCave : MonoBehaviour
{
  
    public Transform downPosition;
    public Transform upperPosition;
    public SpriteRenderer elevator;

    public float speed;
    bool isElevatorUp;
    bool isPlayer;
    public int which;

    void Start()
    {
       
        
    }    
    void Update()
    {

        StartElevator();



        DisplayColor();
    }
    void StartElevator()
    {
        if((Input.GetKeyDown(KeyCode.F)) && isPlayer)
        { 
            if(transform.position.y <= downPosition.position.y)
            {
                if(which==2)
                {
                    isElevatorUp = true;
                }
                else
                {
                    isElevatorUp = false;
                }
               

            }else if(transform.position.y >= upperPosition.position.y)
            {
                if (which == 2)
                {
                    isElevatorUp = false;
                }
                else
                {
                    isElevatorUp = true;
                }
            }
        }
        
        if (isElevatorUp)
        {
            if (which == 2)
            {
                transform.position = Vector2.MoveTowards(transform.position, upperPosition.position, speed * Time.deltaTime);
            }
            else
            {

                transform.position = Vector2.MoveTowards(transform.position, downPosition.position, speed * Time.deltaTime);
            }
        }
        else
        {
            if (which == 2)
            {
                transform.position = Vector2.MoveTowards(transform.position, downPosition.position, speed * Time.deltaTime);
            }
            else
            {

                transform.position = Vector2.MoveTowards(transform.position, upperPosition.position, speed * Time.deltaTime);
            }
        }
             
        
    }

    void DisplayColor()
    {
        if(transform.position.y <= downPosition.position.y|| transform.position.y >= upperPosition.position.y)
        {
            elevator.color = Color.green;
        }
        else
        {
            elevator.color = Color.red;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isPlayer = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPlayer = false;
    }
    
}
