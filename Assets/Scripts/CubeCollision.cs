using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeCollision : MonoBehaviour
{

    public GameObject TPPCamera;
    public GameObject FPPCamera;
    public GameObject[] Meterbar;
    public GameObject ColorPanel;
    public GameObject objectBck;
    public GameObject ColorObj;
    public GameObject SunObj;
    public GameObject ColorSun;
    public GameObject CastleObj;
    public GameObject CastleSun;
    public GameObject PreviewSun;
    public GameObject PreviewButterfly;
    public GameObject PreviewCastle;
    public Slider ProgressBar;
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
        if (CharacterManager.colorCount == 5)
        {
            SunObj.SetActive(false);
            ColorSun.SetActive(false);
        }

        if (CharacterManager.colorCount == 10)
        {
            CastleObj.SetActive(false);
            CastleSun.SetActive(false);
        }
        // Strip.SetActive(false);
        other.gameObject.GetComponent<Animator>().SetTrigger("idle");
        yield return new WaitForSeconds(0.3f);
        LeanTween.moveLocal(TPPCamera, FPPCamera.transform.position, 0.5f);
        LeanTween.rotateLocal(TPPCamera, FPPCamera.gameObject.transform.rotation.eulerAngles, 0.5f);
        TPPCamera.GetComponent<CameraFollow>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        
        if (CharacterManager.colorCount < 5)
        {
            PreviewSun.SetActive(true);
        }

        if (CharacterManager.colorCount == 5 && CharacterManager.colorCount < 10)
        {
            PreviewSun.SetActive(false);
            PreviewCastle.SetActive(true);

        }

        if (CharacterManager.colorCount == 10)
        {
            PreviewCastle.SetActive(false);
            PreviewButterfly.SetActive(true);
        }
        
        
        
        ColorPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        PreviewSun.SetActive(false);
        PreviewCastle.SetActive(false);
        PreviewButterfly.SetActive(false);

       
        ColorObj.SetActive(true);
        objectBck.SetActive(true);
        ProgressBar.gameObject.SetActive(true);
    }
}
