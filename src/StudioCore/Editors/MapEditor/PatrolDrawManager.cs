﻿using Microsoft.Extensions.Logging;
using SoulsFormats;
using StudioCore.Editor;
using StudioCore.MsbEditor;
using StudioCore.UserProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace StudioCore.Editors.MapEditor;

/// <summary>
/// Handles rendering walk / patrol routes.
/// </summary>
public static class PatrolDrawManager
{
    private static readonly HashSet<WeakReference<Entity>> _drawEntities = new();
    private record DrawEntity;

    private const float _verticalOffset = 0.8f;

    private static Entity GetDrawEntity(ObjectContainer map)
    {
        Entity e = new(map, new DrawEntity());
        map.AddObject(e);
        _drawEntities.Add(new WeakReference<Entity>(e));
        return e;
    }

    private static bool GetPoints(string[] regionNames, ObjectContainer map, out List<Vector3> points)
    {
        points = [];

        foreach (var region in regionNames)
        {
            if (!string.IsNullOrWhiteSpace(region))
            {
                var pointObj = map.GetObjectByName(region);
                if (pointObj == null)
                    continue;
                var pos = pointObj.GetRootLocalTransform().Position;
                pos.Y += _verticalOffset;

                points.Add(pos);
            }
        }

        return points.Count > 0;
    }

    /// <summary>
    ///     Generates the renderable walk routes for all loaded maps.
    /// </summary>
    public static void Generate(Universe universe)
    {
        Clear();

        var loadedMaps = universe.LoadedObjectContainers.Values.Where(x => x != null);
        foreach (var map in loadedMaps)
        {
            foreach (var patrolEntity in map.Objects.ToList())
            {
                if (patrolEntity.WrappedObject is MSBD.Part.EnemyBase MSBD_Enemy)
                {
                    if (GetPoints(MSBD_Enemy.MovePointNames, map, out List<Vector3> points))
                    {
                        Entity drawEntity = GetDrawEntity(map);

                        bool endAtStart = MSBD_Enemy.PointMoveType == 0;
                        bool moveRandomly = MSBD_Enemy.PointMoveType == 2;
                        var chain = universe.GetPatrolLineDrawable(patrolEntity, drawEntity,
                            points, [patrolEntity.GetRootLocalTransform().Position], endAtStart, moveRandomly);

                        drawEntity.RenderSceneMesh = chain;
                    }
                }
                else if (patrolEntity.WrappedObject is MSB1.Part.EnemyBase MSB1_Enemy)
                {
                    if (GetPoints(MSB1_Enemy.MovePointNames, map, out List<Vector3> points))
                    {
                        Entity drawEntity = GetDrawEntity(map);

                        bool endAtStart = MSB1_Enemy.PointMoveType == 0;
                        bool moveRandomly = MSB1_Enemy.PointMoveType == 2;
                        var chain = universe.GetPatrolLineDrawable(patrolEntity, drawEntity,
                            points, [patrolEntity.GetRootLocalTransform().Position], endAtStart, moveRandomly);

                        drawEntity.RenderSceneMesh = chain;
                    }
                }
                // DS2 stores walk routes in ESD AI
                else if (patrolEntity.WrappedObject is MSBB.Part.EnemyBase MSBB_Enemy)
                {
                    if (GetPoints(MSBB_Enemy.MovePointNames, map, out List<Vector3> points))
                    {
                        Entity drawEntity = GetDrawEntity(map);

                        // BB move type is probably in an unk somewhere.
                        bool endAtStart = false;
                        bool moveRandomly = false;
                        var chain = universe.GetPatrolLineDrawable(patrolEntity, drawEntity,
                            points, [patrolEntity.GetRootLocalTransform().Position], endAtStart, moveRandomly);

                        drawEntity.RenderSceneMesh = chain;
                    }
                }
                else if (patrolEntity.WrappedObject is MSB3.Event.PatrolInfo MSB3_Patrol)
                {
                    if (GetPoints(MSB3_Patrol.WalkPointNames, map, out List<Vector3> points))
                    {
                        Entity drawEntity = GetDrawEntity(map);
                        List<Vector3> enemies = new();
                        foreach (var ent in map.Objects)
                        {
                            if (ent.WrappedObject is MSB3.Part.EnemyBase ene)
                            {
                                if (ene.WalkRouteName != patrolEntity.Name)
                                    continue;

                                var pos = ent.GetRootLocalTransform().Position;
                                pos.Y += _verticalOffset;
                                enemies.Add(pos);
                            }
                        }

                        bool endAtStart = MSB3_Patrol.PatrolType == 0;
                        bool moveRandomly = MSB3_Patrol.PatrolType == 2;
                        var chain = universe.GetPatrolLineDrawable(patrolEntity, drawEntity,
                            points, enemies, endAtStart, moveRandomly);

                        drawEntity.RenderSceneMesh = chain;
                    }
                }
                else if (patrolEntity.WrappedObject is MSBS.Event.PatrolInfo MSBS_Patrol)
                {
                    if (GetPoints(MSBS_Patrol.WalkRegionNames, map, out List<Vector3> points))
                    {
                        Entity drawEntity = GetDrawEntity(map);
                        List<Vector3> enemies = new();
                        foreach (var ent in map.Objects)
                        {
                            if (ent.WrappedObject is MSBS.Part.EnemyBase ene)
                            {
                                if (ene.WalkRouteName != patrolEntity.Name)
                                    continue;

                                var pos = ent.GetRootLocalTransform().Position;
                                pos.Y += _verticalOffset;
                                enemies.Add(pos);
                            }
                        }

                        bool endAtStart = MSBS_Patrol.PatrolType == 0;
                        bool moveRandomly = MSBS_Patrol.PatrolType == 2;
                        var chain = universe.GetPatrolLineDrawable(patrolEntity, drawEntity,
                            points, enemies, endAtStart, moveRandomly);

                        drawEntity.RenderSceneMesh = chain;
                    }
                }
                else if (patrolEntity.WrappedObject is MSBE.Event.PatrolInfo MSBE_Patrol)
                {
                    if (GetPoints(MSBE_Patrol.WalkRegionNames, map, out List<Vector3> points))
                    {
                        Entity drawEntity = GetDrawEntity(map);
                        List<Vector3> enemies = new();
                        foreach (var ent in map.Objects)
                        {
                            if (ent.WrappedObject is MSBE.Part.EnemyBase ene)
                            {
                                if (ene.WalkRouteName != patrolEntity.Name)
                                    continue;

                                var pos = ent.GetRootLocalTransform().Position;
                                pos.Y += _verticalOffset;
                                enemies.Add(pos);
                            }
                        }

                        bool endAtStart = MSBE_Patrol.PatrolType == 0;
                        bool moveRandomly = MSBE_Patrol.PatrolType == 2;
                        var chain = universe.GetPatrolLineDrawable(patrolEntity, drawEntity,
                            points, enemies, endAtStart, moveRandomly);

                        drawEntity.RenderSceneMesh = chain;
                    }
                }
                else if (patrolEntity.WrappedObject is MSB_AC6.Event.PatrolRoute MSBAC6_Patrol)
                {
                    if (GetPoints(MSBAC6_Patrol.GroupRegionNames, map, out List<Vector3> points))
                    {
                        Entity drawEntity = GetDrawEntity(map);
                        List<Vector3> enemies = new();
                        foreach (var ent in map.Objects)
                        {
                            if (ent.WrappedObject is MSB_AC6.Part.EnemyBase ene)
                            {
                                if (ene.WalkRouteName != patrolEntity.Name)
                                    continue;

                                var pos = ent.GetRootLocalTransform().Position;
                                pos.Y += _verticalOffset;
                                enemies.Add(pos);
                            }
                        }

                        bool endAtStart = MSBAC6_Patrol.PatrolType == 0;
                        bool moveRandomly = MSBAC6_Patrol.PatrolType == 2;
                        var chain = universe.GetPatrolLineDrawable(patrolEntity, drawEntity,
                            points, enemies, endAtStart, moveRandomly);

                        drawEntity.RenderSceneMesh = chain;
                    }
                }
            }
        }
    }

    public static void Clear()
    {
        foreach (var weakEnt in _drawEntities)
        {
            if (weakEnt.TryGetTarget(out var ent))
            {
                ent.Container?.Objects.Remove(ent);
                ent.Dispose();
            }
        }
        _drawEntities.Clear();
    }
}
