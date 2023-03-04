using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] GameObject _npcPrefab;
    [SerializeField] TablesManager _tablesManager;
    // Start is called before the first frame update

    private void Awake()
    {
        _tablesManager = GameObject.FindObjectOfType<TablesManager>();
    }
    void Start()
    {
        for(int x =0;x<200;x++)
        {
            var newNpc =  Instantiate(_npcPrefab, transform);
            var npcController = newNpc.GetComponent<NPCController>();   
            npcController.TablesManager = _tablesManager;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
