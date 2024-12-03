using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/GameManager")]
public class GamaManagerScrptObj : ScriptableObject
{
    public List<Minigame> m_MinigamesData = new List<Minigame>();

    [System.Serializable]
    public class Minigame
    {
        public List<Vector3> startPositions = new List<Vector3>();
    }
}
