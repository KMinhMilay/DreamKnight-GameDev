using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ShowDmg : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI text;
   [SerializeField] private RectTransform rectTransform;
   
   public void Init(float dmg, Vector2 pos)
   {
      text.text = string.Format("-{0}", (int)dmg);
      text.alpha = 1;
      rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(new Vector3(pos.x, pos.y, Camera.main.transform.position.z)) + new Vector3(50f, 50f);
      Sequence sq =  DOTween.Sequence();
      sq.Append(rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + 50f, 1f));
      sq.Join(text.DOFade(0.5f, 1f));
      sq.OnComplete(() =>
      {
         ObjectPoolManager.Unspawn(gameObject);
      });
   }
   
}
