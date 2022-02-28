using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;


public class ServerTalker : MonoBehaviour
{
    //SerializeField make private variables visible to the inspector
    [SerializeField] private TextMeshProUGUI usernameText;
    [SerializeField] private TextMeshProUGUI scoreText;

    //Server address
    [SerializeField] private TMP_InputField urlInputField;
    //[SerializeField] private string serverAddress;

    //Profile changer
    [SerializeField] private TMP_InputField profileInputField;

    //Profile dropdown
    [SerializeField] private TMP_Dropdown dropDownProfileList;
    //public Text TextBox;

    //Profile color
     public GameObject ball;
     public Color newColor;
     public float rFloat;
     public float gFloat;
     public float bFloat;
     public float aFloat;

     public Renderer myRenderer;


    // INIT
    void Start()
    {
        OnProfileClick();
        //ColorDefault();
    }


    //INIT ALL USER REQUEST
    public void OnProfileClick()
    {
        //populate variables
        string profileInput = profileInputField.text;
        string url = urlInputField.text;

        //Check that the variables are not null
        string profile = CheckProfile(profileInput);
        string serverAddress = CheckServerAddress(url);

        //Look into server for information
        StartCoroutine(GetWebData($"{serverAddress}", $"{profile}")); 

        return;
        
    }


    //CONNECTS TO THE JSON
    IEnumerator GetWebData ( string address, string myProfile )
    {
        UnityWebRequest www = UnityWebRequest.Get(address);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Something went wrong: " + www.error);
        }
        else
        {
            //Debug.Log(www.downloadHandler.text);
            ProcessServerResponse(www.downloadHandler.text, myProfile);
        }
    }





    //PROCESS ALL JSON REQUEST
    void ProcessServerResponse(string rawResponse, string myProfile)
    {
        //Parse that into someting we can navigate.
        JSONNode node = JSON.Parse(rawResponse);

        //Layers of json array to look into
        //It could be a wild loop, but i am afraid to get infinite
        int tryTimes = 5;

        ProfileSelector(node, myProfile, tryTimes);
        ProfileDropdown(node, myProfile, tryTimes);

        ColorChanger(rFloat, gFloat, bFloat, aFloat);

    }



    //DROPDOWN MENU
    public void ProfileDropdown(JSONNode node, string myProfile, int tryTimes)
    {

        //Make list and link TMPro.Dropdown 
        List<string> profilesList = new List<string>();
        var dropdown = dropDownProfileList;


        //Clear the dropdown list of any previous profile 
        dropdown.options.Clear();

        
        //Get all profiles and add to the List
        for (int i = 0; i < tryTimes; i++)
        {
            string jsonProfile = node[i]["profile"];

            if (string.IsNullOrEmpty(jsonProfile))
            {
                //Debug.LogError("Empty or Null");
            }
            else
            {
                profilesList.Add(jsonProfile);
            };
        }

        //Debug.Log(profilesList.Count);


        //Fill dropdown with profiles from the list
        foreach ( var profile in profilesList )
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData() { text = profile });
        }

    }





    //GET DATA FROM JSON
    public void ProfileSelector(JSONNode node, string myProfile, int tryTimes)
    {
        //gets username, score and color from profile server 
        //use loop to check each array to find the right profile
        for (int i = 0; i < tryTimes; i++)
        {
            string profile = myProfile;
            string jsonProfile = node[i]["profile"];

            //Debug.Log(profile);
            //Debug.Log(jsonProfile);
            

            if (jsonProfile != profile ) 
            {
                //Debug.LogError("Profile not found");
            }
            else
            {
                //Debug.LogError("Profile found");
                usernameText.text = node[i]["username"];
                scoreText.text = node[i]["wins"];



                //Gets color from profile server
                rFloat = node[i]["red"];
                gFloat = node[i]["green"];
                bFloat = node[i]["blue"];
                aFloat = node[i]["alpha"];
            }
        };
    }






    //CHECK FOR IN DROPDOWN LIST
    public string CheckProfile(string profile)
    {
        if (string.IsNullOrEmpty(profile))
        {
            int index = dropDownProfileList.value;
            profile = dropDownProfileList.options[index].text;
        }
        else { profile = profileInputField.text; };

        return profile;
    }






    //CHECK FOR IN SERVER NAME
    public string CheckServerAddress(string url)
    {
        //Uses local server address or heroku address
        //Check if input url is not empty
        string serverAddress;

        if (string.IsNullOrEmpty(url))
        { serverAddress = "http://localhost:8000/data/"; }
        else { serverAddress = url + "data/"; };

        return serverAddress;
    }





    //COLOR CHANGER
    public void ColorChanger(float r, float g, float b, float a)
    {
        //create newColor template 
        newColor = new Color(r, g, b, a);
        //gets gameobject color
        myRenderer = ball.GetComponent<Renderer>();
        //replace gameobject color with newColor
        myRenderer.material.color = newColor;
    }




/////////////////////////////////////////////////////////////NOT IN USE

    //DEFAULT COLOR
    public void ColorDefault()
    {
        //default colors
        float r = 0.5f;
        float g = 0.5f;
        float b = 0.5f;
        float a = 1.0f;

        //create newColor template 
        newColor = new Color(r, g, b, a);
        //gets gameobject color
        myRenderer = ball.GetComponent<Renderer>();
        //replace gameobject color with newColor
        myRenderer.material.color = newColor;
    }




    //GET DATA FROM JSON
    public void ProfileSelectorORIG(JSONNode node, string myProfile, int tryTimes)
    {
        //gets username, score and color from profile server 
        //use loop to check each array to find the right profile
        for (int i = 0; i < tryTimes; i++)
        {
            string text = node["profiles"][i][myProfile][0]["info"][0]["username"];

            if (string.IsNullOrEmpty(text))
            {
                //Debug.LogError("Profile not found");
            }
            else
            {
                //Debug.LogError("Profile found");
                usernameText.text = text;
                scoreText.text = node["profiles"][i][myProfile][2]["someArray"][1]["value"];

                //Gets color from profile server
                rFloat = node["profiles"][i][myProfile][1]["color"][0]["red"];
                gFloat = node["profiles"][i][myProfile][1]["color"][0]["green"];
                bFloat = node["profiles"][i][myProfile][1]["color"][0]["blue"];
                aFloat = node["profiles"][i][myProfile][1]["color"][0]["alpha"];
            }
        };
    }
}