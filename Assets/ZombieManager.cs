using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{

    public GameManager GameManager;

    private Vector3 _lastFramePosition;
    private Animator _anim;
    private NavMeshAgent _ma;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    void Start()
    {
        _ma = this.GetComponent<NavMeshAgent>();
        _anim = this.GetComponent<Animator>();
        _anim.Play("idle");
        _lastFramePosition = transform.position;

        _ma.SetDestination(GameManager.Player.transform.position);
        _ma.updatePosition = false;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldDeltaPosition = _ma.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = velocity.magnitude > 0.5f && _ma.remainingDistance > (_ma.stoppingDistance +.5);
        Debug.Log("rem.dist:" +_ma.remainingDistance);

        // Switch between idle and walk
        if (shouldMove)
            _anim.Play("walk");
        else
            _anim.Play("idle");

        // Pull agent towards character
        if (worldDeltaPosition.magnitude > _ma.radius)
            _ma.nextPosition = transform.position + 0.9f * worldDeltaPosition;

    }
    void OnAnimatorMove()
    {
        // Update position based on animation movement using navigation surface height
        Vector3 position = _anim.rootPosition;
        position.y = _ma.nextPosition.y;
        transform.position = position;
    }
}
