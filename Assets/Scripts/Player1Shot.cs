using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Shot : Shot
{
    void Awake()
    {
        owner_tag = "Player";
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider col)
    {
        base.OnTriggerStay(col);
    }

    private void OnTriggerEnter(Collider col)
    {
        base.OnTriggerEnter(col);
    }
}
