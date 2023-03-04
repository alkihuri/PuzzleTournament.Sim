using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour
{
    [SerializeField] private bool _isBusy;

    public bool IsBusy { get => _isBusy; set => _isBusy = value; }

    // Start is called before the first frame update
    void Start()
    {
        IsBusy = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<NPCController>())
        {
            IsBusy = true;
            Destroy(other.gameObject,Random.Range(2,15));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<NPCController>())
        {
            IsBusy = false;
        }
    }
}
