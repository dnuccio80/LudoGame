using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerSO : ScriptableObject
{

    [SerializeField] private string colorPlayer;

    public string ColorPlayer {  get { return colorPlayer; } set {  colorPlayer = value; } }


}
