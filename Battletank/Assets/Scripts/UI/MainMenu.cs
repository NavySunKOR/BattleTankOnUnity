using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public GameObject primaryDropdown;
    public GameObject secondaryDropdown;
    public GameObject gameStartPanel;
    
    [SerializeField]
    private PrimaryWeaponType selectedPrimary;
    [SerializeField]
    private SecondaryWeaponType selectedSecondary;

    private void Start()
    {
        List<string> primaryOptions = new List<string>();
        List<string> secondaryOptions = new List<string>();

        foreach(PrimaryWeaponType type in Enum.GetValues(typeof(PrimaryWeaponType)))
        {
            if(type != PrimaryWeaponType.None)
                primaryOptions.Add(type.ToString());
        }

        foreach(SecondaryWeaponType type in Enum.GetValues(typeof(SecondaryWeaponType)))
        {
            if (type != SecondaryWeaponType.None)
                secondaryOptions.Add(type.ToString());
        }

        Dropdown priDropdown = primaryDropdown.GetComponent<Dropdown>();
        Dropdown secDropdown = secondaryDropdown.GetComponent<Dropdown>();

        priDropdown.AddOptions(primaryOptions);
        secDropdown.AddOptions(secondaryOptions);

        SetPrimarySelected(priDropdown.options[priDropdown.value].text);
        SetSecondarySelected(secDropdown.options[secDropdown.value].text);

        primaryDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(delegate
        {
            SetPrimarySelected(priDropdown.options[priDropdown.value].text);
         });

        secondaryDropdown.GetComponent<Dropdown>().onValueChanged.AddListener(delegate
        {
            SetSecondarySelected(secDropdown.options[secDropdown.value].text);
        });
    }

    public void PopupSelection()
    {
        gameStartPanel.SetActive(true);
    }

    public void StartGame(string name)
    {
        PlayerPrefs.SetInt("PrimaryWeapon",(int)selectedPrimary);
        PlayerPrefs.SetInt("SecondaryWeapon",(int)selectedSecondary);
        SceneManager.LoadScene(name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void SetPrimarySelected(string type)
    {
        selectedPrimary = (PrimaryWeaponType)Enum.Parse(typeof(PrimaryWeaponType), type);
    }

    private void SetSecondarySelected(string type)
    {
        selectedSecondary = (SecondaryWeaponType)Enum.Parse(typeof(SecondaryWeaponType), type);
    }
}
