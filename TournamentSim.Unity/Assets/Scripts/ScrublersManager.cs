using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrublersManager : MonoBehaviour
{

    [SerializeField] List<ScrumblerController> _scrublers = new List<ScrumblerController>();

    public List<ScrumblerController> Scrumblers { get => _scrublers; set => _scrublers = value; }

    // Start is called before the first frame update
    void Awake()
    {
        Scrumblers = GetComponentsInChildren<ScrumblerController>().ToList();
    }

}
