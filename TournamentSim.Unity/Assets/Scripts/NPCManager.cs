using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] private int NUM_OF_COMPITORS = 200;
    [SerializeField] private int NUM_OF_RUNNERS = 6;
    [SerializeField] GameObject _compitorsPrefab;
    [SerializeField] GameObject _runnerPrefab;
    [SerializeField] TablesManager _tablesManager;
    [SerializeField] ScrublersManager _scrublersManager;
    [SerializeField] Transform[] _spawmPoints;
    [SerializeField] Transform _registation;
    [SerializeField] Transform _waitingArea;
    [SerializeField] private List<CompitorController> _compitors = new List<CompitorController>();
    [SerializeField] TMPro.TextMeshProUGUI _text;
    // Start is called before the first frame update

    private void Awake()
    {
        _tablesManager = GameObject.FindObjectOfType<TablesManager>();
        _scrublersManager = GameObject.FindObjectOfType<ScrublersManager>();
    }
    void Start()
    {
        NUM_OF_RUNNERS = _tablesManager.Tables.Count;
        StartCoroutine(SpawDelayRunners());
    }


    IEnumerator SpawDelayCompitors()
    {
        for (int x = 0; x < NUM_OF_COMPITORS; x++)
        {
            yield return new WaitForSeconds(1);
            var newNpc = Instantiate(_compitorsPrefab, transform);

            if (_spawmPoints.Length > 0)
                newNpc.transform.position = _spawmPoints[Random.Range(0, _spawmPoints.Length - 1)].position;

            var npcController = newNpc.GetComponent<CompitorController>();
            npcController.TablesManager = _tablesManager;
            npcController.WaitingArea = _waitingArea;
            npcController.Registration = _registation;
            npcController.StartBehavior();
            _compitors.Add(npcController);
        }

    }
    IEnumerator SpawDelayRunners()
    {
        for (int x = 0; x < NUM_OF_RUNNERS; x++)
        {
            yield return new WaitForSeconds(1);
            var newNpc = Instantiate(_runnerPrefab, transform);

            if (_spawmPoints.Length > 0)
                newNpc.transform.position = _spawmPoints[Random.Range(0, _spawmPoints.Length - 1)].position;

            var npcController = newNpc.GetComponent<RunnerController>();
            npcController.ScrumblerManager = _scrublersManager;
            npcController.TablesManager = _tablesManager;
            npcController.WaitingArea = _waitingArea;
            npcController.StartBehavior(x);

        }

        StartCoroutine(SpawDelayCompitors());
    }

    private float _stopWatch = 0;
    private void Update()
    {

        _stopWatch =  _compitors.Where(c => c.gameObject.activeInHierarchy).Count() > 0 ? _stopWatch + Time.deltaTime : _stopWatch; 

        _text.text = "remain comp-s: " + _compitors.Where(c=>c.gameObject.activeInHierarchy).Count().ToString()  + '\n' + _stopWatch.ToString("#.");
    }

    
}
