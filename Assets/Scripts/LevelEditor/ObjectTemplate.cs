using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjectTemplate", menuName = "LevelEditor/ObjectTemplate", order = 1)]
public class ObjectTemplate : ScriptableObject
{
    public GameObject[] wall;
    public GameObject sentry;
    //All other Objects
}