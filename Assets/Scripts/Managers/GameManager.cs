using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject Fighter;
    [SerializeField] private int SmallAsteroidScore;
    [SerializeField] private int MediumAsteroidScore;
    [SerializeField] private int LargeAsteroidScore;
    void Awake()
    {
        if (Instance == null)
        {
        }
    }
    
}
