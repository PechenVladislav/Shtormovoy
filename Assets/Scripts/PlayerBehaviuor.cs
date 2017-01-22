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
        animator.Play("character_idle_back");
        yield return new WaitForSeconds(1.2f);
        RestartPanel.SetActive(true);

        //GameController.Instance.ReloadLevel();
    }

    public IEnumerator Finish()
    {
        PlayerMovement.Instance.enabled = false;
        animator.Play("PlayerFading");
        yield return new WaitForSeconds(1.2f);
        
        GameController.Instance.LoadLevel(2);
    }

    public static PlayerBehaviuor Instance
    {
        get { return instance; }
    }
}
