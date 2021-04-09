using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollisionShuffle : MonoBehaviour
{
    public GameObject UIPanel;
    public GameObject TPPCamera;
    public GameObject FPPCamera;
    //public GameObject Strip;
    public Animator cups;
    public Animator cupsAI;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            StartCoroutine(collionDetect(other));
        }

        if (other.gameObject.CompareTag("AI"))
        {

            StartCoroutine(collionDetectAI());
        }
    }

    IEnumerator collionDetect(Collider other)
    {
        // Strip.SetActive(false);
        other.gameObject.GetComponent<Animator>().SetTrigger("idle");
        yield return new WaitForSeconds(0.3f);
        LeanTween.moveLocal(TPPCamera, FPPCamera.transform.position, 0.5f);
        LeanTween.rotateLocal(TPPCamera, FPPCamera.gameObject.transform.rotation.eulerAngles, 0.5f);
        TPPCamera.GetComponent<CameraFollow>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        cups.enabled = true;
        UIPanel.SetActive(true);
    }

    IEnumerator collionDetectAI()
    {
        yield return new WaitForSeconds(0.5f);
        cupsAI.enabled = true;
    }
}
