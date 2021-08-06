//using DG.Tweening;
//using UnityEngine;

//public abstract class DoTweenPopup : MonoBehaviour
//{
//	[Header("DoTweenPopup")]
//	public bool isShowing = true;
//	[SerializeField] protected bool startEnabled = false;
//	[SerializeField] protected RectTransform panelRectTransform = null;
//	[Space()]
//	[SerializeField] protected float duration = 0.35f;
//	[SerializeField] protected Ease easeIn = Ease.OutBack;
//	[SerializeField] protected Ease easeOut = Ease.InBack;

//	protected Tween popupTween = null;

//	protected virtual void Awake()
//	{
//		if (!startEnabled)
//		{
//			panelRectTransform.localScale = new Vector3(0, 0, 0);
//			HidePopup();
//		}
//	}

//	public virtual void ShowPopup()
//	{
//		popupTween?.Kill();
//		popupTween = panelRectTransform.DOScale(new Vector3(1, 1, 1), duration)
//			.OnStart(OnShowPanelStart)
//			.OnComplete(OnShowPanelComplete)
//			.SetEase(easeIn);
//		isShowing = true;
//	}

//	public virtual void HidePopup()
//	{
//		popupTween?.Kill();
//		popupTween = panelRectTransform.DOScale(new Vector3(0, 0, 0), duration)
//			.OnStart(OnHidePanelStart)
//			.OnComplete(OnHidePanelComplete)
//			.SetEase(easeOut);
//		isShowing = false;
//	}

//	public virtual void OnShowPanelStart() { }
//	public virtual void OnShowPanelComplete() { }

//	public virtual void OnHidePanelStart() { }
//	public virtual void OnHidePanelComplete() { }
//}
