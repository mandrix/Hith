using UnityEngine;
using UnityEngine.SceneManagement;
// Creado por JZ1999

public class pause : MonoBehaviour
{
	#region Variables
	[SerializeField]
	[Space]
	private GameObject pauseMenu;
	public bool Paused { get; set; } = false;
	#endregion

	#region Unity Methods

	void Update()
    {
		shouldPause();
    }
	#endregion

	#region Custom methods
	private void shouldPause()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !Paused)
		{
			pauseGame(pauseMenu);
		}
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			unpauseGame(pauseMenu);
		}
	}

	public void pauseGame(GameObject menu)
	{
		cameraOptions.cursorUnlock();
		menu.SetActive(true);
		Paused = true;
		Time.timeScale = 0f;
	}

	public void unpauseGame(GameObject menu)
	{
		menu.SetActive(false);
		Paused = false;
		Time.timeScale = 1f;
		cameraOptions.cursorLock();
	}

	public void reloadScene()
	{
		unpauseGame(pauseMenu);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void goToMainMenu()
	{
		unpauseGame(pauseMenu);
		SceneManager.LoadScene("Scenes/Inicio");
	}
	#endregion
}
