using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
public class NPCController : MonoBehaviour
{
    private NavMeshAgent _navigation;

    public TablesManager TablesManager { get; internal set; }
    public bool OnWay { get; private set; }

    private void Awake()
    {
        OnWay = false;
        _navigation = GetComponent<NavMeshAgent>();
    }


    // Update is called once per frame
    void Update()
    {
        if (OnWay)
            return;

        if (TablesManager == null)
            return;

        if(TablesManager.Tables.Count > 0)
        {
            var nonBusyTable = TablesManager.Tables.Where(t => t.IsBusy == false);
            nonBusyTable = nonBusyTable.OrderBy(t => Vector3.Distance(t.transform.position, transform.position));
            _navigation.SetDestination(nonBusyTable.First().transform.position);
            OnWay = true;

        }
    }
     


}
