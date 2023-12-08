using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Table", menuName = "ScriptableObject/Table")]
public class Table : ScriptableObject
{
    public int TableId;
    public bool IsTableAvaible;
    public Vector3 TablePos;
}
