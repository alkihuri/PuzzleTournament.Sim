using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class TableController : MonoBehaviour
{
    [SerializeField] RunnerController _thisTableRunner;
    [SerializeField] CompitorController _thisCompitor;

    public bool HasRunner;
    public bool HasCompitor;
    public CompitorController ThisCompitor { get => _thisCompitor; set => _thisCompitor = value; }
    public RunnerController ThisTableRunner { get => _thisTableRunner; set => _thisTableRunner = value; }
    

    private void Awake()
    {
        HasRunner = false;
        HasCompitor = false;
    }
}
