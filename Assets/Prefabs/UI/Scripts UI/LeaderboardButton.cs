using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class LeaderboardButton : MonoBehaviour {
    //[SerializeField]
    //UIDocument shopDoc;

    UIDocument m_thisDoc;

    Button leaderboardButton;

    private void OnEnable() {
        m_thisDoc = GetComponent<UIDocument>();

        leaderboardButton = m_thisDoc.rootVisualElement.Q("LeaderboardButton") as Button;


        leaderboardButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }

    public void OnButtonclick(ClickEvent evt) {
        GameManager.Instance.OpenLeaderboard();
    }

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        leaderboardButton.RegisterCallback<ClickEvent>(OnButtonclick);
    }
}


