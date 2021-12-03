// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.MixedReality.Toolkit.Input;
using System;
using UnityEngine;
using UnityEditor;


namespace Microsoft.MixedReality.Toolkit.UI
{

    //public class SliderEventData
    //{
    //    public SliderEventData(float o, float n, IMixedRealityPointer pointer, MinMaxPinchSlider slider);

    //    public float OldValue { get; }
    //    public float NewValue { get; }
    //    public MinMaxPinchSlider Slider { get; }
    //    public IMixedRealityPointer Pointer { get; set; }
    //}

    /// <summary>
    /// A slider that can be moved by grabbing / pinching a slider thumb
    /// </summary>
    [HelpURL("https://docs.microsoft.com/windows/mixed-reality/mrtk-unity/features/ux-building-blocks/sliders")]
    //[AddComponentMenu("Scripts/MRTK/SDK/PinchSlider")] TODO
    public class MinMaxPinchSlider : MonoBehaviour, IMixedRealityPointerHandler, IMixedRealityFocusHandler
    {

        //[SerializeField]
        //private PinchSlider pin = null;
        //public PinchSlider Pin
        //{
        //    get
        //    {
        //        return pin;
        //    }
        //    set
        //    {
        //        pin = value;
        //    }
        //}

        #region Serialized Fields and Public Properties
        [Tooltip("The gameObject that contains the slider thumb for the max value.")]
        [SerializeField]
        private GameObject thumbRootMax = null;
        public GameObject ThumbRootMax
        {
            get
            {
                return thumbRootMax;
            }
            set
            {
                thumbRootMax = value;
                InitializeSliderThumb();
            }
        }

        [Tooltip("The gameObject that contains the slider thumb for the min value.")]
        [SerializeField]
        private GameObject thumbRootMin = null;
        public GameObject ThumbRootMin
        {
            get
            {
                return thumbRootMin;
            }
            set
            {
                thumbRootMin = value;
                InitializeSliderThumb();
            }
        }

        [Range(0, 1)]
        [SerializeField]
        private float sliderValueMax = 0.5f;
        public float SliderValueMax
        {
            get { return sliderValueMax; }
            set
            {
                var oldSliderValue = sliderValueMax;
                sliderValueMax = value;
                UpdateUI();
                // invoke(blabla , 2 seconds)  in case of 0 seconds invoked on next update cycle

                //OnValueUpdated.Invoke(new SliderEventData(oldSliderValue, value, ActivePointer, pin)); 
            }
        }

        [Range(0, 1)]
        [SerializeField]
        private float sliderValueMin = 0.5f;
        public float SliderValueMin
        {
            get { return sliderValueMin; }
            set
            {
                var oldSliderValue = sliderValueMin;
                sliderValueMin = value;
                UpdateUI();
                //OnValueUpdated.Invoke(new SliderEventData(oldSliderValue, value, ActivePointer, pin)); // TODO 
            }
        }

        //[Range(0, 1)]
        //[SerializeField]
        //private float sliderValue = 0.5f;
        //public float SliderValue
        //{
        //    get { return sliderValue; }
        //    set
        //    {
        //        var oldSliderValue = sliderValue;
        //        sliderValue = value;
        //        UpdateUI();
        //        //OnValueUpdated.Invoke(new SliderEventData(oldSliderValue, value, ActivePointer, this)); TODO 
        //    }
        //}

        [Header("Slider Axis Visuals")]

        [Tooltip("The gameObject that contains the trackVisuals. This will get rotated to match the slider axis")]
        [SerializeField]
        private GameObject trackVisuals = null;
        /// <summary>
        /// Property accessor of trackVisuals, it contains the desired track Visuals. This will get rotated to match the slider axis.
        /// </summary>
        public GameObject TrackVisuals
        {
            get
            {
                return trackVisuals;
            }
            set
            {
                if (trackVisuals != value)
                {
                    trackVisuals = value;
                    UpdateTrackVisuals();
                }
            }
        }

        [Tooltip("The gameObject that contains the tickMarks.  This will get rotated to match the slider axis")]
        [SerializeField]
        private GameObject tickMarks = null;
        /// <summary>
        /// Property accessor of tickMarks, it contains the desired tick Marks.  This will get rotated to match the slider axis.
        /// </summary>
        public GameObject TickMarks
        {
            get
            {
                return tickMarks;
            }
            set
            {
                if (tickMarks != value)
                {
                    tickMarks = value;
                    UpdateTickMarks();
                }
            }
        }

        //[Tooltip("The gameObject that contains the thumb visuals.  This will get rotated to match the slider axis.")]
        //[SerializeField]
        //private GameObject thumbVisuals = null;
        ///// <summary>
        ///// Property accessor of thumbVisuals, it contains the desired tick marks.  This will get rotated to match the slider axis.
        ///// </summary>
        //public GameObject ThumbVisuals
        //{
        //    get
        //    {
        //        return thumbVisuals;
        //    }
        //    set
        //    {
        //        if (thumbVisuals != value)
        //        {
        //            thumbVisuals = value;
        //            UpdateThumbVisuals();
        //        }
        //    }
        //}

        // MAX
        [Tooltip("The gameObject that contains the thumb visuals.  This will get rotated to match the slider axis.")]
        [SerializeField]
        private GameObject thumbVisualsMax = null;
        /// <summary>
        /// Property accessor of thumbVisuals, it contains the desired tick marks.  This will get rotated to match the slider axis.
        /// </summary>
        public GameObject ThumbVisualsMax
        {
            get
            {
                return thumbVisualsMax;
            }
            set
            {
                if (thumbVisualsMax != value)
                {
                    thumbVisualsMax = value;
                    UpdateThumbVisualsMax();
                }
            }
        }

        // MIN
        [Tooltip("The gameObject that contains the thumb visuals.  This will get rotated to match the slider axis.")]
        [SerializeField]
        private GameObject thumbVisualsMin = null;
        /// <summary>
        /// Property accessor of thumbVisuals, it contains the desired tick marks.  This will get rotated to match the slider axis.
        /// </summary>
        public GameObject ThumbVisualsMin
        {
            get
            {
                return thumbVisualsMin;
            }
            set
            {
                if (thumbVisualsMin != value)
                {
                    thumbVisualsMin = value;
                    UpdateThumbVisualsMin();
                }
            }
        }


        [Header("Slider Track")]

        [Tooltip("The axis the slider moves along")]
        [SerializeField]
        private SliderAxis sliderAxis = SliderAxis.XAxis;
        /// <summary>
        /// Property accessor of sliderAxis. The axis the slider moves along.
        /// </summary>
        public SliderAxis CurrentSliderAxis
        {
            get { return sliderAxis; }
            set
            {
                sliderAxis = value;
                UpdateVisualsOrientation();
            }
        }

        /// <summary>
        /// Previous value of slider axis, is used in order to detect change in current slider axis value
        /// </summary>
        private SliderAxis? previousSliderAxis = null;
        /// <summary>
        /// Property accessor for previousSliderAxis that is used also to initialize the property with the current value in case of null value.
        /// </summary>
        private SliderAxis PreviousSliderAxis
        {
            get
            {
                if (previousSliderAxis == null)
                {
                    previousSliderAxis = CurrentSliderAxis;
                }
                return previousSliderAxis.Value;
            }
            set
            {
                previousSliderAxis = value;
            }
        }

        [SerializeField]
        [Tooltip("Where the slider track starts, as distance from center along slider axis, in local space units.")]
        private float sliderStartDistance = -.5f;
        public float SliderStartDistance
        {
            get { return sliderStartDistance; }
            set { sliderStartDistance = value; }
        }

        [SerializeField]
        [Tooltip("Where the slider track ends, as distance from center along slider axis, in local space units.")]
        private float sliderEndDistance = .5f;
        public float SliderEndDistance
        {
            get { return sliderEndDistance; }
            set { sliderEndDistance = value; }
        }

        /// <summary>
        /// Gets the start position of the slider, in world space, or zero if invalid.
        /// Sets the start position of the slider, in world space, projected to the slider's axis.
        /// </summary>
        public Vector3 SliderStartPositionMax
        {
            get { return transform.TransformPoint(GetSliderAxis() * sliderStartDistance); }
            set { sliderStartDistance = Vector3.Dot(transform.InverseTransformPoint(value), GetSliderAxis()); }
        }

        /// <summary>
        /// Gets the start position of the slider, in world space, or zero if invalid.
        /// Sets the start position of the slider, in world space, projected to the slider's axis.
        /// </summary>
        public Vector3 SliderStartPositionMin
        {
            get { return transform.TransformPoint(GetSliderAxis() * sliderStartDistance); }
            set { sliderStartDistance = Vector3.Dot(transform.InverseTransformPoint(value), GetSliderAxis()); }
        }

        /// <summary>
        /// Gets the end position of the slider, in world space, or zero if invalid.
        /// Sets the end position of the slider, in world space, projected to the slider's axis.
        /// </summary>
        public Vector3 SliderEndPosition
        {
            get { return transform.TransformPoint(GetSliderAxis() * sliderEndDistance); }
            set { sliderEndDistance = Vector3.Dot(transform.InverseTransformPoint(value), GetSliderAxis()); }
        }

        /// <summary>
        /// Returns the vector from the slider start to end positions
        /// </summary>
        public Vector3 SliderTrackDirection
        {
            get { return SliderEndPosition - SliderStartPositionMax; }
        }

        #endregion

        #region Event Handlers
        [Header("Events")]
        public SliderEvent OnValueUpdated = new SliderEvent();
        public SliderEvent OnInteractionStarted = new SliderEvent();
        public SliderEvent OnInteractionEnded = new SliderEvent();
        public SliderEvent OnHoverEntered = new SliderEvent();
        public SliderEvent OnHoverExited = new SliderEvent();
        #endregion

        #region Private Fields

        /// <summary>
        /// Position offset for slider handle in world space.
        /// </summary>
        private Vector3 sliderThumbOffset = Vector3.zero;

        #endregion

        #region Protected Properties

        /// <summary>
        /// Float value that holds the starting value of the slider.
        /// </summary>
        protected float StartSliderValueMax { get; private set; }

        /// <summary>
        /// Float value that holds the starting value of the slider.
        /// </summary>
        protected float StartSliderValueMin { get; private set; }


        /// <summary>
        /// Starting position of mixed reality pointer in world space
        /// Used to track pointer movement
        /// </summary>
        protected Vector3 StartPointerPosition { get; private set; }

        /// <summary>
        /// Interface for handling pointer being used in UX interaction.
        /// </summary>
        protected IMixedRealityPointer ActivePointer { get; private set; }

        #endregion

        #region Constants
        /// <summary>
        /// Minimum distance between start and end of slider, in world space
        /// </summary>
        private const float MinSliderLength = 0.001f;
        #endregion  

        #region Unity methods
        protected virtual void Start()
        {
            if (thumbRootMax == null || thumbRootMin == null)
            {
                throw new Exception($"Slider thumb on gameObject {gameObject.name} is not specified. Did you forget to set it?");
            }
            InitializeSliderThumb();
            //OnValueUpdated.Invoke(new SliderEventData(sliderValueMax, sliderValueMax, null, pin)); // TODO
        }

        private void OnDisable()
        {
            if (ActivePointer != null)
            {
                EndInteraction();
            }
        }

        private void OnValidate()
        {
            CurrentSliderAxis = sliderAxis;
        }

        //private void OnGUI()
        //{
        //    var minLimit = 0;
        //    var maxLimit = 0;


        //    EditorGUILayout.LabelField("Min Val:", sliderValueMin.ToString());
        //    EditorGUILayout.LabelField("Max Val:", sliderValueMax.ToString());
        //    EditorGUILayout.MinMaxSlider(ref sliderValueMin, ref sliderValueMax, minLimit, maxLimit);
        //}

        #endregion

        #region Private Methods
        private void InitializeSliderThumb()
        {
            var startToThumb = thumbRootMax.transform.position - SliderStartPositionMax;
            var thumbProjectedOnTrack = SliderStartPositionMax + Vector3.Project(startToThumb, SliderTrackDirection);
            sliderThumbOffset = thumbRootMax.transform.position - thumbProjectedOnTrack;

            var startToThumbMin = thumbRootMin.transform.position - SliderStartPositionMin;
            var thumbProjectedOnTrackMin = SliderStartPositionMin + Vector3.Project(startToThumbMin, SliderTrackDirection);
            sliderThumbOffset = thumbRootMin.transform.position - thumbProjectedOnTrackMin;

            UpdateUI();
        }

        /// <summary>
        /// Update orientation of track visuals based on slider axis orientation
        /// </summary>
        private void UpdateTrackVisuals()
        {
            if (TrackVisuals)
            {
                TrackVisuals.transform.localPosition = Vector3.zero;

                switch (sliderAxis)
                {
                    case SliderAxis.XAxis:
                        TrackVisuals.transform.localRotation = Quaternion.identity;
                        break;
                    case SliderAxis.YAxis:
                        TrackVisuals.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                        break;
                    case SliderAxis.ZAxis:
                        TrackVisuals.transform.localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                        break;
                }
            }
        }

        /// <summary>
        /// Update orientation of tick marks based on slider axis orientation
        /// </summary>
        private void UpdateTickMarks()
        {
            if (TickMarks)
            {
                TickMarks.transform.localPosition = Vector3.zero;
                TickMarks.transform.localRotation = Quaternion.identity;

                var grid = TickMarks.GetComponent<Utilities.GridObjectCollection>();
                if (grid)
                {
                    // Update cellwidth or cellheight depending on what was the previous axis set to
                    var previousAxis = grid.Layout;
                    if (previousAxis == Utilities.LayoutOrder.Vertical)
                    {
                        grid.CellWidth = grid.CellHeight;
                    }
                    else
                    {
                        grid.CellHeight = grid.CellWidth;
                    }

                    grid.Layout = (sliderAxis == SliderAxis.YAxis) ? Utilities.LayoutOrder.Vertical : Utilities.LayoutOrder.Horizontal;
                    grid.UpdateCollection();
                }

                if (sliderAxis == SliderAxis.ZAxis)
                {
                    TickMarks.transform.localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                }
            }
        }

        /// <summary>
        /// Update orientation of thumb visuals based on slider axis orientation
        /// </summary>
        //private void UpdateThumbVisuals()
        //{
        //    if (ThumbVisuals)
        //    {
        //        ThumbVisuals.transform.localPosition = Vector3.zero;

        //        switch (sliderAxis)
        //        {
        //            case SliderAxis.XAxis:
        //                ThumbVisuals.transform.localRotation = Quaternion.identity;
        //                break;
        //            case SliderAxis.YAxis:
        //                ThumbVisuals.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
        //                break;
        //            case SliderAxis.ZAxis:
        //                ThumbVisuals.transform.localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        //                break;
        //        }
        //    }
        //}

        /// <summary>
        /// Update orientation of thumb visuals based on slider axis orientation
        /// </summary>
        private void UpdateThumbVisualsMax()
        {
            if (ThumbVisualsMax)
            {
                ThumbVisualsMax.transform.localPosition = Vector3.zero;

                switch (sliderAxis)
                {
                    case SliderAxis.XAxis:
                        ThumbVisualsMax.transform.localRotation = Quaternion.identity;
                        break;
                    case SliderAxis.YAxis:
                        ThumbVisualsMax.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                        break;
                    case SliderAxis.ZAxis:
                        ThumbVisualsMax.transform.localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                        break;
                }
            }
        }

        /// <summary>
        /// Update orientation of thumb visuals based on slider axis orientation
        /// </summary>
        private void UpdateThumbVisualsMin()
        {
            if (ThumbVisualsMin)
            {
                ThumbVisualsMin.transform.localPosition = Vector3.zero;

                switch (sliderAxis)
                {
                    case SliderAxis.XAxis:
                        ThumbVisualsMin.transform.localRotation = Quaternion.identity;
                        break;
                    case SliderAxis.YAxis:
                        ThumbVisualsMin.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
                        break;
                    case SliderAxis.ZAxis:
                        ThumbVisualsMin.transform.localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
                        break;
                }
            }
        }

        /// <summary>
        /// Update orientation of the visual components of pinch slider
        /// </summary>
        private void UpdateVisualsOrientation()
        {
            if (PreviousSliderAxis != sliderAxis)
            {
                UpdateThumbVisualsMax();
                UpdateThumbVisualsMin(); // TODO samen voegen
                UpdateTrackVisuals();
                UpdateTickMarks();
                PreviousSliderAxis = sliderAxis;
            }
        }

        private Vector3 GetSliderAxis()
        {
            switch (sliderAxis)
            {
                case SliderAxis.XAxis:
                    return Vector3.right;
                case SliderAxis.YAxis:
                    return Vector3.up;
                case SliderAxis.ZAxis:
                    return Vector3.forward;
                default:
                    throw new ArgumentOutOfRangeException("Invalid slider axis");
            }
        }

        private void UpdateUI()
        {
            var newSliderPos = SliderStartPositionMax + sliderThumbOffset + SliderTrackDirection * sliderValueMax;
            thumbRootMax.transform.position = newSliderPos;
            //  or niet allebei aanpassen
            var newSliderPosMin = SliderStartPositionMin + sliderThumbOffset + SliderTrackDirection * sliderValueMin;
            thumbRootMin.transform.position = newSliderPosMin;
        }

        private void EndInteraction()
        {
            if (OnInteractionEnded != null)
            {
                //OnInteractionEnded.Invoke(new SliderEventData(sliderValueMax, sliderValueMax, ActivePointer, pin));
            }
            ActivePointer = null;
        }

        #endregion

        #region IMixedRealityFocusHandler
        public void OnFocusEnter(FocusEventData eventData)
        {
            //OnHoverEntered.Invoke(new SliderEventData(sliderValueMax, sliderValueMax, eventData.Pointer, pin));  
        }

        public void OnFocusExit(FocusEventData eventData)
        {
            //OnHoverExited.Invoke(new SliderEventData(sliderValueMax, sliderValueMax, eventData.Pointer, pin)); 
        }
        #endregion

        #region IMixedRealityPointerHandler

        public void OnPointerUp(MixedRealityPointerEventData eventData)
        {
            if (eventData.Pointer == ActivePointer && !eventData.used)
            {
                EndInteraction();

                // Mark the pointer data as used to prevent other behaviors from handling input events
                eventData.Use();
            }
        }

        public void OnPointerDown(MixedRealityPointerEventData eventData)
        {
            if (ActivePointer == null && !eventData.used)
            {
                ActivePointer = eventData.Pointer;
                StartSliderValueMax = sliderValueMax; // TODO
                StartSliderValueMin = sliderValueMin; // TODO
                StartPointerPosition = ActivePointer.Position;
                if (OnInteractionStarted != null)
                {
                    //OnInteractionStarted.Invoke(new SliderEventData(sliderValueMax, sliderValueMax, ActivePointer, pin)); // TODO
                }

                // Mark the pointer data as used to prevent other behaviors from handling input events
                eventData.Use();
            }
        }

        public virtual void OnPointerDragged(MixedRealityPointerEventData eventData)
        {
            if (eventData.Pointer == ActivePointer && !eventData.used)
            {
                var delta = ActivePointer.Position - StartPointerPosition;
                var handDelta = Vector3.Dot(SliderTrackDirection.normalized, delta);

                SliderValueMax = Mathf.Clamp(StartSliderValueMax + handDelta / SliderTrackDirection.magnitude, 0, 1); // TODO
                SliderValueMin = Mathf.Clamp(StartSliderValueMin + handDelta / SliderTrackDirection.magnitude, 0, 1); // TODO

                // Mark the pointer data as used to prevent other behaviors from handling input events
                eventData.Use();
            }
        }

        public void OnPointerClicked(MixedRealityPointerEventData eventData) { }

        #endregion
    }
}
