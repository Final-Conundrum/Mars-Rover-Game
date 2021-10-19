using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame_Results : MonoBehaviour
{
    [Space]
    [Header("PIXL Result Assets")]
    public Image PIXL_ResultsImage;
    public static Image _PIXL_ResultsImage;
    public Sprite[] PIXL_Sprites;
    public static Sprite[] _PIXL_Sprites;

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
        PIXL_ResultsImage.gameObject.SetActive(false);
        RIMFAX_ResultsImage.gameObject.SetActive(false);
        DRILL_ResultsImage.gameObject.SetActive(false);

        _PIXL_ResultsImage = PIXL_ResultsImage;
        _PIXL_Sprites = PIXL_Sprites;

        _RIMFAX_ResultsImage = RIMFAX_ResultsImage;
        _RIMFAX_Sprites = RIMFAX_Sprites;

        _DRILL_ResultsImage = DRILL_ResultsImage;
        _DRILL_Sprites = DRILL_Sprites;
    }

    public static IEnumerator ShowPIXLResults(float timer)
    {
        int random = Random.Range(0, _PIXL_Sprites.Length - 1);

        _PIXL_ResultsImage.gameObject.SetActive(true);
        _PIXL_ResultsImage.sprite = _PIXL_Sprites[random];
        _PIXL_ResultsImage.GetComponent<Animator>().SetBool("Transition", true);

        yield return new WaitForSeconds(timer);

        _PIXL_ResultsImage.GetComponent<Animator>().SetBool("Transition", false);
        yield return new WaitForSeconds(2f);
        _PIXL_ResultsImage.gameObject.SetActive(false);
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
