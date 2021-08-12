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
    
    public GameObject infoPanel;
   public TMP_Text infoText;
   


    // Start is called before the first frame update
    void Start()
    {
        //infoText.text = "We have started the game! "; //this is for testing atm
        infoPanel.SetActive(false);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    public void CheckPointText()
    {
        infoPanel.SetActive(true);
        infoPanel.GetComponent<Image>().color = new Color(0f, 144f, 123f, 121);
        infoText.text = "Attention! Checkpoint Reached.";
        StartCoroutine("FadeOut");

    }

    public void AragoniteText()
    {
        infoPanel.SetActive(true);
        infoText.text = "Attention! You have collected: Aragonite";
        infoPanel.GetComponent<Image>().color = new Color(1f,0f,0f,121);
        StartCoroutine("FadeOut");
    }

    public void ElevationWarning()
    {
        infoPanel.SetActive(true);
        infoText.text = "Elevation warning";
        
    }

    public void FeldsparText()
    {
        infoPanel.SetActive(true);
        infoText.text = "Attention! You have collected: Feldspar";
       
    }

    public void DamageWarning()
    {

    }

    IEnumerator FadeOut()//eventually set Opacity value to 0 bit by bit, will be able to do this for fade in also.
    {
        yield return new WaitForSeconds(5f);
        infoPanel.SetActive(false);
    }
}
