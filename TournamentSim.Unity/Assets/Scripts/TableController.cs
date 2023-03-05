using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class TableController : MonoBehaviour
{
    [SerializeField] private bool _isBusy;
    [SerializeField] private int _counter;
    [SerializeField] private float _red;
    public int Counter
    {
        get
        {
           // return PlayerPrefs.GetInt(name);
           return _counter;
        }
        set
        {
            PlayerPrefs.SetInt(name, value);
            _counter = value;
            SetColor();
        }
    }

    private void SetColor()
    {
        _red = Mathf.Clamp(Counter, 0, 255) / 2;
        GetComponentInChildren<Renderer>().material.color = new Color(_red, 0, 0);
    }


    [ContextMenu("Clear Data")]
    public void ClearData()
    {
        Counter = 0;
    }

    public bool IsBusy { get => _isBusy; set => _isBusy = value; }

    // Start is called before the first frame update
    void Start()
    {
        IsBusy = false;
        Counter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NPCController>())
        {
            IsBusy = true;
            other.gameObject.GetComponent<NPCController>().Table = this;
            Destroy(other.gameObject, Random.Range(2, 20));
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
