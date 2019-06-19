using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class LevelManager2 : MonoBehaviour
{

    [SerializeField]
    private LevelData _levelData;
    private List<ZombieManager> zombies;
    private bool InventoryChecked;
    private bool MapChecked;
    private bool HasInteracted;
    public DoorInteractable levelExitDoor;

    // Use this for initialization
    void Start()
    {
        //levelExitDoor.onOpened += () =>
        //{
        //    var lm = LevelLoadManager.instance;
        //    lm.Greet(
        //        "Grattis, du har nu klarat introduktionen och är redo för en ritig utmaning! Välkommen till nästa nivå!", "Meny");
        //};

        var ps = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        var pl = GameObject.FindGameObjectWithTag("Player").transform;
        pl.localPosition = ps.localPosition;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
