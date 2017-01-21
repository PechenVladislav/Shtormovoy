﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviuor : MonoBehaviour {

    [SerializeField]
    Transform startPlatform;

    private static PlayerBehaviuor instance;
    

    private void Awake()
    {
        instance = this;
    }

    void Start ()
    {
        transform.position = startPlatform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == Assets.Water.Tag)
        {
            StartCoroutine("PlayerDeath");
        }
    }

    public IEnumerator PlayerDeath()
    {
        PlayerMovement.Instance.enabled = false;
        yield return new WaitForSeconds(0.3f);
        GameController.Instance.ReloadLevel();
    }

    public static PlayerBehaviuor Instance
    {
        get { return instance; }
    }
}