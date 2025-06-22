using UnityEngine;


[CreateAssetMenu(fileName = "BallStats", menuName = "Scriptable Objects/BallStats")]
public class BallStats : ScriptableObject
{
    public GameObject ballPrefab;

    [Header("Shop/UI Info")]
    public ItemData itemInfo;

    [Header("Variables")]
    public int initialScore = 100;             //{score}         → GetFormatScore() #4e5463
    public int scoreDist = 0;                  //{dist}          → GetFormatScore() #4e5463
    public float bounceMultiplier = 1;         //{bounce}        → #b97a60
    public float distanceToScore = 0.01f;      //{distance}      → #b97a60
    public int scoringTimes = 1;               //{scoringTimes}  → #b97a60
    public int goldPerActiveBall = 0;          //{gold}          → #b97a60
    public float turnEndMultiplier = 1;        //{turnEnd}       → #b97a60
    public bool passScore = false;             //{pass}          → if true → #645355
    public bool permanent = false;             //{permanent}     → if true → #645355


    public string GetFormattedDescription()
    {
        string desc = itemInfo.description;

        if (desc.Contains("{score}"))
        {
            desc = ReplaceString(desc, "{score}", GetFormatScore(initialScore));
        }

        if (desc.Contains("{dist}"))
        {
            desc = ReplaceString(desc, "{dist}", GetFormatScore(scoreDist));
        }

        if (desc.Contains("{bounce}"))
        {
            desc = ReplaceString(desc, "{bounce}", FormatFloat(bounceMultiplier, "#b97a60"));
        }

        if (desc.Contains("{distance}"))
        {
            desc = ReplaceString(desc, "{distance}", FormatFloat(distanceToScore, "#b97a60"));
        }

        if (desc.Contains("{scoringTimes}"))
        {
            desc = ReplaceString(desc, "{scoringTimes}", FormatInt(scoringTimes, "#b97a60"));
        }

        if (desc.Contains("{gold}"))
        {
            desc = ReplaceString(desc, "{gold}", FormatInt(goldPerActiveBall, "#b97a60"));
        }

        if (desc.Contains("{turnEnd}"))
        {
            desc = ReplaceString(desc, "{turnEnd}", FormatFloat(turnEndMultiplier, "#b97a60"));
        }

        if (desc.Contains("{pass}"))
        {
            if (passScore)
            {
                desc = ReplaceString(desc, "{pass}", FormatBool("Pass", "#a99c8d", "#4b3d44"));
            }
            else
            {
                desc = ReplaceString(desc, "{pass}", "");
            }
        }

        if (desc.Contains("{permanent}"))
        {
            if (permanent)
            {
                desc = ReplaceString(desc, "{permanent}", FormatBool("Permanent", "#4b3d44", "#a99c8d"));
            }
            else
            {
                desc = ReplaceString(desc, "{permanent}", "");
            }
        }

        return desc;
    }

    private string GetFormatScore(int score)
    {
        string color = "#4e5463";

        if (score < 0)
        {
            color = "#774251";
        }

        return $"<b><color={color}>{score:0}</color></b>";
    }

    private string FormatFloat(float value, string hexColor)
    {
        return $"<b><color={hexColor}>{value:0.00}</color></b>";
    }

    private string FormatInt(int value, string hexColor)
    {
        return $"<b><color={hexColor}>{value}</color></b>";
    }

    private string FormatBool(string label, string backgroundHex, string textHex)
    {
        return $"<b><color={textHex}>{label}</color></b>";
    }

    private string ReplaceString(string desc, string toReplace, string formattedText)
    {
        return desc.Replace(toReplace, formattedText);
    }
}