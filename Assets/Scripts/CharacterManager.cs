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

    public RectTransform[] IconPosition;
    public RectTransform icon;
    public ParticleSystem[] confetti;

    public GameObject ColorPanel;

    public Slider ProgressBar;

    public static int colorCount;
    public static int count;
    public static bool next;

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
        yield return new WaitForSeconds(1f);
        ProgressBar.gameObject.SetActive(false);
        ColorPanel.SetActive(false);
        LeanTween.moveLocal(TPPCamera, TppCamPos.position, 0.5f);
        LeanTween.rotateLocal(TPPCamera, TppCamPos.rotation.eulerAngles, 0.5f);
        yield return new WaitForSeconds(0.5f);
        ConfettiPlay();
        CorrectText.SetTrigger("text");
        TPPCamera.GetComponent<CameraFollow>().enabled = true;
        yield return new WaitForSeconds(1f);
        Strip.SetActive(true);
        Player.GetComponent<Animator>().SetTrigger("run");
        LeanTween.moveLocal(Player, Positons[++count].position, 3f);
        LeanTween.moveX(icon, IconPosition[count].anchoredPosition.x, 3f);
        ProgressBar.minValue = colorCount;
        ProgressBar.maxValue = colorCount + 4;
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
