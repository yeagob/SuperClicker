/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ReticleBehaviour : MonoBehaviour
{
    public GameObject Child;
    public DrivingSurfaceManager DrivingSurfaceManager;

    public ARPlane CurrentPlane;

    private Vector3 _screenCenter;

    [SerializeField] private GameObject _machinePrefab;

    // Start is called before the first frame update
    private void Start()
    {
        _screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        Child = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        DrivingSurfaceManager.RaycastManager.Raycast(_screenCenter, hits, TrackableType.PlaneWithinBounds);

        CurrentPlane = null;
        ARRaycastHit? hit = null;
        if (hits.Count > 0)
        {
            CurrentPlane = DrivingSurfaceManager.LockedPlane;

            if (CurrentPlane == null)
                hit = hits[0];
            else
                hit = hits.SingleOrDefault(x => x.trackableId == CurrentPlane.trackableId);

            //int x = hits.Count > 0 ? 5 : 0; Ejemplo if inline

            if (hit.HasValue)
            {
                CurrentPlane = DrivingSurfaceManager.PlaneManager.GetPlane(hit.Value.trackableId);
                // Move this reticle to the location of the hit.
                transform.position = hit.Value.pose.position;

            }
            if (CurrentPlane != null)
			{
                Child.SetActive(true);

                //Detección de toque
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
				{
                    //instanciamos la máquina en la posición actual
                    GameObject machine = Instantiate(_machinePrefab, transform.position+ Vector3.up*0.6f, _machinePrefab.transform.rotation);
                    machine.transform.forward = Camera.main.transform.forward;
                    machine.transform.rotation = Quaternion.Euler(0, machine.transform.rotation.eulerAngles.y, 0);
                    //Destruimos esto
                    Destroy(gameObject);
				}
			}
            else
                Child.SetActive(false);
        }
    }
}
