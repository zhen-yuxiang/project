using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandManage : MonoBehaviour
{
    public Transform Player;
    private List<cubeLand> m_landList = new List<cubeLand>();
    private int m_labdX = 5;
    private int m_labdY = 5;

    //主角过去位置
    private int m_oldLandX = 0;
    private int m_oldLandY = 0;

    //主角当前位置
    private int m_nowLandX = 0;
    private int m_nowLandY = 0;

    void Start()
    {
        m_oldLandX = (int)Player.transform.position.x;
        m_oldLandY = (int)Player.transform.position.z;
        InitLand();
    }


    void Update()
    {
        //x轴
        m_nowLandX = (int)Player.transform.position.x;
        if (m_nowLandX - m_oldLandX >= 1)
        {
            DeleteCubeByXBigger();
            CreateCubeByXBigger();
            m_oldLandX = m_nowLandX;
        }
        if (m_nowLandX - m_oldLandX <= -1)
        {
            DeleteCubeByXLess();
            CreateCubeByXLess();
            m_oldLandX = m_nowLandX;
        }

        //y轴
        m_nowLandY = (int)Player.transform.position.z;
        if (m_nowLandY - m_oldLandY >= 1)
        {
            DeleteCubeByYBigger();
            CreateCubeByYBigger();
            m_oldLandY = m_nowLandY;
        }
        if (m_nowLandY - m_oldLandY <= -1)
        {
            DeleteCubeByYLess();
            CreateCubeByYLess();
            m_oldLandY = m_nowLandY;
        }
    }

    //y轴
    private void DeleteCubeByYLess()
    {
        int _deleteY = m_oldLandY + m_labdY;
        for (int i = m_landList.Count - 1; i >= 0; i--)
        {
            if (m_landList[i].CubeY == _deleteY)
            {
                Destroy(m_landList[i].gameObject);
                m_landList.Remove(m_landList[i]);

            }
        }
    }

    private void CreateCubeByYLess()
    {
        int _createY = m_oldLandY - m_labdY - 1;
        for (int j = m_nowLandX - m_labdX; j <= m_nowLandX + m_labdX; j++)
        {
            Transform _cude = CreateCude(j, -2, _createY);
            cubeLand _cubeLand = _cude.gameObject.AddComponent<cubeLand>();
            _cude.name = j + "_" + _createY.ToString();
            _cubeLand.SetSubeLandInfo(j, _createY);
            m_landList.Add(_cubeLand);
        }
    }

    private void DeleteCubeByYBigger()
    {
        int _deleteY = m_oldLandY - m_labdY;
        for (int i = m_landList.Count - 1; i >= 0; i--)
        {
            if (m_landList[i].CubeY == _deleteY)
            {
                Destroy(m_landList[i].gameObject);
                m_landList.Remove(m_landList[i]);

            }
        }
    }

    private void CreateCubeByYBigger()
    {
        int _createY = m_oldLandY + m_labdY + 1;
        for (int j = m_nowLandX - m_labdX; j <= m_nowLandX + m_labdX; j++)
        {
            Transform _cude = CreateCude(j, -2, _createY);
            cubeLand _cubeLand = _cude.gameObject.AddComponent<cubeLand>();
            _cude.name = j.ToString() + "_" + _createY;
            _cubeLand.SetSubeLandInfo(j, _createY);
            m_landList.Add(_cubeLand);
        }
    }

    //x轴
    private void DeleteCubeByXBigger()
    {
        int _deleteX = m_oldLandX - m_labdX;
        for (int i = m_landList.Count - 1; i >= 0; i--)
        {
            if (m_landList[i].CubeX == _deleteX)
            {
                Destroy(m_landList[i].gameObject);
                m_landList.Remove(m_landList[i]);

            }
        }
    }

    private void CreateCubeByXBigger()
    {
        int _createX = m_oldLandX + m_labdX + 1;
        for (int j = m_nowLandY - m_labdY; j <= m_nowLandY + m_labdY; j++)
        {
            Transform _cude = CreateCude(_createX, -2, j);
            cubeLand _cubeLand = _cude.gameObject.AddComponent<cubeLand>();
            _cude.name = _createX.ToString() + "_" + j;
            _cubeLand.SetSubeLandInfo(_createX, j);
            m_landList.Add(_cubeLand);
        }
    }

    private void DeleteCubeByXLess()
    {
        int _deleteX = m_oldLandX + m_labdX;
        for (int i = m_landList.Count - 1; i >= 0; i--)
        {
            if (m_landList[i].CubeX == _deleteX)
            {
                Destroy(m_landList[i].gameObject);
                m_landList.Remove(m_landList[i]);

            }
        }
    }

    private void CreateCubeByXLess()
    {
        int _createX = m_oldLandX - m_labdX - 1;
        for (int j = m_nowLandY - m_labdY; j <= m_nowLandY + m_labdY; j++)
        {
            Transform _cude = CreateCude(_createX, -2, j);
            cubeLand _cubeLand = _cude.gameObject.AddComponent<cubeLand>();
            _cude.name = _createX.ToString() + "_" + j;
            _cubeLand.SetSubeLandInfo(_createX, j);
            m_landList.Add(_cubeLand);
        }
    }

    private void InitLand()
    {
        for (int i = 0 - m_labdX; i <= m_labdX; ++i)
        {
            for (int j = 0 - m_labdY; j <= m_labdY; ++j)
            {
                Transform _cude = CreateCude(i, -2, j);
                cubeLand _cubeLand = _cude.gameObject.AddComponent<cubeLand>();
                _cude.name = i.ToString() + "_" + j;
                _cubeLand.SetSubeLandInfo(i, j);
                m_landList.Add(_cubeLand);
            }
        }
    }

    private Transform CreateCude (int _x, int _y,int _z)
    {
        GameObject _gObject = null;
        //if (_assetPath != null)
        //{
        //    _gObject = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(_assetPath + ".prefab");
        //} else
        //{
        //    _gObject = GameObject.Instantiate(Resources.Load<GameObject>(_objectName));
        //}
        _gObject = Instantiate(Resources.Load<GameObject>("prefab/Cube"));
        _gObject.transform.localPosition = new Vector3(_x, _y, _z);
        return _gObject.transform;
    }
}
