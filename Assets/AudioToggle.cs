using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class AudioToggle : MonoBehaviour
{

    [SerializeField] private Sprite audioOnSprite;
    [SerializeField] private Sprite audioOffSprite;
    [SerializeField] private Image checkboxImage;
    [SerializeField] private DropFromMousePosition dropFromMousePosition;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
    }

    public void audioToggle(bool isOn)
    {
        if (isOn)
        {
            TurnOnAudio();
        }
        else
        {
            MuteAudio();
        }
    }
    private void MuteAudio()
    {
        audioSource.mute = true;
        checkboxImage.sprite = audioOffSprite;
    }

    private void TurnOnAudio()
    {
        audioSource.mute = false;
        checkboxImage.sprite = audioOnSprite;
    }

    public void OnHover()
    {
        if (dropFromMousePosition != null)
        {
            dropFromMousePosition.isHoveringAudioToggle = true;
        }
    }

    public void OnHoverExit()
    {
        if (dropFromMousePosition != null)
        {
            dropFromMousePosition.isHoveringAudioToggle = false;
        }
    }
}
