using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollisionPong : MonoBehaviour
{
   public GameObject UIPanel;
    public GameObject TPPCamera;
    public GameObject FPPCamera;
    public GameObject ball;
    public GameObject cup;
    //public GameObject Strip;

    public static bool pong;
    public static bool win;

    private void Start()
    {
        pong = false;
        win = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0) && pong)
        {  
            ball.GetComponent<Rigidbody>().isKinematic = false;
            ball.GetComponent<Rigidbody>().AddForce(ball.transform.forward * 60);
            StartCoroutine(Win());
        }
    }


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
        pong = true;
        other.gameObject.GetComponent<Animator>().SetTrigger("idle");
        yield return new WaitForSeconds(0.3f);
        LeanTween.moveLocal(TPPCamera, FPPCamera.transform.position, 0.5f);
        LeanTween.rotateLocal(TPPCamera, FPPCamera.gameObject.transform.rotation.eulerAngles, 0.5f);
        TPPCamera.GetComponent<CameraFollow>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        UIPanel.SetActive(true);
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(1.5f);
        win = true;
       
    }
}
