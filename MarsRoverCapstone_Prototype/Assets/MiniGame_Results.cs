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
    public Image RIMFAX_ResultsImage;
    public static Image _RIMFAX_ResultsImage;
    public Sprite[] RIMFAX_Sprites;
    public static Sprite[] _RIMFAX_Sprites;

    [Space]
    [Header("DRILL Result Assets")]
    public Image DRILL_ResultsImage;
    public static Image _DRILL_ResultsImage;
    public Sprite[] DRILL_Sprites;
    public static Sprite[] _DRILL_Sprites;

    // Start is called before the first frame update
    void Start()
    {
        PIXL_ResultsObject.gameObject.SetActive(false);
        RIMFAX_ResultsImage.gameObject.SetActive(false);
        DRILL_ResultsImage.gameObject.SetActive(false);

        _PIXL_ResultsObject = PIXL_ResultsObject;
        _PIXL_ImagePrefab = PIXL_ImagePrefab;
        _PIXL_Transform = PIXL_ResultsObject.transform;

        _RIMFAX_ResultsImage = RIMFAX_ResultsImage;
        _RIMFAX_Sprites = RIMFAX_Sprites;

        _DRILL_ResultsImage = DRILL_ResultsImage;
        _DRILL_Sprites = DRILL_Sprites;
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
        int random = Random.Range(0, _RIMFAX_Sprites.Length - 1);

        _RIMFAX_ResultsImage.gameObject.SetActive(true);
        _RIMFAX_ResultsImage.sprite = _RIMFAX_Sprites[random];
        _RIMFAX_ResultsImage.GetComponent<Animator>().SetBool("Transition", true);

        yield return new WaitForSeconds(timer);

        _RIMFAX_ResultsImage.GetComponent<Animator>().SetBool("Transition", false);
        yield return new WaitForSeconds(2f);

        _RIMFAX_ResultsImage.gameObject.SetActive(false);
    }

    public static IEnumerator ShowDRILLResults(float timer)
    {
        int random = Random.Range(0, _DRILL_Sprites.Length - 1);

        _DRILL_ResultsImage.gameObject.SetActive(true);
        _DRILL_ResultsImage.sprite = _DRILL_Sprites[random];
        _DRILL_ResultsImage.GetComponent<Animator>().SetBool("Transition", true);

        yield return new WaitForSeconds(timer);

        _DRILL_ResultsImage.GetComponent<Animator>().SetBool("Transition", false);
        yield return new WaitForSeconds(2f);

        _DRILL_ResultsImage.gameObject.SetActive(false);
    }
}
