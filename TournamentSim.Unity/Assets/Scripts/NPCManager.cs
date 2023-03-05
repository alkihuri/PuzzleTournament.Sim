using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    private const int NUM_OF_PARTICIPIENT = 200;
    [SerializeField] GameObject _npcPrefab;
    [SerializeField] TablesManager _tablesManager;
    [SerializeField] Transform[] _spawmPoints;
    [SerializeField] Transform _registation;
    // Start is called before the first frame update

    private void Awake()
    {
        _tablesManager = GameObject.FindObjectOfType<TablesManager>();
    }
    void Start()
    {
        StartCoroutine(SpawDelay());
    }


    IEnumerator SpawDelay() 
    {
        for (int x = 0; x < NUM_OF_PARTICIPIENT; x++)
        {
            yield return new WaitForSeconds(1);
            var newNpc = Instantiate(_npcPrefab, transform);

            if (_spawmPoints.Length > 0)
                newNpc.transform.position = _spawmPoints[Random.Range(0, _spawmPoints.Length - 1)].position;

            var npcController = newNpc.GetComponent<NPCController>();
            npcController.TablesManager = _tablesManager;
            npcController.GoToRegistationStart(_registation);

        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
