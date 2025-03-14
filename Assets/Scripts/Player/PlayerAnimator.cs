using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private GameObject model_stand;

    [SerializeField]
    private GameObject model_lunge;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void SetState(PlayerState state)
    {
        switch(state)
        {
            case PlayerState.STANDING:
                model_stand.SetActive(true);
                model_lunge.SetActive(false);
                break;
            case PlayerState.LUNGING:
                model_lunge.SetActive(true);
                model_stand.SetActive(false);
                break;
            default:
                Debug.LogWarning("Invalid PlayerState.");
                model_stand.SetActive(true);
                model_lunge.SetActive(false);
                break;
        }
    }
}