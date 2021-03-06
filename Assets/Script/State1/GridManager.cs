using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.NiceVibrations;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public List<Sprite> Sprites = new List<Sprite>();
    public GameObject TilePrefab;
    public int GridDimension = 8;
    public float Distance = 1.0f;
    private GameObject[,] Grid;

    public GameObject Player;
    public GameObject[] weapons;

    public GameObject[] CloneWeapon;


    public List<GameObject> horizontalMatchedTiles = new List<GameObject>();

    public int StartingMoves = 50;
    private int _numMoves;
    public int matchCount;

    public void Update()
    {
      

    }

    public int NumMoves
    {
        get
        {
            return _numMoves;
        }

        set
        {
            _numMoves = value;

        }
    }

    private int _score;
    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;

        }
    }



    public static GridManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        instance = this;
        Score = 0;
        NumMoves = StartingMoves;
        DOTween.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        Grid = new GameObject[GridDimension, GridDimension];

        StartCoroutine(InitGrid());
    }

    IEnumerator InitGrid() 
    {
        Vector3 positionOffset = transform.position - new Vector3(GridDimension * Distance / 2.0f, GridDimension * Distance / 2.0f, 0);

        for (int row = 0; row < GridDimension; row++)
            for (int column = 0; column < GridDimension; column++)
            {
                GameObject newTile = Instantiate(TilePrefab);

                List<Sprite> possibleSprites = new List<Sprite>(Sprites);

                //Choose what sprite to use for this cell
                Sprite left1 = GetSpriteAt(column - 1, row);
                Sprite left2 = GetSpriteAt(column - 2, row);
                if (left2 != null && left1 == left2)
                {
                    possibleSprites.Remove(left1);
                }

                Sprite down1 = GetSpriteAt(column, row - 1);
                Sprite down2 = GetSpriteAt(column, row - 2);
                if (down2 != null && down1 == down2)
                {
                    possibleSprites.Remove(down1);
                }

                SpriteRenderer renderer = newTile.GetComponent<SpriteRenderer>();
                renderer.sprite = possibleSprites[Random.Range(0, possibleSprites.Count)];

                Tile tile = newTile.AddComponent<Tile>();
                tile.Position = new Vector2Int(column, row);



                newTile.transform.parent = transform;
                newTile.transform.position = new Vector3(column * Distance, row * Distance, 0) + positionOffset;

                newTile.transform.DOShakeScale(.5f, 0.25f);
                yield return new WaitForSeconds(0.05f);
                Grid[column, row] = newTile;
            }
    }

    Sprite GetSpriteAt(int column, int row)
    {
        if (column < 0 || column >= GridDimension
         || row < 0 || row >= GridDimension)
            return null;
        GameObject tile = Grid[column, row];
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();

        return renderer.sprite;
    }

    SpriteRenderer GetSpriteRendererAt(int column, int row)
    {
        if (column < 0 || column >= GridDimension
         || row < 0 || row >= GridDimension)
            return null;
        GameObject tile = Grid[column, row];
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
        return renderer;
    }

    public void SwapTiles(Vector2Int tile1Position, Vector2Int tile2Position)
    {
        GameObject tile1 = Grid[tile1Position.x, tile1Position.y];
        SpriteRenderer renderer1 = tile1.GetComponent<SpriteRenderer>();

        GameObject tile2 = Grid[tile2Position.x, tile2Position.y];
        SpriteRenderer renderer2 = tile2.GetComponent<SpriteRenderer>();

        Sprite temp = renderer1.sprite;
        renderer1.sprite = renderer2.sprite;
        renderer2.sprite = temp;

        matchCount = 0;

        bool changesOccurs = CheckMatches();
        if (!changesOccurs)
        {

            temp = renderer1.sprite;
            renderer1.sprite = renderer2.sprite;
            renderer2.sprite = temp;


            renderer1.gameObject.transform.DOShakeScale(0.5f, 0.25f);
            renderer2.gameObject.transform.DOShakeScale(0.5f, 0.25f);
        }
        else
        {

            NumMoves--;
            do
            {
                FillHoles();
            } while (CheckMatches());
            if (NumMoves <= 0)
            {
                NumMoves = 0;
            }

            BotManager.instance.Attack();

        }
    }


    float speed = 10;

    bool CheckMatches()
    {
        horizontalMatchedTiles.Clear();
        HashSet<SpriteRenderer> matchedTiles = new HashSet<SpriteRenderer>();
        for (int row = 0; row < GridDimension; row++)
        {
            for (int column = 0; column < GridDimension; column++)
            {
                SpriteRenderer current = GetSpriteRendererAt(column, row);          // bulunduğu tile sprite

                List<SpriteRenderer> horizontalMatches = FindColumnMatchForTile(column, row, current.sprite);

                if (horizontalMatches.Count >= 2)
                {

                    if (Sprites[0] == current.sprite )
                    {
                        matchedTiles.UnionWith(horizontalMatches);
                        matchedTiles.Add(current);
                        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                        StartCoroutine(Billiard());

                    }

                    if (Sprites[1] == current.sprite)
                    {
                        matchedTiles.UnionWith(horizontalMatches);
                        matchedTiles.Add(current);
                        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                        StartCoroutine(Knife());
                        StopCoroutine(Knife());

                    }

                    if (Sprites[2] == current.sprite)
                    {
                        matchedTiles.UnionWith(horizontalMatches);
                        matchedTiles.Add(current);
                        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                        StartCoroutine(Sword());

                    }

                    if (Sprites[3] == current.sprite && Input.GetMouseButton(0))
                    {
                        matchedTiles.UnionWith(horizontalMatches);
                        matchedTiles.Add(current);
                        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                        StartCoroutine(Spear());


                    }

                    if (Sprites[4] == current.sprite )
                    {
                        matchedTiles.UnionWith(horizontalMatches);
                        matchedTiles.Add(current);
                        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                        StartCoroutine(Branch());

                    }

                    matchCount += horizontalMatches.Count + 1;
                    StartCoroutine(DoExplosion(column, row, horizontalMatches));

                }

                List<SpriteRenderer> verticalMatches = FindRowMatchForTile(column, row, current.sprite);
                if (verticalMatches.Count >= 2)
                {
                    if (Sprites[0] == current.sprite)
                    {
                        matchedTiles.UnionWith(verticalMatches);
                        matchedTiles.Add(current);
                        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                        StartCoroutine(Billiard());


                    }
                    if (Sprites[1] == current.sprite )
                    {
                        matchedTiles.UnionWith(horizontalMatches);
                        matchedTiles.Add(current);
                        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                        StartCoroutine(Knife());


                    }
                    if (Sprites[2] == current.sprite )
                    {
                        matchedTiles.UnionWith(horizontalMatches);
                        matchedTiles.Add(current);
                        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                        StartCoroutine(Sword());
                     
                    }
                    if (Sprites[3] == current.sprite)
                    {
                        matchedTiles.UnionWith(horizontalMatches);
                        matchedTiles.Add(current);
                        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                        StartCoroutine(Spear());


                    }
                    if (Sprites[4] == current.sprite)
                    {
                        matchedTiles.UnionWith(horizontalMatches);
                        matchedTiles.Add(current);
                        MMVibrationManager.Haptic(HapticTypes.SoftImpact);
                        StartCoroutine(Branch()); 
                    }

                    matchCount += verticalMatches.Count+1;
                    StartCoroutine(DoExplosion(column, row, verticalMatches));
                }
            }
        }

        foreach (SpriteRenderer renderer in matchedTiles)
        {
            renderer.sprite = null;
        }

        Score += matchedTiles.Count;

        return matchedTiles.Count > 0;
    }

    IEnumerator DoExplosion(int col, int row, List<SpriteRenderer> matches)
    {
        GameObject trail;
        switch (matches.Count+1)
        {
            case 3:
                trail = Instantiate(Resources.Load<GameObject>("ExplosionTrail3"), Grid[col, row].transform.position, Quaternion.identity);
                MMVibrationManager.Haptic(HapticTypes.Success);
                Destroy(Instantiate(Resources.Load<GameObject>("ExplosionParticle3"), Grid[col, row].transform.position, Quaternion.identity), 2f);
                Grid[col, row].transform.DOShakeScale(1f, 0.5f);

                
                trail.GetComponent<GridTrail>().targetPos = matches[matches.Count - 1].transform.position;
                Destroy(trail, 3f);

                for (int i = 0; i < matches.Count; i++)
                {
                    yield return new WaitForSeconds(0.1f);
                    Destroy(Instantiate(Resources.Load<GameObject>("ExplosionParticle3"), matches[i].transform.position, Quaternion.identity), 2f);
                    matches[i].transform.DOShakeScale(1f, 0.5f);
                }


                break;

            case 4:

                GameObject[] x3efekts = GameObject.FindGameObjectsWithTag("X3Efekt");
                foreach (var item in x3efekts)
                {
                    Destroy(item);
                }

                trail = Instantiate(Resources.Load<GameObject>("ExplosionTrail4"), new Vector3(Grid[col, row].transform.position.x, Grid[col, row].transform.position.y, -0.6f), Quaternion.identity);
                MMVibrationManager.Haptic(HapticTypes.Success);
                Destroy(Instantiate(Resources.Load<GameObject>("ExplosionParticle4"), new Vector3(Grid[col, row].transform.position.x, Grid[col, row].transform.position.y, -0.6f), Quaternion.identity), 2f);
                Grid[col, row].transform.DOShakeScale(1f, 0.5f);

                trail.GetComponent<GridTrail>().targetPos = matches[matches.Count - 1].transform.position;
                Destroy(trail, 3f);

                for (int i = 0; i < matches.Count; i++)
                {
                    yield return new WaitForSeconds(0.1f);
                    Destroy(Instantiate(Resources.Load<GameObject>("ExplosionParticle4"), new Vector3(matches[i].transform.position.x, matches[i].transform.position.y, -0.6f), Quaternion.identity), 2f);
                    matches[i].transform.DOShakeScale(1f, 0.5f);
                }

                StartCoroutine(ControlX(4));
                break;

            case 5:

                GameObject[] x3efektss = GameObject.FindGameObjectsWithTag("X3Efekt");
                foreach (var item in x3efektss)
                {
                    Destroy(item);
                }

                GameObject[] x4efekts = GameObject.FindGameObjectsWithTag("X4Efekt");
                foreach (var item in x4efekts)
                {
                    Destroy(item);
                }

                trail = Instantiate(Resources.Load<GameObject>("ExplosionTrail5"), new Vector3(Grid[col, row].transform.position.x, Grid[col, row].transform.position.y, -0.9f), Quaternion.identity);
                MMVibrationManager.Haptic(HapticTypes.Success);
                Destroy(Instantiate(Resources.Load<GameObject>("ExplosionParticle5"), new Vector3(Grid[col, row].transform.position.x, Grid[col, row].transform.position.y, -0.9f), Quaternion.identity), 2f);
                Grid[col, row].transform.DOShakeScale(1f, 0.5f);

                trail.GetComponent<GridTrail>().targetPos = matches[matches.Count - 1].transform.position;
                Destroy(trail, 3f);

                for (int i = 0; i < matches.Count; i++)
                {
                    yield return new WaitForSeconds(0.1f);
                    Destroy(Instantiate(Resources.Load<GameObject>("ExplosionParticle5"), new Vector3(matches[i].transform.position.x, matches[i].transform.position.y, -0.9f), Quaternion.identity), 2f);
                    matches[i].transform.DOShakeScale(1f, 0.5f);
                }
                StartCoroutine(ControlX(5));
                break;
            default:
                break;
        }


        StartCoroutine(ResizeTiles());
    }

    IEnumerator ControlX(int X) 
    {
        yield return new WaitForSeconds(0.1f);
        if (X == 4)
        {
            GameObject[] x3efekts = GameObject.FindGameObjectsWithTag("X3Efekt");
            foreach (var item in x3efekts)
            {
                Destroy(item);
            }
        }
        else if (X == 5)
        {
            GameObject[] x3efektss = GameObject.FindGameObjectsWithTag("X3Efekt");
            foreach (var item in x3efektss)
            {
                Destroy(item);
            }

            GameObject[] x4efekts = GameObject.FindGameObjectsWithTag("X4Efekt");
            foreach (var item in x4efekts)
            {
                Destroy(item);
            }
        }
    }

    IEnumerator ResizeTiles()
    {
        yield return new WaitForSeconds(.5f);
        for (int row = 0; row < GridDimension; row++)
        {
            for (int column = 0; column < GridDimension; column++)
            {
                Grid[row,column].transform.DOScale(0.5f, 0.5f);
            }
        }
    }

    List<SpriteRenderer> FindColumnMatchForTile(int col, int row, Sprite sprite)
    {
        List<SpriteRenderer> result = new List<SpriteRenderer>();
        for (int i = col + 1; i < GridDimension; i++)
        {
            SpriteRenderer nextColumn = GetSpriteRendererAt(i, row);
            if (nextColumn.sprite != sprite)
            {
                break;
            }
            result.Add(nextColumn);
        }


        return result;
    }

    List<SpriteRenderer> FindRowMatchForTile(int col, int row, Sprite sprite)
    {
        List<SpriteRenderer> result = new List<SpriteRenderer>();
        for (int i = row + 1; i < GridDimension; i++)
        {
            SpriteRenderer nextRow = GetSpriteRendererAt(col, i);
            if (nextRow.sprite != sprite)
            {
                break;
            }

            /*x3trail = Instantiate(Resources.Load<GameObject>("LightningFloorPurpleTrail"), Grid[col, row].transform.position, Quaternion.identity);
            if (x3trail != null)
            {
                x3trail.GetComponent<GridTrail>().targetPos = Grid[col, i].transform.position;
                //Destroy(x3trail, 1f);
            }*/
            result.Add(nextRow);
        }
        return result;
    }

    void FillHoles()
    {
        for (int column = 0; column < GridDimension; column++)
            for (int row = 0; row < GridDimension; row++)
            {
                while (GetSpriteRendererAt(column, row).sprite == null)
                {

                    SpriteRenderer current = GetSpriteRendererAt(column, row);


                    SpriteRenderer next = current;
                    for (int filler = row; filler < GridDimension - 1; filler++)
                    {
                        next = GetSpriteRendererAt(column, filler + 1);
                        current.sprite = next.sprite;
                        current = next;
                    }
                    /*if (x3trail != null)
                    {
                        x3trail.GetComponent<GridTrail>().targetPos = Grid[column, row].transform.position;
                        Destroy(x3trail, 10f);
                    }*/
                    next.sprite = Sprites[Random.Range(0, Sprites.Count)];
                }
            }

    }



    IEnumerator Branch()
    {
        if (matchCount <= 3)
        {
            if (!GameObject.FindGameObjectWithTag("CloneWeapon"))
            {
                GameObject item = Instantiate(Resources.Load<GameObject>("Branch"), weapons[0].transform.position, Quaternion.Euler(0, 0, -90));
                Destroy(item, 1F);
            }

        }
        yield return new WaitForSeconds(1F);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapons[0].transform.position, Quaternion.identity), 0.5f);
        weapons[0].SetActive(true);
        weapons[1].SetActive(false);
        weapons[2].SetActive(false);
        weapons[3].SetActive(false);
        weapons[4].SetActive(false);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BranchControllerPlayer") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Branch");
        yield return new WaitForSeconds(2f);
        weapons[0].SetActive(false);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator Billiard()
    {
        if (matchCount <= 3)
        {
            if (!GameObject.FindGameObjectWithTag("CloneWeapon"))
            {
                GameObject item = Instantiate(Resources.Load<GameObject>("Billiard"),weapons[1].transform.position, Quaternion.Euler(0, 0, -90));
                Destroy(item, 1F);

            }
        }
        yield return new WaitForSeconds(1F);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapons[1].transform.position, Quaternion.identity), 0.5f);
        weapons[0].SetActive(false);
        weapons[1].SetActive(true);
        weapons[2].SetActive(false);
        weapons[3].SetActive(false);
        weapons[4].SetActive(false);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("BilliardControllerPlayer") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Billiard");
        yield return new WaitForSeconds(2.5f);
        weapons[1].SetActive(false);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator Knife()
    {

        if (matchCount <= 3)
        {
            if (!GameObject.FindGameObjectWithTag("CloneWeapon"))
            {
                GameObject item = Instantiate(Resources.Load<GameObject>("Knife"), weapons[2].transform.position, Quaternion.Euler(0, 0, -90));
                Destroy(item, 1.2F);

            }
        }
        yield return new WaitForSeconds(1F);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapons[2].transform.position, Quaternion.identity), 0.5f);
        weapons[0].SetActive(false);
        weapons[1].SetActive(false);
        weapons[2].SetActive(true);
        weapons[3].SetActive(false);
        weapons[4].SetActive(false);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("KnifeControllerPlayer") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Knife");
        yield return new WaitForSeconds(2f);
        weapons[2].SetActive(false);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator Sword()
    {

        if (matchCount <= 3)
        {
            if (!GameObject.FindGameObjectWithTag("CloneWeapon"))
            {
                GameObject item = Instantiate(Resources.Load<GameObject>("Sword"),weapons[3].transform.position, Quaternion.Euler(0, 0, -90));
                Destroy(item, 1F);

            }
        }
        yield return new WaitForSeconds(1F);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapons[3].transform.position, Quaternion.identity), 0.5f);
        weapons[0].SetActive(false);
        weapons[1].SetActive(false);
        weapons[2].SetActive(false);
        weapons[3].SetActive(true);
        weapons[4].SetActive(false);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("SwordControllerPlayer") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Sword");
        yield return new WaitForSeconds(2f);
        weapons[3].SetActive(false);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;
    }
    IEnumerator Spear()
    {
       
        if (matchCount <= 3)
        {
            if (!GameObject.FindGameObjectWithTag("CloneWeapon"))
            {
                GameObject item = Instantiate(Resources.Load<GameObject>("Spear"), weapons[4].transform.position, Quaternion.Euler(0, 0, -90));
                Destroy(item, 1F);

            }
        }
        yield return new WaitForSeconds(1F);
        Destroy(Instantiate(Resources.Load("SelectWeapon"), weapons[4].transform.position, Quaternion.identity), 0.5f);
        weapons[0].SetActive(false);
        weapons[1].SetActive(false);
        weapons[2].SetActive(false);
        weapons[3].SetActive(false);
        weapons[4].SetActive(true);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("SpearControllerPlayer") as RuntimeAnimatorController;
        Player.GetComponent<Animator>().SetTrigger("Spear");
        yield return new WaitForSeconds(2f);
        weapons[4].SetActive(false);
        Player.GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Idle") as RuntimeAnimatorController;

    }


}
