using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame_Results : MonoBehaviour
{
    [Space]
    [Header("PIXL Result Assets")]
    public GameObject PIXL_ResultsObject;
    public static GameObject _PIXL_ResultsObject;
    public GameObject[] PIXL_ImagePrefab;
    public static GameObject[] _PIXL_ImagePrefab;

    public static Transform _PIXL_Transform;

    [Space]
    [Header("RIMFAX Result Assets")]
    public GameObject RIMFAX_ResultsObject;
    public static GameObject _RIMFAX_ResultsObject;
    public GameObject[] RIMFAX_ImagePrefab;
    public static GameObject[] _RIMFAX_ImagePrefab;

    public static Transform _RIMFAX_Transform;

    [Space]
    [Header("DRILL Result Assets")]
    public Image DRILL_ResultsObject;
    public static Image _DRILL_ResultsObject;
    public Sprite[] DRILL_ImagePrefab;
    public static Sprite[] _DRILL_ImagePrefab;

    public static Transform _DRILL_Transform;

    // Start is called before the first frame update
    void Start()
    {
        PIXL_ResultsObject.gameObject.SetActive(false);
        RIMFAX_ResultsObject.gameObject.SetActive(false);
        DRILL_ResultsObject.gameObject.SetActive(false);

        _PIXL_ResultsObject = PIXL_ResultsObject;
        _PIXL_ImagePrefab = PIXL_ImagePrefab;
        _PIXL_Transform = PIXL_ResultsObject.transform;

        _RIMFAX_ResultsObject = RIMFAX_ResultsObject;
        _RIMFAX_ImagePrefab = RIMFAX_ImagePrefab;
        _RIMFAX_Transform = RIMFAX_ResultsObject.transform;

        _DRILL_ResultsObject = DRILL_ResultsObject;
        _DRILL_ImagePrefab = DRILL_ImagePrefab;
        _DRILL_Transform = DRILL_ResultsObject.transform;
    }

    public static IEnumerator ShowPIXLResults(float timer)
    {
        int random = Random.Range(0, _PIXL_ImagePrefab.Length - 1);

        _PIXL_ResultsObject.gameObject.SetActive(true);
        GameObject resultObject = Instantiate(_PIXL_ImagePrefab[random], _PIXL_Transform);
        _PIXL_ResultsObject.GetComponent<Animator>().SetBool("Transition", true);

        yield return new WaitForSeconds(timer);

        _PIXL_ResultsObject.GetComponent<Animator>().SetBool("Transition", false);
        yield return new WaitForSeconds(2f);

        _PIXL_ResultsObject.gameObject.SetActive(false);
        Destroy(resultObject);
    }

    public static IEnumerator ShowRIMFAXResults(float timer)
    {
        int random = Random.Range(0, _RIMFAX_ImagePrefab.Length - 1);

        _RIMFAX_ResultsObject.gameObject.SetActive(true);
        GameObject resultObject = Instantiate(_RIMFAX_ImagePrefab[random], _RIMFAX_Transform);
        _RIMFAX_ResultsObject.GetComponent<Animator>().SetBool("Transition", true);

        yield return new WaitForSeconds(timer);

        _RIMFAX_ResultsObject.GetComponent<Animator>().SetBool("Transition", false);
        yield return new WaitForSeconds(2f);

        _RIMFAX_ResultsObject.gameObject.SetActive(false);
        Destroy(resultObject);

    }

    public static IEnumerator ShowDRILLResults(float timer)
    {
        int random = Random.Range(0, _DRILL_ImagePrefab.Length - 1);

        _DRILL_ResultsObject.gameObject.SetActive(true);
        _DRILL_ResultsObject.sprite = _DRILL_ImagePrefab[random];
        _DRILL_ResultsObject.GetComponent<Animator>().SetBool("Transition", true);

        yield return new WaitForSeconds(timer);

        _DRILL_ResultsObject.GetComponent<Animator>().SetBool("Transition", false);
        yield return new WaitForSeconds(2f);

        _DRILL_ResultsObject.gameObject.SetActive(false);
    }
}
