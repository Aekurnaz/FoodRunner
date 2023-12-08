using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Food", menuName = "ScriptableObject/Food")]
public class Food : ScriptableObject
{
    public string FoodName;
    public int FoodId;
    public float CookTime;
    public Mesh _Mesh;
    public GameObject _Food;
        
    
}
