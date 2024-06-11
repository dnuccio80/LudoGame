using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerSO : ScriptableObject
{

    [SerializeField] private string colorPlayer;
    [SerializeField] private Sprite panelSprite;
    [SerializeField] private Sprite tokenSprite;

    public string ColorPlayer {  get { return colorPlayer; } set {  colorPlayer = value; } }
    public Sprite PanelSprite { get { return panelSprite; } set {  panelSprite = value; } }
    public Sprite TokenSprite { get { return tokenSprite; } set {  tokenSprite = value; } }



}
