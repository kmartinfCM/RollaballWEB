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

    //Old cold
    //[SerializeField] private string serverAddress = "http://localhost:8000/data/";

    //Server address
    [SerializeField] private TMP_InputField urlInputField;
    [SerializeField] private string serverAddress;

    //Profile changer
    [SerializeField] private TMP_InputField profileInputField;


    /// /////////////////////////////////////////////////////////////////////////
    /// </summary>
    //WIP: Trying out how to change the color of the sphere
    //[SerializeField] private TMP_InputField colorBall;

    public GameObject ball;
    public Color newColor;
    public float rFloat;
    public float gFloat;
    public float bFloat;
    public float aFloat;

    public Renderer myRenderer;

    /// /////////////////////////////////////////////////////////////////////////




    // Start is called before the first frame update
    void Start()
    {



        OnProfileClick();


        //default colors
        float rFloat = 0.5f;
        float gFloat = 0.5f;
        float bFloat = 0.5f;
        float aFloat = 1.0f;

        //create newColor template 
        newColor = new Color(rFloat, gFloat, bFloat, aFloat);
        //gets gameobject color
        myRenderer = ball.GetComponent<Renderer>();
        //replace gameobject color with newColor
        myRenderer.material.color = newColor;

    }

    public void OnProfileClick()
    {

        //populate variables
        string profile = profileInputField.text;
        string url = urlInputField.text;
        string serverAddress = "http://localhost:8000/data/main/";


        //Uses local server address or heroku address
        //if (url != null) { serverAddress = url + "data/main/"; }
        //else { serverAddress = "http://localhost:8000/data/main/"; } 





        //make a web request to get info from the server
        //this will be a text response.
        //this will return/continue IMMEDIATELY, but the coroutine
        //will Take several MS to actually get a respinse from the server.

        //Look into server for information
        StartCoroutine(GetWebData($"{serverAddress}", $"{profile}"));

        //Debug.Log("profile: " + $"{profile}");
        //Debug.Log("address: " + $"{serverAddress}");


        //create newColor template 
        newColor = new Color(rFloat, gFloat, bFloat, aFloat);
        //gets gameobject color
        myRenderer = ball.GetComponent<Renderer>();
        //replace gameobject color with newColor
        myRenderer.material.color = newColor;




        //prints to console profile to know if it works
        //Debug.Log("profile: " + $"{profile}");


        return;


    }



    void ProcessServerResponse(string rawResponse, string myProfile)
    {
        //that text, is actually JSON info, so we need to
        //parse that into someting we can navigate.
        JSONNode node = JSON.Parse(rawResponse);

        //gets username and score from profile server
        //usernameText.text = node["profiles"][$"{profileInputField}"]["username"];
        //scoreText.text = node["profiles"][$"{profileInputField}"]["someArray"][1]["value"];

        //myProfile = "kmartinf";

        for (int i = 0; i < 5; i++)
        {

            string text = node["profiles"][i][myProfile][0]["info"][0]["username"];

            if (string.IsNullOrEmpty(text)) { Debug.LogError("Profile not found"); }
            else
            {
                Debug.LogError("Profile found");
                usernameText.text = text;
                scoreText.text = node["profiles"][i][myProfile][2]["someArray"][1]["value"];

                //Gets color from profile server
                rFloat = node["profiles"][i][myProfile][1]["color"][0]["red"];
                gFloat = node["profiles"][i][myProfile][1]["color"][0]["green"];
                bFloat = node["profiles"][i][myProfile][1]["color"][0]["blue"];
                aFloat = node["profiles"][i][myProfile][1]["color"][0]["alpha"];

            }


        };



        //usernameText.text = node["profiles"][1][myProfile][0]["info"][0]["username"];


        //Debug.Log("Username: " + node["profiles"][1][myProfile][0]["info"][0]["username"]);

        //usernameText.text = node["username"];
        //scoreText.text = node["someArray"][1]["value"];

        //Gets color from profile server
        //rFloat = node["color"][0]["red"];
        //gFloat = node["color"][1]["green"];
        //bFloat = node["color"][2]["blue"];
        //aFloat = node["color"][3]["alpha"];




        //Output some stuff to the console so that we know that it worked.
        //Debug.Log("Username: " + node["username"]);
        //Debug.Log("Misc Data: " + node["someArray"][1]["name"] + " = " + node["someArray"][1]["value"]);

    }


    IEnumerator GetWebData(string address, string myProfile)
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
}
