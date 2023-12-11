using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum JumpButtonState
    {
        up,
        down
    }

    private JumpButtonState jumpState;

    public CropManager cropManager;

    // Start is called before the first frame update
    private void Start()
    {
        jumpState = JumpButtonState.up;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {

            if (jumpState == JumpButtonState.up)
            {
                cropManager.PlayerPlant(transform.position.x, transform.position.z);//Important that this is z, not y!
                jumpState = JumpButtonState.down;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (jumpState == JumpButtonState.up)
            {
                cropManager.PlayerHarvest(transform.position.x, transform.position.z);
                jumpState = JumpButtonState.down;
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            cropManager.Undo();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            cropManager.Redo();
        }
        if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.X))
        {
            jumpState = JumpButtonState.up;
            cropManager.TriggerTurn();
        }
    }

    public void MoveUp()
    {
        transform.Translate(Vector3.forward);
        cropManager.TriggerTurn();
    }

    public void MoveDown()
    {
        transform.Translate(Vector3.back);
        cropManager.TriggerTurn();
    }

    public void MoveLeft()
    {
        transform.Translate(Vector3.left);
        cropManager.TriggerTurn();
    }

    public void MoveRight()
    {
        transform.Translate(Vector3.right);
        cropManager.TriggerTurn();
    }
}
