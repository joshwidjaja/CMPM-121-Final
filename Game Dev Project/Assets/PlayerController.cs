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

    public CropManager cropManager;

    // Start is called before the first frame update
    void Start()
    {
        jumpState = jumpButtonState.up;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            transform.Translate(Vector3.forward);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow)){
            transform.Translate(Vector3.back);
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            transform.Translate(Vector3.left);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            transform.Translate(Vector3.right);
        }
        if(Input.GetKeyDown(KeyCode.Z)){
            
            if(jumpState == jumpButtonState.up){
                cropManager.PlayerPlant(transform.position.x, transform.position.z);//Important that this is z, not y!
                jumpState = jumpButtonState.down;
            }
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            if (jumpState == jumpButtonState.up) {
                cropManager.PlayerHarvest(transform.position.x, transform.position.z);
                jumpState = jumpButtonState.down;
            }
        }
        if(Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.X)){
            jumpState = jumpButtonState.up;
            cropManager.triggerTurn();
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) {
            cropManager.triggerTurn();
        }
    }
}
