using MudBlazor;

namespace ebook.Shared
{
    public partial class MainLayout
    {
        private MudTheme _currentTheme;
        private int CurrentUserId { get; set; }
        private string ImageDataUrl { get; set; }
        private string FirstName { get; set; }

        private bool _drawerOpen = true;
        private bool _rightToLeft = false;
        private char FirstLetterOfName { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _currentTheme = BlazorHeroTheme.DefaultTheme;
        }
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }
        private async Task RightToLeftToggle()
        {
            var isRtl = true;
            _rightToLeft = isRtl;
            _drawerOpen = false;
        }
        private async Task Logout()
        {
            _navigationManager.NavigateTo("/login");
        }
        private async Task DarkMode()
        {
            //bool isDarkMode = true;//await __clientPreferenceManager.ToggleDarkModeAsync();
            //_currentTheme = isDarkMode
            //    ? BlazorHeroTheme.DefaultTheme
            //    : BlazorHeroTheme.DarkTheme;
        }
    }
}
