using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public NejikoController Nejiko;
    public TextMeshProUGUI ScoreText;
    public LifePanel LifePanel;

    public void Update()
    {
        int score = CalcScore();
        ScoreText.text = "Score : " + score + "m";
        LifePanel.UpdateLife(Nejiko.Life());
    }

    int CalcScore()
    {
        return (int)Nejiko.transform.position.z;
    }
}
