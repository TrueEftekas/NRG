using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MasterScript : MonoBehaviour
{
    public GameObject Answer;
    public GameObject why;
    public Camcontrol controls;
    public GameObject inQuestion;
    public GameObject togglePfb;
    public GameObject errorCanvas;
    public GameObject scorePanel;
    public Sprite overfullFridge;
    public Sprite fineFridge;
    public bool day;
    public bool summer;
    public Material dayMat;
    public Material NightMat;
    public List<Sprite> bulbSprites;
    public List<Sprite> roomSprites;
    public List<Sprite> darkSprites;
    public List<Sprite> bulbUI;
    // Start is called before the first frame update
    void Awake()
    {
        bool livingBright = true;
        Screen.SetResolution(1920, 1080, true);
        day = Random.value > 0.5f; //randomly generate time of day
        summer = Random.value > 0.5f;// and season
        List<GameObject> rooms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Room")); //get my rooms
        int personCount = Random.Range(1, (rooms.Count/2 +1));// generate a minimum of 1 person to a maximum equal to half the rooms
        for(int i = 0; i<(personCount); i++)//set some people active
        {
           int x = Random.Range(0, rooms.Count);
            rooms[x].transform.Find("Person").gameObject.SetActive(true);
            rooms.RemoveAt(x);
        }
        rooms = new List<GameObject>(GameObject.FindGameObjectsWithTag("Room")); //get my rooms again
        foreach(GameObject room in rooms)//randomly set windows to active assuming they exist
        {
            GameObject window = room.transform.Find("Window").gameObject;
            if (window != null)
            {
                window.SetActive(Random.value > 0.5f);
                if (window.activeSelf && !day)
                {
                    if(room.name == "Bathroom")
                    {
                        window.GetComponent<SpriteRenderer>().sprite = darkSprites[11];
                    }
                    if (room.name == "Bedroom")
                    {
                        window.GetComponent<SpriteRenderer>().sprite = darkSprites[17];
                    }
                    if (room.name == "Kitchen")
                    {
                        window.GetComponent<SpriteRenderer>().sprite = darkSprites[2];
                    }
                }
            }
        }
        GameObject[] apps = GameObject.FindGameObjectsWithTag("Interactable"); //find all the problem objects
        foreach(GameObject g in apps) //time to start genererating their status
        {
            Appliance currApp = g.GetComponent<Appliance>(); //get the appliance script containing all their data
            if (g.name.Contains("Bulb")) //we need to checkthe type of appliance, thankfully we can tell from their names
            {
                currApp.whys.Add("Turned on unnecessarily"); //add all possible problems
                currApp.whys.Add("Inefficient");
                currApp.isOn = Random.value > 0.5f; //randomly decide if it is on
                float type = Random.value;
                currApp.type = (int)(type * 3);
                if (currApp.isOn)
                {
                   if (type > (2.0 / 3.0))
                   {
                        g.GetComponent<SpriteRenderer>().sprite = bulbSprites[4];
                   }  else if(type > (1.0 / 3.0))
                    {
                        g.GetComponent<SpriteRenderer>().sprite = bulbSprites[1];
                    } else
                    {
                        g.GetComponent<SpriteRenderer>().sprite = bulbSprites[7];
                    }
                    if (!(day && GameObject.Find(currApp.location).transform.Find("Window").gameObject.activeSelf))
                    {
                        GameObject currRoom = GameObject.Find(currApp.location);
                        if (currApp.location == "Bathroom")
                        {
                            currRoom.GetComponent<SpriteRenderer>().sprite = roomSprites[1];
                        }
                        else if(currApp.location == "Bedroom")
                        {
                            currRoom.GetComponent<SpriteRenderer>().sprite = roomSprites[4];
                            currRoom.transform.Find("Person").GetComponent<SpriteRenderer>().sprite = darkSprites[14];
                            currRoom.transform.Find("Bed").GetComponent<SpriteRenderer>().sprite = darkSprites[15];
                        }
                        else if(currApp.location == "Kitchen")
                        {
                            currRoom.GetComponent<SpriteRenderer>().sprite = roomSprites[7];
                        }
                        else if(currApp.location == "Living Room")
                        {
                            currRoom.GetComponent<SpriteRenderer>().sprite = roomSprites[11];
                            currRoom.transform.Find("Window").GetComponent<SpriteRenderer>().sprite = darkSprites[5];
                        }
                    }
                }
                else
                {
                    if (type > (2.0 / 3.0))
                    {
                        g.GetComponent<SpriteRenderer>().sprite = bulbSprites[5];
                    }
                    else if (type > (1.0 / 3.0))
                    {
                        g.GetComponent<SpriteRenderer>().sprite = bulbSprites[2];
                    }
                    else
                    {
                        g.GetComponent<SpriteRenderer>().sprite = bulbSprites[8];
                    }
                    if (!(day && GameObject.Find(currApp.location).transform.Find("Window").gameObject.activeSelf))
                    {
                        GameObject currRoom = GameObject.Find(currApp.location);
                        if (currApp.location == "Bathroom")
                        {
                            currRoom.GetComponent<SpriteRenderer>().sprite = roomSprites[2];
                            currRoom.transform.Find("Person").GetComponent<SpriteRenderer>().sprite = darkSprites[12];
                            currRoom.transform.Find("Tub").GetComponent<SpriteRenderer>().sprite = darkSprites[10];
                        }
                        else if (currApp.location == "Bedroom")
                        {
                            currRoom.GetComponent<SpriteRenderer>().sprite = roomSprites[3];
                            currRoom.transform.Find("Person").GetComponent<SpriteRenderer>().sprite = darkSprites[13];
                            currRoom.transform.Find("Bed").GetComponent<SpriteRenderer>().sprite = darkSprites[16];
                        }
                        else if (currApp.location == "Kitchen")
                        {
                            currRoom.GetComponent<SpriteRenderer>().sprite = roomSprites[6];
                            currRoom.transform.Find("Person").GetComponent<SpriteRenderer>().sprite = darkSprites[0];
                            GameObject.Find("Fridge").GetComponent<SpriteRenderer>().sprite = darkSprites[1];
                            currRoom.transform.Find("Set1").Find("Oven").GetComponent<SpriteRenderer>().sprite = darkSprites[4];
                            currRoom.transform.Find("Set1").Find("Sink").GetComponent<SpriteRenderer>().sprite = darkSprites[3];
                            currRoom.transform.Find("Set2").Find("Oven").GetComponent<SpriteRenderer>().sprite = darkSprites[4];
                            currRoom.transform.Find("Set2").Find("Sink").GetComponent<SpriteRenderer>().sprite = darkSprites[3];
                        }
                        else if (currApp.location == "Living Room")
                        {
                            currRoom.GetComponent<SpriteRenderer>().sprite = roomSprites[10];
                            currRoom.transform.Find("Person").GetComponent<SpriteRenderer>().sprite = darkSprites[19];
                            currRoom.transform.Find("Window").GetComponent<SpriteRenderer>().sprite = darkSprites[7];
                            currRoom.transform.Find("Chair").GetComponent<SpriteRenderer>().sprite = darkSprites[6];
                            livingBright = false;
                            Debug.Log("falsified");
                        }
                    }
                    else
                    {
                        if (type > (2.0 / 3.0))
                        {
                            g.GetComponent<SpriteRenderer>().sprite = bulbSprites[3];
                        }
                        else if (type > (1.0 / 3.0))
                        {
                            g.GetComponent<SpriteRenderer>().sprite = bulbSprites[0];
                        }
                        else
                        {
                            g.GetComponent<SpriteRenderer>().sprite = bulbSprites[6];
                        }
                    }
                }
                if((currApp.isOn && !(day && GameObject.Find(currApp.location).transform.Find("Window").gameObject.activeSelf) 
                    && GameObject.Find(currApp.location).transform.Find("Person").gameObject.activeSelf)||!currApp.isOn) //big if to decide whether or not the ligh is wastefully turned on
                {
                    currApp.realAnswers.Add(false);
                }
                else
                {
                    currApp.realAnswers.Add(true);
                }
                bool eff = type > (2.0/3.0); //eff if led
                currApp.realAnswers.Add(!eff);//adjust data
            } else if (g.name.Contains("TV"))//TV, simple enough, just check if on or off
            {

                currApp.whys.Add("Turned on unnecessarily");
                currApp.isOn = Random.value > 0.5f;
                if ((currApp.isOn &&  GameObject.Find(currApp.location).transform.Find("Person").gameObject.activeSelf) || !currApp.isOn)
                {
                    currApp.realAnswers.Add(false);
                }
                else
                {
                    currApp.realAnswers.Add(true);
                }
            } else if (g.name.Contains("AC"))//Air conditioning has a few interesting problems
            {
                currApp.whys.Add("Turned on unnecessarily");
                currApp.whys.Add("Temperature is not optimal");
                currApp.isOn = Random.value > 0.5f;
                if (currApp.isOn)
                {
                    currApp.data += "The AC is on.\n";
                }
                else
                {
                    currApp.data += "The AC is off.\n";
                }
                if ((currApp.isOn && GameObject.Find(currApp.location).transform.Find("Person").gameObject.activeSelf) || !currApp.isOn)
                {
                    currApp.realAnswers.Add(false);
                }
                else
                {
                    currApp.realAnswers.Add(true);
                }
                int actemp = Random.Range(17, 28);//generate temperature setting
                currApp.data += "The temperature is set to " + actemp + "C."; //adjust data
                if((summer && actemp>24)||(!summer && actemp>18 && actemp < 22))//check whether the temperature is good
                {
                    currApp.realAnswers.Add(false);
                }
                else
                {
                    currApp.realAnswers.Add(true);
                }
            } else if (g.name.Contains("Fridge")) //Aaand here's where it starts to get more complicated
            {
                currApp.whys.Add("The food will go bad");
                currApp.whys.Add("The fridge is too close to the oven");
                currApp.whys.Add("The fridge temperature is not appropriate");
                currApp.whys.Add("The fridge is too full");
                currApp.isOn = Random.value > 0.5f;
                if (currApp.isOn)//adjusting data, the fridge should never be off
                {
                    currApp.data += "The fridge is on.\n";
                    currApp.realAnswers.Add(false);
                }
                else
                {
                    currApp.data += "The fridge is off.\n";
                    currApp.realAnswers.Add(true);
                }
                if (Random.value > 0.5) //randomly deciding if it is close to the oven or not and  TODO: decide set and illumination
                {
                    GameObject.Find("Kitchen").transform.Find("Set1").gameObject.SetActive(false);
                    GameObject.Find("Kitchen").transform.Find("Set2").gameObject.SetActive(true);
                    currApp.realAnswers.Add(false);
                }
                else
                {
                    
                    currApp.realAnswers.Add(true);
                }
                int fridgeTemp = Random.Range(2, 7);//deciding the fridge temperature
                currApp.data += "The temperature is set to " + fridgeTemp + "C."; //adjust data
                currApp.realAnswers.Add(!(fridgeTemp > 4)); //add whether or not the temperature is appropriate
                currApp.realAnswers.Add(Random.value > 0.5);//randomly decide whether or not the fridge is too full
            }
            currApp.isActuallyGood = true; //assume the appliance is efficient
            foreach (bool b in currApp.realAnswers)
            {
                if (b)//if there is a problem, we set it to not being efficient
                {
                    currApp.isActuallyGood = false;
                }

                currApp.problem.Add(false);//might as well start loading these while I'm here, these are placeholders for the player's answers,
            }                               //all set to false on start
        }
        GameObject TV = GameObject.Find("TV");
        if (TV.GetComponent<Appliance>().isOn && livingBright)
        {
            TV.GetComponent<SpriteRenderer>().sprite = darkSprites[9];
            Debug.Log(9);
        }
        else if (TV.GetComponent<Appliance>().isOn && !livingBright)
        {
            TV.GetComponent<SpriteRenderer>().sprite = darkSprites[8];
            Debug.Log(8);
        }
        else if (!TV.GetComponent<Appliance>().isOn && !livingBright)
        {
            TV.GetComponent<SpriteRenderer>().sprite = darkSprites[7];
            Debug.Log(7);
        }
        GameObject.Find("Moon").SetActive(!day); //Setting the appropriate visuals
        GameObject.Find("Sun").SetActive(day);
        GameObject.Find("Summer").SetActive(summer);
        GameObject.Find("Winter").SetActive(!summer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openAnswer() //opens the answer panel
    {
        Answer.SetActive(true); // sets panel to active
        controls.enabled = false; //disable this script in order not to have the player move while messing with the answer panel
        Appliance app = inQuestion.GetComponent<Appliance>();//grabs the appliance script with the details from the appliance in question
        int childCount = why.transform.Find("ContentFitter").childCount; // clean the why sheet
        for (int i =0; i<childCount; i++)
        {
            Destroy(why.transform.Find("ContentFitter").transform.GetChild(i).gameObject);
        } // till here
        for(int i = 0; i<app.whys.Count; i++)//populate the why sheet from the list in the appliance script
        {
            GameObject ins = GameObject.Instantiate(togglePfb);//by instantiating toggles
            ins.transform.SetParent(why.transform.Find("ContentFitter"), false);//and setting their parents to the content fitter w/ the layout group
            ins.transform.SetAsLastSibling();
            ins.transform.localScale = new Vector3(1, 1, 1);//because scaling gets a bit messy otherwise
            ins.GetComponent<Toggle>().isOn = app.problem[i];//setting their truth values in case the player had previously listed there being problems
            ins.transform.Find("Label").GetComponent<Text>().text = app.whys[i]; // putting the appropriate text
            Answer.transform.Find("MarkPanel").Find("Good").GetComponent<Toggle>().isOn = app.good;//set the good
            Answer.transform.Find("MarkPanel").Find("Bad").GetComponent<Toggle>().isOn = !app.good;// or bad check according to memory
            why.SetActive(!app.good);//turn on the why panel accordingly
        }
        Answer.transform.Find("DataPane").transform.Find("Text").GetComponent<Text>().text = app.data;//load appropriate data
        if (inQuestion.name.Contains("Fridge"))//here we load the image, possibly images in the future, depending on the appliance, starting with the fridge
        {
            if (app.realAnswers[3])//this refers to the fridge being too full, we then place the appropriate image
            {
                Answer.transform.Find("Image").GetComponent<Image>().overrideSprite = overfullFridge;
            }
            else
            {
                Answer.transform.Find("Image").GetComponent<Image>().overrideSprite = fineFridge;
            }
        } else if (inQuestion.name.Contains("Bulb"))
        {
            Answer.transform.Find("Image").GetComponent<Image>().overrideSprite = bulbUI[app.type];
        }
        else // this else is just to remove the fridge image if it's loaded for now.
        {
            Answer.transform.Find("Image").GetComponent<Image>().overrideSprite = null;
        }
    }
    public void closeAnswer()
    {
        bool isGood = true;// booleans made to check whether or not the player selected bad without picking a reason
        bool weGotAProblem = true;
        Appliance app = inQuestion.GetComponent<Appliance>();
        if (Answer.transform.Find("MarkPanel").Find("Good").GetComponent<Toggle>().isOn) //if the player chose good
        {
            app.good = true; //set accordingly
            weGotAProblem = false; //no issues here
            inQuestion.transform.Find("Wrong").gameObject.SetActive(false);
            inQuestion.transform.Find("Right").gameObject.SetActive(true);
        }
        else
        {
            app.good = false;//set accordingly
            isGood = false;//we might have an issue
            inQuestion.transform.Find("Wrong").gameObject.SetActive(true);
            inQuestion.transform.Find("Right").gameObject.SetActive(false);
        }
        if (!isGood) //if player says bad, start storing choices
        {
            for (int i = 0; i < why.transform.Find("ContentFitter").childCount; i++)
            {
                app.problem[i] = why.transform.Find("ContentFitter").GetChild(i).GetComponent<Toggle>().isOn;
                if (app.problem[i])
                {
                    weGotAProblem = false;
                }
            }
        }
        if (!weGotAProblem) //we good?
        {
            Answer.SetActive(false);
            controls.enabled = true;
        }
        else// no?
        {
            errorCanvas.SetActive(true);
        }
    }
    public void whyOn() //turn on why panel and set appropriate indicator
    {
        why.SetActive(true);
    }
    public void whyOff()//turn off why panel and set appropriate indicator
    {
        why.SetActive(false);
    }
    public void flipPage(int x) //flip pagesin the brochure
    {
        Transform broch = Answer.transform.Find("Brochure");
        broch.Find("BulbView").gameObject.SetActive(x == 0);
        broch.Find("FridgeView").gameObject.SetActive(x == 1);
    }
    public void endGame()
    {
        controls.enabled = false;//disable controls so that the player doesn't skid around while checking score
        List<GameObject> apps =new List<GameObject> (GameObject.FindGameObjectsWithTag("Interactable"));//find all the appliances
        float total = apps.Count;//count them
        float percentPerApp = 100 / total;//assign a percentage to each
        float endPercent =100;//start counting down from 100
        Text mistakes = scorePanel.transform.Find("Mistake Panel").Find("Scroll View").Find("Viewport").Find("Content").Find("Text").GetComponent<Text>();//get the mistakes panel
        mistakes.text = "";//clear it
        foreach (GameObject app in apps)//here we start counting score as well as populating the mistake panel, if any
        {
            Appliance currApp = app.GetComponent<Appliance>(); //get the script of the appliance in question
            if(currApp.isActuallyGood == currApp.good) //did the player correctly mark it?
            {
                if (!currApp.isActuallyGood)//was it inefficient?
                {
                    List<string> incorrectnos = new List<string>(); // make two lists, one for things marked that shouldn't have been
                    List<string> incorrectyes = new List<string>();//and one for things not marked that should have been
                    for(int i = 0; i< currApp.realAnswers.Count; i++) //compare whys
                    {
                        if(currApp.realAnswers[i] != currApp.problem[i]) //if they are not the same
                        {
                            endPercent -= (percentPerApp / (2 * currApp.realAnswers.Count)); //subtract percentage equal to half the percent per app, divided by the total number of whys
                            if (currApp.realAnswers[i]) //we add to the appropriate list
                            {
                                incorrectnos.Add(currApp.whys[i]);
                            }
                            else
                            {
                                incorrectyes.Add(currApp.whys[i]);
                            }
                        }
                    }
                    if(incorrectnos.Count > 0) //if a list is non 0 in side
                    {
                        mistakes.text += "The " + app.name + " in the " + currApp.location + " is actually bad because:\n"; //we start piling reasons in the mistake panel
                        foreach(string mis in incorrectnos)
                        {
                            mistakes.text += "-" + mis + "\n";
                        }
                    }
                    if (incorrectyes.Count > 0) //same as the last one but for the yes list
                    {
                        mistakes.text += "The " + app.name + " in the " + currApp.location + " is not:\n";
                        foreach (string mis in incorrectyes)
                        {
                            mistakes.text += "-" + mis + "\n";
                        }
                    }
                }
            }
            else // if the player did not mark efficiency correctly
            {
                endPercent -= percentPerApp; //subtract the whole grade
                if (currApp.isActuallyGood) //add the appropriate reason for the mistake in the mistake pane
                {
                    mistakes.text += "The " + app.name + " in the " + currApp.location + " is actually good.\n";
                }
                else
                {
                    mistakes.text += "The " + app.name + " in the " + currApp.location + " is actually bad because:\n";
                    for(int i = 0; i < currApp.whys.Count; i++)
                    {
                        if (currApp.realAnswers[i])
                        {
                            mistakes.text += "-" + currApp.whys[i] + "\n";
                        }
                    }
                }
                
            }
        }
        if(endPercent == 100) //Just a short message
        {
            mistakes.text = "You're good!";
        }
        scorePanel.SetActive(true); // turn on the score panel
        scorePanel.transform.Find("Score Panel").Find("Text").GetComponent<Text>().text = "You got " + Mathf.RoundToInt(endPercent) + "%"; //write the appropriate score
    }
    public void mistakeButton() //switch between the score and mistakes
    {
        scorePanel.transform.Find("Mistake Panel").gameObject.SetActive(true);
        scorePanel.transform.Find("Score Panel").gameObject.SetActive(false);
    }
    public void backButton() //switch between the mistakes and score
    {
        scorePanel.transform.Find("Mistake Panel").gameObject.SetActive(false);
        scorePanel.transform.Find("Score Panel").gameObject.SetActive(true);
    }
    public void okButton() //the OK button at the end, mostly placeholder
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void doExitGame() //Quit game
    {
        Application.Quit();
    }

}
