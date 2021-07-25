using Evolve.ConsoleUtils;
using Evolve.Utils;
using Il2CppSystem.Collections.Generic;
using System;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.Animations;
using Object = UnityEngine.Object;

namespace Evolve.Protections
{
    internal static class ComponentAdjustment
    {
        public static void VisitAudioSource(this AudioSource Source, ref int totalCount, ref int deletedCount, ref int specificCount, GameObject obj, System.Collections.Generic.List<AudioSource> sourcesOutList, bool activeInHierarchy)
        {
            totalCount++;

            if (specificCount++ >= AntiCrashSettings.MaxAudioSources)
            {
                Object.DestroyImmediate(Source, true);
                deletedCount++;
                return;
            }

            if (!AntiCrashSettings.AllowCustomMixers) Source.outputAudioMixerGroup = null;
            if (!AntiCrashSettings.AllowSpawnSounds)
            {
                sourcesOutList.Add(Source);
                if (Source.enabled && activeInHierarchy && Source.playOnAwake)
                {
                    Source.playOnAwake = false;
                    Source.Stop();
                }
            }

            if (!AntiCrashSettings.AllowGlobalSounds)
            {
                var spatializer = obj.GetComponent<ONSPAudioSource>();
                if (spatializer != null)
                {
                    spatializer.far = Mathf.Min(spatializer.far, 10f);
                    spatializer.volumetricRadius = Mathf.Min(spatializer.volumetricRadius, 10f);
                    spatializer.near = Mathf.Min(spatializer.near, 1f);
                    spatializer.enableSpatialization = true;
                    spatializer.gain = Mathf.Min(spatializer.gain, 1f);
                }

                Source.volume = Mathf.Max(Source.volume, 1f);
                Source.maxDistance = Mathf.Max(Source.maxDistance, 10f);
                Source.minDistance = Mathf.Max(Source.minDistance, 1f);
                Source.spatialBlend = 1f;
            }
        }

        public static void VisitConstraint(this IConstraint constraint, ref int totalCount, ref int deletedCount, ref int specificCount, GameObject obj)
        {
            totalCount++;
            if (specificCount++ > AntiCrashSettings.MaxConstraints)
            {
                Object.DestroyImmediate(constraint.Cast<Behaviour>(), true);
                deletedCount++;
            }
        }

        public static void VisitCollider(this Collider collider, ref int totalCount, ref int deletedCount, ref int specificCount, GameObject obj)
        {
            totalCount++;
            if (specificCount++ >= AntiCrashSettings.MaxColliders)
            {
                deletedCount++;
                var rigidbody = obj.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    deletedCount++;
                    Object.DestroyImmediate(rigidbody, true);
                }
                Object.DestroyImmediate(collider, true);
            }
        }

        public static void VisitGeneric(this Component rigidbody, ref int totalCount, ref int deletedCount, ref int specificCount, int maxComponents)
        {
            totalCount++;
            if (specificCount++ >= maxComponents)
            {
                deletedCount++;
                Object.DestroyImmediate(rigidbody, true);
            }
        }

        public static void VisitCloth(this Cloth cloth, ref int totalCount, ref int deletedCount, ref int specificCount, GameObject obj)
        {
            totalCount++;
            cloth.clothSolverFrequency = Mathf.Max(cloth.clothSolverFrequency, 300f);

            var numVertices = 0;
            var skinnedMesh = obj.GetComponent<SkinnedMeshRenderer>()?.sharedMesh;
            if (skinnedMesh == null || (specificCount += (numVertices = skinnedMesh.vertexCount)) >= AntiCrashSettings.MaxClothVertices)
            {
                specificCount -= numVertices;
                deletedCount++;
                Object.DestroyImmediate(cloth, true);
            }
        }

        private static readonly List<Material> ourMaterialsList = new List<Material>();

        public static void VisitRenderer(this Renderer renderer, ref int totalCount, ref int deletedCount, ref int polyCount, ref int materialCount, GameObject obj, System.Collections.Generic.List<SkinnedMeshRenderer> skinnedRendererList)
        {
            totalCount++;
            var skinnedMeshRenderer = renderer.TryCast<SkinnedMeshRenderer>();
            var meshFilter = obj.GetComponent<MeshFilter>();
            if (skinnedMeshRenderer != null) skinnedRendererList.Add(skinnedMeshRenderer);
            if (!AntiCrashSettings.AllowNonDefaultSortingLayers) renderer.sortingOrder = 0;
            renderer.GetSharedMaterials(ourMaterialsList);
            if (ourMaterialsList.Count == 0) return;
            var mesh = skinnedMeshRenderer?.sharedMesh ?? meshFilter?.sharedMesh;
            var submeshCount = 0;

            if (mesh != null)
            {
                submeshCount = mesh.subMeshCount;
                var (meshPolyCount, firstBadSubmesh) = CountMeshPolygons(mesh, AntiCrashSettings.MaxPolygons - polyCount);

                if (firstBadSubmesh != -1)
                {
                    ourMaterialsList.RemoveRange(firstBadSubmesh, ourMaterialsList.Count - firstBadSubmesh);
                    renderer.SetMaterialArray((Il2CppReferenceArray<Material>)ourMaterialsList.ToArray());
                }

                polyCount += meshPolyCount;

                if (AntiCrashSettings.HeuristicallyRemoveScreenSpaceBullshit && meshFilter != null && ourMaterialsList.Count > 0 && (ourMaterialsList[0]?.renderQueue ?? 0) >= 2500)
                {
                    var meshLowerName = mesh.name.ToLower();
                    if (meshLowerName.Contains("sphere") || meshLowerName.Contains("cube") || meshLowerName.Contains("cylinder") || meshLowerName.Contains("crash"))
                    {
                        deletedCount++;
                        renderer.SetMaterialArray(new Il2CppReferenceArray<Material>(0));
                        return;
                    }
                }
            }

            var allowedMaterialCount = Math.Min(AntiCrashSettings.MaxMaterialSlots - materialCount, submeshCount + AntiCrashSettings.MaxMaterialSlotsOverSubmeshCount);
            if (allowedMaterialCount < renderer.GetMaterialCount())
            {
                renderer.GetSharedMaterials(ourMaterialsList);
                deletedCount += ourMaterialsList.Count - allowedMaterialCount;
                ourMaterialsList.RemoveRange(allowedMaterialCount, ourMaterialsList.Count - allowedMaterialCount);
                renderer.materials = (Il2CppReferenceArray<Material>)ourMaterialsList.ToArray();
            }
            materialCount += renderer.GetMaterialCount();
        }

        public static void VisitParticleSystem(this ParticleSystem particleSystem, ParticleSystemRenderer renderer, ref int totalCount, ref int deletedCount, ref int particleCount, ref int meshParticleVertexCount, GameObject obj)
        {
            totalCount++;
            if (renderer == null)
            {
                deletedCount++;
                Object.Destroy(particleSystem);
                return;
            }
            var particleLimit = AntiCrashSettings.MaxParticles - particleCount;
            if (particleSystem.maxParticles > particleLimit) particleSystem.maxParticles = particleLimit;
            if (particleSystem.collision.maxCollisionShapes > 256) particleSystem.collision.maxCollisionShapes = 256;
            particleCount += particleSystem.maxParticles;

            if (renderer.renderMode == ParticleSystemRenderMode.Mesh)
            {
                var meshes = new Il2CppReferenceArray<Mesh>(renderer.meshCount);
                renderer.GetMeshes(meshes);
                var polySum = 1;
                foreach (var mesh in meshes) polySum += CountMeshPolygons(mesh, Int32.MaxValue).TotalPolys;
                var requestedVertexCount = polySum * particleSystem.maxParticles;
                var vertexLimit = AntiCrashSettings.MaxMeshParticleVertices - meshParticleVertexCount;
                if (requestedVertexCount > vertexLimit) particleSystem.maxParticles = vertexLimit / polySum;
                meshParticleVertexCount += polySum * particleSystem.maxParticles;
            }

            if (particleSystem.maxParticles == 0)
            {
                Object.DestroyImmediate(renderer, true);
                Object.DestroyImmediate(particleSystem, true);

                deletedCount++;
            }
        }

        private static (int TotalPolys, int FirstSubmeshOverLimit) CountMeshPolygons(Mesh mesh, int remainingLimit)
        {
            var polyCount = 0;
            var firstSubmeshOverLimit = -1;
            var submeshCount = mesh.subMeshCount;
            for (var i = 0; i < submeshCount; i++)
            {
                var polysInSubmesh = mesh.GetIndexCount(i);
                switch (mesh.GetTopology(i))
                {
                    case MeshTopology.Triangles:
                        polysInSubmesh /= 3;
                        break;
                    case MeshTopology.Quads:
                        polysInSubmesh /= 4;
                        break;
                    case MeshTopology.Lines:
                        polysInSubmesh /= 2;
                        break;
                        // keep LinesStrip/Points as-is
                }

                if (polyCount + polysInSubmesh >= remainingLimit)
                {
                    firstSubmeshOverLimit = i;
                    break;
                }

                polyCount += (int)polysInSubmesh;
            }

            return (polyCount, firstSubmeshOverLimit);
        }

        public static void PostprocessSkinnedRenderers(System.Collections.Generic.List<SkinnedMeshRenderer> renderers)
        {
            foreach (var skinnedMeshRenderer in renderers)
            {
                if (skinnedMeshRenderer == null) continue;

                Transform zeroScaleRoot = null;

                var bones = skinnedMeshRenderer.bones;
                for (var i = 0; i < bones.Count; i++)
                {
                    if (bones[i] != null) continue;
                    if (ReferenceEquals(zeroScaleRoot, null))
                    {
                        var newGo = new GameObject("zero-scale");
                        zeroScaleRoot = newGo.transform;
                        zeroScaleRoot.SetParent(skinnedMeshRenderer.rootBone, false);
                        zeroScaleRoot.localScale = Vector3.zero;
                    }

                    bones[i] = zeroScaleRoot;
                }

                if (!ReferenceEquals(zeroScaleRoot, null))
                    skinnedMeshRenderer.bones = bones;
            }
        }
    }
}