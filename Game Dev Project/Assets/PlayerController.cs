using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum moveState{    
        still,
        moving
    }
    moveState playerMoveState;
    // Start is called before the first frame update
    void Start()
    {
        playerMoveState = moveState.still;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            transform.Translate(new Vector3(0.0f, 0.0f, 1.0f));
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            transform.Translate(new Vector3(0.0f, 0.0f, -1.0f));
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            transform.Translate(new Vector3(-1.0f, 0.0f, 0.0f));
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            transform.Translate(new Vector3(1.0f, 0.0f, 0.0f));
        }
    }
    void triggerTurn(){

    }
}
