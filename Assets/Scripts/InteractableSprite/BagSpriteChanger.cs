using Assets.Scripts.Quests;
using Assets.Scripts.Quests.Storage;
using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BagSpriteChanger : MonoBehaviour
{
    [SerializeField] private Sprite _openEmptySprite;
    [SerializeField] private Sprite _openFullSprite;
    [SerializeField] private Sprite _closeSprite;

    [SerializeField] private InteractableSprite _interactableSprite;

    private SpriteRenderer _renderer;
    private QuestStorage _bag;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _interactableSprite.Pressed += OnPressed;
    }

    private void Start()
    {
        _renderer.sprite = _closeSprite;
    }

    public void Init(QuestStorage bag)
    {
        _bag = bag != null ? bag : throw new ArgumentNullException(nameof(bag));
        _bag.NewQuestTaken += OnPressed;
        _bag.QuestRemoved += OnPressed;
    }

    public void CloseBag()
    {
        _renderer.sprite = _closeSprite;
    }

    private void OnPressed(Quest _)
    {
        OnPressed();
    }

    private void OnPressed()
    {
        if (_bag.Quests.Count() > 0 && _interactableSprite.IsEnabled == false)
            _renderer.sprite = _openFullSprite;
        else if (_bag.Quests.Count() == 0 && _interactableSprite.IsEnabled == false)
            _renderer.sprite = _openEmptySprite;
    }
}
