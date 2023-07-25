using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class InfiniteScroll : UIBehaviour
{
	[SerializeField]
	private RectTransform itemPrototype;

	[SerializeField, Range(0, 30)]
	int instantateItemCount = 9;

	[SerializeField]
	private Direction direction;

	public OnItemPositionChange onUpdateItem = new OnItemPositionChange();

	[System.NonSerialized]
	public LinkedList<RectTransform> itemList = new LinkedList<RectTransform>();


	protected float diffPreFramePosition = 0;

	protected int currentItemNo = 0;

	[Header("������� �����ɼ� $20")]



	[SerializeField] private int maxWheelCount; // �ִ�� ���� �� �ִ� �� Ƚ��
	[SerializeField] private int fadeOutObjCount; // ����� ���� ������Ʈ���� ������� ���������
	[SerializeField] private float fadeTime; // ������µ� �ɸ��� �ð�
	[SerializeField] private int oneTimeDeleteObject; // �ѹ��� ����� ������Ʈ ����

	[SerializeField] private Image[] peopleImages;


	private int currentWheelCount = 0;
	private int checkWheelCount = 0;

	public UnityEvent scrollCheckEvent; // ��ũ�� ���� ���� �� �̺�Ʈ 
	public enum Direction
	{
		Vertical,
		Horizontal,
	}

	// cache component

	private RectTransform _rectTransform;
	protected RectTransform rectTransform
	{
		get
		{
			if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
			return _rectTransform;
		}
	}

	private float anchoredPosition
	{
		get
		{
			return direction == Direction.Vertical ? -rectTransform.anchoredPosition.y : rectTransform.anchoredPosition.x;
		}
	}

	private float _itemScale = -1;
	public float itemScale
	{
		get
		{
			if (itemPrototype != null && _itemScale == -1)
			{
				_itemScale = direction == Direction.Vertical ? itemPrototype.sizeDelta.y : itemPrototype.sizeDelta.x;
			}
			return _itemScale;
		}
	}

	protected override void Start()
	{
		var controllers = GetComponents<MonoBehaviour>()
				.Where(item => item is IInfiniteScrollSetup)
				.Select(item => item as IInfiniteScrollSetup)
				.ToList();

		// create items

		var scrollRect = GetComponentInParent<ScrollRect>();
		scrollRect.horizontal = direction == Direction.Horizontal;
		scrollRect.vertical = direction == Direction.Vertical;
		scrollRect.content = rectTransform;

		itemPrototype.gameObject.SetActive(false);

		for (int i = 0; i < instantateItemCount; i++)
		{
			var item = GameObject.Instantiate(itemPrototype) as RectTransform;
			item.SetParent(transform, false);
			item.name = i.ToString();
			item.anchoredPosition = direction == Direction.Vertical ? new Vector2(0, -itemScale * i) : new Vector2(itemScale * i, 0);
			itemList.AddLast(item);

			item.gameObject.SetActive(true);

			foreach (var controller in controllers)
			{
				controller.OnUpdateItem(i, item.gameObject);
			}
		}

		foreach (var controller in controllers)
		{
			controller.OnPostSetupItems();
		}
	}

	void Update()
	{
		if (itemList.First == null)
		{
			return;
		}

		// while �� �� ������ ó������.
		while (anchoredPosition - diffPreFramePosition < -itemScale * 2)
		{
			diffPreFramePosition -= itemScale;

			var item = itemList.First.Value;
			itemList.RemoveFirst();
			itemList.AddLast(item);

			var pos = itemScale * instantateItemCount + itemScale * currentItemNo;
			item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

			onUpdateItem.Invoke(currentItemNo + instantateItemCount, item.gameObject);

			currentItemNo++;

		
			//if(currentItemNo % itemList.Count == 0 && currentItemNo != 0)
   //         {
			//	Debug.Log("���� �� ��: " + currentWheelCount);
			//	Debug.Log("���� �� ��: " + checkWheelCount);
			//	currentWheelCount++;
			//	checkWheelCount++;
				
			//}

			int countResult = maxWheelCount / fadeOutObjCount;

			//if (currentWheelCount  == countResult) // currentWheelCheck �� ī��Ʈ�� "maxWheelCount  / fadeOutCount; 
			//{
			//	currentWheelCount = 0;

			//	int deleteCount = Mathf.Min(oneTimeDeleteObject, peopleImages.Length);
			//	int startIndex = (currentWheelCount - 1) * deleteCount;
			//	int endIndex = startIndex + deleteCount;
			//	// scrollCheckEvent.Invoke();
			//	for (int i = startIndex; i < endIndex && i < peopleImages.Length; i++)
			//	{
			//		if (i >= 0 && i < peopleImages.Length && peopleImages[i].gameObject.activeSelf)
			//		{
			//			StartCoroutine(FadeOutImage(peopleImages[i]));
			//		}
			//	}

			//}

			int totalCount = maxWheelCount * fadeOutObjCount;

			// currentWheelCount�� totalCount�� ���Ͽ� �ش� ������ �����ϴ� ��쿡�� ó���մϴ�.
			if (currentWheelCount < totalCount)
			{
				if (currentItemNo % itemList.Count == 0 && currentItemNo != 0)
				{
					currentWheelCount++;
					checkWheelCount++;

					Debug.Log("currentWheelCount : " + currentWheelCount + " || " + "CheckWheelCount : " + checkWheelCount);

					if (currentWheelCount % fadeOutObjCount == 0)
					{
						int deleteCount = Mathf.Min(oneTimeDeleteObject, peopleImages.Length);
						int startIndex = (currentWheelCount / fadeOutObjCount - 1) * deleteCount;
						int endIndex = startIndex + deleteCount;

						for (int i = startIndex; i < endIndex && i < peopleImages.Length; i++)
						{
							if (i >= 0 && i < peopleImages.Length && peopleImages[i].gameObject.activeSelf)
							{
								StartCoroutine(FadeOutImage(peopleImages[i]));
							}
						}
					}
				}
			}

		}

		while (anchoredPosition - diffPreFramePosition > 0)
		{
			diffPreFramePosition += itemScale;

			var item = itemList.Last.Value;
			itemList.RemoveLast();
			itemList.AddFirst(item);

			currentItemNo--;

			var pos = itemScale * currentItemNo;
			item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);
			onUpdateItem.Invoke(currentItemNo, item.gameObject);
        }

		if(checkWheelCount >= maxWheelCount)
        {
			Debug.Log("���� ������ �մϴ�");
			// ���⿡�� ����Ƽ �̺�Ʈ�� �־ ó���ϴ°��� ���� �� ���ƺ��̱� ��;; 

        }

    }

	private System.Collections.IEnumerator FadeOutImage(Image image)
	{
		float elapsedTime = 0;
		Color originalColor = image.color;
		Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

		while (elapsedTime < fadeTime)
		{
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(elapsedTime / fadeTime);
			image.color = Color.Lerp(originalColor, targetColor, t);
			yield return null;
		}

		image.color = targetColor;
		image.gameObject.SetActive(false);
	}

	[System.Serializable]
    public class OnItemPositionChange : UnityEvent<int, GameObject> { }

}