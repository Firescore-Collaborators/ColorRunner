using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    public GameObject UIPanel;
    public GameObject TPPCamera;
    public GameObject FPPCamera;
    public GameObject[] Meterbar;
    public CupFlipper cupflipper;
    public GameObject Arrow;
    //public GameObject Strip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(collionDetect(other));
            
           
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
        Arrow.SetActive(true);
        if (!cupflipper.swipe)
        {
            foreach(GameObject go in Meterbar)
            {

                go.SetActive(true);
            }
        }
        UIPanel.SetActive(true);
    }
}
