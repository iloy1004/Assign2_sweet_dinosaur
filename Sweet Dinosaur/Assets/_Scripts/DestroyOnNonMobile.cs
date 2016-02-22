/*----------------------------------------------------------------------------
Source file name: DestroyOnNonMobile.cs
Author's name: Jihee Seo
Last modified by: Jihee Seo
Last modified date: Feb 17, 2016
Program description: controlling of canvas including movement buttons for mobile users.
Revision history: 0.0 - Created document, and made basic methods, Start and Update()
                  0.1 - Added reset method
                  1.0 - Added Trigger Event for destruction
                  1.1 - Added animation of explosion
----------------------------------------------------------------------------*/

using UnityEngine;
using System.Collections;

public class DestroyOnNonMobile : MonoBehaviour {

    //If user doesn't use mobile, destory canvas including buttons for mobile
    #if !UNITY_ANDROID && !UNITY_IPHONE && !UNITY_BLACKBERRY && !UNITY_WINRT || UNITY_EDITOR
    // Use this for initialization
    void Start () {
        Destroy(this.gameObject);
	}
    #endif
}
