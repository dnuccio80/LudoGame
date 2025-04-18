using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {

        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (!rayHit.collider) return;

        if(rayHit.collider.gameObject.TryGetComponent(out TokenScript token)){
            if (GameManager.instance.GetCurrentPlayerIsCpu()) return;
            token.TryMovePiece();
        }

        if(rayHit.collider.gameObject.TryGetComponent(out DiceLogic die))
        {
            if (GameManager.instance.GetCurrentPlayerIsCpu()) return;
            die.RollDice();
        }
    }


}
