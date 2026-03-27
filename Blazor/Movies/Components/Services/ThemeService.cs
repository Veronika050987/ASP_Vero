using Microsoft.JSInterop;

namespace Movies.Components.Services
{
	public class ThemeService
	{
		private bool _isDarkMode = false;
		private readonly IJSRuntime _jsRuntime;

		public event Action? OnChange;

		public bool IsDarkMode => _isDarkMode;
		public ThemeService(IJSRuntime jsRuntime)
		{
			_jsRuntime = jsRuntime;
		}

		public async Task InitializeThemeAsync()
		{
			var storedTheme = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "theme");
			_isDarkMode = (storedTheme == "dark");
			await ApplyTheme();
			OnChange?.Invoke(); // Уведомляем UI
		}

		public async Task ToggleThemeAsync()
		{
			_isDarkMode = !_isDarkMode;
			await ApplyTheme();
			OnChange?.Invoke(); // Уведомляем UI
		}

		private async Task ApplyTheme()
		{
			// Здесь используем JS, чтобы изменить класс у body
			await _jsRuntime.InvokeVoidAsync("document.body.classList.toggle", "dark-theme", _isDarkMode);
			await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "theme", _isDarkMode ? "dark" : "light");
		}
	}
}