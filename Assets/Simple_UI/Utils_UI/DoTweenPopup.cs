//using DG.Tweening;
//using UnityEngine;

//public abstract class DoTweenPopup : MonoBehaviour
//{
//    [Header("DoTweenPopup")]
//    [SerializeField] private GameObject panelGO = null;
//    [Space()]
//    [SerializeField] protected float duration = 0.35f;
//    [SerializeField] protected Ease easeIn = Ease.Flash;
//    [SerializeField] protected Ease easeOut = Ease.Flash;

//    protected RectTransform panelRectTransform = null;
//    protected Tween popupTween = null;

//    protected virtual void Awake()
//    {
//        panelRectTransform = this.panelGO.GetComponent<RectTransform>();
//        panelRectTransform.localScale = new Vector3(0, 0, 0);

//        HidePopup();
//    }

//    public virtual void ShowPopup()
//    {
//        popupTween.Kill();
//        popupTween = panelRectTransform.DOScale(new Vector3(1, 1, 1), duration)
//            .OnStart(OnShowPanelStart)
//            .OnComplete(OnShowPanelComplete)
//            .SetEase(easeIn);
//    }

//    public virtual void HidePopup()
//    {
//        popupTween.Kill();
//        popupTween = panelRectTransform.DOScale(new Vector3(0, 0, 0), duration)
//            .OnStart(OnHidePanelStart)
//            .OnComplete(OnHidePanelComplete)
//            .SetEase(easeOut);
//    }

//    public virtual void OnShowPanelStart() { }
//    public virtual void OnShowPanelComplete() { }

//    public virtual void OnHidePanelStart() { }
//    public virtual void OnHidePanelComplete() { }
//}