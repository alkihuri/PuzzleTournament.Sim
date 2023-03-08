using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class CompitorController : MonoBehaviour
{
    [SerializeField, Range(0, 15)] private float _delay_at_registation = 2f;
    [SerializeField, Range(0, 15)] private float Distance_registration_ok = 2f;
    private NavMeshAgent _navigation;
    private Transform _target;

    public TablesManager TablesManager { get; internal set; }
    public bool OnWay { get; private set; }
    public TableController Table { get; internal set; }
    public int GameCounter { get; private set; }

    private void Awake()
    {
        GameCounter= 0;
        OnWay = false;
        _navigation = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        StartCoroutine(LiteUpdate());
    }

    private void OnDisable()
    {
        StopCoroutine(LiteUpdate());
    }

    public void GoToTable()
    {
        StartCoroutine(FindAndFollow());
        StartCoroutine(LiteUpdate());
    }

    public IEnumerator GoToRegistation(Transform registration)
    {
        var registationPoint = registration.position;



        if (!_navigation.isOnNavMesh)
            _navigation.Warp(transform.position);

        yield return new WaitWhile(() => registration.gameObject.GetComponent<RegistrationTableController>().IsBusy);
        _navigation.SetDestination(registationPoint); 
        yield return new WaitWhile(() => _navigation.remainingDistance == 0);
        registration.gameObject.GetComponent<RegistrationTableController>().IsBusy = true; 
        yield return new WaitForSeconds(_delay_at_registation);
        registration.gameObject.GetComponent<RegistrationTableController>().IsBusy = false;
        GoToTable();
    }

    private IEnumerator FindAndFollow()
    {
        var nonBusy = TablesManager.Tables.Where(t => t.IsBusy == false);
        OnWay = true;
        if (!(nonBusy.Count() > 0))
        {
            yield break;
        }


        var closestTable = nonBusy.Select(t => t.transform).OrderBy(tP => Vector3.Distance(transform.position, tP.position)).ToList()[0];
        _target = closestTable;
        _navigation.SetDestination(_target.position);
        yield return new WaitWhile(() => Vector3.Distance(transform.position, _target.position) > 0.2f);
        Debug.Log("+1");
        OnWay = false;
    }




    IEnumerator LiteUpdate()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(1f);
            LiteUpdateLogic();
        }
    }

    private void LiteUpdateLogic()
    {
        GoToNextOne();
    }

    private void GoToNextOne()
    {
        if (_target == null)
            return;


        if (_target.gameObject.GetComponent<TableController>().IsBusy == true)
        {
            StopAllCoroutines();
            StartCoroutine(FindAndFollow());
        }
    }

    private void OnDrawGizmos()
    {
        if (_target != null)
        {
            Gizmos.DrawLine(transform.position, _target.position);
        }
    }

    internal void GoToRegistationStart(Transform registation) => StartCoroutine(GoToRegistation(registation));

     public void  NextPlay()
    {
        GameCounter++;
        Table.Counter++;
    }
}
