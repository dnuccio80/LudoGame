using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WayScript : MonoBehaviour
{

    private bool isOccupied;
    private List<TokenScript> tokenInPlaceList;

    private bool isSecureZone;
    private bool isGoalZone;


    private void Start()
    {
        isSecureZone = gameObject.TryGetComponent(out SecureZone secureZone);
        isGoalZone = gameObject.TryGetComponent(out Goal goal);
        tokenInPlaceList = new List<TokenScript>();
    }

    public void OccupyPosition(TokenScript token)
    {
        if(isGoalZone)
        {
            GameManager.instance.SamePlayerAgain();
        }

        if(!isSecureZone)
        {
            if(tokenInPlaceList.Count > 0)
            {
                if(tokenInPlaceList[0].GetColorPlayer() != token.GetColorPlayer())
                {
                    tokenInPlaceList[0].TokenCaptured();
                    tokenInPlaceList.Add(token);
                    return;
                } else
                {
                    tokenInPlaceList.Add(token);
                }
            } else
            {
                tokenInPlaceList.Add(token);
            }

        } else
        {
            tokenInPlaceList.Add(token);
        }

        GameManager.instance.EndTurn();
        
    }

    public void RemoveOcuppyPosition(TokenScript token)
    {
        tokenInPlaceList.Remove(token);
    }

    public bool GetIfSecureZone()
    {
        return isSecureZone;
    }

}
