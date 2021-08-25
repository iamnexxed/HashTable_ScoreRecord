
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] InputField searchInput;

    [SerializeField] Text playerText;
  


    HashTable<Player> scoreTable;
    // Start is called before the first frame update


   
    void Start()
    {
        scoreTable = new HashTable<Player>(10);
        Player player1 = new Player("Sudhanshu", "2", "100");
        Player player2 = new Player("Alex", "3", "200");
        Player player3 = new Player("Asad", "4", "300");
        Player player4 = new Player("Austin", "4", "500");
        Player player5 = new Player("Mohammad", "5", "500");

        scoreTable.insert(player1.name, player1);
        scoreTable.insert(player2.name, player2);
        scoreTable.insert(player3.name, player3);
        scoreTable.insert(player4.name, player4);
        scoreTable.insert(player5.name, player5);

    }



    
    public void Search()
    {
        if (searchInput.text.Length <= 0)
        {
            playerText.text = "";
            return;
        }
            
       Find(searchInput.text);
    }

    // display with error handling
    void Find(string name)
    {
        Player getPlayer = scoreTable.find(name);
        if(getPlayer == null)
        {
            // Debug.Log("Player not found!");
            playerText.alignment = TextAnchor.UpperCenter;
            playerText.text = "Player Not Found!";
        }
        else
        {
            // Debug.Log("Player found! : " + getPlayer.name);
            playerText.alignment = TextAnchor.UpperCenter;
            playerText.text = "Name : " + getPlayer.name;
            playerText.text += "\nLevel : " + getPlayer.level;
            playerText.text += "\nScore : " + getPlayer.score;
        }
    }
}
