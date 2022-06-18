using UnityEngine;

public class SpeederHeightChanger : MonoBehaviour
{
    [SerializeField] private GameObject speederArt;

    private SpriteRenderer _mainRenderer;
    private SpriteRenderer[] _effectRenderers;

    private int _highSortingLayer = 110;
    private int _lowSortingLayer = 90;
    private int _layerTrackColliderLow = 9;
    private int _layerTrackColliderHigh = 10;

    private void Awake()
    {
        _mainRenderer = speederArt.GetComponent<SpriteRenderer>();
        _effectRenderers = speederArt.transform.Find("Flames").gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    public void SetHeightHigh()
    {
        gameObject.layer = _layerTrackColliderHigh;

        SetSortingLayerOfSpeeder(_highSortingLayer);
    }

    public void SetHeightLow()
    {
        gameObject.layer = _layerTrackColliderLow;

        SetSortingLayerOfSpeeder(_lowSortingLayer);
    }

    private void SetSortingLayerOfSpeeder(int layer)
    {
        _mainRenderer.sortingOrder = layer;

        foreach(SpriteRenderer renderer in _effectRenderers)
        {
            renderer.sortingOrder = layer - 1;
        }
    }
}
