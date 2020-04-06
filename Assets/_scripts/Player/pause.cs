using UnityEngine;
using UnityEngine.SceneManagement;
// Creado por JZ1999

public class pause : MonoBehaviour
{
	#region Variables
	[SerializeField]
	[Space]
	private GameObject pauseMenu;
	public bool Paused { get; private set; } = false;
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
			pauseGame();
		}
		else if (Input.GetKeyDown(KeyCode.Escape))
		{
			unpauseGame();
		}
	}

	public void pauseGame()
	{
		cameraOptions.cursorUnlock();
		cameraOptions.cursorSetVisible(true);
		pauseMenu.SetActive(true);
		Paused = true;
		Time.timeScale = 0f;
	}

	public void unpauseGame()
	{
		pauseMenu.SetActive(false);
		cameraOptions.cursorSetVisible(false);
		Paused = false;
		Time.timeScale = 1f;
		cameraOptions.cursorLock();
	}

	public void reloadScene()
	{
		unpauseGame();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void goToMainMenu()
	{
		unpauseGame();
		SceneManager.LoadScene("Scenes/Inicio");
	}
	#endregion
}
