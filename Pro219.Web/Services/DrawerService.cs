namespace Pro219.Web.Services
{
    public class DrawerService
    {
        public bool IsOpen { get; set; } = false;

        public event Action OnChange;

        public void ToggleDrawer()
        {
            IsOpen = !IsOpen;
            NotifyStateChanged();
        }

        public void OpenDrawer()
        {
            if (!IsOpen)
            {
                IsOpen = true;
                NotifyStateChanged();
            }
        }

        public void CloseDrawer()
        {
            if (IsOpen)
            {
                IsOpen = false;
                NotifyStateChanged();
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
