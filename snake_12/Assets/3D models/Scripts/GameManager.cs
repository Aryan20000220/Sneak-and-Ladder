using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Route route;
    //public List<Node> nodeList = new List<Node>();
    //public string GameState = "new";
    public static GameManager instance;
    private AsyncOperation _SceneAsync;

    public Dice dice;
    public WinPanel winPanel;
    public Camera mainCam;

    public Text scores;

    int currentScore = 0;
    int activePlayer;
    int diceNumber;
    public static bool movetoLoadPosdone = false;
 
 [System.Serializable]
 public class Player
 {
        public string playerName;
        public Stone stone;

        public GameObject rollDiceButton;

     public enum PlayerType
     {
         CPU,
         HUMAN,
     }

     public PlayerType playerType;
 }

 public List<Player> playerList = new List<Player>();

 public enum States
 {
     WAITING,
     ROLL_DICE,
     SWITCH_PLAYER
 }

 public States state;

 void Awake()
 {
     instance = this;
     for (int i = 0; i < playerList.Count; i++)
     {
         if(SaveSettings.players[i] == "HUMAN")
         {
             playerList[i].playerType = Player.PlayerType.HUMAN;
         }
         if(SaveSettings.players[i] == "CPU")
         {
             playerList[i].playerType = Player.PlayerType.CPU;
         }
     }
 }

 void Start()
 {
        Physics.IgnoreLayerCollision(6, 8);
        Physics.IgnoreLayerCollision(7, 8);
        Physics.IgnoreLayerCollision(6, 7);
        Time.timeScale = 1;
        if (movetoLoadPosdone)
        {
            activePlayer = 0;
            for (int i = 0; i < playerList.Count; i++)
            {
                state = States.WAITING;
                playerList[i].stone.MakeTurn(LoadGame.stepsdoneDB[i]);
                Debug.Log(playerList[activePlayer].playerName+" :"+i);
            }
            //playerList[0].stone.MakeTurn(10);
            //playerList[1].stone.MakeTurn(20);
            //playerList[2].stone.MakeTurn(12);
            //playerList[3].stone.MakeTurn(22);

            movetoLoadPosdone = false;
        }

        DeactivateAllButtons();
        winPanel.gameObject.SetActive(false);
        activePlayer = Random.Range(0,playerList.Count);
        InfoBox.instance.ShowMessage(playerList[activePlayer].playerName+" Starts First!");
 }

 void Update()
 {
        if (!movetoLoadPosdone)
        {
            Debug.Log("Pos Done Not True");
            if (playerList[activePlayer].playerType == Player.PlayerType.CPU)
            {
                switch (state)
                {
                    case States.WAITING:
                        {

                        }
                        break;
                    case States.ROLL_DICE:
                        {
                            StartCoroutine(RollDiceDelay());
                            state = States.WAITING;
                        }
                        break;
                    case States.SWITCH_PLAYER:
                        {
                            activePlayer++;
                            activePlayer %= playerList.Count;
                            InfoBox.instance.ShowMessage(playerList[activePlayer].playerName + " has the Move");
                            state = States.ROLL_DICE;
                        }
                        break;
                }
            }

            if (playerList[activePlayer].playerType == Player.PlayerType.HUMAN)
            {
                switch (state)
                {
                    case States.WAITING:
                        {

                        }
                        break;
                    case States.ROLL_DICE:
                        {
                            ActivateEachButton(true);
                            state = States.WAITING;
                        }
                        break;
                    case States.SWITCH_PLAYER:
                        {
                            activePlayer++;
                            activePlayer %= playerList.Count;
                            InfoBox.instance.ShowMessage(playerList[activePlayer].playerName + " has the Move");
                            Debug.Log("Index: " + playerList[activePlayer]);
                            state = States.ROLL_DICE;
                        }
                        break;
                }
            }
        }
 }

 IEnumerator RollDiceDelay()
 {
     yield return new WaitForSeconds(2);
     //diceNumber = Random.Range(1,7);
     //playerList[activePlayer].stone.MakeTurn(diceNumber);
     dice.RollDice();
 }

 public void RolledDiceNum(int _diceNumber)
 {
     diceNumber = _diceNumber;
     InfoBox.instance.ShowMessage(playerList[activePlayer].playerName+ " has rolled "+ diceNumber);

        playerList[activePlayer].stone.MakeTurn(diceNumber);
    // Debug.Log(playerList[activePlayer].stone.doneSteps+diceNumber);
        

 }

//  void ActivateButton(bool on)
//  {
//      rollDiceButton.SetActive(on);
//  }

 public void HumanRollDice()
 {
     ActivateEachButton(false);
     StartCoroutine(RollDiceDelay());
 }

 void ActivateEachButton(bool on)
 {
     playerList[activePlayer].rollDiceButton.SetActive(on);
 }

 void DeactivateAllButtons()
 {
     for (int i = 0; i < playerList.Count; i++)
     {
            playerList[i].rollDiceButton.SetActive(false);
           
     }
 }

 public void ReportWinner()
 {
     Debug.Log(playerList[activePlayer].playerName+ " has won the game");
     winPanel.gameObject.SetActive(true);
     winPanel.ShowWinMessage(playerList[activePlayer].playerName);
     InfoBox.instance.ShowMessage(playerList[activePlayer].playerName+ " has won the game !!!");

 }

    public void LaunchScene(int currentPos)
    {
        //Debug.Log("Test:");
        //Debug.Log(Route.instance.nodeList.Count);

    /*Transform[] nodes;
    nodes = GetComponentsInChildren<Transform>();
    foreach (Transform child in nodes)
    {

        Node n = child.GetComponent<Node>();
    }*/

    Debug.Log(playerList[activePlayer].playerName + ": " + currentPos +" "+ (int)playerList[activePlayer].playerType);
        
        //StartCoroutine(updateData(currentPos));
        if (playerList[activePlayer].playerType == Player.PlayerType.HUMAN)
        {
           /* if (currentPos == 1 || currentPos == 2 || currentPos == 3 || currentPos == 5 || currentPos == 6 || currentPos == 7)
            {
                DialogBox.Instance.ShowQuestion("You Have a Present", () =>
                {
                }, () => { });

                scores.text = "200";
            }
            else if (currentPos == 4)
            {
                StartCoroutine(onYourFunction(8));
            }*/

            if (currentPos == 1) currentScore += 100;
            if (currentPos == 2) currentScore += 100;
            if (currentPos == 3) currentScore -= 100;
            if (currentPos == 4) currentScore += 100;
            if (currentPos == 5) currentScore -= 100;
            if (currentPos == 6) currentScore += 100;
            if (currentPos == 7) currentScore += 100;
            if (currentPos == 8) currentScore -= 100;
            if (currentPos == 9) currentScore += 100;
            if (currentPos == 10) currentScore += 100;
            if (currentPos == 11) currentScore += 100;
            if (currentPos == 12) currentScore -= 100;
            if (currentPos == 13) currentScore += 100;
            if (currentPos == 14) currentScore += 100;
            if (currentPos == 15) currentScore -= 100;
            if (currentPos == 16) currentScore += 100;
            if (currentPos == 17) currentScore -= 100;
            if (currentPos == 18) currentScore += 100;
            if (currentPos == 19) currentScore += 100;
            if (currentPos == 20) currentScore += 100;
            if (currentPos == 21) currentScore += 100;
            if (currentPos == 22) currentScore += 100;
            if (currentPos == 23) currentScore -= 100;
            if (currentPos == 24) currentScore += 100;
            if (currentPos == 25) currentScore += 100;
            if (currentPos == 26) currentScore -= 100;
            if (currentPos == 27) currentScore -= 100;
            if (currentPos == 28) currentScore -= 100;
            if (currentPos == 29) currentScore += 100;
            if (currentPos == 30) currentScore -= 100;

            scores.text = currentScore.ToString();
        

        }
    }

    /*IEnumerator updateData(int position)
    {
        Debug.Log("DB Running: " + DBManager.username);
        /*WWWForm form = new WWWForm();
        form.AddField("Name", playerList[activePlayer].playerName);
        form.AddField("Type", (int)playerList[activePlayer].playerType);
        form.AddField("Position", position);
        form.AddField("Username", DBManager.username);
        form.AddField("scores", currentScore);
       // WWW www = new WWW("https://agrohub.ml/sqlconnect/addScore.php", form);
        yield return www;

        //Debug.Log(www.text);
    }*/

    public IEnumerator onYourFunction(int scene)
    {
        Time.timeScale = 0; //pauses the current scene       
                            //  SceneManager.LoadScene(scene, LoadSceneMode.Additive); //loads your desired other scene
                            //  SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(scene));
        AsyncOperation nScene = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
        nScene.allowSceneActivation = false;
        _SceneAsync = nScene;

        while (nScene.progress < 0.9f)
        {
            Debug.Log("Loading scene " + " [][] Progress: " + nScene.progress);
            yield return null;
        }

        _SceneAsync.allowSceneActivation = true;

        while (!nScene.isDone)
        {
            yield return null;
        }

        Scene nThisScene = SceneManager.GetSceneByBuildIndex(scene);

        if (nThisScene.IsValid())
        {
            Debug.Log("Scene is Valid");
            SceneManager.SetActiveScene(nThisScene);
        }
        else
        {
            Debug.Log("Invalid scene!!");
        }


    }

}
