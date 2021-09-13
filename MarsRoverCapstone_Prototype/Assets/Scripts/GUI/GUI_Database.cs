using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class deals with all database functionality. 
 * It will connect each of the panels together.  
 *  I was going to try and make this nice, I will if there's time. Sorry in advance to anyone who opens this
 **/

public class GUI_Database : MonoBehaviour
{
   public GameObject PerseverancePanel; 
    public GameObject JezeroCraterPanel;
    public GameObject NASAPanel;
    public GameObject MarsPanel;
    public GameObject InstrumentsPanel;
    public GameObject ExternalPanel;


    // Start is called before the first frame update
    void Start()
    {
        //set all inactive apart from one 'home screen' panel 
        PerseverancePanel.SetActive(false);
        JezeroCraterPanel.SetActive(false);
        NASAPanel.SetActive(false);
        MarsPanel.SetActive(false);
        InstrumentsPanel.SetActive(false);
        ExternalPanel.SetActive(false);
    // somehow get a way to get the currently activated panel and then find a way to swap between active and inactive 
}

    // Update is called once per frame
    void Update()
    {
        
    }

    //method for each button

    public void ActivatePerseverancePanel()
    {
        PerseverancePanel.SetActive(true);
        JezeroCraterPanel.SetActive(false);
        NASAPanel.SetActive(false);
        MarsPanel.SetActive(false);
        InstrumentsPanel.SetActive(false);
        ExternalPanel.SetActive(false);
    }
    public void ActivateJezeroPanel()
    {
        JezeroCraterPanel.SetActive(true);
        PerseverancePanel.SetActive(false);
        NASAPanel.SetActive(false);
        MarsPanel.SetActive(false);
        InstrumentsPanel.SetActive(false);
        ExternalPanel.SetActive(false);
    }
    public void ActivateNasaPanel()
    {
        NASAPanel.SetActive(true);
        PerseverancePanel.SetActive(false);
        JezeroCraterPanel.SetActive(false);
        MarsPanel.SetActive(false);
        InstrumentsPanel.SetActive(false);
        ExternalPanel.SetActive(false);
    }
    public void ActivateMarsPanel()
    {
        MarsPanel.SetActive(true);
        PerseverancePanel.SetActive(false);
        JezeroCraterPanel.SetActive(false);
        InstrumentsPanel.SetActive(false);
        ExternalPanel.SetActive(false);
    }
    public void ActivateInstrumentPanel()
    {
        PerseverancePanel.SetActive(false);
        JezeroCraterPanel.SetActive(false);
        NASAPanel.SetActive(false);
        MarsPanel.SetActive(false);
        InstrumentsPanel.SetActive(true);
        ExternalPanel.SetActive(false);
    }
    public void ActivateExternalPanel()
    {
        PerseverancePanel.SetActive(false);
        JezeroCraterPanel.SetActive(false);
        NASAPanel.SetActive(false);
        MarsPanel.SetActive(false);
        InstrumentsPanel.SetActive(false);
        ExternalPanel.SetActive(true);
    }
}
