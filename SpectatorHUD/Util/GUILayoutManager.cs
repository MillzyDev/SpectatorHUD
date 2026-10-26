using UnityEngine;

namespace SpectatorHUD.Util;

public class GUILayoutManager
{
    private readonly float _elementHeight;
    private readonly float _elementWidth;
    private readonly float _elementSpacing;
    private readonly float _indentSize;
    private readonly float _spaceSize;
    private readonly Rect _original;
    
    private readonly List<Rect> _cachedRects;
    private int _state;
    private int _currentIndent;
    private Rect _cursor;

    public GUILayoutManager(float startX, float startY, float elementWidth, float elementHeight, float elementSpacing, float indentSize,
        float spaceSize, int cacheSize = 50)
    {
        this._elementWidth = elementWidth;
        this._elementHeight = elementHeight;
        this._elementSpacing = elementSpacing;
        this._indentSize = indentSize;
        this._spaceSize = spaceSize;

        this._cachedRects = new List<Rect>(cacheSize);

        this._cursor = new Rect(startX, startY, elementWidth, elementHeight);
        this._original = this._cursor;
    }

    public void Start()
    {
        this._state = 0;
    }

    public void End()
    {
        // TrimExcess performs a float multiplication for a threshold heuristic and compares that to the capacity
        // Because I am a massive nerd, float multiplication takes ~15 times longer than just a comparison
        // An in the interest of meaningless optimisation, I am not using TrimExcess
        // this._cachedRects.TrimExcess();
        int count = this._cachedRects.Count;
        if (this._cachedRects.Capacity == count)
        {
            this._cachedRects.Capacity = count;
        }
    }

    public void Reset()
    {
        this._cursor = this._original;
    }

    public void Indent()
    {
        this._currentIndent++;
    }

    public void Outdent()
    {
        this._currentIndent--;
    }

    public void Column(int column)
    {
        this._cursor.x = this._original.x + column * (this._elementWidth + this._spaceSize);
        this._cursor.y = this._original.y;
    }

    public void Space(int times = 1)
    {
        this._cursor.y += this._spaceSize * times;
    }

    public Rect GetNext()
    {
        if (this._state < this._cachedRects.Count)
        {
            return this._cachedRects[this._state++];
        }

        Rect rect = this._cursor;
        rect.x += this._indentSize * this._currentIndent;

        this._state = this._cachedRects.Count;
        this._cachedRects.Add(rect);
        this._state++;

        this._cursor.y += this._elementHeight + this._elementSpacing;

        return rect;
    }
}