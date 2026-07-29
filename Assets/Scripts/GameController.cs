using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
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

        if(Nejiko.Life() <= 0)
        {
            enabled = false;

            Invoke("ReturnToTitle",2.0f);
        }
    }

    int CalcScore()
    {
        return (int)Nejiko.transform.position.z;
    }

    void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
