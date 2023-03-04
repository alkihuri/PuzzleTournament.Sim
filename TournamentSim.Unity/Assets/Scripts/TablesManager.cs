using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TablesManager : MonoBehaviour
{

    [SerializeField] List<TableController> _tables = new List<TableController>();

    public List<TableController> Tables { get => _tables; set => _tables = value; }

    // Start is called before the first frame update
    void Start()
    {
        Tables = GetComponentsInChildren<TableController>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
