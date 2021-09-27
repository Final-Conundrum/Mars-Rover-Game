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

    //other
    public bool isWarning; //This will help determine which panels should self destruct after popping up. (ensure to destroy game object trigger too)
    public List<string> factStrings = new List<string>(); 
    public string defaultFact = "Perseverance can only move 152 meters per hour!";

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
        factStrings.Add("Ingenuity is the name of the small helicopter that hitched a ride with Perseverance. It took its first flight on April 19th, 2021"); 
        factStrings.Add("Perseverance cannot Jump.");
        factStrings.Add(defaultFact);
        factStrings.Add("Olympus Mons, located on Mars is the highest mountain and volcano\n in the ENTIRE solar system and is 24KM high.");
        factStrings.Add("Sounds of wind on Mars have been recorded and can be found on SoundCloud");
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

        if (factStrings.Count == 0)
        {
            factStrings.Add(defaultFact);
        }
    }

    public void CheckPointMessage() 
    {
        infoPanel.SetActive(true);
        infoText.text = "> You have reached a checkpoint! ";
        isWarning = true;
        FadePanel();
    }

    public void AragoniteText()
    {
        infoPanel.SetActive(true);
        infoText.text = "> You have collected: Aragonite! \n > Open the Database to view your collection";
        FadePanel();
    }

    public void ElevationWarning()
    {
        infoPanel.SetActive(true);
        isWarning = true;
        infoText.text = "> Warning! You may take damage if you fall from this height. n Be careful!";
        
    }

    public void FeldsparText()
    {
        infoPanel.SetActive(true);
        infoText.text = "> You have collected: Feldspar";
       
    }

    public void DamageWarning()
    {
        isWarning = true;
    }

    public void DustDevilNotification()
    {
        infoPanel.SetActive(true);
        isWarning = true;
        infoText.text = "Warning: Martain dust can often settle on machinery and cause damage. \n It would be best to avoid it.";
        FadePanel();
    }

    public void ActivateFactPanel()
    {
        infoPanel.SetActive(false);
        factPanel.SetActive(true);
        
        FadePanel();
    }
     public void GenerateFact() //now to find a way to make sure we don't get duplicates 
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

}
