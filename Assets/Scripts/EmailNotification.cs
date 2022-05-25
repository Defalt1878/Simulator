using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserData;

public class EmailNotification : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI count;

    private void Update()
    {
        var newEmailsCount = StaticData.GetInstance().Emails.Count(email => !email.IsRead);
        if (newEmailsCount == 0)
        {
            background.gameObject.SetActive(false);
            count.gameObject.SetActive(false);
            return;
        }
        background.gameObject.SetActive(true);
        count.gameObject.SetActive(true);
        count.text = newEmailsCount.ToString();
    }
}
