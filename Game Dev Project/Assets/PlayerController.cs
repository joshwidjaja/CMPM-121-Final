using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum jumpButtonState{    
        up,
        down
    }
    jumpButtonState jumpState;
    // Start is called before the first frame update
    void Start()
    {
        jumpState = jumpButtonState.up;
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
        if(Input.GetKeyDown(KeyCode.Space)){
            
            if(jumpState == jumpButtonState.up){
                gameObject.GetComponent<CropManager>().PlayerPlant(transform.position.x, transform.position.z);//Important that this is z, not y!
                jumpState = jumpButtonState.down;
            }
        }
        if(Input.GetKeyUp(KeyCode.Space)){
            jumpState = jumpButtonState.up;
        }
    }
    void triggerTurn(){ //Once player does an action, this is triggered, should randomly add water or sun to cells or whatever

    }
}
