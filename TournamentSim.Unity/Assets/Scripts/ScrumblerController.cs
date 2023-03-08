using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrumblerController : MonoBehaviour
{
    public bool IsBusy;

    // Start is called before the first frame update
    void Start()
    {
        IsBusy = false;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<RunnerController>()) { IsBusy = false; }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<RunnerController>()) { IsBusy = false; }
    }
}
