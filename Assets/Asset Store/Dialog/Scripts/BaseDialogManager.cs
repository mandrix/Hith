using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Dialog.ScriptableObjects;

namespace Dialog
{

	public abstract class BaseDialogManager : MonoBehaviour
	{

		public static BaseDialogManager instance;

		public GameObject DialogBox;
		public GameObject TimerBar;
		public Scrollbar TimerBarScrollBar;
		public GameObject CloseButton;
		public GameObject PrevButton;
		public GameObject Alt1Button;
		public GameObject Alt2Button;
		public GameObject NextButton;
		public TMPro.TextMeshProUGUI NPCName;
		public RawImage NPCImage;
		public TMPro.TextMeshProUGUI Content;
		public GameObject ButtonBar;
		public Text PrevButtonText;
		public Text Alt1ButtonText;
		public Text Alt2ButtonText;
		public Text NextButtonText;

		protected int CurrentLineIndex = 0;
		protected Lines CurrentLine;
		protected Dialogs CurrentDialog;
		protected BaseNPCBehaviour CurrentNPC;

		UnityEvent alt1Triggers = new UnityEvent();
		UnityEvent alt2Triggers = new UnityEvent();
		UnityEvent openTriggers = new UnityEvent();
		UnityEvent closeTriggers = new UnityEvent();
		float timeoutTime = 0;
		float maxTimeout = 0;
		bool isShown = false;
		bool closedHasFired = false;
		Coroutine activeCoroutine;

		BindingFlags bindFlags = BindingFlags.NonPublic | BindingFlags.Instance;

		public virtual void Awake()
		{
			instance = this;
			CloseButton.GetComponent<Button>().onClick.AddListener(HideDialog);
			NextButton.GetComponent<Button>().onClick.AddListener(ShowNextLine);
			PrevButton.GetComponent<Button>().onClick.AddListener(ShowPrevLine);
			Alt1Button.GetComponent<Button>().onClick.AddListener(FireAlt1Triggers);
			Alt2Button.GetComponent<Button>().onClick.AddListener(FireAlt2Triggers);
			DialogBox.SetActive(false);
		}

		private void Update()
		{
			if (isShown && CurrentLine && CurrentDialog && CurrentDialog.HasHotKeys)
			{
				if (CurrentDialog.HasCloseButton && Input.GetKeyDown(CurrentDialog.CloseHotKey))
				{
					HideDialog();
				}
				else if (CurrentLine.HasPrev && Input.GetKeyDown(CurrentDialog.PrevHotKey))
				{
					ShowPrevLine();
				}
				else if (CurrentLine.HasNext && Input.GetKeyDown(CurrentDialog.NextHotKey))
				{
					ShowNextLine();
				}
				else if (CurrentLine.HasAlternative1 && Input.GetKeyDown(CurrentDialog.Alt1HotKey))
				{
					FireAlt1Triggers();
				}
				else if (CurrentLine.HasAlternative2 && Input.GetKeyDown(CurrentDialog.Alt2HotKey))
				{
					FireAlt2Triggers();
				}
			}
		}

		public virtual void HideDialog()
		{
			if (!closedHasFired)
			{
				FireClosedTriggers();
				closedHasFired = true;
			}
			StopActiveCoroutine();
			DeactivateDialogGameObject();
			isShown = false;
		}

		public virtual void OpenDialog(BaseNPCBehaviour NPC, Dialogs dialog, int lineIndex)
		{
			CurrentDialog = dialog;
			CurrentNPC = NPC;
			CurrentLineIndex = lineIndex;
			ShowLine();
		}

		public virtual void ShowNextLine()
		{
			if (CurrentLineIndex <= CurrentDialog.Lines.Length - 1)
			{
				ShowLine(CurrentLineIndex + 1);
			}
		}

		public virtual void ShowLine(int lineIndex)
		{
			if (!closedHasFired)
			{
				FireClosedTriggers();
				closedHasFired = true;
			}
			StopActiveCoroutine();
			CurrentLineIndex = lineIndex;
			CurrentLine = CurrentDialog.Lines[CurrentLineIndex];

			// Setup Events
			if (CurrentLine.OnOpenTriggerEvents != null)
			{
				openTriggers = CurrentLine.OnOpenTriggerEvents;
			}
			else
			{
				openTriggers = new UnityEvent();
			}
			if (CurrentLine.OnCloseTriggerEvents != null)
			{
				closeTriggers = CurrentLine.OnCloseTriggerEvents;
			}
			else
			{
				closeTriggers = new UnityEvent();
			}

			// call the prep dialog box
			PrepDialogBox(CurrentLine, CurrentNPC);

			if (CurrentLine.HasAlternative1)
			{
				Alt1Button.SetActive(true);
				alt1Triggers = CurrentLine.Alternative1TriggerEvents;
			}
			else
			{
				Alt1Button.SetActive(false);
			}
			if (CurrentLine.HasAlternative2)
			{
				Alt2Button.SetActive(true);
				alt2Triggers = CurrentLine.Alternative2TriggerEvents;
			}
			else
			{
				Alt2Button.SetActive(false);
			}
			if (CurrentLine.HasPrev)
			{
				PrevButton.SetActive(true);
			}
			else
			{
				PrevButton.SetActive(false);
			}
			if (CurrentLine.HasNext)
			{
				NextButton.SetActive(true);
			}
			else
			{
				NextButton.SetActive(false);
			}
			if (CurrentDialog.HasCloseButton)
			{
				CloseButton.SetActive(true);
			}
			else
			{
				CloseButton.SetActive(false);
			}
			if (!isShown)
			{
				ActivateDialogGameObject();
			}

			if (CurrentLine.IsAutoClosing)
			{
				activeCoroutine = StartCoroutine(TimeoutCountDown(CurrentLine.AutoCloseTimeout));
			}
			else
			{
				TimerBar.SetActive(false);
			}
			closedHasFired = false;
			FireOpenedTriggers();
			isShown = true;
		}

		public void ShowLine()
		{
			ShowLine(CurrentLineIndex);
		}

		public virtual void ShowPrevLine()
		{
			if (CurrentLineIndex > 0)
			{
				ShowLine(CurrentLineIndex - 1);
			}
		}

		protected virtual void PrepDialogBox(Lines line, BaseNPCBehaviour npc)
		{
			if (line.OverrideNPC)
			{
				NPCName.text = line.NPCName;
				if (line.NPCThumbnail != null)
				{
					NPCImage.texture = line.NPCThumbnail;
					NPCImage.gameObject.SetActive(true);
				}
				else
				{
					NPCImage.gameObject.SetActive(false);
				}
			}
			else
			{
				NPCName.text = npc.Name;
				if (npc.Thumbnail != null)
				{
					NPCImage.texture = npc.Thumbnail;
					NPCImage.gameObject.SetActive(true);
				}
				else
				{
					NPCImage.gameObject.SetActive(false);
				}
			}
			Content.text = line.Line;
			if (line.HasPrev || line.HasNext || line.HasAlternative1 || line.HasAlternative2)
			{
				ButtonBar.SetActive(true);
			}
			else
			{
				ButtonBar.SetActive(false);
			}
			PrevButtonText.text = (line.PrevText.Length > 0 ? line.PrevText : "Prev") + (CurrentDialog.HasHotKeys && CurrentDialog.PrevHotKey != KeyCode.None ? " [" + CurrentDialog.PrevHotKey.ToString() + "]" : "");
			NextButtonText.text = (line.NextText.Length > 0 ? line.NextText : "Next") + (CurrentDialog.HasHotKeys && CurrentDialog.NextHotKey != KeyCode.None ? " [" + CurrentDialog.NextHotKey.ToString() + "]" : "");
			Alt1ButtonText.text = (CurrentLine.AlternativeText1.Length > 0 ? CurrentLine.AlternativeText1 : "Alt 1") + (CurrentDialog.HasHotKeys && CurrentDialog.Alt1HotKey != KeyCode.None ? " [" + CurrentDialog.Alt1HotKey.ToString() + "]" : "");
			Alt2ButtonText.text = (CurrentLine.AlternativeText2.Length > 0 ? CurrentLine.AlternativeText2 : "Alt 2") + (CurrentDialog.HasHotKeys && CurrentDialog.Alt2HotKey != KeyCode.None ? " [" + CurrentDialog.Alt2HotKey.ToString() + "]" : "");
		}

		void StopActiveCoroutine()
		{
			if (activeCoroutine != null)
			{
				StopCoroutine(activeCoroutine);
			}
			activeCoroutine = null;
		}

		IEnumerator TimeoutCountDown(float timeout)
		{
			maxTimeout = timeoutTime = timeout > 0 ? timeout : 3;
			yield return new WaitForEndOfFrame();
			TimerBar.SetActive(true);
			TimerBarScrollBar.size = 1;
			yield return new WaitUntil(TimeoutIs0);
			HideDialog();
		}

		bool TimeoutIs0()
		{
			TimerBarScrollBar.size = Mathf.Clamp01(timeoutTime / maxTimeout);
			return (timeoutTime -= Time.deltaTime) < 0;
		}

		void FireOpenedTriggers()
		{
			for (int i = 0; i < openTriggers.GetPersistentEventCount(); i++)
			{
				string name = openTriggers.GetPersistentTarget(i).name;
				foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
				{
					if (gameObj.name == name || gameObj.name == name + "(Clone)")
					{
						ApplyTrigger(gameObj, openTriggers, i);
					}
				}
			}
		}

		void FireClosedTriggers()
		{
			for (int i = 0; i < closeTriggers.GetPersistentEventCount(); i++)
			{
				string name = closeTriggers.GetPersistentTarget(i).name;
				foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
				{
					if (gameObj.name == name || gameObj.name == name + "(Clone)")
					{
						ApplyTrigger(gameObj, closeTriggers, i);
					}
				}
			}
		}

		void FireAlt1Triggers()
		{
			for (int i = 0; i < alt1Triggers.GetPersistentEventCount(); i++)
			{
				string name = alt1Triggers.GetPersistentTarget(i).name;
				foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
				{
					if (gameObj.name == name || gameObj.name == name + "(Clone)")
					{
						ApplyTrigger(gameObj, alt1Triggers, i);
					}
				}
			}
		}

		void FireAlt2Triggers()
		{
			for (int i = 0; i < alt2Triggers.GetPersistentEventCount(); i++)
			{
				string name = alt2Triggers.GetPersistentTarget(i).name;
				foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
				{
					if (gameObj.name == name || gameObj.name == name + "(Clone)")
					{
						ApplyTrigger(gameObj, alt2Triggers, i);
					}
				}
			}
		}

		protected virtual void DeactivateDialogGameObject()
		{
			DialogBox.SetActive(false);
		}

		protected virtual void ActivateDialogGameObject()
		{
			DialogBox.SetActive(true);
		}

		void ApplyTrigger(GameObject target, UnityEvent triggers, int index)
		{
			FieldInfo persistantCalls = typeof(UnityEventBase).GetField("m_PersistentCalls", bindFlags);
			var persistentCalls = persistantCalls.GetValue(triggers);
			IEnumerable calls = persistentCalls.GetType().GetField("m_Calls", bindFlags).GetValue(persistentCalls) as IEnumerable;
			List<object> callList = calls.Cast<object>().ToList();
			if (callList.Count > 0)
			{
				var persistentCall = callList[index].GetType();
				var argCache = persistentCall.GetField("m_Arguments", bindFlags).GetValue(callList[index]);
				PersistentListenerMode mode = (PersistentListenerMode) persistentCall.GetField("m_Mode", bindFlags).GetValue(callList[index]);
				var argument = new object();
				switch (mode)
				{
					case PersistentListenerMode.Bool:
						argument = (bool) argCache.GetType().GetField("m_BoolArgument", bindFlags).GetValue(argCache);
						break;
					case PersistentListenerMode.Float:
						argument = (float) argCache.GetType().GetField("m_FloatArgument", bindFlags).GetValue(argCache);
						break;
					case PersistentListenerMode.String:
						argument = (string) argCache.GetType().GetField("m_StringArgument", bindFlags).GetValue(argCache);
						if (triggers.GetPersistentMethodName(index) == "SendMessage")
						{
							target.SendMessage((string) argument);
							return;
						}
						break;
					case PersistentListenerMode.Int:
						argument = (int) argCache.GetType().GetField("m_IntArgument", bindFlags).GetValue(argCache);
						break;
					case PersistentListenerMode.Object:
						argument = argCache.GetType().GetField("m_ObjectArgument", bindFlags).GetValue(argCache);
						break;
					case PersistentListenerMode.Void:
						target.SendMessage(triggers.GetPersistentMethodName(index));
						return;
				}
				target.SendMessage(triggers.GetPersistentMethodName(index), argument);
			}
			else
			{
				target.SendMessage(triggers.GetPersistentMethodName(index));
			}
		}
	}
}
