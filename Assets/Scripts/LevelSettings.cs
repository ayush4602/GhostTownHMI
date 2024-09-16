using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSettings : MonoBehaviour
{
    // Sliders for player health, enemy health, and weapon damage
    public Slider playerHealthSlider;
    public Slider enemyHealthSlider;
    public Slider weaponDamageSlider;

    // Texts to display the slider values
    public Text playerHealthText;
    public Text enemyHealthText;
    public Text weaponDamageText;

    private void Start()
    {
        // Initialize the sliders to the values stored in GameSettings
        playerHealthSlider.value = GameSettings.instance.playerHealth;
        enemyHealthSlider.value = GameSettings.instance.enemyHealth;
        weaponDamageSlider.value = GameSettings.instance.weaponDamage;

        // Update the UI text based on the initial slider values
        UpdateSliderTexts();

        // Add listeners to update the text when sliders change
        playerHealthSlider.onValueChanged.AddListener(delegate { UpdatePlayerHealthText(); });
        enemyHealthSlider.onValueChanged.AddListener(delegate { UpdateEnemyHealthText(); });
        weaponDamageSlider.onValueChanged.AddListener(delegate { UpdateWeaponDamageText(); });
    }

    // Method to update the text display for player health
    private void UpdatePlayerHealthText()
    {
        playerHealthText.text = "Player Health: " + playerHealthSlider.value.ToString("F0");
    }

    // Method to update the text display for enemy health
    private void UpdateEnemyHealthText()
    {
        enemyHealthText.text = "Enemy Health: " + enemyHealthSlider.value.ToString("F0");
    }

    // Method to update the text display for weapon damage
    private void UpdateWeaponDamageText()
    {
        weaponDamageText.text = "Weapon Damage: " + weaponDamageSlider.value.ToString("F0");
    }

    // Method to update all the slider texts when the scene starts or values change
    private void UpdateSliderTexts()
    {
        UpdatePlayerHealthText();
        UpdateEnemyHealthText();
        UpdateWeaponDamageText();
    }

    // Method to save the slider values and go back to the options menu
    public void SaveAndGoBack()
    {
        // Store the selected values in GameSettings
        GameSettings.instance.playerHealth = playerHealthSlider.value;
        GameSettings.instance.enemyHealth = enemyHealthSlider.value;
        GameSettings.instance.weaponDamage = weaponDamageSlider.value;

        // Return to the options menu
        SceneManager.LoadScene("OptionsScene");
    }

    // Method to return to the options menu without saving
    public void GoBackToOptions()
    {
        SceneManager.LoadScene("OptionsScene");
    }
}
