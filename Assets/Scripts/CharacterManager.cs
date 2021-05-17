using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject Strip;
    public Transform[] Positons;
    public Transform TppCamPos;
    public Transform FailPos;
    public GameObject TPPCamera;
    public Animator CorrectText;
    public Animator WrongText;

    public GameObject[] Table;
    public RectTransform[] IconPosition;
    public RectTransform icon;
    public ParticleSystem[] confetti;

    public GameObject ColorPanel;

    public Slider ProgressBar;

    public GameObject BrokenWall;
    public static int colorCount;
    public static int count;
    public static bool next;

    public Animator SunAnim;
    public Animator FruitAnim;
    public Animator WindAnim;


    private void Start()
    {
        colorCount = 0;
        count = 0;
        LeanTween.moveLocal(Player, Positons[count].position,3f);

        LeanTween.moveX(icon, IconPosition[count].anchoredPosition.x, 3f);
    }

    private void Update()
    {
      
        if(next)
        {
            RightAnswer();
            next = false;
        }
        ProgressBar.value = colorCount;
    }

    public void RightAnswer()
    {

        StartCoroutine(Right());
    }


    IEnumerator Right()
    {
        if (colorCount == 5)
        {
            SunAnim.gameObject.SetActive(true);
            SunAnim.enabled = true;
        }

        if (colorCount == 9)
        {
            FruitAnim.gameObject.SetActive(true);
            FruitAnim.enabled = true;
        }

        if (colorCount == 12)
        {
            WindAnim.gameObject.SetActive(true);
            WindAnim.enabled = true;
        }

        yield return new WaitForSeconds(3f);
        SunAnim.gameObject.SetActive(false);
        FruitAnim.gameObject.SetActive(false);
        WindAnim.gameObject.SetActive(false);

        
        ProgressBar.gameObject.SetActive(false);
        ColorPanel.SetActive(false);
        LeanTween.moveLocal(TPPCamera, TppCamPos.position, 0.5f);
        LeanTween.rotateLocal(TPPCamera, TppCamPos.rotation.eulerAngles, 0.5f);
        yield return new WaitForSeconds(0.5f);
        Player.GetComponent<Animator>().SetTrigger("boxing");
        yield return new WaitForSeconds(0.8f);
        Instantiate(BrokenWall, Table[count].transform.position, BrokenWall.transform.rotation);
        Table[count].SetActive(false);
        ConfettiPlay();
        CorrectText.SetTrigger("text");
        TPPCamera.GetComponent<CameraFollow>().enabled = true;
        yield return new WaitForSeconds(1f);
        Strip.SetActive(true);
        Player.GetComponent<Animator>().SetTrigger("run");
        LeanTween.moveLocal(Player, Positons[++count].position, 3f);
        LeanTween.moveX(icon, IconPosition[count].anchoredPosition.x, 3f);
        if(colorCount < 10)
        {
            ProgressBar.minValue = colorCount;
            ProgressBar.maxValue = colorCount + 4;
        }

        if(colorCount>=10)
        {
            ProgressBar.minValue = colorCount;
            ProgressBar.maxValue = colorCount + 3;
        }

    }

    public void ConfettiPlay()
    {
        foreach (ParticleSystem p in confetti)
        {
            p.Play();
        }
    }

  


    public void WrongAnswer()
    {

        StartCoroutine(Wrong());
    }


    IEnumerator Wrong()
    {
        WrongText.SetTrigger("text");
        yield return new WaitForSeconds(1f);
    }


    public void ChangeColor(Image image)
    {
        image.color = Color.gray;
    }


    public void ChangeGreen(Image image)
    {
        StartCoroutine(Green(image));
    }

    IEnumerator Green(Image image)
    {
        image.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        image.color = Color.white;
        yield return new WaitForSeconds(0.2f);

        image.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        image.color = Color.white;
        yield return new WaitForSeconds(0.2f);

        image.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        image.color = Color.white;
        yield return new WaitForSeconds(0.2f);
        image.color = Color.green;
    }


    public void DisableGB(GameObject Panel)
    {
        StartCoroutine(disable(Panel));
    }

    IEnumerator disable(GameObject Panel)
    {
        yield return new WaitForSeconds(2f);
        Panel.SetActive(false);
    }
}
