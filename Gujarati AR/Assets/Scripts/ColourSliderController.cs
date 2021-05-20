using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourSliderController : MonoBehaviour     //Controls the colour picker menu for light spawning
{
    private Slider intensitySlider;                     //Slider to change the brightness

    private ColorPaletteController colourWheel;         //Colour wheel by Abdullah Aldandarawy from the asset store

    private Color colour;           //Current selected colour

    private float intensityValue;   //Value of the intensity slider
    private bool isWhite;           //Bool to determine if the player has selected white light (no white on the wheel)

    void Start()
    {
        intensitySlider = GameObject.Find("BrightnessSlider").GetComponent<Slider>();
        colourWheel = GameObject.Find("ColorPalette").GetComponent<ColorPaletteController>();

        intensitySlider.onValueChanged.AddListener(delegate { UpdateColour(); });   //Add listeners to the colour wheel and intensity sliders
        colourWheel.OnColorChange.AddListener(delegate { UpdateColour(); });        //These listeners delegate to the UpdateColour function

        isWhite = false;
        colour.a = 1;       //Set transparency to 1 (opaque)
        UpdateColour();
    }

    private void UpdateColour()             //Called whenever the colour is changed, slider, wheel, or white toggle
    {
        colour = colourWheel.SelectedColor;         //Takes the colour from the colour wheel
        intensityValue = intensitySlider.value;     //Takes the intensity from the intensity slider

        if (isWhite) colourWheel.Saturation = 0;    //Set the saturation to 0 if white is selected
        else colourWheel.Saturation = 1;
    }

    public void ToggleWhite()       //Toggles the white flag and updates the colour
    {
        isWhite = !isWhite;
        UpdateColour();
    }

    public Color GetColour()        //Returns the current colour
    {
        return colour;
    }

    public float GetIntensity()     //Returns the current intensity
    {
        return intensityValue;
    }
}
