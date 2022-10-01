
using Dreamteck.Splines;
using UnityEngine;

public class SplineEnemyModel : SplineWorldItemModel
{
    public SplineFollower SplineFollower => splineFollower;
    [SerializeField] private SplineFollower splineFollower;
    protected virtual void OnValidate()
    {
        if (showSplineTracer)
        {
            SplineFollower.hideFlags = HideFlags.None;
        }
        else
        {
            SplineFollower.hideFlags = HideFlags.HideInInspector;
        }
        splineFollower.SetPercent(positionRange);
        splineFollower.motion.offset = offset;
    }

    private void Reset()
    {
        mainRoad = GameObject.FindObjectOfType<SplineMainRoad>();
        id = transform.GetSiblingIndex();
        splineFollower.spline = mainRoad.Computer;

        if (gameObject.GetComponent<SplineFollower>() != null)
        {
            splineFollower = gameObject.GetComponent<SplineFollower>();
        }
        else
        {
            splineFollower = gameObject.AddComponent<SplineFollower>();
        }

        splineFollower.buildOnEnable = true;
        splineFollower.spline = mainRoad.Computer;
        splineFollower.buildOnEnable = true;
        splineFollower.buildOnAwake = false;
        splineFollower.autoUpdate = true;
        splineFollower.multithreaded = true;
        splineFollower.hideFlags = HideFlags.None;
    }

    public override void SetPositionAndOffset(double range, Vector2 offset)
    {
        positionRange = range;
        this.offset = offset;
        splineFollower.SetPercent(positionRange);
        splineFollower.motion.offset = offset;
        //  splinePositioner.enabled = true;
        splineFollower.RebuildImmediate();
    }

    public override SplineItemDataModel GetData()
    {
        SplineItemDataModel dataModel = new SplineItemDataModel();
        dataModel.Id = id;
        dataModel.percentage = splineFollower.GetPercent();
        dataModel.offset = splineFollower.motion.offset;
        return dataModel;
    }
}
