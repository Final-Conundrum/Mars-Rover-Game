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
    //public Text infoText;

    // Start is called before the first frame update
    void Start()
    {
        infoText.text = "We have started the game! "; //this is for testing atm
        //infoPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    public void checkPointText()
    {
        infoPanel.SetActive(true);
        infoText.text = "Attention! Checkpoint Reached.";
        
        
    }

    public void aragoniteText()
    {
        infoPanel.SetActive(true);
        infoText.text = "Attention! You have collected: Aragonite";
       // StartCoroutine("Fade");
    }

    public void elevationWarning()
    {
        infoPanel.SetActive(true);
        infoText.text = "Attention! Checkpoint Reached.";
        
    }

    public void FeldsparText()
    {
        infoPanel.SetActive(true);
        infoText.text = "Attention! You have collected: Feldspar";
        

    }

    IEnumerator Fade()
    {
        infoPanel.SetActive(false);
        yield return new WaitForSeconds(1f);
    }
}
