using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WayScript : MonoBehaviour
{

    private List<TokenScript> tokenInPlaceList;

    private bool isSecureZone;
    private bool isFinalWayZone;
    private bool isGoalZone;

    private enum AlignType
    {
        Horizontal,
        Vertical
    }

    [SerializeField] private AlignType alignType;

    private void Start()
    {
        isSecureZone = gameObject.TryGetComponent(out SecureZone secureZone);
        isFinalWayZone = gameObject.TryGetComponent(out FinalWay finalWayZone);
        isGoalZone = gameObject.TryGetComponent(out Goal goal);
        tokenInPlaceList = new List<TokenScript>();
    }

    public void OccupyPosition(TokenScript token)
    {
        if(isGoalZone)
        {
            GameManager.instance.SamePlayerAgain();
            tokenInPlaceList.Add(token);
            DistributeTokens();
            return;
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
                    DistributeTokens();
                }
            } else
            {
                tokenInPlaceList.Add(token);
            }

        } else
        {
            tokenInPlaceList.Add(token);
            DistributeTokens();
        }

        GameManager.instance.EndTurn();
        
    }

    public void RemoveOcuppyPosition(TokenScript token)
    {
        tokenInPlaceList.Remove(token);
        DistributeTokens();
    }

    public void DistributeTokens()
    {
        int count = tokenInPlaceList.Count;
        float offset = .06f;

        if (count == 0) return;

        float totalWidth = (count - 1) * offset;
        float start = -totalWidth / 2f;

        for (int i = 0; i < count; i++)
        {
            tokenInPlaceList[i].transform.position = this.transform.position;
            Vector3 newPos;

            if (alignType == AlignType.Horizontal) newPos = new Vector3(start + i * offset, 0, 0);
            else newPos = new Vector3(0, start + i * offset, 0);

            tokenInPlaceList[i].transform.position += newPos;
        }
    }


    public bool IsSecureZone()
    {
        return isSecureZone;
    }

    public bool IsFinalWayZone()
    {
        return isFinalWayZone;
    }

    public bool IsGoalZone()
    {
        return isGoalZone;
    }

    public bool HaveAToken()
    {
        return tokenInPlaceList.Count > 0;
    }

    public string GetTokenColor()
    {
        return tokenInPlaceList[0].GetComponent<TokenScript>().GetColorPlayer();
    }

}
