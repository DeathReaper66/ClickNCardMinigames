using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [NonSerialized] public int cardNumber;
    [NonSerialized] private int _cardType;
    [SerializeField] private TMP_Text _cardNumberTMP;
    [SerializeField] private Image _cardTypeImage;
    [SerializeField] private Color[] _cardTypeColor;

    [SerializeField] private Card _leftFrontCard;
    [SerializeField] private Card _rightFrontCard;

    [SerializeField] private float _animationDuration;
    [SerializeField] private CurrentCardInDeck _currentCardInDeck;
    private RectTransform _currentCardInDeckPosition;

    [NonSerialized] public Action OnCardPutInTheDeck;
    private bool _canPutInTheDeck = false;
    private int _toOpenCardNumber = 2;
    private RectTransform _rect;

    private void Awake()
    {
        cardNumber = Random.Range(2, 11);
        _cardType = Random.Range(0, 4);

        _rect = GetComponent<RectTransform>();
        _currentCardInDeckPosition = _currentCardInDeck.GetComponent<RectTransform>();

        if (!_leftFrontCard)
        {
            _cardNumberTMP.text = cardNumber.ToString();
            _cardTypeImage.color = _cardTypeColor[_cardType];
            _canPutInTheDeck = true;
        }
        else
        {
            _leftFrontCard.OnCardPutInTheDeck += () => OpenCard();
            _rightFrontCard.OnCardPutInTheDeck += () => OpenCard();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_canPutInTheDeck)
        {
            int num = Mathf.Abs(cardNumber - _currentCardInDeck.cardNumber);
            int minMaxManagerNum = Mathf.Abs(TriPeaksManager.Instance.maxCardNumber - TriPeaksManager.Instance.minCardNumber);

            if (_currentCardInDeck.cardNumber != 1)
            {
                if (num == 1 || num == minMaxManagerNum)
                {
                    CardInDeck();
                    _rect.DOMove(_currentCardInDeckPosition.position, _animationDuration);
                    Invoke(nameof(ToChild), _animationDuration / 2f);
                }
            }
            else
            {
                if (num < 2 || num > 8)
                {
                    CardInDeck();
                    _rect.DOMove(_currentCardInDeckPosition.position, _animationDuration);
                    Invoke(nameof(ToChild), _animationDuration / 2f);
                }
            }
        }
    }

    private void ToChild()
    {
        _rect.parent = _currentCardInDeckPosition;
    }

    private void CardInDeck()
    {
        TriPeaksManager.Instance.MoveTaken();
        TriPeaksManager.Instance.interactableCards.Remove(this);
        _canPutInTheDeck = false;
        _currentCardInDeck.cardNumber = cardNumber;
        OnCardPutInTheDeck?.Invoke();
    }

    private void OpenCard()
    {
        _toOpenCardNumber--;
        if (_toOpenCardNumber == 0)
        {
            TriPeaksManager.Instance.interactableCards.Add(this);
            _cardNumberTMP.text = cardNumber.ToString();
            _cardTypeImage.color = _cardTypeColor[_cardType];
            _canPutInTheDeck = true;
        }
    }
}
