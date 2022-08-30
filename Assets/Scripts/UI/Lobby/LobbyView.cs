using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyView : View
{
    [SerializeField]
    private LobbyPlayerContainer lobbyPlayerContainerPrefab;

    [SerializeField]
    private RectTransform lobbyPlayerContainerContent;

    [SerializeField]
    private InputField inputUsername;

    [SerializeField]
    private Button readyButton;

    [SerializeField]
    private Button startButton;

    private List<LobbyPlayerContainer> lobbyPlayerContainers;

    public override void Initialize()
    {
        lobbyPlayerContainers = new List<LobbyPlayerContainer>();
        inputUsername.onEndEdit.AddListener((string input) =>
        {
            LevelManager.Instance.onwerPlayer.ServerSetUsername(input);
        });
        readyButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.onwerPlayer.ServerSetIsReady(!LevelManager.Instance.onwerPlayer.isReady);
        });
        startButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.ServerStartGame();
        });

        base.Initialize();
    }

    public override void Show(object args = null)
    {
        base.Show(args);

        if (LevelManager.Instance.players.Count > 0)
        {
            if (LevelManager.Instance.players[0].IsOwner)
            {
                startButton.gameObject.SetActive(true);
            }
            else
            {
                startButton.gameObject.SetActive(false);
            }
        }
    }

    public override void Hide()
    {
        base.Hide();
    }

    void Update()
    {
        if (_initialized)
        {
            if (lobbyPlayerContainers.Count != LevelManager.Instance.players.Count)
            {
                RespawnPlayerContainers();
            }

            UpdatePlayerContainers();

            startButton.interactable = LevelManager.Instance.canStart;
        }

    }

    private void RespawnPlayerContainers()
    {
        if (lobbyPlayerContainers.Count < LevelManager.Instance.players.Count)
        {
            while (lobbyPlayerContainers.Count < LevelManager.Instance.players.Count)
            {
                lobbyPlayerContainers.Add(Instantiate(lobbyPlayerContainerPrefab, lobbyPlayerContainerContent).GetComponent<LobbyPlayerContainer>());
            }
        }
        else if (lobbyPlayerContainers.Count > LevelManager.Instance.players.Count)
        {
            while (lobbyPlayerContainers.Count > LevelManager.Instance.players.Count)
            {
                Destroy(lobbyPlayerContainers[lobbyPlayerContainers.Count - 1].gameObject);
                lobbyPlayerContainers.RemoveAt(lobbyPlayerContainers.Count - 1);
            }
        }

        lobbyPlayerContainerContent.sizeDelta = new Vector2(lobbyPlayerContainerContent.sizeDelta.x, lobbyPlayerContainers.Count * 70 + 20);

        for (int i = 0; i < lobbyPlayerContainers.Count; i++)
        {
            lobbyPlayerContainers[i].GetComponent<RectTransform>().localPosition = new Vector2(0, -20 - 70 * i);
        }
    }

    private void UpdatePlayerContainers()
    {
        for (int i = 0; i < Mathf.Min(lobbyPlayerContainers.Count, LevelManager.Instance.players.Count); i++)
        {
            lobbyPlayerContainers[i].SetData(LevelManager.Instance.players[i].username, LevelManager.Instance.players[i].isReady);
            lobbyPlayerContainers[i].SetOwner(LevelManager.Instance.players[i].IsOwner);
        }
    }
}
