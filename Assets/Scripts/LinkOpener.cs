using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class LinkOpener : MonoBehaviour, IPointerClickHandler
{
	[SerializeField]
	private TextMeshProUGUI m_textMeshPro;
	public Camera MainCamera;

	public void OnPointerClick(PointerEventData eventData)
		{
			int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_textMeshPro, eventData.position, MainCamera); // If you are not in a Canvas using Screen Overlay, put your camera instead of null
			if (linkIndex != -1)
			{                                                                           // was a link clicked?
				TMP_LinkInfo linkInfo = m_textMeshPro.textInfo.linkInfo[linkIndex];
				Application.OpenURL(linkInfo.GetLinkID());
			}
		}
}
