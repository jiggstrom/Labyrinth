using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    private Vector3 _lastFramePosition;
    private Animator _anim;
    private NavMeshAgent _ma;
    NavMeshPath path;

    [SerializeField] private GameObject Waypoints;

    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;
    private bool _onWay;
    private bool _attacking = false;

    public delegate void OnArrived();
    public OnArrived onArrived;

    public delegate void OnKilled();
    public OnKilled onKilled;

    private AudioSource _audio;

    void Start()
    {
        _ma = this.GetComponent<NavMeshAgent>();
        _anim = this.GetComponent<Animator>();
        _anim.Play("idle");
        _lastFramePosition = transform.position;
        _onWay = false;
        _attacking = false;
        _ma.updatePosition = false;
        _audio = this.GetComponent<AudioSource>();
        SetDestination(GameManager.instance.Player.transform.position);

    }

    public bool OnWay {
        get
        {
            return _onWay;
        }
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
        //Debug.Log("rem.dist:" +_ma.remainingDistance);

        // Switch between idle and walk
        if (_attacking)
        {
            if (_audio.isPlaying == false)
            {
                _anim.Play("attack");
                _audio.Play();
            }
        }
        else if (shouldMove)
        {
            _anim.Play("walk");
            if (_audio.isPlaying == false)
                if(Random.value > 0.99)
                    _audio.Play();
        }
        else
        {
            _anim.Play("idle");
            if (_audio.isPlaying == false)
                if (Random.value > 0.99)
                    _audio.Play();

            if (_onWay)
            {
                if (_ma.remainingDistance < 2f && _ma.pathStatus == NavMeshPathStatus.PathComplete)
                {
                    Debug.Log("Zombie arrived");
                    if (onArrived != null) onArrived.Invoke();
                    _onWay = false;

                    var cntWaypoints = Waypoints.transform.childCount;
                    if (cntWaypoints > 0)
                    {
                        var newTarget = Waypoints.transform.GetChild(Random.Range(0, cntWaypoints)).transform;
                        SetDestination(newTarget.position);
                    }
                }
            }

        }

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

    public void SetDestination(Vector3 position)
    {
        _ma.SetDestination(position);
        _onWay = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
            _attacking = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _attacking = false;
    }
}
