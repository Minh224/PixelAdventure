using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            StartCoroutine(DieAnimation());

        }
    }
    IEnumerator DieAnimation()
    {
        anim.SetTrigger("Death");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        AudioManager.instance.Play("GameOver");
        PlayerManager.isGameOver = true;
        gameObject.SetActive(false);
    }


}
