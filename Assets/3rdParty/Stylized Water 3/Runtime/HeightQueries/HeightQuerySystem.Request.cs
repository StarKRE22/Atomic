// Stylized Water 3 by Staggart Creations (http://staggart.xyz)
// COPYRIGHT PROTECTED UNDER THE UNITY ASSET STORE EULA (https://unity.com/legal/as-terms)
//    • Copying or referencing source code for the production of new asset store, or public, content is strictly prohibited!
//    • Uploading this file to a public repository will subject it to an automated DMCA takedown request.

using System;
using System.Collections.Generic;

namespace StylizedWater3
{
    public partial class HeightQuerySystem
    {
        /// <summary>
        /// Issues the data from a <see cref="HeightQuerySystem.Sampler"/> to an asynchronous GPU readback request.
        /// </summary>
        public class AsyncRequest
        {
            public delegate void OnRequestCompleted();
            /// <summary>
            /// Callback fired whenever a height query was successfully returned from the GPU.
            /// </summary>
            public event OnRequestCompleted onCompleted;
            
            //GUID
            public readonly int hashCode;
            //Identifier, mainly for debugging
            public string label;

            public Sampler sampler;
            
            /// <summary>
            /// If the sampling position falls outside of the camera frustum, or is not above a water surface, it will hit a void. A default value of -1000 is then used.
            /// Use this option to specify if the (invalid) value should be kept or not. If not, the value will represent that of the last successful hit.
            /// </summary>
            public bool invalidateMisses = true;
            
            //The indices this request occupies in the array
            public readonly List<int> indices = new List<int>();
            
            public int SampleCount => indices.Count;

            public AsyncRequest(int hashCode, Sampler sampler, string label = "")
            {
                this.hashCode = hashCode;
                this.sampler = sampler;
                this.label = label;
            }
            
            public void Issue()
            {
                AddRequest(this);
            }

            public void Withdraw()
            {
                WithdrawRequest(this);
            }

            public void Dispose()
            {
                Withdraw();
                
                sampler.Dispose();
            }

            internal void InvokeCallback()
            {
                onCompleted?.Invoke();
            }
        }
    }
}