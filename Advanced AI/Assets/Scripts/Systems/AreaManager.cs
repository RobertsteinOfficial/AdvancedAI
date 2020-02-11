using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AreaType
{
    Outside,
    Dormitory,
    StoreRoom,
    Armory,
    PowderKeg,
    Office,
    ShootingRange,
    Canteen
}

[System.Serializable]
public struct Area
{
    public string areaName;
    public AreaType areaType;
    public Color gizmoColor;
    public Bounds areaBounds;

    public Area(string name = "New Area")
    {
        areaName = name;
        areaType = AreaType.Outside;
        gizmoColor = Color.white;
        areaBounds = new Bounds();
    }
}

public class AreaManager : MonoBehaviour
{
    public bool showButtons = false;
    public List<Area> areas;

    #region Static Instance Declaration
    public static AreaManager instance;
    private void OnEnable()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion
    
    public static bool GetArea(Vector3 searchedPosition, out Area findedArea)
    {
        foreach (var area in instance.areas)
        {
            if (area.areaBounds.Contains(searchedPosition))
            {
                findedArea = area;
                return true;
            }
        }

        findedArea = new Area();
        return false;
    }

    #region Editor
    public void AddArea()
    {
        areas.Add(new Area());
    }

    public void RemoveArea()
    {
        areas.RemoveAt(areas.Count - 1);
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        if (areas != null && areas.Count > 0)
        {
            foreach (var area in areas)
            {
                Gizmos.color = area.gizmoColor;
                Gizmos.DrawWireCube(area.areaBounds.center, area.areaBounds.size);
            }
        }
    }
}
