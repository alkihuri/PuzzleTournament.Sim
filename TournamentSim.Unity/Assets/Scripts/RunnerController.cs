using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class RunnerController : MonoBehaviour
{
    public TablesManager TablesManager;
    public ScrublersManager ScrumblerManager;
    public Transform WaitingArea;
    public bool HasSchema;
    public bool CanGoToScrubler;
    public bool OnWay;

    NavMeshAgent _navigation;

    private void Awake()
    {
        _navigation = GetComponent<NavMeshAgent>();
        HasSchema = false;
    }
    public void StartBehavior(int x)
    {
        _navigation.Warp(transform.position);
        CanGoToScrubler = true;
        StartCoroutine(BehaviourOfCompitor(x));
    }

    private IEnumerator BehaviourOfCompitor(int x)
    {
        var table = TablesManager.Tables[x];
        table.HasRunner = true;
        table.ThisTableRunner = this;
        _navigation.SetDestination(table.transform.position);

        yield return new WaitUntil(() => table.ThisCompitor != null);

        yield return new WaitWhile(() => _navigation.remainingDistance > 0); 
         
        while (true)
        { 
            yield return new WaitUntil(() => CanGoToScrubler);

            yield return new WaitUntil(() => ScrumblerManager.Scrumblers.Where(s => s.IsBusy == false).Count() > 0);
            OnWay = true;
            var freeScubler = ScrumblerManager.Scrumblers.Where(s => s.IsBusy == false).ToArray()[0];
            _navigation.SetDestination(freeScubler.transform.position);
            yield return new WaitWhile(() => _navigation.remainingDistance > 0.1f);
            yield return new WaitForSeconds(5);
            _navigation.SetDestination(table.transform.position);
            yield return new WaitWhile(() => _navigation.remainingDistance > 0.1f); 
            OnWay = false;
            HasSchema = true;   
            yield return new WaitWhile(() => table.ThisCompitor.IsPlaying);
        }

    }


}
