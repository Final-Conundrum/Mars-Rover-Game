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
    
    public static GameObject INFO_PANEL;
    public static TMP_Text INFO_TEXT;
    public static GameObject PANEL_PARENT;
    public GameObject infoPanel;
    public GameObject parentPanel;
    public CanvasGroup _canvasGroup;
    Image panelImage;
    public TMP_Text infoText;

    //coroutine variables
    public float maxCount = 10f;
    public float count = 0f;
    public float currentAlpha;

    private void Awake()
    {
       panelImage = infoPanel.GetComponent<Image>();
        panelImage.CrossFadeAlpha(1f, 0f, false);
        currentAlpha = _canvasGroup.alpha;
    }
    // Start is called before the first frame update
    void Start()
    {
        INFO_PANEL = infoPanel;
        INFO_TEXT = infoText;
        PANEL_PARENT = parentPanel;
        infoPanel.SetActive(false);
    }

    public void CheckPointMessage() //Dallas has a function for setting checkpoint- ask if he wants same fade out on that. 
    {
        infoPanel.SetActive(true);
        infoText.text = "> Be advised that on your journey you will encounter many hazrads.";
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
        infoText.text = "Warning! You may take damage if you fall from this height. n Be careful!";
        
    }

    public void FeldsparText()
    {
        infoPanel.SetActive(true);
        infoText.text = "Attention! You have collected: Feldspar";
       
    }

    public void DamageWarning()
    {

    }

    public void DustDevilNotification()
    {
        infoPanel.SetActive(true);
        infoText.text = "Warning: Martain dust can often settle on machinery and cause damage. \n It would be best to avoid it.";
        FadePanel();
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
        
        //while(count < maxCount)
        //{
        //    float valueToReduce = 0.1f;
        //    _canvasGroup.alpha = currentAlpha - valueToReduce;
        //    count++;
        //    Debug.Log(count);
        //}
        yield return null;
    }
  

    //IEnumerator FadeOut()//eventually set Opacity value to 0 bit by bit, will be able to do this for fade in also.
    //{
    //    yield return new WaitForSeconds(5f);
    //    infoPanel.SetActive(false);
    //}

    //public void Fade()
    //{
    //    var canvGroup = parentPanel.GetComponent<CanvasGroup>();
    //    //Toggle the end value based on the faded state
    //  StartCoroutine(StartFade(canvGroup, canvGroup.alpha, isFaded ? 1 : 0));
    //    Debug.Log("Coroutine Called");
    //    isFaded = !isFaded;
    //}
    //Reduce the alpha value of canvas groud to get the desired 'fade' effect

    //IEnumerator StartFade(CanvasGroup canvGroup, float start, float end) // potentially have this moved to another script
    //{
    //    float counter = 0f;
    //    while(counter < duration)
    //    {
    //        counter += Time.deltaTime;
    //        canvGroup.alpha = Mathf.Lerp(start, end, counter / duration);
            
    //    }
    //    yield return null;
    //    Debug.Log("Coroutine ended");
    //}


}
