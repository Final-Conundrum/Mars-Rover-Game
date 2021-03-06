using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/**
 * This class deals with Displaying information to the player based on their current circumstances.
 *  This may include: Taking Damage, 
 * Educational Data, in-game notifications about things such as weather reports. 
 **/
public class GUI_infoPanel : MonoBehaviour
{
    //static variables
    public static GameObject INFO_PANEL;
    public static TMP_Text INFO_TEXT;
    public static GameObject PANEL_PARENT;
    public static GameObject FACT_PANEL;
    public static TMP_Text FACT_TEXT;

    //Game Objects
    public GameObject infoPanel;
    public GameObject parentPanel;
    public CanvasGroup _canvasGroup;
    Image panelImage;
    public TMP_Text infoText;
    public GameObject factPanel;
    public TMP_Text factText;
    public GameObject checkmarkIcon;
    public static GameObject _checkmarkIcon;

    //other
    public bool isWarning; //This will help determine which panels should self destruct after popping up. (ensure to destroy game object trigger too)
    public List<string> factStrings = new List<string>(); 
    public string defaultFact = "Perseverance moves at a speed of 152 meters per hour!";

    //coroutine variables
    public float maxCount = 10f;
    public float count = 0f;
    public float currentAlpha;

    private void Awake()
    {
       panelImage = infoPanel.GetComponent<Image>();
        panelImage.CrossFadeAlpha(1f, 0f, false);
        currentAlpha = _canvasGroup.alpha;

        //Fill the list
        factStrings.Add("Ingenuity is the small helicopter that went to Mars with Perseverance."); 
        factStrings.Add("Perseverance weighs 1025 Kilograms and is the size of a small car!");
        factStrings.Add(defaultFact);
        factStrings.Add("Mars is home to the tallest volcano and mountain in the ENTIRE universe!");
        factStrings.Add("Sounds on Mars have been recorded and can be found on SoundCloud.");
        factStrings.Add("It's understood by scientists that Jezero Crater was filled with water 3.5 billion years ago.");
        factStrings.Add("Perseverance is able to turn carbon-dioxide into oxygen.");
        factStrings.Add("One of Perseverance's goals is to prepare humans for life on Mars.");
        factStrings.Add("Samples collected by Perseverance will one day return to Earth. ");
        //factStrings.Add("PIXL measures the elemental chemistry of rocks and soil by focusing an X-Ray beam on it.");
        //factStrings.Add("SHERLOC is able to detect and identify organics and minerals on Mars.");
        //factStrings.Add("RIMFAX uses a ground-probing radar to see under the Martian surface. ");
        Debug.Log(factStrings.Count.ToString());

    }
    // Start is called before the first frame update
    void Start()
    {
        INFO_PANEL = infoPanel;
        INFO_TEXT = infoText;
        PANEL_PARENT = parentPanel;
        FACT_PANEL = factPanel;
        FACT_TEXT = factText;
        infoPanel.SetActive(false);
        factPanel.SetActive(false);

        _checkmarkIcon = checkmarkIcon;
        _checkmarkIcon.gameObject.SetActive(false);

        //Fill the list with something to avoid a crash
        if (factStrings.Count == 0)
        {
            factStrings.Add(defaultFact);
            factStrings.Add("The list is empty");
        }
    }

    public void CheckPointMessage() 
    {
        infoPanel.SetActive(true);
        infoText.text = "You have reached a checkpoint! ";
        isWarning = true;
        FadePanel();
    }

    public void OnApproachNotification()
    {
        infoPanel.SetActive(true);
        infoText.text = "There's a potential sample nearby that we should check out!";
        isWarning = false;
        FadePanel();
    }

    public void AragoniteText()
    {
        infoPanel.SetActive(true);
        infoText.text = " You have collected: Aragonite!";
        FadePanel();
    }

    public void ElevationWarning()
    {
        infoPanel.SetActive(true);
        isWarning = true;
        infoText.text = "Warning! You may take damage if you fall from this height. Be careful!";
        
    }

    public void FeldsparText()
    {
        infoPanel.SetActive(true);
        infoText.text = "You have collected: Feldspar";
       
    }

    public void DamageWarning()
    {
        isWarning = true;
    }

    public void DustDevilNotification()
    {
        infoPanel.SetActive(true);
        isWarning = true;
        infoText.text = "Martain dust can often settle on machinery and can cause some serious damage.";
        FadePanel();
    }

    public void ActivateFactPanel()
    {
        infoPanel.SetActive(false);
        factPanel.SetActive(true);
        
        FadePanel();
    }
     public void GenerateFact() 
    {
        for(int i = 0; i < factStrings.Count; i++)
        {
            int index = Random.Range(1, factStrings.Count); //our 'random' number variable 
            factText.text = factStrings[index];
            i++;
            Debug.Log("Currently printing fact: " + factStrings[index].ToString());
        }
       
    }

    //call this to invoke coroutine
    public void FadePanel()
    {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        panelImage.CrossFadeAlpha(0, 8f, true);
        yield return new WaitForSeconds(5f);
           StartCoroutine("FadeCanvasGroup");
    }
    IEnumerator FadeCanvasGroup()
    {
        infoPanel.SetActive(false);
        factPanel.SetActive(false);
        yield return null;
    }  

    public static IEnumerator ImageFadeout()
    {
        _checkmarkIcon.gameObject.SetActive(true);
        _checkmarkIcon.GetComponent<Animator>().SetBool("Fade", true);
        yield return new WaitForSeconds(5f);
        _checkmarkIcon.GetComponent<Animator>().SetBool("Fade", false);
        _checkmarkIcon.gameObject.SetActive(false);
    }
}
