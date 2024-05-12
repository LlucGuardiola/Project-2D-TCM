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
        
        if((Input.GetKeyDown(KeyCode.F)))
        { 
            if(transform.position.y <= downPosition.position.y)
            {
                isElevatorUp = false;

            }else if(transform.position.y >= upperPosition.position.y)
            {
                isElevatorUp = true;
            }

           
        }
        
        if (isElevatorUp)
        {
            transform.position = Vector2.MoveTowards(transform.position, downPosition.position, speed * Time.deltaTime);
        }
        else
        {

            transform.position = Vector2.MoveTowards(transform.position, upperPosition.position, speed * Time.deltaTime);
           
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
    
}
