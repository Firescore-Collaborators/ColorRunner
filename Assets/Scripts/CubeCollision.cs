using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{

    public GameObject TPPCamera;
    public GameObject FPPCamera;
    public GameObject[] Meterbar;
    public GameObject ColorPanel;
    public GameObject objectBck;
    public GameObject ColorObj;
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
        
        ColorPanel.SetActive(true);
        objectBck.SetActive(true);
        ColorObj.SetActive(true);
    }
}
