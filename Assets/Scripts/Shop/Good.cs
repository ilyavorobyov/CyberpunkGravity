using UnityEngine;

[CreateAssetMenu(fileName = "Good", menuName = "Create New Good")]
public class Good : ScriptableObject
{
    [SerializeField] private string _label;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;

    public string Label { get; private set; }
    public int Price { get; private set; }
    public Sprite Icon { get; private set; }

    public void SetInfo()
    {
        Label = _label;
        Price = _price;
        Icon = _icon;
    }
}