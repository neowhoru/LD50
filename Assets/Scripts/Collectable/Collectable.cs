using System;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int amount = 3;
    public COLLECT_TYPE collectType;
    public enum COLLECT_TYPE
    {
        HEART,
        WINGS
    }
    
    
}