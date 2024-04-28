using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiRewardCard : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public RewardManager rewardManager;
    public Reward reward;
    public Image frontImage;
    public Image backImage;
    public Image infoImage;
    public TextMeshProUGUI itemNameFront;
    public TextMeshProUGUI itemNameDack;
    public TextMeshProUGUI quantity;
    public TextMeshProUGUI description;
    public TextMeshProUGUI powerCost;
    [Header("Hover")]
    public float hoverEffectSpeed;
    public Vector3 hoverScale;
    public Ease hoverEaseType = Ease.Linear;
    [Header("Move")]
    public bool doMoveOnStart;
    public float moveSpeed;
    public Vector2 gotoPos;
    public Ease moveEaseType = Ease.Linear;
    public UnityEvent moveEndEvent;
    Vector3 defaultScale;
    Tween scaleUpTween, scaleDownTween,gotoTween,backToTween;
    bool canHover= false;
    private void Start()
    {
        defaultScale = transform.localScale;
    }
    private void OnEnable()
    {
        canHover = false;
        if (doMoveOnStart)
        {
            GotoCardSelectPos();
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        rewardManager.SetPlaceableObject(reward,this);
    }
    public void CanHover() { canHover = true; }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!canHover) { return; }
        scaleDownTween.Kill();
        scaleUpTween.Kill();
        scaleUpTween = transform.DOScale(hoverScale, hoverEffectSpeed).SetEase(hoverEaseType);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!canHover) { return; }
        scaleDownTween.Kill();
        scaleUpTween.Kill();
        scaleDownTween = transform.DOScale(defaultScale, hoverEffectSpeed).SetEase(hoverEaseType);
    }

    public void SetCardDerails(Reward reward)
    {
        this.reward = reward;
        frontImage.sprite = reward.cardSprite;
        itemNameFront.text = reward.name;
        quantity.text = "x"+reward.quantity.ToString();
        description.text = reward.description;
    }

    public void GotoCardSelectPos()
    {
        gotoTween.Kill();
        gotoTween = transform.DOLocalMoveX(gotoPos.x, moveSpeed).SetEase(moveEaseType);
        gotoTween.OnComplete(() =>
        {
            moveEndEvent?.Invoke();
        });
    }
}
