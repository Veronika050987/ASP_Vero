using Microsoft.JSInterop;

namespace Movies.Components.Services
{
	public class ThemeService
	{
		private bool _isDarkMode = false;
		private readonly IJSRuntime _jsRuntime;

		public bool IsDarkMode
		{
			get => _isDarkMode;
			set
			{
				if (_isDarkMode != value)
				{
					_isDarkMode = value;
					ApplyTheme();
				}
			}
		}

		public ThemeService(IJSRuntime jsRuntime)
		{
			_jsRuntime = jsRuntime;
		}

		public async Task InitializeThemeAsync()
		{
			// Попробуйте загрузить тему из localStorage, если она там есть
			var storedTheme = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "theme");
			if (storedTheme == "dark")
			{
				IsDarkMode = true;
			}
			else
			{
				IsDarkMode = false;
			}
			ApplyTheme(); // Применяем начальную тему
		}

		private async Task ApplyTheme()
		{
			if (_isDarkMode)
			{
				await _jsRuntime.InvokeVoidAsync("document.body.classList.add", "dark-theme");
				await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "theme", "dark");
			}
			else
			{
				await _jsRuntime.InvokeVoidAsync("document.body.classList.remove", "dark-theme");
				await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "theme", "light");
			}
		}

		public void ToggleTheme()
		{
			IsDarkMode = !_isDarkMode;
		}
	}
}