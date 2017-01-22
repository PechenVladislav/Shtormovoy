using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviuor : MonoBehaviour {

    [SerializeField]
    Transform startPlatform;
    [SerializeField]
    ParticleSystem paricleDeath;
    [SerializeField]
    GameObject RestartPanel;

    private static PlayerBehaviuor instance;

    private Animator animator;    

    private void Awake()
    {
        instance = this;
    }

    void Start ()
    {
        animator = GetComponentInChildren<Animator>();
        transform.position = startPlatform.position;
    }

    public IEnumerator PlayerDeath()
    {
        PlayerMovement.Instance.enabled = false;
        paricleDeath.Play();
        animator.Play("PlayerFading");
        yield return new WaitForSeconds(1.5f);
        RestartPanel.SetActive(true);

        //GameController.Instance.ReloadLevel();
    }

    public static PlayerBehaviuor Instance
    {
        get { return instance; }
    }
}
