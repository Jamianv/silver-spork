using UnityEngine;

using System.Collections;

using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;




[RequireComponent(typeof(Button))]

public class ClickSound : MonoBehaviour, ISelectHandler , IPointerEnterHandler

{


    public AudioClip clickSound;
    public AudioClip hlightSound;



    private Button button { get { return GetComponent<Button>(); } }

    private AudioSource source { get { return GetComponent<AudioSource>(); } }





    // Use this for initialization

    void Start()

    {

        gameObject.AddComponent<AudioSource>();

        source.clip = clickSound;

        source.playOnAwake = false;


    }



    // When highlighted with mouse.
    public void OnPointerEnter(PointerEventData eventData)
    {
        source.PlayOneShot(hlightSound);
        // Do something.
        Debug.Log("<color=red>Event:</color> Completed mouse highlight.");
    }
    // When selected.
    public void OnSelect(BaseEventData eventData)
    {
        source.PlayOneShot(clickSound);
        // Do something.
        Debug.Log("<color=red>Event:</color> Completed selection.");
    }

}