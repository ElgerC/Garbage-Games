using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerApearance", menuName = "ScriptableObjects/PlayerApearance")]
public class PlayerApearanceScrptObj : ScriptableObject
{
    public Sprite namecard;
    public Color C_color;
    public GameObject G_model;
}
