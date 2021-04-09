using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winning : MonoBehaviour
{
    public GameObject MainCAm;
    public GameObject WinCAm;
    public GameObject skate;
    public GameObject Strip;
    public ParticleSystem[] Confetti;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Win(other));
        }
    }

    IEnumerator Win(Collider other)
    {
        LeanTween.rotateY(other.gameObject, 180, 0.5f);
        yield return new WaitForSeconds(0.4f);
        foreach(ParticleSystem con in Confetti)
        {
            con.Play();
        }
        other.GetComponent<Animator>().SetTrigger("dance");
    }
}
