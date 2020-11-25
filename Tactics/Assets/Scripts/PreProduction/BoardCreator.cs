using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class BoardCreator : MonoBehaviour
{
    [SerializeField] GameObject tileViewPrefab;
    [SerializeField] GameObject tileSelectionIndicatorPrefab;

    [SerializeField] int width = 10;
    [SerializeField] int depth = 10;
    [SerializeField] int height = 14;

    Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
    Dictionary<(Point,float), Tile> heightTile = new Dictionary<(Point, float), Tile>();

    [SerializeField] Point pos;
    [SerializeField] int tileHeight;
    [SerializeField] LevelData levelData;
    Transform marker
    {
        get
        {
            if (_marker == null)
            {
                GameObject instance = Instantiate(tileSelectionIndicatorPrefab) as GameObject;
                _marker = instance.transform;
            }
            return _marker;
        }
    }
    Transform _marker;

    Rect RandomRect()
    {
        int x = UnityEngine.Random.Range(0, width);
        int y = UnityEngine.Random.Range(0, depth);
        int w = UnityEngine.Random.Range(1, width - x + 1);
        int h = UnityEngine.Random.Range(1, depth - y + 1);
        return new Rect(x, y, w, h);
    }

    Tile Create()
    {
        GameObject instance = Instantiate(tileViewPrefab) as GameObject;
        instance.transform.parent = transform;
        return instance.GetComponent<Tile>();
    }

    Tile GetOrCreate(Point p, int h)
    {
        if (tiles.ContainsKey(p) && heightTile.ContainsKey((p,h)))
            return tiles[p];

        Tile t = Create();
        t.Load(p, 0);
        tiles.Add(p, t);
        heightTile.Add((p,h), t);
        return t;
    }

    Tile OCreate(Point p, int h)
    {
        if (heightTile.ContainsKey((p, h)))
            return heightTile[(p,h)];

        Tile t = Create();
        t.Load(p, h);
        t.slope = false;
        t.outCorner = false;
        t.inCorner = false;
        //tiles.Add(p, t);
        heightTile.Add((p, h), t);
        return t;
    }

    Tile SOCreate(Point p, int h)
    {
        if (heightTile.ContainsKey((p, h)))
            return heightTile[(p, h)];

        Tile t = Create();
        t.Load(p, h);
        t.slope = true;
        t.outCorner = false;
        t.inCorner = false;

        //tiles.Add(p, t);
        heightTile.Add((p, h), t);
        return t;
    }

    Tile OOCreate(Point p, int h)
    {
        if (heightTile.ContainsKey((p, h)))
            return heightTile[(p, h)];

        Tile t = Create();
        t.Load(p, h);
        t.slope = false;
        t.outCorner = true;
        t.inCorner = false;
        //tiles.Add(p, t);
        heightTile.Add((p, h), t);
        return t;
    }

    Tile IOCreate(Point p, int h)
    {
        if (heightTile.ContainsKey((p, h)))
            return heightTile[(p, h)];

        Tile t = Create();
        t.Load(p, h);
        t.slope = false;
        t.outCorner = false;
        t.inCorner = true;
        //tiles.Add(p, t);
        heightTile.Add((p, h), t);
        return t;
    }

    void GrowSingle(Point p, int h)
    {
        Tile t;
        if (tiles.ContainsKey(p))
        {
            t = tiles[p];
            if (t.height < height)
                t.Grow();
        }
        else
        {
            t = GetOrCreate(p, h);

        }

    }

    void GrowRect(Rect rect)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                GrowSingle(p, 0);
            }
        }
    }

    void ShrinkSingle(Point p, int h)
    {
        if (!tiles.ContainsKey(p))
            return;

        Tile t = tiles[p];
        t.Shrink();

        if (t.height <= 0)
        {
            tiles.Remove(p);
            heightTile.Remove((p, h));
            DestroyImmediate(t.gameObject);
        }
    }

    void ShrinkRect(Rect rect)
    {
        for (int y = (int)rect.yMin; y < (int)rect.yMax; ++y)
        {
            for (int x = (int)rect.xMin; x < (int)rect.xMax; ++x)
            {
                Point p = new Point(x, y);
                ShrinkSingle(p, 0);
            }
        }
    }

    void RotateSingle(Point p, int h)
    {
        if (!heightTile.ContainsKey((p, h)))
            return;

        Tile t = heightTile[(p, h)];
        t.transform.Rotate(0, 90, 0);
    }

    void ORemove(Point p, int h)
    {
        if (!heightTile.ContainsKey((p, h)))
            return;
        Tile t = heightTile[(p, h)];
        heightTile.Remove((p, h));
        DestroyImmediate(t.gameObject);
    }

    public void GrowArea()
    {
        Rect r = RandomRect();
        GrowRect(r);
    }
    public void ShrinkArea()
    {
        Rect r = RandomRect();
        ShrinkRect(r);
    }

    public void Grow()
    {
        GrowSingle(pos, tileHeight);
    }
    public void Shrink()
    {
        ShrinkSingle(pos, tileHeight);
    }

    public void Rotate()
    {
        RotateSingle(pos, tileHeight);
    }

    public void CreateTile()
    {
        OCreate(pos, tileHeight);
    }

    public void SCreateTile()
    {
        SOCreate(pos, tileHeight);
    }

    public void OCreateTile()
    {
        OOCreate(pos, tileHeight);
    }

    public void ICreateTile()
    {
        IOCreate(pos, tileHeight);
    }

    public void RemoveTile()
    {
        ORemove(pos, tileHeight);
    }

    public void UpdateMarker()
    {
        Tile t = tiles.ContainsKey(pos) ? tiles[pos] : null;
        marker.localPosition = t != null ? new Vector3(t.center.x, tileHeight*0.125f, t.center.z) : new Vector3(pos.x, tileHeight*0.125f, pos.y);
        //marker.localPosition += new Vector3(0, tileHeight * 0.125f, 0);
    }

    public void Clear()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
            DestroyImmediate(transform.GetChild(i).gameObject);
        tiles.Clear();
        heightTile.Clear();

    }

    public void Save()
    {
        string filePath = Application.dataPath + "/Resources/Levels";
        if (!Directory.Exists(filePath))
            CreateSaveDirectory();

        LevelData board = ScriptableObject.CreateInstance<LevelData>();
        board.tiles = new List<Vector3>(heightTile.Count);
        board.slope = new List<bool>(heightTile.Count);
        board.rotation = new List<Vector3>(heightTile.Count);
        board.height = new List<float>(heightTile.Count);
        board.outCorner = new List<bool>(heightTile.Count);
        board.inCorner = new List<bool>(heightTile.Count);
        foreach (Tile t in heightTile.Values)
        {
            board.tiles.Add(new Vector3(t.pos.x, t.height, t.pos.y));
            board.slope.Add(t.slope);
            board.outCorner.Add(t.outCorner);
            board.inCorner.Add(t.inCorner);
            board.rotation.Add(t.transform.rotation.eulerAngles);
            board.height.Add(t.transform.position.y);
        }

        string fileName = string.Format("Assets/Resources/Levels/{1}.asset", filePath, name);
        AssetDatabase.CreateAsset(board, fileName);
    }

    public void Load()
    {
        Clear();
        if (levelData == null)
            return;
        int index = 0;
        foreach (Vector3 v in levelData.rotation)
        {
            Tile t = Create();
            t.Load2(levelData.tiles[index], levelData.slope[index], levelData.outCorner[index], levelData.inCorner[index], v);
            //tiles.Add(t.pos, t);
            heightTile.Add((t.pos, t.height), t);
            index += 1;
        }

        //foreach (Vector3 v in levelData.tiles)
        //{
        //    Tile t = Create();
        //    t.Load(v, levelData.slope);
        //    tiles.Add(t.pos, t);
        //}
    }

    void CreateSaveDirectory()
    {
        string filePath = Application.dataPath + "/Resources";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets", "Resources");
        filePath += "/Levels";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        AssetDatabase.Refresh();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
