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
    [SerializeField] int _numOfGames = 0;
    public TablesManager TablesManager { get; internal set; }
    public Transform WaitingArea { get; internal set; }
    public Transform Registration { get; internal set; }
    public bool IsPlaying { get; internal set; }

    NavMeshAgent _navigation;

    private void Awake()
    {
        _navigation= GetComponent<NavMeshAgent>();  
    }

    public void StartBehavior()
    {
        _navigation.Warp(transform.position);
        StartCoroutine(BehaviourOfCompitor());
    }

    private IEnumerator BehaviourOfCompitor()
    {
        _navigation.SetDestination(Registration.position);
        yield return new WaitWhile(() => _navigation.remainingDistance > 0); 
        yield return new WaitForSeconds(12);

        _navigation.SetDestination(WaitingArea.position);
        yield return new WaitWhile(() => _navigation.remainingDistance > 2);


        yield return new WaitUntil(() => TablesManager.Tables.Where(t => t.HasCompitor == false).Count() > 0);

        var closestTable = TablesManager.Tables
                            .Where(t => t.HasCompitor == false)
                                    .OrderBy(t => Vector3.Distance(transform.position, t.transform.position)).First();


        _navigation.SetDestination(closestTable.transform.position);
        closestTable.HasCompitor = true;
        closestTable.ThisCompitor = this;
        yield return new WaitWhile(() => _navigation.remainingDistance > 0);
        yield return new WaitUntil(() => closestTable.ThisTableRunner != null);

        for (int x = 0; x < 5; x++)
        {
           

            IsPlaying = true;
            yield return new WaitForSeconds(UnityEngine.Random.Range(2, 5));
            IsPlaying = false;
            _numOfGames++;
        }
        closestTable.ThisCompitor = null;
        closestTable.HasCompitor = false;
        gameObject.SetActive(false);
    }
     
}
