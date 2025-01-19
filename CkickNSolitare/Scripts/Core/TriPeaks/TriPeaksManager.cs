using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TriPeaksManager : MonoBehaviour
{
    public static TriPeaksManager Instance { get; private set; }

    [SerializeField] public int maxCardNumber;
    [SerializeField] public int minCardNumber;

    [SerializeField] public List<Card> interactableCards;
    [SerializeField] public List<DeckCard> remainingDeckCards;
    [SerializeField] private CurrentCardInDeck _currentCardInDeck;
    private float _timer = 0f;

    [SerializeField] private Timer _gameTimer;
    [SerializeField] private TMP_Text _movesCountTMP;
    private int _numberOfMoves = 0;

    [SerializeField] private GameObject _winScreen;
    [SerializeField] private GameObject _loseScreen;

    public static bool GameIsOver;

    private void Awake()
    {
        Instance = this;
        GameIsOver = false;
    }

    public void MoveTaken()
    {
        _numberOfMoves++;
        _movesCountTMP.text = $"{_numberOfMoves} moves";
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 1f)
        {
            _timer = 0f;

            if (remainingDeckCards.Count == 0)
            {
                int cantListCount = 0;

                foreach (Card card in interactableCards)
                {
                    int canInteractNum = Mathf.Abs(card.cardNumber - _currentCardInDeck.cardNumber);

                    if (canInteractNum == 0 || canInteractNum > 1)
                        cantListCount++;

                    if (cantListCount == interactableCards.Count)
                    {
                        GameIsOver = true;
                        _loseScreen.SetActive(true);
                        _gameTimer.enabled = false;
                    }
                }

            }
        }

        if (interactableCards.Count == 0)
        {
            GameIsOver = true;
            _winScreen.SetActive(true);
            _gameTimer.enabled = false;
        }
    }
}
