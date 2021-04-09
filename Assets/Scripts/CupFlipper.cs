using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupFlipper : MonoBehaviour
{
    public GameObject[] cups;
    public GameObject[] cupsPrefab;
    public float force, torque;
    int cupCounts;
    GameObject Cup;
    GameObject CupArrow;
    public Animator PlayerAnim;
    
    public GameObject TableBroken;
    public GameObject[] Table;

    public GameObject[] MeterObject;
    public GameObject[] CupArrows;

    public Transform TppCamPos;
    public GameObject TPPCamera;
    public GameObject Panel1st;
    public GameObject Panel2nd;
    public GameObject Panel3rd;
    public GameObject CupPong;

    public Material greenMaterial;

    public static bool run;
    public static bool shuffle;
    public bool swipe;
    public bool cupdown;
    int count;
    bool fail;
    bool done;
    public Animator Cup2ndAnim;

    void Start()
    {
        count = 0;
        cupdown = false;
        shuffle = false;
        run = false;
        done = false;
        fail = false;
        cupCounts = 0;
        Cup = cups[cupCounts];
        CupArrow = CupArrows[cupCounts];
    }


    void Update()
    {
        if (shuffle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cup2ndAnim.SetTrigger("win");
                cupdown = true;
                StartCoroutine(Next());
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (fail)
                {
                    cupdown = true;
                    Cup2ndAnim.SetTrigger("failwin");
                    StartCoroutine(Next());
                }

                if (!fail)
                {
                    fail = true;
                    Cup2ndAnim.SetTrigger("fail");
                }
            }

            if(CubeCollisionPong.win && !done)
            {
                done = true;
                StartCoroutine(Next());

            }
        }


        if (Input.GetMouseButtonUp(0) && !swipe)
        {
            Transform cupTransform = cups[cupCounts].transform;
            Cup.GetComponent<Rigidbody>().AddForce(new Vector3(0f, Cup.transform.up.y * force, 10f));
            Cup.GetComponent<Rigidbody>().AddTorque(Cup.transform.right * torque * Time.deltaTime);
            CupArrow.SetActive(false);
            StartCoroutine(Checkcup(Cup, cupTransform));
        }

        if (Input.GetMouseButtonUp(0) && swipe && !CubeCollisionPong.pong)
        {
            Transform cupTransform = cups[cupCounts].transform;
            Cup.GetComponent<Rigidbody>().AddForce(new Vector3(0f, 100, 10f));
            Cup.GetComponent<Rigidbody>().AddTorque(Cup.transform.right * torque * Time.deltaTime);
            CupArrow.SetActive(false);
            StartCoroutine(CheckcupSwipe(Cup, cupTransform));
        }
    }

    IEnumerator Checkcup(GameObject cup, Transform cupTransform)
    {
        if (force == 100)
        {
            StartCoroutine(Blink());
        }
        yield return new WaitForSeconds(1f);

        float x = UnityEditor.TransformUtils.GetInspectorRotation(cup.transform).x;

        if (cup.transform.position.y > 0.5f && (x <= -179 && x >= -181) || (x >= 179 && x <= 181))
        {
            cupCounts++;
            if (cupCounts >= 3)
            {
               
                StartCoroutine(Next());
               
            }
            if(cupCounts < 3)
            {
                Cup = cups[cupCounts];
                CupArrow = CupArrows[cupCounts];
                CupArrow.SetActive(true);
            }
            
        }
        else
        {
            Cup = Instantiate(cupsPrefab[cupCounts], cupsPrefab[cupCounts].transform.position, cupsPrefab[cupCounts].transform.rotation);
            cup.SetActive(false);
        }

    }


    IEnumerator Blink()
    {

        GetComponent<Animator>().enabled = false;
        greenMaterial.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.2f);
        greenMaterial.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.2f);
        greenMaterial.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.2f);
        greenMaterial.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.2f);
        greenMaterial.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.2f);
        greenMaterial.DisableKeyword("_EMISSION");
        yield return new WaitForSeconds(0.2f);
        GetComponent<Animator>().enabled = true;
    }


    IEnumerator Next()
    {
        Panel1st.SetActive(false);
        Panel2nd.SetActive(false);
        Panel3rd.SetActive(false);
        yield return new WaitForSeconds(1f);
        LeanTween.moveLocal(TPPCamera, TppCamPos.position, 0.5f);
        LeanTween.rotateLocal(TPPCamera, TppCamPos.rotation.eulerAngles, 0.5f);
        yield return new WaitForSeconds(0.5f);
        PlayerAnim.SetTrigger("boxing");
        yield return new WaitForSeconds(0.8f);
        if(shuffle)
        {
            Cup2ndAnim.enabled = false;
        }
        if(cupdown)
        {
            Cup2ndAnim.GetComponent<Rigidbody>().isKinematic = false;
        }
        if(CubeCollisionPong.win)
        {
            CupPong.GetComponent<Rigidbody>().isKinematic = false;
        }
        Instantiate(TableBroken, Table[count].transform.position, Table[count].transform.rotation);
        Table[count].SetActive(false);
        count++;
        run = true;
        foreach (GameObject go in MeterObject)
        {
            go.SetActive(false);
        }
    }



    IEnumerator CheckcupSwipe(GameObject cup, Transform cupTransform)
    {

        yield return new WaitForSeconds(1f);
        float x = UnityEditor.TransformUtils.GetInspectorRotation(cup.transform).x;
        if (cup.transform.position.y > 0.5f && (x <= -179 && x >= -181) || (x >= 179 && x <= 181))
        {
            cupCounts++;
            if (cupCounts >= 3)
            {
                shuffle = true;
                StartCoroutine(Next());
            }
            if (cupCounts < 3)
            {
                Cup = cups[cupCounts];
                CupArrow = CupArrows[cupCounts];
                CupArrow.SetActive(true);
            }

        }
        else
        {
            Cup = Instantiate(cupsPrefab[cupCounts], cupsPrefab[cupCounts].transform.position, cupsPrefab[cupCounts].transform.rotation);
            cup.SetActive(false);
        }
    }
}
