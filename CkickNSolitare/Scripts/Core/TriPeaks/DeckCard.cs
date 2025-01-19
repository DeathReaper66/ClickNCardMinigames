using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DeckCard : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int _cardNumber;
    [SerializeField] private int _cardType;
    [SerializeField] private TMP_Text _cardNumberTMP;
    [SerializeField] private Image _cardTypeImage;
    [SerializeField] private Color[] _cardTypeColor;

    [SerializeField] private float _animationDuration;
    [SerializeField] private CurrentCardInDeck _currentCardInDeck;
    private RectTransform _currentCardInDeckPosition;

    private bool _canPutInTheDeck = true;
    private RectTransform _rect;

    private void Awake()
    {
        _cardNumber = Random.Range(2, 11);
        _cardType = Random.Range(0, 4);

        _rect = GetComponent<RectTransform>();
        _currentCardInDeckPosition = _currentCardInDeck.GetComponent<RectTransform>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_canPutInTheDeck)
        {
            TriPeaksManager.Instance.MoveTaken();
            TriPeaksManager.Instance.remainingDeckCards.Remove(this);
            _canPutInTheDeck = false;
            _currentCardInDeck.cardNumber = _cardNumber;
            _rect.DOMove(_currentCardInDeckPosition.position, _animationDuration);
            Invoke(nameof(OpenCard), _animationDuration / 2f);
        }
    }
    private void OpenCard()
    {
        _cardNumberTMP.text = _cardNumber.ToString();
        _cardTypeImage.color = _cardTypeColor[_cardType];
        _rect.parent = _currentCardInDeckPosition;
    }
}
