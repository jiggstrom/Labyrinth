using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{

    public GameManager GameManager;

    private Vector3 _lastFramePosition;
    private Animator _anim;

    void Start()
    {
        var ma = this.GetComponent<NavMeshAgent>();
        _anim = this.GetComponent<Animator>();
        _anim.Play("idle");
        _lastFramePosition = transform.position;

        ma.SetDestination(GameManager.Player.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentFramePosition = transform.position;
        float distance = Vector3.Distance(_lastFramePosition, currentFramePosition);

        _lastFramePosition = currentFramePosition;
        float currentSpeed = Mathf.Abs(distance) / Time.deltaTime;

        // Switch between idle and walk
        if (currentSpeed > 0.1)
            _anim.Play("walk");
        else
            _anim.Play("idle");

    }
}
