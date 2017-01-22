using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheapPlatform : MonoBehaviour
{
    public float lifeTime = 0.5f;
    [SerializeField]
    private ParticleSystem destrParticles;
    [SerializeField]
    private Animator animParent;
    [SerializeField]
    private Animator animSecond;

    void OnEnable()
    {
        PlayerMovement.CheapPlatformAction += StartDestroy;
    }

    void OnDisable()
    {
        PlayerMovement.CheapPlatformAction -= StartDestroy;
    }

    // Update is called once per frame
    void StartDestroy()
    {
        StartCoroutine(cheapPlatform());
    }

    IEnumerator cheapPlatform()
    {
        animSecond.Play("platform_cheap");
        yield return new WaitForSeconds(lifeTime);
        destrParticles.Play();
        animParent.Play("PlatformFading");
        animSecond.Play("CheapFading");
        Vector2 playerPos = new Vector2(PlayerBehaviuor.Instance.gameObject.transform.position.x, PlayerBehaviuor.Instance.gameObject.transform.position.y);
        Vector2 platformPos = new Vector2(transform.position.x, transform.position.y);

        if (playerPos == platformPos)
        {
            StartCoroutine(PlayerBehaviuor.Instance.PlayerDeath());
        }
        yield return new WaitForSeconds(lifeTime);
        //Destroy(gameObject);
    }

}
