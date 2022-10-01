using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineCollectableModel : SplineWorldItemModel
{
    public SplinePositioner SplinePositioner => splinePositioner;
    [SerializeField] private SplinePositioner splinePositioner;
    protected virtual void OnValidate()
    {
        if (showSplineTracer)
        {
            SplinePositioner.hideFlags = HideFlags.None;
        }
        else
        {
            SplinePositioner.hideFlags = HideFlags.HideInInspector;
        }
        splinePositioner.SetPercent(positionRange);
        splinePositioner.motion.offset = offset;
    }

    private void Reset()
    {
        mainRoad = GameObject.FindObjectOfType<SplineMainRoad>();
        id = transform.GetSiblingIndex();
        splinePositioner.spline = mainRoad.Computer;

        if (gameObject.GetComponent<SplinePositioner>() != null)
        {
            splinePositioner = gameObject.GetComponent<SplinePositioner>();
        }
        else
        {
            splinePositioner = gameObject.AddComponent<SplinePositioner>();
        }

        splinePositioner.buildOnEnable = true;
        splinePositioner.spline = mainRoad.Computer;
        splinePositioner.buildOnEnable = true;
        splinePositioner.buildOnAwake = false;
        splinePositioner.autoUpdate = true;
        splinePositioner.multithreaded = true;
        splinePositioner.hideFlags = HideFlags.None;
    }

    public override void SetPositionAndOffset(double range, Vector2 offset)
    {
        positionRange = range;
        this.offset = offset;
        splinePositioner.SetPercent(positionRange);
        splinePositioner.motion.offset = offset;
        //  splinePositioner.enabled = true;
        splinePositioner.RebuildImmediate();
    }

    public override SplineItemDataModel GetData()
    {
        SplineItemDataModel dataModel = new SplineItemDataModel();
        dataModel.Id = id;
        dataModel.percentage = splinePositioner.GetPercent();
        dataModel.offset = splinePositioner.motion.offset;
        return dataModel;
    }

}
