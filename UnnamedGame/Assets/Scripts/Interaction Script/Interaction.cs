using UnityEngine;
using TMPro;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance;

    [SerializeField] private float requiredDistance = 10f;
    TMP_Text interaction_text;

    public GameObject interaction_Info_UI;
    public GameObject ClipBoardText;
    public bool isClipBoardOpen = false;
    public bool IsPaused;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<TMP_Text>();
        ClipBoardText.SetActive(false);
        interaction_Info_UI.SetActive(false);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            var playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

            if (Vector3.Distance(selectionTransform.position, playerPosition) < requiredDistance)
            {
                if (selectionTransform.GetComponent<InteractableObject>())
                {
                    interaction_text.text = selectionTransform.GetComponent<InteractableObject>().GetItemName();
                    if(!PauseSystem.Instance.IsPaused) interaction_Info_UI.SetActive(true);

                    ClipBoardInteraction();
                    DoorInteractionOpen();
                    DoorInteractionClose();
                }
                else interaction_Info_UI.SetActive(false);
            }
            else interaction_Info_UI.SetActive(false);
        }
    }

    public bool DoorInteractionOpen()
    {
        if (interaction_text.text == "open" && Input.GetKey(KeyCode.E) && !PauseSystem.Instance.IsPaused)
        {
            interaction_text.text = "locked";
            return true;
        }
        else return false;
    }

    public bool DoorInteractionClose()
    {
        if (interaction_text.text == "locked" && Input.GetKey(KeyCode.R) && !PauseSystem.Instance.IsPaused)
        {
            interaction_text.text = "open";
            return false;
        }

        else return true;
    }

    private void ClipBoardInteraction()
    {
        if (interaction_text.text == "Clipboard" && Input.GetKey(KeyCode.E) && !PauseSystem.Instance.IsPaused)
        {
            ClipBoardText.SetActive(true);
            isClipBoardOpen = true;
            TogglePause();
        }

        if (isClipBoardOpen && Input.GetKey(KeyCode.R) && !PauseSystem.Instance.IsPaused)
        {
            isClipBoardOpen = false;
            ClipBoardText.SetActive(false);
            if (IsPaused) Unpause();
            interaction_Info_UI.SetActive(false);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void TogglePause()
    {
        Pause();
    }
}



