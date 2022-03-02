using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] private string serverAddress;

    //Profile changer
    [SerializeField] private TMP_InputField profileInputField;

    //List down
    [SerializeField] private TMP_Dropdown profileList;

    //Profile color
    public GameObject ball;
     public Color newColor;
     public float rFloat;
     public float gFloat;
     public float bFloat;
     public float aFloat;

     public Renderer myRenderer;


    // Start is called before the first frame update
    void Start()
    {
        OnProfileClick();

        //ColorDefault();



    }



    public void OnProfileClick()
    {

        //populate variables
        string profile = profileInputField.text;
        string url = urlInputField.text;
        string serverAddress = "http://localhost:8000/data/main/";

        //Check that the variables are not null
        CheckProfile(profile);
        CheckServerAddress(url);

        //Look into server for information
        StartCoroutine(GetWebData($"{serverAddress}", $"{profile}")); 

        return;
        
    }


    public string CheckProfile(string profile)
    {
        if (string.IsNullOrEmpty(profile))
        {
            int index = profileList.value;
            profile = profileList.options[index].text;
        }
        else { profile = profileInputField.text; };

        return profile;
    }



    public string CheckServerAddress(string url)
    {
        //Uses local server address or heroku address
        //Check if input url is not empty
        if (string.IsNullOrEmpty(url))
        { serverAddress = "http://localhost:8000/data/main/"; }
        else { serverAddress = url + "data/main/"; };

        return serverAddress;
    }



    IEnumerator GetWebData ( string address, string myProfile )
    {
        //UnityWebRequest www = UnityWebRequest.Get(address + myProfile);
        UnityWebRequest www = UnityWebRequest.Get(address);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Something went wrong: " + www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            ProcessServerResponse(www.downloadHandler.text, myProfile);
        }
    }


    void ProcessServerResponse(string rawResponse, string myProfile)
    {
        //parse that into someting we can navigate.
        JSONNode node = JSON.Parse(rawResponse);

        int tryTimes = 5;

        ProfileSelector(node, myProfile, tryTimes);
        //ProfileDropdown(node, myProfile, tryTimes);

        ColorChanger(rFloat, gFloat, bFloat, aFloat);

    }

    public void ProfileDropdown(JSONNode node, string myProfile, int tryTimes)
    {
        /*
     list;

    string

    for (int i = 0; i < tryTimes; i++)
    {
        list = node["profiles"][i][myProfile][0]["info"][0]["username"];

        if (string.IsNullOrEmpty(list))
        {
            //Debug.LogError("Profile not found");
        }
        else
        {
            //Debug.LogError("Profile found");
            profileList.AddOptions(List<string> list);

        }
    };
    */
    }


    public void ProfileSelector(JSONNode node, string myProfile, int tryTimes)
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


    public void ColorChanger(float r, float g, float b, float a)
    {
        //create newColor template 
        newColor = new Color(r, g, b, a);
        //gets gameobject color
        myRenderer = ball.GetComponent<Renderer>();
        //replace gameobject color with newColor
        myRenderer.material.color = newColor;
    }




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

}